using Users.Domain.Aggregates.UserAggregate.Entities;
using Users.Domain.ValueObjects;

namespace Users.Application.Common.Services;
public interface IKeycloakService
{
    Task CreateUserAsync(User user, string password, CancellationToken cancellationToken);
    Task UpdateUserAsync(User user, CancellationToken cancellationToken);
    Task DeleteUserAsync(User user, CancellationToken cancellationToken);
    Task<bool> UserExists(Email email);
}