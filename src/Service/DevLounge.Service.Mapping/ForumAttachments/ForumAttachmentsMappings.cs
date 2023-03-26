using DevLounge.Data.Models;
using DevLounge.Service.Mapping.ForumSections;
using DevLounge.Service.Mapping.ForumThreads;
using DevLounge.Service.Mapping.ForumUsers;
using DevLounge.Service.Models.ForumAttachments;

namespace DevLounge.Service.Mapping.ForumAttachments
{
    public static class ForumAttachmentsMappings
    {
        public static ForumAttachment ToEntity(this ForumAttachmentDto forumAttachmentDto)
        {
            return new ForumAttachment
            {
                Id = forumAttachmentDto.Id,
                FileUrl = forumAttachmentDto.FileUrl,
                IsImage = forumAttachmentDto.IsImage,
                IsTextType = forumAttachmentDto.IsTextType
            };  
        }

        public static ForumAttachmentDto ToDto(
            this ForumAttachment forumAttachment,
            bool fetchUser = true)
        {
            return new ForumAttachmentDto
            {
                Id = forumAttachment.Id,
                FileUrl = forumAttachment.FileUrl,
                IsImage = forumAttachment.IsImage,
                IsTextType = forumAttachment.IsTextType,
                CreatedOn = forumAttachment.CreatedOn,
                CreatedBy = fetchUser ? forumAttachment.CreatedBy?.ToDto() : null,
                ModifiedOn = forumAttachment.ModifiedOn,
                ModifiedBy = fetchUser ? forumAttachment.ModifiedBy?.ToDto() : null,
                DeletedOn = forumAttachment.DeletedOn,
                DeletedBy = fetchUser ? forumAttachment.DeletedBy?.ToDto() : null,
            };
        }
    }
}
