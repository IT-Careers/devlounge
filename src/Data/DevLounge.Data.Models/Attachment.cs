namespace DevLounge.Data.Models
{
    public class Attachment : BaseEntity
    {
        public string FileUrl { get; set; }

        public bool IsImage { get; set; }

        public bool IsTextType { get; set; }
    }
}
