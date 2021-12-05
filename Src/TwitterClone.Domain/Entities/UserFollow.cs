using TwitterClone.Domain.Entities.Base;

namespace TwitterClone.Domain.Entities;

public class UserFollow : EntityBase
{
    public UserFollow(Guid userId, Guid userFollowedId)
    {
        UserId = userId;
        UserFollowedId = userFollowedId;
    }

    public Guid UserId { get; }
    public virtual User? User { get; }

    public Guid UserFollowedId { get; }
    public virtual User? UserFollowed { get; }
}
