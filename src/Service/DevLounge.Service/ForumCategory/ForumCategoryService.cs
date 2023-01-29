using DevLounge.Data;
using DevLounge.Data.Models;
using DevLounge.Service.Mapping.ForumCategories;
using DevLounge.Service.Models.ForumCategories;
using Microsoft.EntityFrameworkCore;

namespace DevLounge.Service.ForumCategories
{
    public class ForumCategoryService : IForumCategoryService
    {
        private readonly DevLoungeDbContext devLoungeDbContext;

        public ForumCategoryService(DevLoungeDbContext devLoungeDbContext)
        {
            this.devLoungeDbContext = devLoungeDbContext;
        }

        public async Task<ForumCategoryDto> CreateForumCategory(ForumCategoryDto forumCategoryDto)
        {
            ForumCategory forumCategory = forumCategoryDto.ToEntity();
            var userCreator = await this.devLoungeDbContext.Users.SingleOrDefaultAsync(user => user.Id == forumCategoryDto.CreatedBy.Id);

            if(userCreator == null)
            {
                throw new ArgumentException("The user that created this forum category is invalid.");
            }

            forumCategory.CreatedBy = userCreator;
            forumCategory.CreatedOn = DateTime.Now;

            var categorySection = await this.devLoungeDbContext.Sections.SingleOrDefaultAsync(section => section.Id == forumCategoryDto.Section.Id);

            if(categorySection == null)
            {
                throw new ArgumentException("The section - assigned to this forum category, is invalid.");
            }

            forumCategory.Section = categorySection;

            await this.devLoungeDbContext.AddAsync(forumCategory);
            await this.devLoungeDbContext.SaveChangesAsync();

            return forumCategory.ToDto();
        }

        public async Task<ForumCategoryDto> DeleteForumCategory(long id)
        {
            ForumCategory forumCategory = await this.devLoungeDbContext.Categories
                .SingleOrDefaultAsync(category => category.Id == id);

            if(forumCategory == null)
            {
                throw new ArgumentException("The category you are trying to delete does not exist."); 
            }

            ForumCategoryDto forumCategoryDto = forumCategory.ToDto();

            this.devLoungeDbContext.Remove(forumCategory);
            await this.devLoungeDbContext.SaveChangesAsync();

            return forumCategoryDto;
        }

        public IQueryable<ForumCategoryDto> GetAllForumCategories(bool isExtended = false)
        {
            IQueryable<ForumCategory> forumCategories = this.devLoungeDbContext.Categories;

            if(isExtended)
            {
                forumCategories = forumCategories
                    .Include(category => category.CreatedBy)
                    .Include(category => category.Section);
            }

            return forumCategories.Select(category => category.ToDto(isExtended));
        }

        public async Task<ForumCategoryDto> GetForumCategoryById(long id)
        {
            ForumCategory forumCategory = await this.devLoungeDbContext.Categories
                .Include(category => category.CreatedBy)
                .Include(category => category.Section)
                .SingleOrDefaultAsync(category => category.Id == id);

            if (forumCategory == null)
            {
                throw new ArgumentException("The category you are trying to delete does not exist.");
            }

            return forumCategory.ToDto();
        }

        public async Task<ForumCategoryDto> UpdateForumCategory(long id, ForumCategoryDto forumCategoryDto)
        {
            ForumCategory forumCategory = await this.devLoungeDbContext.Categories.SingleOrDefaultAsync(category => category.Id == id);

            if (forumCategory == null)
            {
                throw new ArgumentException("The category you are trying to delete does not exist.");
            }

            forumCategory.Name = forumCategoryDto.Name;
            forumCategory.Description = forumCategoryDto.Description;
            forumCategory.ThumbnailImageUrl = forumCategoryDto.ThumbnailImageUrl;
            forumCategory.CoverImageUrl = forumCategoryDto.CoverImageUrl;
            // TODO: Categories set;

            return forumCategory.ToDto();
        }
    }
}
