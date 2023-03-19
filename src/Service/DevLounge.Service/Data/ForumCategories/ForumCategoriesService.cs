using DevLounge.Data.Models;
using DevLounge.Data.Repositories;
using DevLounge.Service.Mapping.ForumCategories;
using DevLounge.Service.Models.ForumCategories;
using Microsoft.EntityFrameworkCore;

namespace DevLounge.Service.Data.ForumCategories
{
    public class ForumCategoriesService : IForumCategoriesService
    {
        private readonly ForumSectionsRepository forumSectionRepository;

        private readonly ForumCategoriesRepository forumCategoryRepository;

        public ForumCategoriesService(
            ForumCategoriesRepository forumCategoryRepository, 
            ForumSectionsRepository forumSectionRepository)
        {
            this.forumCategoryRepository = forumCategoryRepository;
            this.forumSectionRepository = forumSectionRepository;
        }

        public async Task<ForumCategoryDto> CreateForumCategory(ForumCategoryDto forumCategoryDto)
        {
            ForumCategory forumCategory = forumCategoryDto.ToEntity();

            var categorySection = await this.forumSectionRepository
                .RetrieveAllTracked()
                .SingleOrDefaultAsync(section => section.Id == forumCategoryDto.Section.Id);

            if(categorySection == null)
            {
                throw new ArgumentException("The section - assigned to this forum category, is invalid.");
            }

            forumCategory.Section = categorySection;

            await forumCategoryRepository.AddAsync(forumCategory);

            return forumCategory.ToDto();
        }

        public async Task<ForumCategoryDto> DeleteForumCategory(long id)
        {
            ForumCategory forumCategory = await this.forumCategoryRepository.RetrieveAllTracked()
                .SingleOrDefaultAsync(category => category.Id == id);

            if(forumCategory == null)
            {
                throw new ArgumentException("The category you are trying to delete does not exist."); 
            }

            ForumCategoryDto forumCategoryDto = forumCategory.ToDto();

            await this.forumCategoryRepository.RemoveAsync(forumCategory);

            return forumCategoryDto;
        }

        public IQueryable<ForumCategoryDto> GetAllForumCategories(bool fetchDeleted = false)
        {
            IQueryable<ForumCategory> forumCategories = this.forumCategoryRepository.RetrieveAll()
                .Include(category => category.Section);

            if (!fetchDeleted)
            {
                forumCategories = forumCategories
                    .Where(category => category.DeletedBy == null);
            }

            return forumCategories.Select(category => category.ToDto(true, true, true));
        }

        public async Task<ForumCategoryDto> GetForumCategoryById(long id)
        {
            ForumCategory forumCategory = await this.forumCategoryRepository.RetrieveAll()
                .Include(category => category.Section)
                .Include(category => category.Threads).ThenInclude(thread => thread.CreatedBy)
                .Include(category => category.Threads).ThenInclude(thread => thread.ModifiedBy)
                .Include(category => category.Threads).ThenInclude(thread => thread.DeletedBy)
                .Include(category => category.Threads).ThenInclude(thread => thread.Replies).ThenInclude(reply => reply.CreatedBy)
                .Include(category => category.Threads).ThenInclude(thread => thread.Replies).ThenInclude(reply => reply.ModifiedBy)
                .SingleOrDefaultAsync(category => category.Id == id);

            if (forumCategory == null)
            {
                throw new ArgumentException("The category you are trying to retrieve does not exist.");
            }

            return forumCategory.ToDto();
        }

        public async Task<ForumCategoryDto> UpdateForumCategory(long id, ForumCategoryDto forumCategoryDto)
        {
            ForumCategory forumCategory = await this.forumCategoryRepository.RetrieveAllTracked()
                .Include(category => category.Section)
                .SingleOrDefaultAsync(category => category.Id == id);

            if (forumCategory == null)
            {
                throw new ArgumentException("The category you are trying to modify does not exist.");
            }

            forumCategory.Name = forumCategoryDto.Name;
            forumCategory.Description = forumCategoryDto.Description;
            forumCategory.ThumbnailImageUrl = forumCategoryDto.ThumbnailImageUrl;
            forumCategory.CoverImageUrl = forumCategoryDto.CoverImageUrl;
            forumCategory.Section = (await this.forumSectionRepository.RetrieveAllTracked()
                .SingleOrDefaultAsync(section => section.Id == forumCategoryDto.Section.Id));

            await this.forumCategoryRepository.EditAsync(forumCategory);

            return forumCategory.ToDto();
        }
    }
}
