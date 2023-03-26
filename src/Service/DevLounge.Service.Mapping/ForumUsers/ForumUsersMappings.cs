using DevLounge.Data.Models;
using DevLounge.Service.Mapping.ForumAttachments;
using DevLounge.Service.Mapping.ForumReplies;
using DevLounge.Service.Mapping.ForumThreads;
using DevLounge.Service.Models.ForumUsers;

namespace DevLounge.Service.Mapping.ForumUsers
{
    public static class ForumUsersMappings
    {
        public static DevLoungeUser ToEntity(this DevLoungeUserDto devLoungeUserDto)
        {
            return new DevLoungeUser
            {
                Id = devLoungeUserDto.Id,
                UserName = devLoungeUserDto.UserName,
                Email = devLoungeUserDto.Email,
            };
        }

        public static DevLoungeUserDto ToDto(
            this DevLoungeUser devLoungeUser, 
            bool fetchCreatedReplies = false,
            bool fetchModifiedReplies = false,
            bool fetchDeletedReplies = false,
            bool fetchCreatedThreads = false,
            bool fetchModifiedThreads = false,
            bool fetchDeletedThreads = false)
        {
            return new DevLoungeUserDto
            {
                Id = devLoungeUser.Id,
                UserName = devLoungeUser.UserName,
                Email = devLoungeUser.Email,
                ThumbnailUrl = devLoungeUser.ThumbnailUrl,
                //Thumbnail = devLoungeUser.Thumbnail.ToDto(),
                RegisteredOn = devLoungeUser.RegisteredOn,
                RepliesCreated = fetchCreatedReplies ? devLoungeUser.RepliesCreated?.Select(reply => reply.ToDto(fetchThread: false, fetchUser: false)).ToList() : null,
                RepliesModified = fetchModifiedReplies ? devLoungeUser.RepliesModified?.Select(reply => reply.ToDto(fetchThread: false, fetchUser: false)).ToList() : null,
                RepliesDeleted = fetchDeletedReplies ? devLoungeUser.RepliesDeleted?.Select(reply => reply.ToDto(fetchThread: false, fetchUser: false)).ToList() : null,
                ThreadsCreated = fetchCreatedThreads ? devLoungeUser.ThreadsCreated?.Select(thread => thread.ToDto(fetchCategory: false, fetchReplies: false, fetchUser: false)).ToList() : null,
                ThreadsModified = fetchModifiedThreads ? devLoungeUser.ThreadsModified?.Select(thread => thread.ToDto(fetchCategory: false, fetchReplies: false, fetchUser: false)).ToList() : null,
                ThreadsDeleted = fetchDeletedThreads ? devLoungeUser.ThreadsDeleted?.Select(thread => thread.ToDto(fetchCategory: false, fetchReplies: false, fetchUser: false)).ToList() : null,
            };
        }
    }
}
