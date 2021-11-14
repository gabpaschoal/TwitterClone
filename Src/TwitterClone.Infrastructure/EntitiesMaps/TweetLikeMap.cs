using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterClone.Domain.Entities;
using TwitterClone.Infrastructure.EntitiesMaps.Base;

namespace TwitterClone.Infrastructure.EntitiesMaps;

public class TweetLikeMap : BaseEntityConfiguration<TweetLike>
{
    public override void CustomConfiguration(EntityTypeBuilder<TweetLike> builder)
    {
        builder
            .HasOne(x => x.User).WithMany()
            .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.Tweet).WithMany()
            .HasForeignKey(x => x.TweetId).OnDelete(DeleteBehavior.NoAction);
    }
}
