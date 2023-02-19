using DevLounge.Service.Models.ForumCategories;
using DevLounge.Service.Models.ForumReplies;
using DevLounge.Service.Models.ForumSections;
using DevLounge.Service.Models.ForumThreads;

namespace DevLounge.Service.Models.ForumUsers
{
    public class DevLoungeUserDto
    {
        public string Id { get; set; }
       
        public string UserName { get; set; }

        public string Email { get; set; }

        public string ThumbnailUrl { get; set; }

        public DateTime RegisteredOn { get; set; }

        public ICollection<ForumReplyDto> RepliesCreated { get; set; }

        public ICollection<ForumReplyDto> RepliesModified { get; set; }

        public ICollection<ForumReplyDto> RepliesDeleted { get; set; }

        public ICollection<ForumThreadDto> ThreadsCreated { get; set; }

        public ICollection<ForumThreadDto> ThreadsModified { get; set; }

        public ICollection<ForumThreadDto> ThreadsDeleted { get; set; }

        public ICollection<ForumCategoryDto> CategoriesCreated { get; set; }

        public ICollection<ForumCategoryDto> CategoriesModified { get; set; }

        public ICollection<ForumCategoryDto> CategoriesDeleted { get; set; }

        public ICollection<ForumSectionDto> SectionsCreated { get; set; }

        public ICollection<ForumSectionDto> SectionsModified { get; set; }

        public ICollection<ForumSectionDto> SectionsDeleted { get; set; }
    }
}
