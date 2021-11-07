using Microsoft.EntityFrameworkCore.Storage;
using TwitterClone.Domain.Repositories.Data.Base;
using TwitterClone.Infrastructure.Contexts;

namespace TwitterClone.Infrastructure.Repositories.Data.Base;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    private IDbContextTransaction _transaction;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        _transaction.Commit();
    }

    public void RollBackTransaction()
    {
        _transaction.Rollback();
    }
}
