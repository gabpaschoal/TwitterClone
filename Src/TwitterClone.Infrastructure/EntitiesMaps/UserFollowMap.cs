using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterClone.Domain.Entities;
using TwitterClone.Infrastructure.EntitiesMaps.Base;

namespace TwitterClone.Infrastructure.EntitiesMaps;

public class UserFollowMap : BaseEntityConfiguration<UserFollow>
{
    public override void CustomConfiguration(EntityTypeBuilder<UserFollow> builder)
    {
        builder
            .HasOne(x => x.User).WithMany()
            .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.UserFollowed).WithMany()
            .HasForeignKey(x => x.UserFollowedId).OnDelete(DeleteBehavior.NoAction);
    }
}
