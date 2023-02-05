using DevLounge.Service.Models.ForumThreads;

namespace DevLounge.Service.ForumThreads
{
    public interface IForumThreadsService
    {
        Task<ForumThreadDto> CreateForumThread(ForumThreadDto forumThreadDto);

        IQueryable<ForumThreadDto> GetAllForumThreads();

        Task<ForumThreadDto> GetForumThreadById(long id);

        Task<ForumThreadDto> UpdateForumThread(long id, ForumThreadDto forumThreadDto, string userId, string userRole);

        Task<ForumThreadDto> DeleteForumThread(long id, string userId, string userRole);
    }
}
