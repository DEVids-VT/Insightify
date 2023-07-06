using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Framework.MongoDb.Abstractions.Interfaces;
using Insightify.Posts.Domain.Common;
using Insightify.Posts.Domain.Posts.Models;
using Insightify.Posts.Domain.Posts.Specifications;

namespace Insightify.Posts.Application.Posts.Queries.Common
{
    public abstract class PostsQuery
    {
        public string? Title { get; set; }
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 10;

        public abstract class PostsQueryHandler
        {
            private readonly IPostQueryRepository postQueryRepository;

            public PostsQueryHandler(IPostQueryRepository postQueryRepository)
            {
                this.postQueryRepository = postQueryRepository;
            }

            protected async Task<IPagedList<TOutputModel>> GetPosts<TOutputModel>(
                PostsQuery request, 
                string? authorId = default,
                CancellationToken cancellationToken = default)
            {
                var postSpecification = this.GetPostSpecification(request, authorId);

                return await this.postQueryRepository.GetPosts<TOutputModel>(
                    postSpecification, 
                    request.Page,
                    request.PageSize, 
                    cancellationToken);
            }

            private Specification<Post> GetPostSpecification(PostsQuery request, string? authorId)
                => new PostTitleSpecification(request.Title).And(new PostAuthorIdSpecification(authorId));

        }
    }
}
