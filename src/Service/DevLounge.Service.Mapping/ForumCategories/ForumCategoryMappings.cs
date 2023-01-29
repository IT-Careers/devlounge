using DevLounge.Data.Models;
using DevLounge.Service.Mapping.ForumSections;
using DevLounge.Service.Mapping.ForumUsers;
using DevLounge.Service.Models.ForumCategories;

namespace DevLounge.Service.Mapping.ForumCategories
{
    public static class ForumCategoryMappings
    {
        public static ForumCategory ToEntity(this ForumCategoryDto forumCategoryDto)
        {
            return new ForumCategory
            {
                Id = forumCategoryDto.Id,
                Name = forumCategoryDto.Name,
                Description = forumCategoryDto.Description,
                ThumbnailImageUrl = forumCategoryDto.ThumbnailImageUrl,
                CoverImageUrl = forumCategoryDto.CoverImageUrl,
                CreatedOn = forumCategoryDto.CreatedOn
            };  
        }

        public static ForumCategoryDto ToDto(this ForumCategory forumCategory, bool isExtended = false)
        {
            return new ForumCategoryDto
            {
                Id = forumCategory.Id,
                Name = forumCategory.Name,
                Description = forumCategory.Description,
                ThumbnailImageUrl = forumCategory.ThumbnailImageUrl,
                CoverImageUrl = forumCategory.CoverImageUrl,
                Section = isExtended ? forumCategory.Section?.ToDto() : null,
                CreatedOn = forumCategory.CreatedOn,
                CreatedBy = isExtended ? forumCategory.CreatedBy?.ToDto() : null,
            };
        }
    }
}
