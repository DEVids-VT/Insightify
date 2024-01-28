using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Domain.Posts.Models;
using Microsoft.EntityFrameworkCore;

namespace Insightify.Posts.Infrastructure.Common
{
    public class PostsDbContext : DbContext
    {
        public PostsDbContext() {}
        public PostsDbContext(DbContextOptions<PostsDbContext> options) : base(options) { }


        public DbSet<Post> Posts { get; set; } = default!;
        public DbSet<Like> Likes { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;
        public DbSet<Save> Saves { get; set; } = default!;
        public DbSet<Tag> Tags { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
