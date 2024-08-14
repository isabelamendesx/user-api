using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Users.Infrastructure.EntityFramework;

namespace Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationExternalDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEFCore(configuration);
        return services;
    }
}
