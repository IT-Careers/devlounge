using DevLounge.Data.Models;
using Microsoft.AspNetCore.Http;

namespace DevLounge.Data.Repositories
{
    public class ForumReplyRepository : BaseRepository<ForumReply>
    {
        public ForumReplyRepository(DevLoungeDbContext devLoungeDbContext, IHttpContextAccessor httpContextAccessor) : base(devLoungeDbContext, httpContextAccessor)
        {
        }
    }
}
