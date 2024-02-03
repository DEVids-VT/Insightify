using Insightify.Posts.Application.Posts.Queries.Comments;
using Insightify.Posts.Application.Posts.Queries.Saves;

namespace Insightify.Posts.Web.Features
{
    using Insightify.Posts.Application.Posts.Commands.Create;
    using Insightify.Posts.Application.Posts.Commands.Delete;
    using Insightify.Posts.Application.Posts.Queries.Common;
    using Insightify.Posts.Application.Posts.Queries.Like;
    using Insightify.Framework.Pagination.Abstractions;
    using Insightify.Posts.Application.Posts.Commands.Edit;
    using Insightify.Posts.Application.Posts.Commands.Like;
    using Insightify.Posts.Application.Posts.Commands.Save;
    using Insightify.Posts.Application.Posts.Queries.Likes;
    using Insightify.Posts.Application.Posts.Queries.Mine;
    using Insightify.Posts.Application.Posts.Queries.Search;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : ApiController
    {
        #region Queries

        [HttpGet]
        public async Task<ActionResult<IPage<PostOutputModel>>> Search([FromQuery] SearchPostsQuery postsQuery)
            => await this.Send(postsQuery);

        [HttpGet("{postId}")]
        public async Task<ActionResult<PostOutputModel>> Get([FromRoute] int postId)
            => await this.Send(new GetPostByIdQuery() { Id = postId });

        [HttpGet("mine")]
        public async Task<ActionResult<IPage<PostOutputModel>>> Mine([FromQuery] MinePostsQuery postsQuery)
            => await this.Send(postsQuery);

        [HttpGet("{postId}/likes")]
        public async Task<ActionResult<IEnumerable<LikeOutputModel>>> Likes([FromRoute] int postId)
            => await this.Send(new LikeQuery() { Id = postId });
        [HttpGet("{postId}/saves")]
        public async Task<ActionResult<IEnumerable<SaveOutputModel>>> Saves([FromRoute] int postId)
            => await this.Send(new SaveQuery() { Id = postId });
        [HttpGet("{postId}/comments")]
        public async Task<ActionResult<IEnumerable<CommentOutputModel>>> Comments([FromRoute] int postId)
            => await this.Send(new CommentQuery() { Id = postId });

        #endregion


        #region Posts

        [HttpPost("create")]
        public async Task<ActionResult<CreatePostOutputModel>> Create([FromBody] CreatePostCommand command)
            => await this.Send(command);

        [HttpPost("edit")]
        public async Task<ActionResult> Edit([FromBody] EditPostCommand command)
            => await this.Send(command);

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
            => await this.Send(new DeletePostCommand() { Id = id });

        #endregion

        #region Reactions

        [HttpPost("{postId}/like")]
        public async Task<ActionResult> Like([FromRoute] int postId)
            => await this.Send(new LikePostCommand() { Id = postId });

        [HttpPost("{postId}/dislike")]
        public async Task<ActionResult> Dislike([FromRoute] int postId)
            => await this.Send(new DislikePostCommand() { Id = postId });

        [HttpPost("{postId}/save")]
        public async Task<ActionResult> Save([FromRoute] int postId)
            => await this.Send(new SavePostCommand() { Id = postId });

        [HttpPost("{postId}/unsave")]
        public async Task<ActionResult> Unsave([FromRoute] int postId)
            => await this.Send(new UnsavePostCommand() { Id = postId });

        [HttpPost("comment")]
        public async Task<ActionResult> Comment([FromBody] AddCommentCommand command)
            => await this.Send(command);

        [HttpPost("uncomment")]
        public async Task<ActionResult> RemoveComment([FromBody] DeleteCommentCommand command)
            => await this.Send(command);
        #endregion

    }
}
