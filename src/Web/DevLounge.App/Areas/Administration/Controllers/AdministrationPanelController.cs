using DevLounge.Service.ForumCategories;
using DevLounge.Service.ForumSections;
using DevLounge.Web.Models.Administration.AdministrationPanel;
using Microsoft.AspNetCore.Mvc;

namespace DevLounge.Web.Areas.Administration.Controllers
{
    [Route("/Administration/Home")]
    public class AdministrationPanelController : BaseAdministrationController
    {
        private readonly IForumSectionsService forumSectionService;

        private readonly IForumCategoriesService forumCategoryService;

        public AdministrationPanelController(IForumSectionsService forumSectionService, IForumCategoriesService forumCategoryService)
        {
            this.forumSectionService = forumSectionService;
            this.forumCategoryService = forumCategoryService;
        }

        public IActionResult Index()
        {
            return View(new AdministrationPanelModel 
            {
                Sections = this.forumSectionService.GetAllForumSections(true).ToList(),
                Categories = this.forumCategoryService.GetAllForumCategories().ToList()
            });
        }
    }
}
