namespace DevLounge.Data.Models
{
    public class ForumSection : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<ForumCategory> Categories { get; set; }
    }
}
