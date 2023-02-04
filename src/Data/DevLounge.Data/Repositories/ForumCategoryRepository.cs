using DevLounge.Data.Models;
using Microsoft.AspNetCore.Http;

namespace DevLounge.Data.Repositories
{
    public class ForumCategoryRepository : BaseRepository<ForumCategory>
    {
        public ForumCategoryRepository(DevLoungeDbContext devLoungeDbContext, IHttpContextAccessor httpContextAccessor) : base(devLoungeDbContext, httpContextAccessor)
        {
        }
    }
}
