namespace DevLounge.Data.Models
{
    public class ForumReply : BaseEntity
    {
        public string Content { get; set; }

        public ForumThread Thread { get; set; }

        public List<ForumReplyAttachment> Attachments { get; set; }
    }
}
