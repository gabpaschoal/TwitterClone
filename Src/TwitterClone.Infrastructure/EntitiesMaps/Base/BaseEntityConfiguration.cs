using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterClone.Domain.Entities.Base;

namespace TwitterClone.Infrastructure.EntitiesMaps.Base;

public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        CustomConfiguration(builder);

        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.UserCreate).WithMany()
            .HasForeignKey(x => x.UserCreateId).OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.UserLastUpdate).WithMany()
            .HasForeignKey(x => x.UserLastUpdateId).OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.UserDelete).WithMany()
            .HasForeignKey(x => x.UserDeletedId).OnDelete(DeleteBehavior.NoAction);
    }

    public abstract void CustomConfiguration(EntityTypeBuilder<TEntity> builder);
}
