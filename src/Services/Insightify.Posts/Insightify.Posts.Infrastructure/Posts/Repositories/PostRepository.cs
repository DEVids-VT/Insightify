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
    }
}
