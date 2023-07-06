using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Insightify.Framework.MongoDb.Abstractions.Interfaces;
using Insightify.Posts.Application.Posts;
using Insightify.Posts.Domain.Common;
using Insightify.Posts.Domain.Posts.Models;
using Insightify.Posts.Domain.Posts.Repositories;
using Insightify.Posts.Infrastructure.Common;
using Insightify.Posts.Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Insightify.Posts.Infrastructure.Posts.Repositories
{
    internal class PostRepository : DataRepository<PostsDbContext, Post>, IPostDomainRepository, IPostQueryRepository
    {
        private readonly IMapper mapper;
        public PostRepository(PostsDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }
        public async Task<Post?> Find(int id, CancellationToken cancellationToken = default)
            => await this
                .All()
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .Include(p => p.Saves)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            var post = await this.Data.Posts.FindAsync(id);

            if (post == null)
            {
                return false;
            }

            this.Data.Posts.Remove(post);

            await this.Data.SaveChangesAsync(cancellationToken);

            return true;
        }

        public bool UserHasPost(string userId, int postId)
            => this.All().Where(p => p.AuthorId == userId).Any(p => p.Id == postId);

        public bool UserHasLiked(string userId, int postId)
            => this.All().Include(p => p.Likes).First(p => p.Id == postId).Likes.Any(l => l.UserId == userId);

        public bool UserHasCommentOnPost(string userId, int postId, int commentId)
            => this.All().Include(p => p.Comments).First(p => p.Id == postId).Comments
                .Any(c => c.Id == commentId && c.AuthorId == userId);
        public async Task<int> FindLikeId(string userId, int postId, CancellationToken cancellationToken = default)
        => (await this
            .All()
            .Include(p => p.Likes)
            .FirstAsync(p => p.Id == postId, cancellationToken))
            .Likes
            .First(l => l.UserId == userId)
            .Id;

        public bool UserHasSaved(string userId, int postId)
            => this.All().Include(p => p.Saves).First(p => p.Id == postId).Saves.Any(s => s.UserId == userId);

        public async Task<int> FindSaveId(string userId, int postId, CancellationToken cancellationToken = default)
            => (await this
                    .All()
                    .Include(p => p.Saves)
                    .FirstAsync(p => p.Id == postId, cancellationToken))
                .Saves
                .First(l => l.UserId == userId)
                .Id;

        //EFCore doesnt track comments for some reason. Temporary solution not following DDD.
        public async Task<bool> DeleteComment(int commentId, CancellationToken cancellationToken = default)
        {
            var comment = Data.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null)
            {
                return false;
            }
            this.Data.Comments.Remove(comment);

            await this.Data.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IPagedList<TOutputModel>> GetPosts<TOutputModel>(Specification<Post> postSpecification,
            int pageNumber, int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            int totalCount = Data.Posts.Count(postSpecification);

            var postsQuery = this.GetPostsQuery(postSpecification).ToList();

            var posts = postsQuery
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToList();

            var mappedPosts = mapper.Map<List<TOutputModel>>(posts);

            var totalPages = 0;
            if (totalCount > 0)
            {
                totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            }

            return new PagedList<TOutputModel>(mappedPosts, pageNumber + 1, pageSize, totalPages, totalCount);
        }

        public Task<IEnumerable<TCommentOutputModel>> GetComments<TCommentOutputModel>(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TLikeOutputModel>> GetLikes<TLikeOutputModel>(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TSaveOutputModel>> GetSaves<TSaveOutputModel>(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        private IQueryable<Post> GetPostsQuery(Specification<Post> specification)
        {
            var posts = this.Data.Posts
                .Include(p => p.Saves)
                .Include(p => p.Comments)
                .Include(p => p.Likes).Where(specification);
            return posts;
        }
    }
}
