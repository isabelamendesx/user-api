using Microsoft.Extensions.DependencyInjection;

namespace Users.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationUseCases(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        return services;
    }
}
