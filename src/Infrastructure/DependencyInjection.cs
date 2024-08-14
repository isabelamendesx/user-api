using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationExternalDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}
