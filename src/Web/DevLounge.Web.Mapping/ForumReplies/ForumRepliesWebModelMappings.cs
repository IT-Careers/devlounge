using DevLounge.Service.Models.ForumReplies;
using DevLounge.Service.Models.ForumThreads;
using DevLounge.Web.Models.ForumReplies;

namespace DevLounge.Web.Mapping.ForumReplies
{
    public static class ForumRepliesWebModelMappings
    {
        public static ForumReplyDto ToDto(this CreateForumReplyBindingModel createForumReplyBindingModel)
        {
            return new ForumReplyDto
            {
                Content = createForumReplyBindingModel.Content,
                Thread = new ForumThreadDto
                {
                    Id = createForumReplyBindingModel.ThreadId
                }
            };
        }
    }
}
