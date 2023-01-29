namespace DevLounge.Web.Models.Error
{
    public class ErrorModel
    {
        public string Message { get; set; }

        public DateTime TimeStamp { get; set; }

        public Exception Details { get; set; }
    }
}
