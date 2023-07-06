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
    public class SavePostCommand : EntityCommand<int>, IRequest<Result>
    {
        public class SavePostCommandHandler : IRequestHandler<SavePostCommand, Result>
        {
            private readonly IPostDomainRepository postRepository;
            private readonly ICurrentUser currentUser;

            public SavePostCommandHandler(IPostDomainRepository postRepository, ICurrentUser currentUser)
            {
                this.postRepository = postRepository;
                this.currentUser = currentUser;
            }

            public async Task<Result> Handle(SavePostCommand request, CancellationToken cancellationToken)
            {
                var post = await this.postRepository.Find(request.Id, cancellationToken);
                if (post == null)
                {
                    return false;
                }

                post.AddSave(currentUser.UserId, DateTime.UtcNow);
                await this.postRepository.Save(post, cancellationToken);
                return true;
            }
        }
    }
}
