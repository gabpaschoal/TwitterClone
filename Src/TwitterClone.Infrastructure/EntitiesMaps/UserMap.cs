using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterClone.Domain.Entities;
using TwitterClone.Infrastructure.EntitiesMaps.Base;

namespace TwitterClone.Infrastructure.EntitiesMaps;

public class UserMap : BaseEntityConfiguration<User>
{
    public override void CustomConfiguration(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasColumnType("char").HasMaxLength(100);

        builder.Property(x => x.NickName).IsRequired().HasColumnType("char").HasMaxLength(30);

        builder.Property(x => x.Email).IsRequired().HasColumnType("char").HasMaxLength(70);

        builder.Property(x => x.Password).IsRequired().HasColumnType("char").HasMaxLength(70);

        builder.Property(x => x.IsActive).IsRequired();

        builder.HasMany(x => x.Tweets).WithOne(x => x.User).HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.TweetLikes).WithOne(x => x.User).HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.UserBlocks).WithOne(x => x.User).HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.UserFollows).WithOne(x => x.User).HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
