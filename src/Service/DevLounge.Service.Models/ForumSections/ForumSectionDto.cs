using DevLounge.Service.Models.ForumCategories;

namespace DevLounge.Service.Models.ForumSections
{
    public class ForumSectionDto : BaseDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<ForumCategoryDto> Categories { get; set; }
    }
}
