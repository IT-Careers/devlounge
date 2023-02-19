using DevLounge.Data.Models;
using DevLounge.Service.Mapping.ForumUsers;
using DevLounge.Service.Mapping.ForumCategories;
using DevLounge.Service.Models.ForumThreads;
using DevLounge.Service.Mapping.ForumReplies;

namespace DevLounge.Service.Mapping.ForumThreads
{
    public static class ForumThreadsMappings
    {
        public static ForumThread ToEntity(this ForumThreadDto forumThreadDto)
        {
            return new ForumThread
            {
                Id = forumThreadDto.Id,
                Title = forumThreadDto.Title
            };
        }

        public static ForumThreadDto ToDto(
            this ForumThread forumThread,
            bool fetchCategory = true,
            bool fetchReplies = true,
            bool fetchUser = true)
        {
            return new ForumThreadDto
            {
                Id = forumThread.Id,
                Title = forumThread.Title,
                Views = forumThread.Views,
                Replies = fetchReplies ? forumThread.Replies?.Select(reply => reply.ToDto(fetchThread: false)).ToList() : null,
                Category = fetchCategory ? forumThread.Category?.ToDto(fetchThreads: false) : null,
                CreatedOn = forumThread.CreatedOn,
                CreatedBy = fetchUser ? forumThread.CreatedBy?.ToDto() : null,
                ModifiedOn = forumThread.ModifiedOn,
                ModifiedBy = fetchUser ? forumThread.ModifiedBy?.ToDto() : null,
                DeletedOn = forumThread.DeletedOn,
                DeletedBy = fetchUser ? forumThread.DeletedBy?.ToDto() : null,
            };
        }
    }
}
