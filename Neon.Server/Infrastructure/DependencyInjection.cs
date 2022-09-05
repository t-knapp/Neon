using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neon.Application;

namespace Neon.Infrastructure;

public static class DependencyInjection {
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) {

        services.AddScoped<IApplicationDbContext, 

        return services;
    }
}