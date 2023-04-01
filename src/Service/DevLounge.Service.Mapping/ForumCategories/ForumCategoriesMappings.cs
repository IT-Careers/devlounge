using DevLounge.Data.Models;
using DevLounge.Service.Mapping.ForumAttachments;
using DevLounge.Service.Mapping.ForumSections;
using DevLounge.Service.Mapping.ForumThreads;
using DevLounge.Service.Mapping.ForumUsers;
using DevLounge.Service.Models.ForumCategories;
using System.Runtime.InteropServices;

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
                ThumbnailImage = forumCategoryDto.ThumbnailImage?.ToEntity(),
                CoverImage = forumCategoryDto.CoverImage?.ToEntity(),
                CreatedOn = forumCategoryDto.CreatedOn
            };  
        }

        public static ForumCategoryDto ToDto(
            this ForumCategory forumCategory,
            bool fetchImages = true,
            bool fetchSection = true,
            bool fetchThreads = true,
            bool fetchUser = true)
        {
            return new ForumCategoryDto
            {
                Id = forumCategory.Id,
                Name = forumCategory.Name,
                Description = forumCategory.Description,
                ThumbnailImage = fetchImages ? forumCategory.ThumbnailImage?.ToDto() : null,
                CoverImage = fetchImages ? forumCategory.CoverImage?.ToDto() : null,
                Section = fetchSection ? forumCategory.Section?.ToDto(fetchCategories: false) : null,
                Threads = fetchThreads ? forumCategory.Threads?.Select(thread => thread.ToDto(fetchCategory: false)).ToList() : null,
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
