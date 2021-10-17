using TwitterClone.Domain.Entities.Base;

namespace TwitterClone.Domain.Entities;

public class TweetLike : EntityBase
{
    public TweetLike(Guid userId, Guid tweetId)
    {
        UserId = userId;
        TweetId = tweetId;
    }

    public Guid UserId { get; }
    public virtual User? User { get; }
    public Guid TweetId { get; }
    public virtual Tweet? Tweet { get; }
}
