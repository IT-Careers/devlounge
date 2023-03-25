using DevLounge.Service.Data.ForumThreads;
using Microsoft.AspNetCore.Mvc;

namespace DevLounge.Web.Views.Shared.Components
{
    public class ForumLatestThreadsViewComponent : ViewComponent
    {
        private const int LatestForumThreadsCount = 5;

        private readonly IForumThreadsService forumThreadsService;

        public ForumLatestThreadsViewComponent(IForumThreadsService forumThreadsService)
        {
            this.forumThreadsService = forumThreadsService;
        }

        public IViewComponentResult Invoke()
        {
            var latestThreads = this.forumThreadsService.GetAllForumThreads()
                .ToList()
                .OrderByDescending(thread => thread.CreatedOn)
                .Take(LatestForumThreadsCount)
                .ToList();

            return View("/Views/Shared/_ForumLatestThreadsView.cshtml", latestThreads);
        }
    }
}
