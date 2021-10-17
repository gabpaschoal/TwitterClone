using TwitterClone.Domain.Entities.Base;

namespace TwitterClone.Domain.Repositories.Data.Base;

public interface IDataRepositoryBase<TEntity> where TEntity : EntityBase
{
    ValueTask<TEntity?> GetByIdAsync(Guid id);
    ValueTask AddAsync(TEntity data);
    ValueTask UpdateAsync(TEntity data);
    ValueTask DeleteAsync(Guid id);
}
