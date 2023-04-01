using DevLounge.Service.Models.ForumAttachments;
using DevLounge.Service.Models.ForumSections;
using DevLounge.Service.Models.ForumThreads;

namespace DevLounge.Service.Models.ForumCategories
{
    public class ForumCategoryDto : BaseDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ForumAttachmentDto ThumbnailImage { get; set; }

        public ForumAttachmentDto CoverImage { get; set; }

        public ForumSectionDto Section { get; set; }

        public List<ForumThreadDto> Threads { get; set; }
    }
}
