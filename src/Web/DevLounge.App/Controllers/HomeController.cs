using DevLounge.Service.ForumCategories;
using DevLounge.Service.ForumSections;
using DevLounge.Web.Models.Administration.AdministrationPanel;
using DevLounge.Web.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace DevLounge.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IForumSectionService forumSectionService;

        public HomeController(IForumSectionService forumSectionService)
        {
            this.forumSectionService = forumSectionService;
        }

        public IActionResult Index()
        {
            return View(new HomeViewModel
            {
                Sections = this.forumSectionService.GetAllForumSections(isExtended: true).ToList()
            });
        }
    }
}
