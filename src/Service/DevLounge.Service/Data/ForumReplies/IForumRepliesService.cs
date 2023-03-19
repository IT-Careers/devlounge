using DevLounge.Service.Models.ForumReplies;

namespace DevLounge.Service.Data.ForumReplies
{
    public interface IForumRepliesService
    {
        Task<ForumReplyDto> CreateForumReply(ForumReplyDto forumReplyDto);

        IQueryable<ForumReplyDto> GetAllForumReplies();

        Task<ForumReplyDto> GetForumReplyById(long id);

        Task<ForumReplyDto> UpdateForumReply(long id, ForumReplyDto forumReplyDto, string userId, string userRole);

        Task<ForumReplyDto> DeleteForumReply(long id, string userId, string userRole);
    }
}
