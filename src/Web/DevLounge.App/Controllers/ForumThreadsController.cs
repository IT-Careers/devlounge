using DevLounge.Data.Models;
using DevLounge.Service.ForumCategories;
using DevLounge.Service.ForumThreads;
using DevLounge.Service.Mapping.ForumUsers;
using DevLounge.Service.Models.ForumThreads;
using DevLounge.Web.Mapping.ForumThreads;
using DevLounge.Web.Models.ForumThreads;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DevLounge.Web.Controllers
{
    [Route("/Threads")]
    public class ForumThreadsController : BaseUserController
    {
        private readonly IForumCategoriesService forumCategoriesService;

        private readonly IForumThreadsService forumThreadsService;

        private readonly UserManager<DevLoungeUser> forumUserManager;

        public ForumThreadsController(
            IForumThreadsService forumThreadsService,
            IForumCategoriesService forumCategoriesService,
            UserManager<DevLoungeUser> forumUserManager)
        {
            this.forumThreadsService = forumThreadsService;
            this.forumCategoriesService = forumCategoriesService;
            this.forumUserManager = forumUserManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(long id)
        {
            var forumThread = await this.forumThreadsService.GetForumThreadById(id);

            // TODO: Build a global reusable mechanism for this.
            //       We might need it elsewhere...
            var currentUser = (await this.forumUserManager.GetUserAsync(this.User))
                .ToDto(fetchCreatedReplies: true, fetchCreatedThreads: true);

            this.ViewData["CurrentUser"] = currentUser;

            return this.View(forumThread);
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
            await this.forumThreadsService.CreateForumThread(createForumThreadBindingModel.ToDto());

            return this.Redirect("/");
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
