namespace DevLounge.Data.Models
{
    public class ForumUserAttachment
    {
        public long UserId { get; set; }

        public DevLoungeUser User { get; set; }

        public long AttachmentId { get; set; }

        public ForumAttachment Attachment { get; set; }
    }
}
