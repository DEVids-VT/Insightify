using Insightify.Framework.Pagination.Abstractions;
using Insightify.Posts.Application.Posts.Queries.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Framework.Pagination.Extensions;
using Insightify.Posts.Application.Common.Gateways;
using Insightify.Posts.Application.Posts.Queries.Search;

namespace Insightify.Posts.Application.Posts.Queries.Mine
{
    public class MinePostsQuery : PostsQuery, IRequest<IPage<PostOutputModel>>
    {
        public string? AuthorId { get; set; }
        public class MinePostsQueryHandler : PostsQueryHandler, IRequestHandler<MinePostsQuery, IPage<PostOutputModel>>
        {
            private readonly ICurrentUser currentUser;
            public MinePostsQueryHandler(IPostQueryRepository postQueryRepository, ICurrentUser currentUser) : base(postQueryRepository)
            {
                this.currentUser = currentUser;
            }

            public async Task<IPage<PostOutputModel>> Handle(MinePostsQuery request,
                CancellationToken cancellationToken)
            {
                var authorId = string.IsNullOrEmpty(request.AuthorId) ? currentUser.UserId : request.AuthorId;

                var posts = await base.GetPosts<PostOutputModel>(request, authorId, cancellationToken: cancellationToken);

                return posts.ToPage();
            }
        }
    }
}
