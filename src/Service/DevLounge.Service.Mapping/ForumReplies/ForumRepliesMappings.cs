using DevLounge.Data.Models;
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
    }
}
