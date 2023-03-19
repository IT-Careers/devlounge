using DevLounge.Data.Models;
using DevLounge.Service.ForumCategories;
using DevLounge.Service.ForumSections;
using DevLounge.Service.ForumThreads;
using DevLounge.Service.Mapping.ForumUsers;
using DevLounge.Service.Models.ForumUsers;
using DevLounge.Web.Models.Administration.AdministrationPanel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevLounge.Web.Areas.Administration.Controllers
{
    [Route("/Administration/Home")]
    public class AdministrationPanelController : BaseAdministrationController
    {
        private readonly IForumSectionsService forumSectionsService;

        private readonly IForumCategoriesService forumCategoriesService;

        private readonly IForumThreadsService forumThreadsService;

        private readonly UserManager<DevLoungeUser> forumUserManager;

        public AdministrationPanelController(
            IForumSectionsService forumSectionService,
            IForumCategoriesService forumCategoryService,
            IForumThreadsService forumThreadsService,
            UserManager<DevLoungeUser> forumUserManager)
        {
            this.forumSectionsService = forumSectionService;
            this.forumCategoriesService = forumCategoryService;
            this.forumThreadsService = forumThreadsService;
            this.forumUserManager = forumUserManager;
        }

        private IQueryable<DevLoungeUserDto> GetAllUsers()
        {
            return this.forumUserManager.Users.Select(user => user.ToDto(false, false, false, false, false, false));
        }

        public IActionResult Index()
        {
            return View(new AdministrationPanelModel
            {
                Sections = this.forumSectionsService.GetAllForumSections(true).ToList(),
                Categories = this.forumCategoriesService.GetAllForumCategories(true).ToList(),
                Threads = this.forumThreadsService.GetAllForumThreads(true).ToList(),
                Users = this.GetAllUsers().ToList()
            });
        }
    }
}
