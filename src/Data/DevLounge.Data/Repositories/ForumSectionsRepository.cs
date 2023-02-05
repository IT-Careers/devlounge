using DevLounge.Data.Models;
using Microsoft.AspNetCore.Http;

namespace DevLounge.Data.Repositories
{
    public class ForumSectionsRepository : BaseRepository<ForumSection>
    {
        public ForumSectionsRepository(DevLoungeDbContext devLoungeDbContext, IHttpContextAccessor httpContextAccessor) 
            : base(devLoungeDbContext, httpContextAccessor)
        {
        }
    }
}
