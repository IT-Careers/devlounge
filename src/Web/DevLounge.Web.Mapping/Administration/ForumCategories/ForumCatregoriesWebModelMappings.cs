using DevLounge.Service.Models.ForumCategories;
using DevLounge.Service.Models.ForumSections;
using DevLounge.Web.Models.Administration.ForumCategories;

namespace DevLounge.Web.Mapping.Administration.ForumCategories
{
    public static class ForumCatregoriesWebModelMappings
    {
        public static ForumCategoryDto ToDto(this CreateForumCategoryBindingModel createForumCategoryBindingModel)
        {
            return new ForumCategoryDto
            {
                Name = createForumCategoryBindingModel.Name,
                Description = createForumCategoryBindingModel.Description,
                Section = new ForumSectionDto
                {
                    Id = createForumCategoryBindingModel.SectionId
                }
            };
        }

        public static CreateForumCategoryBindingModel ToWebModel(this ForumCategoryDto forumCategoryDto)
        {
            return new CreateForumCategoryBindingModel
            {
                Name = forumCategoryDto.Name,
                Description = forumCategoryDto.Description,
                SectionId = forumCategoryDto.Section.Id,
                ThumbnailImageUrl = forumCategoryDto.ThumbnailImage.FileUrl,
                CoverImageUrl = forumCategoryDto.CoverImage.FileUrl
            };
        }
    }
}
