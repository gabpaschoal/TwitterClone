namespace TwitterClone.Domain.Repositories.Data.Base;

public interface IUnitOfWork
{
    public void BeginTransaction();
    public void CommitTransaction();
    public void RollBackTransaction();
}
