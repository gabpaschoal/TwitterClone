using TwitterClone.Domain.Entities.Base;

namespace TwitterClone.Domain.Entities;

public class User : EntityBase, IActivable
{
    public User(string name, string nickName, string email, string password)
    {
        Name = name;
        NickName = nickName;
        Email = email;
        Password = password;
        IsActive = true;

        Tweets = new List<Tweet>();
        TweetLikes = new List<TweetLike>();
        UserBlocks = new List<UserBlock>();
        UserFollows = new List<UserFollow>();
    }

    public string Name { get; private set; }
    public string NickName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public bool IsActive { get; private set; }

    public virtual ICollection<Tweet> Tweets { get; }
    public virtual ICollection<TweetLike> TweetLikes { get; }
    public virtual ICollection<UserBlock> UserBlocks { get; }
    public virtual ICollection<UserFollow> UserFollows { get; }

    public void Activate() => IsActive = true;
    public void Inactivate() => IsActive = false;
}
