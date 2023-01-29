using DevLounge.Service.Models.ForumUsers;

namespace DevLounge.Service.Models
{
    public class BaseDto
    {
        public long Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DevLoungeUserDto CreatedBy { get; set; }
    }
}
