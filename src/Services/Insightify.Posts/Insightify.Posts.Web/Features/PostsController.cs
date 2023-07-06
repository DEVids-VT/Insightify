using Insightify.Framework.Pagination;
using Insightify.Framework.Pagination.Abstractions;
using Insightify.Posts.Application.Posts.Commands.Edit;
using Insightify.Posts.Application.Posts.Commands.Like;
using Insightify.Posts.Application.Posts.Commands.Save;
using Insightify.Posts.Application.Posts.Queries.Search;
using Microsoft.AspNetCore.Authorization;

namespace Insightify.Posts.Web.Features
{
    using Insightify.Posts.Application.Posts.Commands.Create;
    using Insightify.Posts.Application.Posts.Commands.Delete;
    using Insightify.Posts.Application.Posts.Queries.Common;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : ApiController
    {
        [HttpPost("create")]
        public async Task<ActionResult<CreatePostOutputModel>> Create([FromBody] CreatePostCommand command) 
            => await this.Send(command);

        [HttpPost("edit")]
        public async Task<ActionResult> Edit([FromBody] EditPostCommand command)
            => await this.Send(command);

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
            => await this.Send(new DeletePostCommand(){Id = id});


        [HttpPost("like/{id}")]
        public async Task<ActionResult> Like([FromRoute] int id)
            => await this.Send(new LikePostCommand() { Id = id });

        [HttpPost("dislike/{id}")]
        public async Task<ActionResult> Dislike([FromRoute] int id)
            => await this.Send(new DislikePostCommand() { Id = id });

        [HttpPost("save/{id}")]
        public async Task<ActionResult> Save([FromRoute] int id)
            => await this.Send(new SavePostCommand() { Id = id });

        [HttpPost("unsave/{id}")]
        public async Task<ActionResult> Unsave([FromRoute] int id)
            => await this.Send(new UnsavePostCommand() { Id = id });

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<ActionResult<IPage<PostOutputModel>>> Search([FromQuery] SearchPostsQuery postsQuery)
            => await this.Send(postsQuery);
    }
}
