using Insightify.Posts.Application.Common;
using Insightify.Posts.Application.Common.Gateways;
using Insightify.Posts.Domain.Posts.Repositories;
using MediatR;

namespace Insightify.Posts.Application.Posts.Commands.Delete
{
    public class DeleteCommentCommand : EntityCommand<int>, IRequest<Result>
    {
        public int PostId { get; set; }
        public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPostDomainRepository postRepository;

            public DeleteCommentCommandHandler(ICurrentUser currentUser, IPostDomainRepository postRepository)
            {
                this.currentUser = currentUser;
                this.postRepository = postRepository;
            }
            public async Task<Result> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
            {
                var post = await this.postRepository.Find(request.PostId, cancellationToken);
                if (post == null)
                {
                    return false;
                }

                var userHasComment = this.postRepository.UserHasCommentOnPost(this.currentUser.UserId, request.PostId, request.Id);
                if (!userHasComment)
                {
                    return userHasComment;
                }

                await this.postRepository.DeleteComment(request.Id, cancellationToken);
                post.RemoveComment(request.Id);
                await this.postRepository.Save(post, cancellationToken);
                return Result.Success;
            }
        }
    }
}
