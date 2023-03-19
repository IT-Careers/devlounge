using DevLounge.Service.Models.ForumCategories;

namespace DevLounge.Service.Data.ForumCategories
{
    public interface IForumCategoriesService
    {
        Task<ForumCategoryDto> CreateForumCategory(ForumCategoryDto forumCategoryDto);

        IQueryable<ForumCategoryDto> GetAllForumCategories(bool fetchDeleted = false);

        Task<ForumCategoryDto> GetForumCategoryById(long id);

        Task<ForumCategoryDto> UpdateForumCategory(long id, ForumCategoryDto forumCategoryDto);

        Task<ForumCategoryDto> DeleteForumCategory(long id);
    }
}
