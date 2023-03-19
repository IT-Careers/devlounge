using DevLounge.Service.ForumSections;
using Microsoft.AspNetCore.Mvc;

namespace DevLounge.Web.Controllers
{
    [Route("/Sections")]
    public class ForumSectionsController : Controller
    {
        private readonly IForumSectionsService forumSectionService;

        public ForumSectionsController(IForumSectionsService forumSectionService)
        {
            this.forumSectionService = forumSectionService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(long id)
        {
            return View(await this.forumSectionService.GetForumSectionById(id));
        }
    }
}
