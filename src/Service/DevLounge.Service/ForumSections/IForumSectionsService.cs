using DevLounge.Service.Models.ForumSections;

namespace DevLounge.Service.ForumSections
{
    public interface IForumSectionsService
    {
        Task<ForumSectionDto> CreateForumSection(ForumSectionDto forumSectionDto);

        IQueryable<ForumSectionDto> GetAllForumSections(bool fetchDeleted = false);

        Task<ForumSectionDto> GetForumSectionById(long id);

        Task<ForumSectionDto> UpdateForumSection(long id, ForumSectionDto forumSectionDto);

        Task<ForumSectionDto> DeleteForumSection(long id);
    }
}
