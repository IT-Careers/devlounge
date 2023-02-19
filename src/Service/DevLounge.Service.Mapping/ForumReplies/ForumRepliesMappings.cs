using DevLounge.Data.Models;
using DevLounge.Service.Mapping.ForumThreads;
using DevLounge.Service.Mapping.ForumUsers;
using DevLounge.Service.Models.ForumReplies;

namespace DevLounge.Service.Mapping.ForumReplies
{
    public static class ForumRepliesMappings
    {
        public static ForumReply ToEntity(this ForumReplyDto forumReplyDto)
        {
            return new ForumReply {
                Content = forumReplyDto.Content
            };
        }

        public static ForumReplyDto ToDto(
            this ForumReply forumReply,
            bool fetchAttachments = true,
            bool fetchThread = true,
            bool fetchUser = true)
        {
            return new ForumReplyDto
            {
                Id = forumReply.Id,
                Content = forumReply.Content,
                // Attachments = fetchAttachments ? forumReply.Attachments.Select(attachment => attachment.ToDto(fetchReply: false)).ToList() : null;
                Thread = fetchThread ? forumReply.Thread.ToDto(fetchReplies: false) : null,
                CreatedOn = forumReply.CreatedOn,
                CreatedBy = fetchUser ? forumReply.CreatedBy?.ToDto(fetchCreatedReplies: true, fetchCreatedThreads: true) : null,
                ModifiedOn = forumReply.ModifiedOn,
                ModifiedBy = fetchUser ? forumReply.ModifiedBy?.ToDto() : null,
                DeletedOn = forumReply.DeletedOn,
                DeletedBy = fetchUser ? forumReply.DeletedBy?.ToDto() : null,
            };
        }
    }
}
