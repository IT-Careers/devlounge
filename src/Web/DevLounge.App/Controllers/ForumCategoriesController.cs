using DevLounge.Service.ForumCategories;
using Microsoft.AspNetCore.Mvc;

namespace DevLounge.Web.Controllers
{
    [Route("/Categories")]
    public class ForumCategoriesController : Controller
    {
        private readonly IForumCategoryService forumCategoryService;

        public ForumCategoriesController(IForumCategoryService forumCategoryService)
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
