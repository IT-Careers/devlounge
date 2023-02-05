using DevLounge.Service.Models.ForumCategories;

namespace DevLounge.Web.Models.ForumThreads
{
    public class CreateForumThreadBindingModel
    {
        public string Title { get; set; }

        public string Discussion { get; set; }

        public ForumCategoryDto Category { get; set; }
    }
}
