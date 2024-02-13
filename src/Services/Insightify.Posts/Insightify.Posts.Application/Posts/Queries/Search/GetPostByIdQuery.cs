using Insightify.Framework.Pagination.Abstractions;
using Insightify.Framework.Pagination.Extensions;
using Insightify.Posts.Application.Posts.Queries.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Application.Common;

namespace Insightify.Posts.Application.Posts.Queries.Search
{
    public class GetPostByIdQuery : EntityCommand<int>, IRequest<PostOutputModel>
    {
        public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostOutputModel>
        {
            private readonly IPostQueryRepository postRepository;

            public GetPostByIdQueryHandler(IPostQueryRepository postQueryRepository)
            {
                postRepository = postQueryRepository;
            }

            public async Task<PostOutputModel> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
            {
                var post = await postRepository.GetPostById<PostOutputModel>(request.Id, cancellationToken: cancellationToken);

                return post;
            }
        }
    }
}
