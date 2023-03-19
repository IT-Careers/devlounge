using DevLounge.Data.Models;
using DevLounge.Data.Repositories;
using DevLounge.Service.Mapping.ForumReplies;
using DevLounge.Service.Mapping.ForumThreads;
using DevLounge.Service.Models.ForumReplies;
using Microsoft.EntityFrameworkCore;

namespace DevLounge.Service.Data.ForumReplies
{
    public class ForumRepliesService : IForumRepliesService
    {
        private readonly ForumRepliesRepository forumRepliesRepository;

        private readonly ForumThreadsRepository forumThreadsRepository;

        public ForumRepliesService(ForumThreadsRepository forumThreadsRepository, ForumRepliesRepository forumRepliesRepository)
        {
            this.forumThreadsRepository = forumThreadsRepository;
            this.forumRepliesRepository = forumRepliesRepository;
        }

        public async Task<ForumReplyDto> CreateForumReply(ForumReplyDto forumReplyDto)
        {
            ForumReply forumReply = forumReplyDto.ToEntity();

            var replyThread = await this.forumThreadsRepository
                .RetrieveAllTracked()
                .SingleOrDefaultAsync(thread => thread.Id == forumReplyDto.Thread.Id);

            if (replyThread == null)
            {
                throw new ArgumentException("The thread - that this reply is created into, is invalid.");
            }

            forumReply.Thread = replyThread;

            await this.forumRepliesRepository.AddAsync(forumReply);

            // TODO: Forum Attachments
            //
            //var threadReplies = forumReplyDto.Attachments?
            //    .Select(attachment => attachment.ToEntity())
            //    .Select(attachment =>
            //    {
            //        attachment.Reply = forumReply;
            //        return attachment;
            //    });

            //await this.forumRepliesRepository.AddManyAsync(threadReplies);

            return forumReply.ToDto();
        }

        public async Task<ForumReplyDto> DeleteForumReply(long id, string userId, string userRole)
        {
            ForumReply forumReply = await this.forumRepliesRepository.RetrieveAllTracked()
                .SingleOrDefaultAsync(reply => reply.Id == id);

            if (forumReply == null)
            {
                throw new ArgumentException("The reply you are trying to delete does not exist.");
            }

            if (forumReply.CreatedBy.Id != userId && userRole != "Admin" && userRole != "Moderator")
            {
                throw new ArgumentException("Insufficient permissions to delete this reply.");
            }

            await this.forumRepliesRepository.RemoveAsync(forumReply);

            ForumReplyDto forumReplyDto = forumReply.ToDto();

            return forumReplyDto;
        }

        public IQueryable<ForumReplyDto> GetAllForumReplies()
        {
            IQueryable<ForumReply> forumReplies = this.forumRepliesRepository.RetrieveAll()
                .Include(reply => reply.Thread);

            return forumReplies.Select(reply => reply.ToDto(true, true, true));
        }

        public async Task<ForumReplyDto> GetForumReplyById(long id)
        {
            ForumReply forumReply = await this.forumRepliesRepository.RetrieveAll()
                .Include(reply => reply.Thread)
                .SingleOrDefaultAsync(reply => reply.Id == id);

            if (forumReply == null)
            {
                throw new ArgumentException("The reply you are trying to retrieve does not exist.");
            }

            return forumReply.ToDto();
        }

        public async Task<ForumReplyDto> UpdateForumReply(long id, ForumReplyDto forumReplyDto, string userId, string userRole)
        {
            ForumReply forumReply = await this.forumRepliesRepository.RetrieveAllTracked()
                .Include(reply => reply.Thread)
                .SingleOrDefaultAsync(reply => reply.Id == id);

            if (forumReply == null)
            {
                throw new ArgumentException("The reply you are trying to modify does not exist.");
            }

            if (forumReply.CreatedBy.Id != userId && userRole != "Admin" && userRole != "Moderator")
            {
                throw new ArgumentException("Insufficient permissions to modify this reply.");
            }

            forumReply.Content = forumReplyDto.Content;

            await this.forumRepliesRepository.EditAsync(forumReply);

            return forumReply.ToDto();
        }
    }
}
