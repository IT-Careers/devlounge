namespace DevLounge.Data.Models
{
    public class ForumAttachment : BaseEntity
    {
        public string FileUrl { get; set; }

        public bool IsImage { get; set; }

        public bool IsTextType { get; set; }
    }
}
