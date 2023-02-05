using DevLounge.Service.Models.ForumAttachments;

namespace DevLounge.Service.Models.ForumReplies
{
    public class ForumReplyAttachmentDto
    {
        public long ReplyId { get; set; }

        public ForumReplyDto Reply { get; set; }

        public long AttachmentId { get; set; }

        public ForumAttachmentDto Attachment { get; set; }
    }
}