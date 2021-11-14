using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterClone.Domain.Entities;
using TwitterClone.Infrastructure.EntitiesMaps.Base;

namespace TwitterClone.Infrastructure.EntitiesMaps;

public class TweetMap : BaseEntityConfiguration<Tweet>
{
    public override void CustomConfiguration(EntityTypeBuilder<Tweet> builder)
    {
        builder
            .HasOne(x => x.User).WithMany()
            .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.TweetMessage).IsRequired().HasColumnType("char").HasMaxLength(240);

        builder
            .HasOne(x => x.TweetReference).WithMany()
            .HasForeignKey(x => x.TweetReferenceId).OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.ETweetType).IsRequired().HasColumnType("int");

        builder
            .HasMany(x => x.TweetLikes).WithOne(x => x.Tweet)
            .HasForeignKey(x => x.TweetId).OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(x => x.TweetRetweets).WithOne(x => x.TweetReference)
            .HasForeignKey(x => x.TweetReferenceId).OnDelete(DeleteBehavior.NoAction);
    }
}
