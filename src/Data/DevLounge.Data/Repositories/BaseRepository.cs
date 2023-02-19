using DevLounge.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DevLounge.Data.Repositories
{
    public class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DevLoungeDbContext devLoungeDbContext;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private DevLoungeUser cachedUser;

        public BaseRepository(DevLoungeDbContext devLoungeDbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.devLoungeDbContext = devLoungeDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreatedOn = DateTime.Now;
            entity.CreatedBy = await this.GetCurrentUserAsync();
            await this.devLoungeDbContext.AddAsync(entity);
            await this.devLoungeDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddManyAsync(IEnumerable<TEntity> entities)
        {
            var currentUser = await this.GetCurrentUserAsync();

            await this.devLoungeDbContext.AddRangeAsync(entities.Select(entity =>
            {
                entity.CreatedOn = DateTime.Now;
                entity.CreatedBy = currentUser;

                return entity;
            }).ToList());

            await this.devLoungeDbContext.SaveChangesAsync();
            return entities;
        }

        public IQueryable<TEntity> RetrieveAll()
        {
            return this.RetrieveAllTracked().AsNoTracking();
        }

        public IQueryable<TEntity> RetrieveAllTracked()
        {
            return this.devLoungeDbContext.Set<TEntity>()
                .Include(section => section.CreatedBy)
                .Include(section => section.ModifiedBy)
                .Include(section => section.DeletedBy);
        }

        public async Task<TEntity> EditAsync(TEntity entity)
        {
            entity.ModifiedOn = DateTime.Now;
            entity.ModifiedBy = await this.GetCurrentUserAsync();
            this.devLoungeDbContext.Update(entity);
            await this.devLoungeDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> EditWithoutMetadataAsync(TEntity entity)
        {
            this.devLoungeDbContext.Update(entity);
            await this.devLoungeDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> RemoveAsync(TEntity entity)
        {
            entity.DeletedOn = DateTime.Now;
            entity.DeletedBy = await this.GetCurrentUserAsync();
            this.devLoungeDbContext.Update(entity);
            await this.devLoungeDbContext.SaveChangesAsync();
            return entity;
        }

        protected async Task<DevLoungeUser> GetCurrentUserAsync()
        {
            if(this.cachedUser == null)
            {
                string userId = this._httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                this.cachedUser = await this.devLoungeDbContext.Users
                    .Include(user => user.RepliesCreated)
                    .Include(user => user.RepliesModified)
                    .Include(user => user.RepliesDeleted)
                    .Include(user => user.ThreadsCreated)
                    .Include(user => user.ThreadsModified)
                    .Include(user => user.ThreadsDeleted)
                    .SingleOrDefaultAsync(user => user.Id == userId);
            }

            return this.cachedUser;
        }
    }
}
