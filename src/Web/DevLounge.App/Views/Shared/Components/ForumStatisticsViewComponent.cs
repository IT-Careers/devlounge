using DevLounge.Data.Models;
using DevLounge.Service.ForumReplies;
using DevLounge.Service.ForumThreads;
using DevLounge.Web.Models.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevLounge.Web.Views.Shared.Components
{
    public class ForumStatisticsViewComponent : ViewComponent
    {
        private readonly IForumThreadsService forumThreadsService;

        private readonly IForumRepliesService forumRepliesService;

        private readonly UserManager<DevLoungeUser> forumUserManager;

        public ForumStatisticsViewComponent(
            IForumThreadsService forumThreadsService,
            IForumRepliesService forumRepliesService,
            UserManager<DevLoungeUser> forumUserManager
            )
        {
            this.forumThreadsService = forumThreadsService;
            this.forumRepliesService = forumRepliesService;
            this.forumUserManager = forumUserManager;
        }

        private string AcquireLatestUser()
        {
            var orderedUsers = this.forumUserManager.Users.OrderByDescending(user => user.RegisteredOn);
            var latestUser = orderedUsers.FirstOrDefault();

            if (latestUser == null) return null;

            return latestUser.UserName;
        }

        public IViewComponentResult Invoke()
        {
            return View("/Views/Shared/_ForumStatisticsView.cshtml", new StatisticsViewModel
            {
                ThreadsCount = this.forumThreadsService.GetAllForumThreads().Count(),
                UsersCount = this.forumUserManager.Users.Count(),
                RepliesCount = this.forumRepliesService.GetAllForumReplies().Count(),
                LatestMember = this.AcquireLatestUser()
            });
        }
    }
}
