using Microsoft.AspNetCore.Http;

namespace DevLounge.Web.Models.Administration.ForumCategories
{
    public class CreateForumCategoryBindingModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        // This is only for visualization
        public string ThumbnailImageUrl { get; set; }

        public IFormFile ThumbnailImage { get; set; }

        // This is only for visualization
        public string CoverImageUrl { get; set; }

        public IFormFile CoverImage { get; set; }

        public long SectionId { get; set; }
    }
}
