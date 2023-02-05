using DevLounge.Service.Models.ForumThreads;

namespace DevLounge.Service.Models.ForumReplies
{
    public class ForumReplyDto : BaseDto
    {
        public string Content { get; set; }

        public ForumThreadDto Thread { get; set; }

        public List<ForumReplyAttachmentDto> Attachments { get; set; }
    }
}
