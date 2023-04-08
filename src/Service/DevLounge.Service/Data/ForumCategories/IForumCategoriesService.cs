using DevLounge.Service.Models.ForumCategories;
using Microsoft.AspNetCore.Http;

namespace DevLounge.Service.Data.ForumCategories
{
    public interface IForumCategoriesService
    {
        Task<ForumCategoryDto> CreateForumCategory(ForumCategoryDto forumCategoryDto, IFormFile thumbnailImage, IFormFile coverImage);

        IQueryable<ForumCategoryDto> GetAllForumCategories(bool fetchDeleted = false);

        Task<ForumCategoryDto> GetForumCategoryById(long id);

        Task<ForumCategoryDto> UpdateForumCategory(long id, ForumCategoryDto forumCategoryDto, IFormFile thumbnailImage, IFormFile coverImage);

        Task<ForumCategoryDto> DeleteForumCategory(long id);
    }
}
