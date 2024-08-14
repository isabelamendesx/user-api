using Users.Domain.Aggregates.UserAggregate.Entities;
using Users.Domain.Common;
using Users.Domain.ValueObjects;

namespace Users.Domain.Aggregates.UserAggregate;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool> EmailAlreadyExistsAsync(Email email, CancellationToken cancellation);
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellation);
}
