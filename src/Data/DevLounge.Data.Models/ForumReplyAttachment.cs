namespace DevLounge.Data.Models
{
    public class ForumReplyAttachment
    {
        public long ReplyId { get; set; }

        public ForumReply Reply { get; set; }

        public long AttachmentId { get; set; }

        public Attachment Attachment { get; set; }
    }
}
