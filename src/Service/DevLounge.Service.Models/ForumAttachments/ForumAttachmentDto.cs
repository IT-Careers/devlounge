namespace DevLounge.Service.Models.ForumAttachments
{
    public class ForumAttachmentDto : BaseDto
    {
        public string FileUrl { get; set; }

        public bool IsImage { get; set; }

        public bool IsTextType { get; set; }
    }
}
