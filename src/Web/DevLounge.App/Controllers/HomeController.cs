using DevLounge.Service.Data.ForumSections;
using DevLounge.Service.Utility;
using DevLounge.Web.Models;
using DevLounge.Web.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace DevLounge.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IForumSectionsService forumSectionService;

        public HomeController(
            IForumSectionsService forumSectionService)
        {
            this.forumSectionService = forumSectionService;
        }

        public IActionResult Index()
        {
            var sections = this.forumSectionService.GetAllForumSections().ToList();

            return View(new HomeViewModel
            {
                Sections = sections
            });
        }
    }
}
