using DevLounge.Service.Data.ForumReplies;
using DevLounge.Service.Models.ForumReplies;
using DevLounge.Web.Mapping.ForumReplies;
using DevLounge.Web.Models.ForumReplies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevLounge.Web.Controllers
{
    [Route("/Threads/{threadId}/Replies")]
    [ApiController]
    public class ForumRepliesController : BaseUserController
    {
        private readonly IForumRepliesService forumRepliesService;

        public ForumRepliesController(IForumRepliesService forumRepliesService)
        {
            this.forumRepliesService = forumRepliesService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromRoute] long threadId, [FromBody] CreateForumReplyBindingModel createForumReplyBindingModel)
        {
            ForumReplyDto result = await this.forumRepliesService.CreateForumReply(createForumReplyBindingModel.ToDto());

            return this.Json(result);
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(long threadId, long id, ForumReplyDto forumReplyDto)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userRole = this.User.FindFirst(ClaimTypes.Role).Value;

            await this.forumRepliesService.UpdateForumReply(id, forumReplyDto, userId, userRole);

            return Redirect("/Threads" + threadId);
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(long threadId, long id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userRole = this.User.FindFirst(ClaimTypes.Role).Value;

            await this.forumRepliesService.DeleteForumReply(id, userId, userRole);

            return Redirect("/Threads" + threadId);
        }
    }
}
