using DevLounge.Service.ForumCategories;
using DevLounge.Service.ForumSections;
using DevLounge.Service.Models.ForumCategories;
using DevLounge.Service.Models.ForumUsers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevLounge.Web.Areas.Administration.Controllers
{
    [Route("/Administration/Categories")]
    public class ForumCategoriesAdministrationController : BaseAdministrationController
    {
        private readonly IForumSectionsService forumSectionService;

        private readonly IForumCategoriesService forumCategoryService;

        public ForumCategoriesAdministrationController(
            IForumCategoriesService forumCategoryService, 
            IForumSectionsService forumSectionService)
        {
            this.forumCategoryService = forumCategoryService;
            this.forumSectionService = forumSectionService;
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            this.ViewData["Sections"] = this.forumSectionService.GetAllForumSections().ToList(); 

            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ForumCategoryDto forumCategoryDto)
        {
            forumCategoryDto.CreatedBy = new DevLoungeUserDto
            {
                Id = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
            };

            await this.forumCategoryService.CreateForumCategory(forumCategoryDto);

            return Redirect("/Administration/Home");
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(long id)
        {
            this.ViewData["Sections"] = this.forumSectionService.GetAllForumSections().ToList();

            return View(await this.forumCategoryService.GetForumCategoryById(id));
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(long id, ForumCategoryDto forumCategoryDto)
        {
            await this.forumCategoryService.UpdateForumCategory(id, forumCategoryDto);

            return Redirect("/Administration/Home");
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await this.forumCategoryService.DeleteForumCategory(id);

            return Redirect("/Administration/Home");
        }
    }
}
