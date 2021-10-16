using TwitterClone.Domain.Entities.Base;

namespace TwitterClone.Domain.Repositories.Data.Base;

public interface IDataRepositoryBase<TEntity> where TEntity : EntityBase
{
    ValueTask<TEntity> GetById(Guid id);
    ValueTask Add(TEntity data);
    ValueTask Update(TEntity data);
    ValueTask Delete(Guid id);
}
