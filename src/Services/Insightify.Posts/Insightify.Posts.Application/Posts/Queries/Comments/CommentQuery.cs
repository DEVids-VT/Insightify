namespace Insightify.Posts.Application.Posts.Queries.Comments
{
    using Insightify.Posts.Application.Common;
    using MediatR;

    public class CommentQuery : EntityCommand<int>, IRequest<IEnumerable<CommentOutputModel>>
    {
        public class CommentQueryHandler : IRequestHandler<CommentQuery, IEnumerable<CommentOutputModel>>
        {
            private readonly IPostQueryRepository postRepository;
            public CommentQueryHandler(IPostQueryRepository postRepository)
            {
                this.postRepository = postRepository;
            }
            public async Task<IEnumerable<CommentOutputModel>> Handle(CommentQuery request, CancellationToken cancellationToken)
            {
                var comments = await this.postRepository.GetComments<CommentOutputModel>(request.Id, cancellationToken);
                return comments;
            }
        }
    }
}
