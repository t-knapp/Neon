using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neon.Application;

namespace Neon.Infrastructure;

public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure( this IServiceCollection services, IConfiguration configuration ) {

        services.AddDbContext<ApplicationDbContext, SqliteDbContext>(
            o => o.UseSqlite( configuration.GetConnectionString( "Database" ), b => b.MigrationsAssembly( "Infrastructure" ) ), contextLifetime: ServiceLifetime.Transient );

        services.AddScoped<IApplicationDbContext>( provider => provider.GetRequiredService<ApplicationDbContext>() );

        services.AddTransient<IImageResizeService, ImageResizeService>();

        return services;
    }
}