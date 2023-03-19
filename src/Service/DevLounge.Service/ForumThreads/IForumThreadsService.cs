using DevLounge.Service.Models.ForumThreads;

namespace DevLounge.Service.ForumThreads
{
    public interface IForumThreadsService
    {
        Task<ForumThreadDto> CreateForumThread(ForumThreadDto forumThreadDto);

        IQueryable<ForumThreadDto> GetAllForumThreads(bool fetchDeleted = false);

        Task<ForumThreadDto> GetForumThreadById(long id);

        Task<ForumThreadDto> UpdateForumThread(long id, ForumThreadDto forumThreadDto, string userId, string userRole);

        Task<ForumThreadDto> DeleteForumThread(long id, string userId, string userRole);
    }
}
