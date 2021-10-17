using TwitterClone.Domain.Entities.Base;

namespace TwitterClone.Domain.Entities;

public class UserBlock : EntityBase
{
    public UserBlock(Guid userId, Guid userBlockedId)
    {
        UserId = userId;
        UserBlockedId = userBlockedId;
    }

    public Guid UserId { get; }
    public virtual User? User { get; }

    public Guid UserBlockedId { get; }
    public virtual User? UserBlocked { get; }
}
