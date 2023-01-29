namespace DevLounge.Data.Models
{
    public class ForumCategory : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ThumbnailImageUrl { get; set; }

        public string CoverImageUrl { get; set; }

        public ForumSection Section { get; set; }
    }
}
