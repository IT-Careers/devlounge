using DevLounge.Data.Models;
using Microsoft.AspNetCore.Http;

namespace DevLounge.Data.Repositories
{
    public class ForumAttachmentRepository : BaseRepository<ForumAttachment>
    {
        public ForumAttachmentRepository(DevLoungeDbContext devLoungeDbContext, IHttpContextAccessor httpContextAccessor) : base(devLoungeDbContext, httpContextAccessor)
        {
        }
    }
}
