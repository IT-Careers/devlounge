namespace DevLounge.Data.Models
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DevLoungeUser CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public DevLoungeUser ModifiedBy { get; set; }

        public DateTime DeletedOn { get; set; }

        public DevLoungeUser DeletedBy { get; set; }
    }
}
