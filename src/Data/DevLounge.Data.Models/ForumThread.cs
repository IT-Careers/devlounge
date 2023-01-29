namespace DevLounge.Data.Models
{
    public class ForumThread : BaseEntity
    {
        public string Title { get; set; }

        public List<ForumReply> Replies { get; set; }

        public ForumCategory Category { get; set; }
    }
}
