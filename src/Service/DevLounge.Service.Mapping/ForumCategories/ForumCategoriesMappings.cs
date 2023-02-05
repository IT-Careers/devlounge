using DevLounge.Data.Models;
using DevLounge.Service.Mapping.ForumSections;
using DevLounge.Service.Mapping.ForumUsers;
using DevLounge.Service.Models.ForumCategories;

namespace DevLounge.Service.Mapping.ForumCategories
{
    public static class ForumCategoriesMappings
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

        public static ForumCategoryDto ToDto(
            this ForumCategory forumCategory, 
            bool fetchSection = true, 
            bool fetchUser = true)
        {
            return new ForumCategoryDto
            {
                Id = forumCategory.Id,
                Name = forumCategory.Name,
                Description = forumCategory.Description,
                ThumbnailImageUrl = forumCategory.ThumbnailImageUrl,
                CoverImageUrl = forumCategory.CoverImageUrl,
                Section = fetchSection ? forumCategory.Section?.ToDto(fetchCategories: false) : null,
                CreatedOn = forumCategory.CreatedOn,
                CreatedBy = fetchUser ? forumCategory.CreatedBy?.ToDto() : null,
                ModifiedOn = forumCategory.ModifiedOn,
                ModifiedBy = fetchUser ? forumCategory.ModifiedBy?.ToDto() : null,
                DeletedOn = forumCategory.DeletedOn,
                DeletedBy = fetchUser ? forumCategory.DeletedBy?.ToDto() : null,
            };
        }
    }
}
