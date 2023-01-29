using DevLounge.Service.Models.ForumCategories;

namespace DevLounge.Service.ForumCategories
{
    public interface IForumCategoryService
    {
        Task<ForumCategoryDto> CreateForumCategory(ForumCategoryDto forumCategoryDto);

        IQueryable<ForumCategoryDto> GetAllForumCategories(bool isExtended = false);

        Task<ForumCategoryDto> GetForumCategoryById(long id);

        Task<ForumCategoryDto> UpdateForumCategory(long id, ForumCategoryDto forumCategoryDto);

        Task<ForumCategoryDto> DeleteForumCategory(long id);
    }
}
