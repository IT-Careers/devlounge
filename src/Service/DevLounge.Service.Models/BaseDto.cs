using DevLounge.Service.Models.ForumUsers;

namespace DevLounge.Service.Models
{
    public class BaseDto
    {
        public long Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DevLoungeUserDto CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public DevLoungeUserDto ModifiedBy { get; set; }

        public DateTime DeletedOn { get; set; }

        public DevLoungeUserDto DeletedBy { get; set; }
    }
}
