using DevLounge.Data.Models;
using DevLounge.Data.Repositories;
using DevLounge.Service.Mapping.ForumCategories;
using DevLounge.Service.Mapping.ForumReplies;
using DevLounge.Service.Mapping.ForumThreads;
using DevLounge.Service.Models.ForumThreads;
using Microsoft.EntityFrameworkCore;

namespace DevLounge.Service.ForumThreads
{
    public class ForumThreadsService : IForumThreadsService
    {
        private readonly ForumRepliesRepository forumRepliesRepository;

        private readonly ForumCategoriesRepository forumCategoriesRepository;

        private readonly ForumThreadsRepository forumThreadsRepository;

        public ForumThreadsService(
            ForumThreadsRepository forumThreadsRepository,
            ForumCategoriesRepository forumCategoriesRepository,
            ForumRepliesRepository forumRepliesRepository)
        {
            this.forumThreadsRepository = forumThreadsRepository;
            this.forumCategoriesRepository = forumCategoriesRepository;
            this.forumRepliesRepository = forumRepliesRepository;
        }

        public async Task<ForumThreadDto> CreateForumThread(ForumThreadDto forumThreadDto)
        {
            ForumThread forumThread = forumThreadDto.ToEntity();

            var threadCategory = await this.forumCategoriesRepository
                .RetrieveAllTracked()
                .SingleOrDefaultAsync(category => category.Id == forumThreadDto.Category.Id);

            if (threadCategory == null)
            {
                throw new ArgumentException("The category - that this thread is created into, is invalid.");
            }

            forumThread.Category = threadCategory;

            await this.forumThreadsRepository.AddAsync(forumThread);

            var threadReplies = forumThreadDto.Replies?
                .Select(reply => reply.ToEntity())
                .Select(reply =>
                {
                    reply.Thread = forumThread;
                    return reply;
                });

            await this.forumRepliesRepository.AddManyAsync(threadReplies);

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

            await this.forumThreadsRepository.RemoveAsync(forumThread);

            ForumThreadDto forumThreadDto = forumThread.ToDto();

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
            ForumThread forumThread = await this.forumThreadsRepository.RetrieveAllTracked()
                .Include(thread => thread.Category)
                .Include(thread => thread.Replies).ThenInclude(reply => reply.CreatedBy).ThenInclude(user => user.ThreadsCreated)
                .Include(thread => thread.Replies).ThenInclude(reply => reply.CreatedBy).ThenInclude(user => user.RepliesCreated)
                .Include(thread => thread.Replies).ThenInclude(reply => reply.ModifiedBy)
                .Include(thread => thread.Replies).ThenInclude(reply => reply.DeletedBy)
                .SingleOrDefaultAsync(thread => thread.Id == id);

            if (forumThread == null)
            {
                throw new ArgumentException("The thread you are trying to retrieve does not exist.");
            }

            forumThread.Views = forumThread.Views + 1;
            await this.forumThreadsRepository.EditWithoutMetadataAsync(forumThread);

            return forumThread.ToDto(true, true, true);
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

            forumThread.Title = forumThreadDto.Title;
            forumThread.Views = forumThread.Views;
            forumThread.Category = (await this.forumCategoriesRepository.RetrieveAllTracked()
                .SingleOrDefaultAsync(category => category.Id == forumThreadDto.Category.Id));

            await this.forumThreadsRepository.EditAsync(forumThread);

            return forumThread.ToDto();
        }
    }
}
