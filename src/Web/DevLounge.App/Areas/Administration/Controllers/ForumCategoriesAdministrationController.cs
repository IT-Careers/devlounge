using DevLounge.Service.Data.ForumCategories;
using DevLounge.Service.Data.ForumSections;
using DevLounge.Service.Models.ForumCategories;
using DevLounge.Service.Models.ForumUsers;
using DevLounge.Web.Mapping.Administration.ForumCategories;
using DevLounge.Web.Models.Administration.ForumCategories;
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
        public async Task<IActionResult> Create(CreateForumCategoryBindingModel createForumCategoryBindingModel)
        {
            ForumCategoryDto forumCategoryDto = createForumCategoryBindingModel.ToDto();

            forumCategoryDto.CreatedBy = new DevLoungeUserDto
            {
                Id = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
            };

            await this.forumCategoryService.CreateForumCategory(
                forumCategoryDto,
                createForumCategoryBindingModel.ThumbnailImage, 
                createForumCategoryBindingModel.CoverImage);

            return Redirect("/Administration/Home");
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(long id)
        {
            this.ViewData["Sections"] = this.forumSectionService.GetAllForumSections().ToList();

            return View((await this.forumCategoryService.GetForumCategoryById(id)).ToWebModel());
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(long id, CreateForumCategoryBindingModel createForumCategoryBindingModel)
        {
            ForumCategoryDto forumCategoryDto = createForumCategoryBindingModel.ToDto();

            await this.forumCategoryService.UpdateForumCategory(
                id, 
                forumCategoryDto, 
                createForumCategoryBindingModel.ThumbnailImage, 
                createForumCategoryBindingModel.CoverImage);

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
