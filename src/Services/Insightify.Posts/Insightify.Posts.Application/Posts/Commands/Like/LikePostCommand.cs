using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Application.Common;
using Insightify.Posts.Application.Common.Gateways;
using Insightify.Posts.Application.Posts.Commands.Delete;
using Insightify.Posts.Domain.Posts.Repositories;
using MediatR;

namespace Insightify.Posts.Application.Posts.Commands.Like
{
    public class LikePostCommand : EntityCommand<int>, IRequest<Result>
    {
        public class LikePostCommandHandler : IRequestHandler<LikePostCommand, Result>
        {
            private readonly IPostDomainRepository postRepository;
            private readonly ICurrentUser currentUser;

            public LikePostCommandHandler(IPostDomainRepository postRepository, ICurrentUser currentUser)
            {
                this.postRepository = postRepository;
                this.currentUser = currentUser;
            }

            public async Task<Result> Handle(LikePostCommand request, CancellationToken cancellationToken)
            {
                var post = await this.postRepository.Find(request.Id, cancellationToken);
                if (post == null)
                {
                    return false;
                }
                if (post.Likes.Any(p => p.UserId == currentUser.UserId))
                {
                    return "Already liked.";
                }

                post.AddLike(currentUser.UserId, DateTime.UtcNow);
                await this.postRepository.Save(post, cancellationToken);
                return true;
            }
        }
    }
}
