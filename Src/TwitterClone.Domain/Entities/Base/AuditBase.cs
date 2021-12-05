namespace TwitterClone.Domain.Entities.Base;

public abstract class AuditBase
{
    public DateTime CreatedAt { get; private set; }
    public Guid UserCreateId { get; private set; }
    public virtual User? UserCreate { get; }

    public DateTime? UpdatedAt { get; private set; }
    public Guid? UserLastUpdateId { get; private set; }
    public virtual User? UserLastUpdate { get; }

    public DateTime? DeletedAt { get; private set; }
    public Guid? UserDeletedId { get; private set; }
    public virtual User? UserDelete { get; }
}