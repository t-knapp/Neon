using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Neon.Application;

public static class DependencyInjection {
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) {
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}