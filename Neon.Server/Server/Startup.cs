using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Neon.Application;
using Neon.Infrastructure;

namespace Neon.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddOptions<ImageOptions>().Bind(Configuration.GetSection(nameof(ImageOptions)));
            services.AddOptions<JwtOptions>().Bind(Configuration.GetSection(nameof(JwtOptions)));

            services.AddApplication(Configuration);
            services.AddInfrastructure(Configuration);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration.GetSection(nameof(JwtOptions)).GetValue<string>(nameof(JwtOptions.AuthorityUrl));
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.RequireHttpsMetadata = false;

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = static context =>
                        {
                            if( context.Request.Cookies.TryGetValue( "authorization", out var cookieToken ) )
                                context.Token = cookieToken;

                            if( context.Request.Query.TryGetValue( "authorization", out var queryToken ) )
                                context.Token = queryToken;

                            if( context.Request.Headers.TryGetValue( "authorization", out var headerToken ) )
                                context.Token = headerToken.ToString().StartsWith( "Bearer " )
                                    ? headerToken.ToString().Remove( 0, "Bearer ".Length )
                                    : headerToken.ToString();

                            return Task.CompletedTask;
                        }
                    };

                });

            services.AddControllers(options =>
            {
                options.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
            });

            services.AddSwaggerGen(config =>
            {
                config.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(Configuration.GetSection(nameof(JwtOptions)).GetValue<string>(nameof(JwtOptions.AuthorizationUrl))),
                            TokenUrl = new Uri(Configuration.GetSection(nameof(JwtOptions)).GetValue<string>(nameof(JwtOptions.TokenUrl))),
                            Scopes = new Dictionary<string, string>
                            {
                                { "profile" , "Profil" },
                                { "openid", "OpenId" },
                                { "scopes", "Scopes" },
                                { "email", "E-Mail"},
                                { "roles", "Roles" }
                            },
                        },
                    },
                    In = ParameterLocation.Header,
                    Name = "Authorize",
                    Description = "Neon.Server API"
                });

                config.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Neon.Server API");
                c.OAuthClientId( "" );
                c.OAuthScopeSeparator( " " );
                c.OAuthUsePkce();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            var builder = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();

            return builder
                .GetRequiredService<IOptions<MvcOptions>>()
                .Value
                .InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
        }
    }
}
