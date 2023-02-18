using DevLounge.Service.Models.ForumReplies;
using DevLounge.Service.Models.ForumThreads;
using DevLounge.Web.Models.ForumThreads;

namespace DevLounge.Web.Mapping.ForumThreads
{
    public static class ForumThreadsWebModelMappings
    {
        public static ForumThreadDto ToDto(this CreateForumThreadBindingModel createForumThreadBindingModel)
        {
            return new ForumThreadDto
            {
                Title = createForumThreadBindingModel.Title,
                Category = createForumThreadBindingModel.Category,
                Replies = new List<ForumReplyDto>
                {
                    new ForumReplyDto()
                    {
                        Content = createForumThreadBindingModel.Discussion
                    }
                }
            };
        }
    }
}
