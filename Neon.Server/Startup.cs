using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using MediatR;
using MongoDB.Driver;
using MongoDB.Entities;
using AutoMapper;
using Neon.Server.Configuration;

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
            services.AddOptions<MongoDbOptions>().Bind(Configuration.GetSection(MongoDbOptions.IDENTIFIER));
            services.AddOptions<ImageOptions>().Bind(Configuration.GetSection(ImageOptions.IDENTIFIER));

            services
                .AddSingleton<DB>( s => {
                    var options = s.GetService<IOptions<MongoDbOptions>>();
                    var settings = new MongoClientSettings()
                    {
                        Credential = MongoCredential.CreateCredential("admin", options.Value.Username, options.Value.Password),
                        Server = new MongoServerAddress(options.Value.Hostname, options.Value.Port)
                    };
                    return new DB(settings, options.Value.Database);
                })
                .AddSingleton( typeof( IMongoCollection<> ), typeof( MongoCollection<> ) );

            services.AddControllers(options =>
            {
                options.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
            });

            services.AddAutoMapper( typeof( Startup ) );

            services.AddMediatR( typeof( Startup ) );

            services.AddSwaggerGen();

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

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Neon.Server API");
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
