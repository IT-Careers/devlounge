using DevLounge.Service.ForumCategories;
using DevLounge.Service.ForumSections;
using DevLounge.Service.Models.ForumCategories;
using DevLounge.Service.Models.ForumUsers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevLounge.Web.Areas.Administration.Controllers
{
    [Route("/Administration/Categories")]
    public class ForumCategoryAdministrationController : BaseAdministrationController
    {
        private readonly IForumSectionService forumSectionService;

        private readonly IForumCategoryService forumCategoryService;

        public ForumCategoryAdministrationController(
            IForumCategoryService forumCategoryService, 
            IForumSectionService forumSectionService)
        {
            this.forumCategoryService = forumCategoryService;
            this.forumSectionService = forumSectionService;
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            // TODO: Includes categories in service... Probably a bad idea
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await this.forumCategoryService.GetForumCategoryById(id));
        }
    }
}
