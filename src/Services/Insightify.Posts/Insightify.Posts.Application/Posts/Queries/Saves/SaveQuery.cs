namespace Insightify.Posts.Application.Posts.Queries.Like
{
    using Insightify.Posts.Application.Common;
    using MediatR;
    using Insightify.Posts.Application.Posts.Queries.Saves;

    public class SaveQuery : EntityCommand<int>, IRequest<IEnumerable<SaveOutputModel>>
    {
        public class SaveQueryHandler : IRequestHandler<SaveQuery, IEnumerable<SaveOutputModel>>
        {
            private readonly IPostQueryRepository postRepository;
            public SaveQueryHandler(IPostQueryRepository postRepository)
            {
                this.postRepository = postRepository;
            }
            public async Task<IEnumerable<SaveOutputModel>> Handle(SaveQuery request, CancellationToken cancellationToken)
            {
                var saves = await this.postRepository.GetSaves<SaveOutputModel>(request.Id, cancellationToken);
                return saves;
            }
        }
    }
}