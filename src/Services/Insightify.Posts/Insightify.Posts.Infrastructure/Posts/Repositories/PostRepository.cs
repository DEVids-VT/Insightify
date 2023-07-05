using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Domain.Posts.Models;
using Insightify.Posts.Domain.Posts.Repositories;
using Insightify.Posts.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Insightify.Posts.Infrastructure.Posts.Repositories
{
    internal class PostRepository : DataRepository<PostsDbContext, Post>, IPostDomainRepository
    {
        public PostRepository(PostsDbContext context) : base(context)
        {

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
            =>  this.All().Where(p => p.AuthorId == userId).Any(p => p.Id == postId);

        public bool UserHasLiked(string userId, int postId)
            => this.All().Include(p => p.Likes).First(p => p.Id == postId).Likes.Any(l => l.UserId == userId);

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

    }
}
