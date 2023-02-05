using DevLounge.Data.Models;
using Microsoft.AspNetCore.Http;

namespace DevLounge.Data.Repositories
{
    public class ForumCategoriesRepository : BaseRepository<ForumCategory>
    {
        public ForumCategoriesRepository(DevLoungeDbContext devLoungeDbContext, IHttpContextAccessor httpContextAccessor) : base(devLoungeDbContext, httpContextAccessor)
        {
        }
    }
}
