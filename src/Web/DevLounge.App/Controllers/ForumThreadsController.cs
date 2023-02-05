using DevLounge.Service.ForumCategories;
using DevLounge.Service.ForumThreads;
using DevLounge.Service.Models.ForumThreads;
using DevLounge.Web.Models.ForumThreads;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DevLounge.Web.Controllers
{
    [Route("/Threads")]
    public class ForumThreadsController : Controller
    {
        private readonly IForumCategoriesService forumCategoriesService;

        private readonly IForumThreadsService forumThreadsService;

        public ForumThreadsController(
            IForumThreadsService forumThreadsService, 
            IForumCategoriesService forumCategoriesService)
        {
            this.forumThreadsService = forumThreadsService;
            this.forumCategoriesService = forumCategoriesService;
        }

        public async Task<IActionResult> Index()
        {
            return null;
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create(int categoryId)
        {
            this.ViewData["SelectedCategory"] = categoryId;
            this.ViewData["Categories"] = await this.forumCategoriesService.GetAllForumCategories().ToListAsync();

            return this.View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateForumThreadBindingModel createForumThreadBindingModel)
        {
            await this.forumThreadsService.CreateForumThread(null);

            return this.Redirect("/Threads");
        }


        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(long id, ForumThreadDto forumThreadDto)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userRole = this.User.FindFirst(ClaimTypes.Role).Value;

            await this.forumThreadsService.UpdateForumThread(id, forumThreadDto, userId, userRole);

            return Redirect("/Threads");
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userRole = this.User.FindFirst(ClaimTypes.Role).Value;

            await this.forumThreadsService.DeleteForumThread(id, userId, userRole);

            return Redirect("/Threads");
        }
    }
}
