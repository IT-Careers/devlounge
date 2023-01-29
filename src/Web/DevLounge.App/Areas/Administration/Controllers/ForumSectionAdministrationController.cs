using DevLounge.Service.ForumCategories;
using DevLounge.Service.ForumSections;
using DevLounge.Service.Models.ForumSections;
using DevLounge.Service.Models.ForumUsers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevLounge.Web.Areas.Administration.Controllers
{
    [Route("/Administration/Sections")]
    public class ForumSectionAdministrationController : BaseAdministrationController
    {
        private readonly IForumSectionService forumSectionService;

        public ForumSectionAdministrationController(IForumSectionService forumSectionService)
        {
            this.forumSectionService = forumSectionService;
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ForumSectionDto forumSectionDto)
        {
            forumSectionDto.CreatedBy = new DevLoungeUserDto
            {
                Id = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
            };

            await this.forumSectionService.CreateForumSection(forumSectionDto);

            return Redirect("/Administration/Home");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await this.forumSectionService.GetForumSectionById(id));
        }
    }
}
