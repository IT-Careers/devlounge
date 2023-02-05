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
            string userId = this._httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return await this.devLoungeDbContext.Users
                .SingleOrDefaultAsync(user => user.Id == userId);
        }
    }
}
