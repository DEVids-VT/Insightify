using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Framework.Pagination.Abstractions;
using Insightify.Framework.Pagination.Extensions;
using Insightify.Posts.Application.Posts.Queries.Common;
using MediatR;

namespace Insightify.Posts.Application.Posts.Queries.Search
{
    public class SearchPostsQuery : PostsQuery, IRequest<IPage<PostOutputModel>>
    {
        public class SearchPostsQueryHandler : PostsQueryHandler,
            IRequestHandler<SearchPostsQuery, IPage<PostOutputModel>>
        {
            public SearchPostsQueryHandler(IPostQueryRepository postQueryRepository) : base(postQueryRepository)
            {
            }

            public async Task<IPage<PostOutputModel>> Handle(SearchPostsQuery request, CancellationToken cancellationToken)
            {
                var posts = await base.GetPosts<PostOutputModel>(request, cancellationToken);

                return posts.ToPage();
            }
        }
    }
}
