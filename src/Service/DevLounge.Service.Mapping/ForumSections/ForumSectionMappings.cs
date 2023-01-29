using DevLounge.Data.Models;
using DevLounge.Service.Mapping.ForumCategories;
using DevLounge.Service.Mapping.ForumSections;
using DevLounge.Service.Mapping.ForumUsers;
using DevLounge.Service.Models.ForumSections;

namespace DevLounge.Service.Mapping.ForumSections
{
    public static class ForumSectionMappings
    {
        public static ForumSection ToEntity(this ForumSectionDto forumSectionDto)
        {
            return new ForumSection
            {
                Id = forumSectionDto.Id,
                Name = forumSectionDto.Name,
                Description = forumSectionDto.Description,
                CreatedOn = forumSectionDto.CreatedOn
            };
        }

        public static ForumSectionDto ToDto(this ForumSection forumSection, bool isExtended = false)
        {
            return new ForumSectionDto
            {
                Id = forumSection.Id,
                Name = forumSection.Name,
                Description = forumSection.Description,
                CreatedOn = forumSection.CreatedOn,
                CreatedBy = forumSection.CreatedBy?.ToDto(),
                ModifiedOn = forumSection.ModifiedOn,
                ModifiedBy = forumSection.ModifiedBy?.ToDto(),
                DeletedOn = forumSection.DeletedOn,
                DeletedBy = forumSection.DeletedBy?.ToDto(),
                Categories = isExtended ? forumSection.Categories?.Select(category => category.ToDto()).ToList() : null
            };
        }
    }
}
