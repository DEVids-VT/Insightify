using Insightify.Posts.Application.Common.Gateways;
using Insightify.Posts.Domain.Posts.Repositories;

namespace Insightify.Posts.Application.Posts.Commands.Delete
{
    using Insightify.Posts.Application.Common;
    using MediatR;

    public class DeletePostCommand : EntityCommand<int>, IRequest<Result>
    {
        public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPostDomainRepository postRepository;

            public DeletePostCommandHandler(ICurrentUser currentUser, IPostDomainRepository postRepository)
            {
                this.currentUser = currentUser;
                this.postRepository = postRepository;
            }
            public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken)
            {
                var authorHasPost = this.postRepository.UserHasPost(currentUser.UserId, request.Id);
                    
                if (!authorHasPost)
                {
                    return authorHasPost;
                }

                return await this.postRepository.Delete(request.Id, cancellationToken);
            }
        }
    }
}
