using DevLounge.Data;
using DevLounge.Data.Models;
using DevLounge.Service.Mapping.ForumSections;
using DevLounge.Service.Models.ForumSections;
using Microsoft.EntityFrameworkCore;

namespace DevLounge.Service.ForumSections
{
    public class ForumSectionService : IForumSectionService
    {
        private readonly DevLoungeDbContext devLoungeDbContext;

        public ForumSectionService(DevLoungeDbContext devLoungeDbContext)
        {
            this.devLoungeDbContext = devLoungeDbContext;
        }

        public async Task<ForumSectionDto> CreateForumSection(ForumSectionDto forumSectionDto)
        {
            ForumSection forumSection = forumSectionDto.ToEntity();
            var userCreator = await this.devLoungeDbContext.Users.SingleOrDefaultAsync(user => user.Id == forumSectionDto.CreatedBy.Id);

            if(userCreator == null)
            {
                throw new ArgumentException("The user that created this forum section is invalid.");
            }

            forumSection.CreatedBy = userCreator;
            forumSection.CreatedOn = DateTime.Now;

            await this.devLoungeDbContext.AddAsync(forumSection);
            await this.devLoungeDbContext.SaveChangesAsync();

            return forumSection.ToDto();
        }

        public async Task<ForumSectionDto> DeleteForumSection(long id)
        {
            ForumSection forumSection = await this.devLoungeDbContext.Sections.SingleOrDefaultAsync(section => section.Id == id);

            if(forumSection == null)
            {
                throw new ArgumentException("The section you are trying to delete does not exist."); 
            }

            ForumSectionDto forumSectionDto = forumSection.ToDto();

            this.devLoungeDbContext.Remove(forumSection);
            await this.devLoungeDbContext.SaveChangesAsync();

            return forumSectionDto;
        }

        public IQueryable<ForumSectionDto> GetAllForumSections(bool isExtended = false)
        {
            IQueryable<ForumSection> forumSections = this.devLoungeDbContext.Sections;

            if(isExtended)
            {
                forumSections = forumSections
                    .Include(section => section.CreatedBy)
                    .Include(section => section.Categories);
            }

            return forumSections.Select(section => section.ToDto(isExtended));
        }

        public async Task<ForumSectionDto> GetForumSectionById(long id)
        {
            ForumSection forumSection = await this.devLoungeDbContext.Sections
                .Include(section => section.CreatedBy)
                .Include(section => section.Categories)
                .SingleOrDefaultAsync(section => section.Id == id);

            if (forumSection == null)
            {
                throw new ArgumentException("The section you are trying to delete does not exist.");
            }

            return forumSection.ToDto();
        }

        public async Task<ForumSectionDto> UpdateForumSection(long id, ForumSectionDto forumSectionDto)
        {
            ForumSection forumSection = await this.devLoungeDbContext.Sections.SingleOrDefaultAsync(section => section.Id == id);

            if (forumSection == null)
            {
                throw new ArgumentException("The section you are trying to delete does not exist.");
            }

            forumSection.Name = forumSectionDto.Name;
            forumSection.Description = forumSectionDto.Description;

            return forumSection.ToDto();
        }
    }
}
