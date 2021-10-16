namespace TwitterClone.Domain.Entities.Base;

public abstract class EntityBase : AuditBase
{
    public EntityBase()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
}
