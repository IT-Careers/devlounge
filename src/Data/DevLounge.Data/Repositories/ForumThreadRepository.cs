using DevLounge.Data.Models;
using Microsoft.AspNetCore.Http;

namespace DevLounge.Data.Repositories
{
    public class ForumThreadRepository : BaseRepository<ForumThread>
    {
        public ForumThreadRepository(DevLoungeDbContext devLoungeDbContext, IHttpContextAccessor httpContextAccessor) : base(devLoungeDbContext, httpContextAccessor)
        {
        }
    }
}
