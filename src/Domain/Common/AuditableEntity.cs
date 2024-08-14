namespace Users.Domain.Common;

public abstract class AuditableEntity : Entity
{
    public DateTime Created { get; private set; }
    public DateTime? Updated { get; private set; }

    protected AuditableEntity()
    {
        Created = DateTime.UtcNow;
        Updated = DateTime.UtcNow;
    }

    public void Modify() => Updated = DateTime.UtcNow;
}
