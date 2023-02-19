using Microsoft.AspNetCore.Identity;

namespace DevLounge.Data.Models
{
    public class DevLoungeUser : IdentityUser
    {
        public string? ThumbnailUrl { get; set; }

        public DateTime RegisteredOn { get; set; }

        public ICollection<ForumReply> RepliesCreated { get; set; }

        public ICollection<ForumReply> RepliesModified { get; set; }

        public ICollection<ForumReply> RepliesDeleted { get; set; }

        public ICollection<ForumThread> ThreadsCreated { get; set; }

        public ICollection<ForumThread> ThreadsModified { get; set; }

        public ICollection<ForumThread> ThreadsDeleted { get; set; }

        public ICollection<ForumCategory> CategoriesCreated { get; set; }

        public ICollection<ForumCategory> CategoriesModified { get; set; }

        public ICollection<ForumCategory> CategoriesDeleted { get; set; }

        public ICollection<ForumSection> SectionsCreated { get; set; }

        public ICollection<ForumSection> SectionsModified { get; set; }

        public ICollection<ForumSection> SectionsDeleted { get; set; }
    }
}
