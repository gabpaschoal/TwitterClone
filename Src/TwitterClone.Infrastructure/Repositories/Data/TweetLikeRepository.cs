using TwitterClone.Domain.Entities;
using TwitterClone.Domain.Repositories.Cache;
using TwitterClone.Infrastructure.Contexts;
using TwitterClone.Infrastructure.Repositories.Data.Base;

namespace TwitterClone.Domain.Repositories.Data;

public class TweetLikeRepository : DataRepositoryBase<TweetLike>, ITweetLikeRepository
{
    public TweetLikeRepository(
        AppDbContext context,
        ICacheRepository cacheRepository) : base(context, cacheRepository)
    { }
}
