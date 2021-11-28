using TwitterClone.Domain.Entities;
using TwitterClone.Domain.Repositories.Cache;
using TwitterClone.Infrastructure.Contexts;
using TwitterClone.Infrastructure.Repositories.Data.Base;

namespace TwitterClone.Domain.Repositories.Data;

public class UserRepository : DataRepositoryBase<User>, IUserRepository
{
    public UserRepository(
        AppDbContext context,
        ICacheRepository cacheRepository) : base(context, cacheRepository)
    { }

    public bool ExistsUserWithThisEmail(string email)
    {
        var result = Queryable.Any(x => x.Email == email);
        return result;
    }

    public bool ExistsUserWithThisNickName(string nickName)
    {
        var result = Queryable.Any(x => x.NickName == nickName);
        return result;
    }
}
