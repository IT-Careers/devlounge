using DevLounge.Service.Models.ForumCategories;
using DevLounge.Service.Models.ForumSections;
using DevLounge.Service.Models.ForumThreads;
using DevLounge.Service.Models.ForumUsers;

namespace DevLounge.Web.Models.Administration.AdministrationPanel
{
    public class AdministrationPanelModel
    {
        public List<ForumSectionDto> Sections { get; set; }

        public List<ForumCategoryDto> Categories { get; set; }

        public List<ForumThreadDto> Threads { get; set; }

        public List<DevLoungeUserDto> Users { get; set; }
    }
}
