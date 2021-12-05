using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterClone.Domain.Entities;
using TwitterClone.Infrastructure.EntitiesMaps.Base;

namespace TwitterClone.Infrastructure.EntitiesMaps;

public class UserBlockMap : BaseEntityConfiguration<UserBlock>
{
    public override void CustomConfiguration(EntityTypeBuilder<UserBlock> builder)
    {
        builder
            .HasOne(x => x.User).WithMany()
            .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.UserBlocked).WithMany()
            .HasForeignKey(x => x.UserBlockedId).OnDelete(DeleteBehavior.NoAction);
    }
}
