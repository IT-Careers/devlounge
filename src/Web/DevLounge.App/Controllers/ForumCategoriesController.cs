using DevLounge.Service.ForumCategories;
using Microsoft.AspNetCore.Mvc;

namespace DevLounge.Web.Controllers
{
    [Route("/Categories")]
    public class ForumCategoriesController : Controller
    {
        private readonly IForumCategoriesService forumCategoryService;

        public ForumCategoriesController(IForumCategoriesService forumCategoryService)
        {
            this.forumCategoryService = forumCategoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(long id)
        {
            return View(await this.forumCategoryService.GetForumCategoryById(id));
        }
    }
}
