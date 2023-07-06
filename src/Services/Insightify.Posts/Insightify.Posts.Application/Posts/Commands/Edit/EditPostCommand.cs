using Insightify.Posts.Application.Posts.Commands.Common;
using Insightify.Posts.Application.Posts.Commands.Create;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Application.Common;
using Insightify.Posts.Application.Common.Gateways;
using Insightify.Posts.Domain.Posts.Repositories;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client.Interfaces;

namespace Insightify.Posts.Application.Posts.Commands.Edit
{
    public class EditPostCommand : PostCommand<EditPostCommand>, IRequest<Result>
    {
        public class EditPostCommandHandler : IRequestHandler<EditPostCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPostDomainRepository postRepository;

            public EditPostCommandHandler(ICurrentUser currentUser, IPostDomainRepository postRepository)
            {
                this.currentUser = currentUser;
                this.postRepository = postRepository;
            }

            public async Task<Result> Handle(EditPostCommand request, CancellationToken cancellationToken)
            {
                var userHasPost = this.postRepository.UserHasPost(this.currentUser.UserId, request.Id);

                if (!userHasPost)
                {
                    return userHasPost;
                }

                var post = await this.postRepository.Find(request.Id, cancellationToken);

                post.UpdateTitle(request.Title).UpdateDescription(request.Description);
                if (request.ImageUrl != null)
                {
                    post.UpdateImageUrl(request.ImageUrl);
                }

                await this.postRepository.Save(post, cancellationToken);
                return Result.Success;
            }
        }
    }
}
