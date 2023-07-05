using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Application.Common.Gateways;
using Insightify.Posts.Application.Common;
using Insightify.Posts.Application.Posts.Commands.Common;
using Insightify.Posts.Application.Posts.Commands.Like;
using Insightify.Posts.Domain.Posts.Repositories;
using MediatR;

namespace Insightify.Posts.Application.Posts.Commands.Create
{
    public class AddCommentCommand : CommentCommand<AddCommentCommand>, IRequest<Result>
    {
        public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Result>
        {
            private readonly IPostDomainRepository postRepository;
            private readonly ICurrentUser currentUser;

            public AddCommentCommandHandler(IPostDomainRepository postRepository, ICurrentUser currentUser)
            {
                this.postRepository = postRepository;
                this.currentUser = currentUser;
            }

            public async Task<Result> Handle(AddCommentCommand request, CancellationToken cancellationToken)
            {
                if (request is { PostId: { }, CommentId: null })
                {
                    var post = await this.postRepository.Find((int)request.PostId, cancellationToken);
                    if (post == null)
                    {
                        return false;
                    }
                    post.AddComment(request.Content, currentUser.UserId);
                    await this.postRepository.Save(post, cancellationToken);
                    return true;

                }
                else if (request is { PostId: { }, CommentId: { }})
                {
                    var post = await this.postRepository.Find((int)request.PostId, cancellationToken);
                    if (post == null)
                    {
                        return false;
                    }
                    var comment = post.Comments.FirstOrDefault(c => c.Id == request.CommentId);
                    if (comment == null)
                    {
                        return false;
                    }
                    comment.AddComment(request.Content, currentUser.UserId);
                    await this.postRepository.Save(post, cancellationToken);
                    return true;
                }

                return false;

            }
        }
    }
}
