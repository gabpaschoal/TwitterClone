using TwitterClone.Domain.Entities;
using TwitterClone.Domain.Repositories.Cache;
using TwitterClone.Domain.Repositories.Data;
using TwitterClone.Infrastructure.Contexts;
using TwitterClone.Infrastructure.Repositories.Data.Base;

namespace TwitterClone.Infrastructure.Repositories.Data;

public class TweetRepository : DataRepositoryBase<Tweet>, ITweetRepository
{
    public TweetRepository(
        AppDbContext context, 
        ICacheRepository cacheRepository) : base(context, cacheRepository)
    { }
}
