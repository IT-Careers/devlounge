using DevLounge.Service.Data.ForumSections;
using DevLounge.Web.Models.Home;
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
