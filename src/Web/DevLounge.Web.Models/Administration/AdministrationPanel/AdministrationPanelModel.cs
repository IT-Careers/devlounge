using DevLounge.Service.Models.ForumCategories;
using DevLounge.Service.Models.ForumSections;

namespace DevLounge.Web.Models.Administration.AdministrationPanel
{
    public class AdministrationPanelModel
    {
        public List<ForumSectionDto> Sections { get; set; }

        public List<ForumCategoryDto> Categories { get; set; }
    }
}
