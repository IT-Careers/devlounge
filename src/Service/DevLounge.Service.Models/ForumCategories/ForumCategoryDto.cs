using DevLounge.Service.Models.ForumSections;
using DevLounge.Service.Models.ForumThreads;

namespace DevLounge.Service.Models.ForumCategories
{
    public class ForumCategoryDto : BaseDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ThumbnailImageUrl { get; set; }

        public string CoverImageUrl { get; set; }

        public ForumSectionDto Section { get; set; }

        public List<ForumThreadDto> Threads { get; set; }
    }
}
