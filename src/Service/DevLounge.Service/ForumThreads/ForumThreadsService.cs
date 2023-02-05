using DevLounge.Data.Models;
using DevLounge.Data.Repositories;
using DevLounge.Service.Mapping.ForumCategories;
using DevLounge.Service.Mapping.ForumThreads;
using DevLounge.Service.Models.ForumThreads;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevLounge.Service.ForumThreads
{
    public class ForumThreadsService : IForumThreadsService
    {
        private readonly ForumCategoriesRepository forumCategoriesRepository;

        private readonly ForumThreadsRepository forumThreadsRepository;

        public ForumThreadsService(
            ForumThreadsRepository forumThreadsRepository,
            ForumCategoriesRepository forumCategoriesRepository)
        {
            this.forumThreadsRepository = forumThreadsRepository;
            this.forumCategoriesRepository = forumCategoriesRepository;
        }

        public async Task<ForumThreadDto> CreateForumThread(ForumThreadDto forumThreadDto)
        {
            ForumThread forumThread = forumThreadDto.ToEntity();

            var threadCategory = await this.forumCategoriesRepository
                .RetrieveAll()
                .SingleOrDefaultAsync(category => category.Id == forumThreadDto.Category.Id);

            if (threadCategory == null)
            {
                throw new ArgumentException("The category - that this thread is created into, is invalid.");
            }

            forumThread.Category = threadCategory;

            await this.forumThreadsRepository.AddAsync(forumThread);

            return forumThread.ToDto();
        }

        public async Task<ForumThreadDto> DeleteForumThread(long id, string userId, string userRole)
        {
            ForumThread forumThread = await this.forumThreadsRepository.RetrieveAllTracked()
                .SingleOrDefaultAsync(thread => thread.Id == id);

            if (forumThread == null)
            {
                throw new ArgumentException("The thread you are trying to delete does not exist.");
            }

            if (forumThread.CreatedBy.Id != userId && userRole != "Admin" && userRole != "Moderator")
            {
                throw new ArgumentException("Insufficient permissions to delete this thread.");
            }

            ForumThreadDto forumThreadDto = forumThread.ToDto();

            await this.forumThreadsRepository.RemoveAsync(forumThread);

            return forumThreadDto;
        }

        public IQueryable<ForumThreadDto> GetAllForumThreads()
        {
            IQueryable<ForumThread> forumThreads = this.forumThreadsRepository.RetrieveAll()
                .Include(thread => thread.Category)
                .Include(thread => thread.Replies);

            return forumThreads.Select(thread => thread.ToDto(true, true, true));
        }

        public async Task<ForumThreadDto> GetForumThreadById(long id)
        {
            ForumThread forumThread = await this.forumThreadsRepository.RetrieveAll()
                .Include(thread => thread.Category)
                .Include(thread => thread.Replies)
                .SingleOrDefaultAsync(thread => thread.Id == id);

            if (forumThread == null)
            {
                throw new ArgumentException("The thread you are trying to retrieve does not exist.");
            }

            forumThread.Views = forumThread.Views + 1;
            await this.forumThreadsRepository.EditWithoutMetadataAsync(forumThread);

            return forumThread.ToDto();
        }

        public async Task<ForumThreadDto> UpdateForumThread(long id, ForumThreadDto forumThreadDto, string userId, string userRole)
        {
            ForumThread forumThread = await this.forumThreadsRepository.RetrieveAllTracked()
                .Include(thread => thread.Category)
                .Include(thread => thread.Replies)
                .SingleOrDefaultAsync(thread => thread.Id == id);

            if (forumThread == null)
            {
                throw new ArgumentException("The thread you are trying to modify does not exist.");
            }

            if (forumThread.CreatedBy.Id != userId && userRole != "Admin" && userRole != "Moderator")
            {
                throw new ArgumentException("Insufficient permissions to modify this thread.");
            } 

            forumThread.Title = forumThread.Title;
            forumThread.Views = forumThread.Views;
            forumThread.Category = (await this.forumCategoriesRepository.RetrieveAllTracked()
                .SingleOrDefaultAsync(category => category.Id == forumThreadDto.Category.Id));

            await this.forumThreadsRepository.EditAsync(forumThread);

            return forumThread.ToDto();
        }
    }
}
