using Microsoft.EntityFrameworkCore;
using Users.Domain.Aggregates.UserAggregate.Entities;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.ValueObjects;
using Users.Infrastructure.EntityFramework.Context;

namespace Users.Infrastructure.Persistence.Repositories.Users;

public class UserRepository(UserDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<bool> EmailAlreadyExistsAsync(Email email, CancellationToken cancellation)
    {
        var user = await ListAsync(x => x.Email.Address == email.Address, cancellation);

        return user.Any();
    }

    public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellation)
    {
        return await _context.Users.SingleOrDefaultAsync(x => x.Email.Address == email.Address, cancellation);
    }
}
