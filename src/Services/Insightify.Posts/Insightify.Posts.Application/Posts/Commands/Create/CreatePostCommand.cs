using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Application.Common.Gateways;
using Insightify.Posts.Application.Posts.Commands.Common;
using Insightify.Posts.Domain.Posts.Factories;
using Insightify.Posts.Domain.Posts.Repositories;
using MediatR;

namespace Insightify.Posts.Application.Posts.Commands.Create
{
    public class CreatePostCommand : PostCommand<CreatePostCommand>, IRequest<CreatePostOutputModel>
    {
        public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, CreatePostOutputModel>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPostFactory postFactory;
            private readonly IPostDomainRepository postRepository;

            public CreatePostCommandHandler(ICurrentUser currentUser, IPostFactory postFactory, IPostDomainRepository postRepository)
            {
                this.currentUser = currentUser;
                this.postFactory = postFactory;
                this.postRepository = postRepository;
            }
            public async Task<CreatePostOutputModel> Handle(CreatePostCommand request, CancellationToken cancellationToken)
            {
                var factory = request.ImageUrl != null ? postFactory.WithImageUrl(request.ImageUrl) : postFactory;

                var post = factory
                    .WithTitle(request.Title)
                    .WithDescription(request.Description)
                    .WithAuthor("1")//TEST
                    .Build();

                await this.postRepository.Save(post, cancellationToken);

                return new CreatePostOutputModel(post.Id);
            }
        }
    }
}
