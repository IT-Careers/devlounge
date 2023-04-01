namespace DevLounge.Data.Models
{
    public class ForumCategory : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ForumAttachment ThumbnailImage { get; set; }

        public ForumAttachment CoverImage { get; set; }

        public ForumSection Section { get; set; }

        public List<ForumThread> Threads { get; set; }
    }
}
