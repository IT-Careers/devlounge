using DevLounge.Data.Models;
using Microsoft.AspNetCore.Http;

namespace DevLounge.Data.Repositories
{
    public class ForumSectionRepository : BaseRepository<ForumSection>
    {
        public ForumSectionRepository(DevLoungeDbContext devLoungeDbContext, IHttpContextAccessor httpContextAccessor) 
            : base(devLoungeDbContext, httpContextAccessor)
        {
        }
    }
}
