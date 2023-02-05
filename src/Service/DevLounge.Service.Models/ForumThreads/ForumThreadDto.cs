using DevLounge.Service.Models.ForumCategories;
using DevLounge.Service.Models.ForumReplies;

namespace DevLounge.Service.Models.ForumThreads
{
    public class ForumThreadDto : BaseDto
    {
        public string Title { get; set; }

        public int Views { get; set; }

        public List<ForumReplyDto> Replies { get; set; }

        public ForumCategoryDto Category { get; set; }
    }
}
