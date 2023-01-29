using DevLounge.Service.ForumCategories;
using DevLounge.Service.ForumSections;
using DevLounge.Web.Models.Administration.AdministrationPanel;
using Microsoft.AspNetCore.Mvc;

namespace DevLounge.Web.Areas.Administration.Controllers
{
    [Route("/Administration/Home")]
    public class AdministrationPanelController : BaseAdministrationController
    {
        private readonly IForumSectionService forumSectionService;

        private readonly IForumCategoryService forumCategoryService;

        public AdministrationPanelController(IForumSectionService forumSectionService, IForumCategoryService forumCategoryService)
        {
            this.forumSectionService = forumSectionService;
            this.forumCategoryService = forumCategoryService;
        }

        public IActionResult Index()
        {
            return View(new AdministrationPanelModel 
            {
                // TODO: Includes categories in service... Probably a bad idea
                Sections = this.forumSectionService.GetAllForumSections().ToList(),
                Categories = this.forumCategoryService.GetAllForumCategories().ToList()
            });
        }
    }
}
