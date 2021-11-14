using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TwitterClone.Domain.Entities.Base;
using TwitterClone.Domain.Utils.Extensions;
using TwitterClone.Infrastructure.EntitiesMaps;

namespace TwitterClone.Infrastructure.Contexts;

public class AppDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AppDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TweetLikeMap());
        modelBuilder.ApplyConfiguration(new TweetMap());
        modelBuilder.ApplyConfiguration(new UserBlockMap());
        modelBuilder.ApplyConfiguration(new UserFollowMap());
        modelBuilder.ApplyConfiguration(new UserMap());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.State == EntityState.Added
                        || e.State == EntityState.Modified
                        || e.State == EntityState.Deleted);

        if (!entries.Any())
            return await base.SaveChangesAsync(cancellationToken);

        var currentlyUser = _httpContextAccessor?.HttpContext?.GetUserId();

        foreach (var entityEntry in entries)
        {
            var auditBase = (AuditBase)entityEntry.Entity;

            if (entityEntry.State == EntityState.Added)
            {
                auditBase.SetValue(x => x.CreatedAt, DateTime.UtcNow);
                auditBase.SetValue(x => x.UserCreateId, currentlyUser);
            }
            else if (entityEntry.State == EntityState.Deleted)
            {
                entityEntry.State = EntityState.Unchanged;
                auditBase.SetValue(x => x.DeletedAt, DateTime.UtcNow);
                auditBase.SetValue(x => x.UserDeletedId, currentlyUser);
            }
            else
            {
                Entry(auditBase).Property(p => p.CreatedAt).IsModified = false;
                Entry(auditBase).Property(p => p.UserCreateId).IsModified = false;
            }

            auditBase.SetValue(x => x.UpdatedAt, DateTime.UtcNow);
            auditBase.SetValue(x => x.UserLastUpdateId, currentlyUser);
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
