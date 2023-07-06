using Insightify.Posts.Application.Posts.Commands.Create;
using Insightify.Posts.Application.Posts.Commands.Delete;
using Microsoft.AspNetCore.Mvc;

namespace Insightify.Posts.Web.Features
{
    public class CommentsController : ApiController
    {
        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] AddCommentCommand command)
            => await this.Send(command);

        [HttpPost("delete")]
        public async Task<ActionResult> Delete([FromBody] DeleteCommentCommand command)
            => await this.Send(command);
    }
}
