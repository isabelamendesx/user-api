using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Users.Domain.Aggregates.UserAggregate.Entities;
using Users.Domain.Common;

namespace Users.Infrastructure.EntityFramework.Context;

public class UserDbContext : DbContext, IUnitOfWork
{
    public DbSet<User> Users => Set<User>();

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {      
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await base.SaveChangesAsync(cancellationToken);
    }
}
