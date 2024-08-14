using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Domain.Aggregates.UserAggregate;
using Users.Infrastructure.EntityFramework;
using Users.Infrastructure.Persistence.Repositories.Users;

namespace Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationExternalDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddEFCore(configuration);
        return services;
    }
}
