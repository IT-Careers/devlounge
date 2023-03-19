using DevLounge.Data.Models;
using DevLounge.Service.ForumReplies;
using DevLounge.Service.ForumSections;
using DevLounge.Service.ForumThreads;
using DevLounge.Web.Models.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevLounge.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IForumSectionsService forumSectionService;

        public HomeController(IForumSectionsService forumSectionService)
        {
            this.forumSectionService = forumSectionService;
        }

        public IActionResult Index()
        {
            return View(new HomeViewModel
            {
                Sections = this.forumSectionService.GetAllForumSections().ToList()
            });
        }
    }
}
