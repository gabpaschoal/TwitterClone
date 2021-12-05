using TwitterClone.Domain.Entities;
using TwitterClone.Domain.Repositories.Cache;
using TwitterClone.Domain.Repositories.Data;
using TwitterClone.Infrastructure.Contexts;
using TwitterClone.Infrastructure.Repositories.Data.Base;

namespace TwitterClone.Infrastructure.Repositories.Data;

public class UserFollowRepository : DataRepositoryBase<UserFollow>, IUserFollowRepository
{
    public UserFollowRepository(
        AppDbContext context,
        ICacheRepository cacheRepository) : base(context, cacheRepository)
    { }
}
