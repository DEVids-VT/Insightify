using Insightify.Posts.Application.Common.Gateways;
using Insightify.Posts.Application.Common;
using Insightify.Posts.Domain.Posts.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Posts.Application.Posts.Commands.Save
{
    public class UnsavePostCommand : EntityCommand<int>, IRequest<Result>
    {
        public class UnsavePostCommandHandler : IRequestHandler<UnsavePostCommand, Result>
        {
            private readonly IPostDomainRepository postRepository;
            private readonly ICurrentUser currentUser;

            public UnsavePostCommandHandler(IPostDomainRepository postRepository, ICurrentUser currentUser)
            {
                this.postRepository = postRepository;
                this.currentUser = currentUser;
            }

            public async Task<Result> Handle(UnsavePostCommand request, CancellationToken cancellationToken)
            {
                var post = await this.postRepository.Find(request.Id, cancellationToken);
                if (post == null)
                {
                    return false;
                }

                var userHasSaved = this.postRepository.UserHasSaved(this.currentUser.UserId, request.Id);
                if (!userHasSaved)
                {
                    return userHasSaved;
                }

                var saveId = await this.postRepository.FindSaveId(currentUser.UserId, request.Id, cancellationToken);
                post.RemoveSave(saveId);
                await this.postRepository.Save(post, cancellationToken);
                return true;
            }
        }
    }
}
