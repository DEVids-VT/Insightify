namespace Insightify.Posts.Application.Posts.Queries.Like
{
    using Insightify.Posts.Application.Posts.Queries.Likes;
    using Insightify.Posts.Application.Common;
    using MediatR;
    public class LikeQuery : EntityCommand<int>, IRequest<IEnumerable<LikeOutputModel>>
    {
        public class LikeQueryHandler : IRequestHandler<LikeQuery, IEnumerable<LikeOutputModel>>
        {
            private readonly IPostQueryRepository postRepository;
            public LikeQueryHandler(IPostQueryRepository postRepository)
            {
                this.postRepository = postRepository;
            }
            public async Task<IEnumerable<LikeOutputModel>> Handle(LikeQuery request, CancellationToken cancellationToken)
            {
                var likes = await this.postRepository.GetLikes<LikeOutputModel>(request.Id, cancellationToken);
                return likes;
            }
        }
    }
}
