namespace DevLounge.Data.Models
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DevLoungeUser CreatedBy { get; set; }
    }
}
