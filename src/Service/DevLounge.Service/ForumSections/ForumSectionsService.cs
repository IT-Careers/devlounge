using DevLounge.Data.Models;
using DevLounge.Data.Repositories;
using DevLounge.Service.Mapping.ForumCategories;
using DevLounge.Service.Mapping.ForumSections;
using DevLounge.Service.Mapping.ForumUsers;
using DevLounge.Service.Models.ForumSections;
using Microsoft.EntityFrameworkCore;

namespace DevLounge.Service.ForumSections
{
    public class ForumSectionService : IForumSectionService
    {
        private readonly ForumSectionRepository forumSectionRepository;

        public ForumSectionService(ForumSectionRepository forumSectionRepository)
        {
            this.forumSectionRepository = forumSectionRepository;
        }

        public async Task<ForumSectionDto> CreateForumSection(ForumSectionDto forumSectionDto)
        {
            ForumSection forumSection = forumSectionDto.ToEntity();

            await this.forumSectionRepository.AddAsync(forumSection);

            return forumSection.ToDto();
        }

        public async Task<ForumSectionDto> DeleteForumSection(long id)
        {
            ForumSection forumSection = await this.forumSectionRepository
                .RetrieveAll()
                .SingleOrDefaultAsync(section => section.Id == id);

            if(forumSection == null)
            {
                throw new ArgumentException("The section you are trying to delete does not exist."); 
            }

            await this.forumSectionRepository.RemoveAsync(forumSection);
            
            return forumSection.ToDto();
        }

        public IQueryable<ForumSectionDto> GetAllForumSections(bool fetchDeleted = false)
        {
            IQueryable<ForumSection> forumSections = this.forumSectionRepository.RetrieveAll()
                .Include(section => section.Categories);

            if (!fetchDeleted)
            {
                forumSections = forumSections
                    .Where(section => section.DeletedBy == null);
            }

            return forumSections.Select(section => section.ToDto(true));
        }

        public async Task<ForumSectionDto> GetForumSectionById(long id)
        {
            ForumSection forumSection = await this.forumSectionRepository.RetrieveAll()
                .Include(section => section.Categories)
                .ThenInclude(category => category.CreatedBy)
                .SingleOrDefaultAsync(section => section.Id == id);

            if (forumSection == null)
            {
                throw new ArgumentException("The section you are trying to delete does not exist.");
            }

            ForumSectionDto forumSectionDto = forumSection.ToDto();
            forumSectionDto.Categories = forumSection.Categories.Select(category => category.ToDto()).ToList();

            return forumSectionDto;
        }

        public async Task<ForumSectionDto> UpdateForumSection(long id, ForumSectionDto forumSectionDto)
        {
            ForumSection forumSection = await this.forumSectionRepository
                .RetrieveAll()
                .SingleOrDefaultAsync(section => section.Id == id);

            if (forumSection == null)
            {
                throw new ArgumentException("The section you are trying to delete does not exist.");
            }

            forumSection.Name = forumSectionDto.Name;
            forumSection.Description = forumSectionDto.Description;

            await this.forumSectionRepository.EditAsync(forumSection);

            return forumSection.ToDto();
        }
    }
}
