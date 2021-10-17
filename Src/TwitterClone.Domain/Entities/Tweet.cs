using TwitterClone.Domain.Entities.Base;
using TwitterClone.Domain.Enums;

namespace TwitterClone.Domain.Entities;

public class Tweet : EntityBase
{
    /// <summary>
    /// Tweet Contructor
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="tweetMessage"></param>
    public Tweet(Guid userId, string tweetMessage)
    {
        UserId = userId;
        TweetMessage = tweetMessage;
        ETweetType = ETweetType.Tweet;
        TweetLikes = new List<TweetLike>();
    }

    /// <summary>
    /// Retweet Contructor
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="tweetReferenceId"></param>
    public Tweet(Guid userId, Guid tweetReferenceId)
    {
        UserId = userId;
        TweetReferenceId = tweetReferenceId;
        ETweetType = ETweetType.Retweet;
        TweetLikes = new List<TweetLike>();
    }

    public Guid UserId { get; }
    public virtual User? User { get; }
    public string? TweetMessage { get; private set; }

    public Guid? TweetReferenceId { get; }
    public virtual Tweet? TweetReference { get; }

    public ETweetType ETweetType { get; }

    public virtual ICollection<TweetLike> TweetLikes { get; }
}
