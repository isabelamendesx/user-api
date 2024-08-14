using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Domain.Common;
using Users.Infrastructure.EntityFramework.Context;
using Users.Infrastructure.EntityFramework.Interceptors;

namespace Users.Infrastructure.EntityFramework;

public static class DependencyInjection
{
    internal static IServiceCollection AddEFCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddDbContext<IUnitOfWork, UserDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>())
            .UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}
