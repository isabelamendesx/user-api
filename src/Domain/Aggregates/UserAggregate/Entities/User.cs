using Users.Domain.Common;
using Users.Domain.ValueObjects;

namespace Users.Domain.Aggregates.UserAggregate.Entities;

public class User : AuditableEntity, IAggregateRoot
{
    public Name Name { get; private set; } = null!;
    public Nickname Nickname { get; private set; } = null!;

    public User(Name name, Nickname nickname, Email email, Phone phone, bool isActive = true)
    {
        Name = name;
        Nickname = nickname;
    }

    protected User() { }

    public void SetName(Name name) => Name = name;
    public void SetPhone(Phone? phone) => Phone = phone;
}
