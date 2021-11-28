using Microsoft.EntityFrameworkCore;
using TwitterClone.Domain.Entities.Base;
using TwitterClone.Domain.Repositories.Cache;
using TwitterClone.Domain.Repositories.Data.Base;
using TwitterClone.Infrastructure.Contexts;

namespace TwitterClone.Infrastructure.Repositories.Data.Base;

public abstract class DataRepositoryBase<TEntity>
    : IDataRepositoryBase<TEntity> where TEntity : EntityBase
{
    private readonly AppDbContext _context;
    private readonly ICacheRepository _cacheRepository;
    public readonly DbSet<TEntity> Queryable;

    protected DataRepositoryBase(
        AppDbContext context,
        ICacheRepository cacheRepository)
    {
        _context = context;
        _cacheRepository = cacheRepository;
        Queryable = _context.Set<TEntity>();
    }

    public async ValueTask AddAsync(TEntity data)
    {
        await _context.AddAsync(data);
        await _cacheRepository.SetAsync(data.Id.ToString(), data);
    }

    public async ValueTask DeleteAsync(Guid id)
    {
        var entity = GetByIdAsync(id);
        _context.Remove(entity);
        await _cacheRepository.RemoveAsync(id.ToString());
    }

    public async ValueTask<TEntity?> GetByIdAsync(Guid id)
    {
        _ = await _cacheRepository.TryGetAsync(id.ToString(), out TEntity? result);

        if (result is not null)
            return result;

        result = await Queryable.FirstOrDefaultAsync(x => x.Id == id);

        await _cacheRepository.SetAsync(id.ToString(), result);

        return result;
    }

    public async ValueTask UpdateAsync(TEntity data)
    {
        _context.Update(data);
        await _cacheRepository.RemoveAsync(data.Id.ToString());
    }
}
