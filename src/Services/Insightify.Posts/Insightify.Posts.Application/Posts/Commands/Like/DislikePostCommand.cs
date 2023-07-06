using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Application.Common;
using Insightify.Posts.Application.Common.Gateways;
using Insightify.Posts.Domain.Posts.Repositories;
using MediatR;

namespace Insightify.Posts.Application.Posts.Commands.Like
{
    public class DislikePostCommand : EntityCommand<int>, IRequest<Result>
    {
        public class DislikePostCommandHandler : IRequestHandler<DislikePostCommand, Result>
        {
            private readonly IPostDomainRepository postRepository;
            private readonly ICurrentUser currentUser;

            public DislikePostCommandHandler(IPostDomainRepository postRepository, ICurrentUser currentUser)
            {
                this.postRepository = postRepository;
                this.currentUser = currentUser;
            }

            public async Task<Result> Handle(DislikePostCommand request, CancellationToken cancellationToken)
            {
                var post = await this.postRepository.Find(request.Id, cancellationToken);
                if (post == null)
                {
                    return false;
                }

                var userHasLiked = this.postRepository.UserHasLiked(this.currentUser.UserId, request.Id);
                if (!userHasLiked)
                {
                    return userHasLiked;
                }

                var likeId = await this.postRepository.FindLikeId(currentUser.UserId, request.Id, cancellationToken);
                post.RemoveLike(likeId);
                await this.postRepository.Save(post, cancellationToken);
                return true;
            }
        }
    }
}
