using TwitterClone.Domain.Entities;
using TwitterClone.Domain.Repositories.Data.Base;

namespace TwitterClone.Domain.Repositories.Data;

public interface IUserRepository : IDataRepositoryBase<User>
{
    bool ExistsNickName(string nickName);
}
