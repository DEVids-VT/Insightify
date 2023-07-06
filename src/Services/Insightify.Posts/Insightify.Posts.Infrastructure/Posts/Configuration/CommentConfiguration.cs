using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Domain.Posts;
using Insightify.Posts.Domain.Posts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Insightify.Posts.Domain.Posts.ModelConstants.Comment;


namespace Insightify.Posts.Infrastructure.Posts.Configuration
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Content)
                .IsRequired()
                .HasMaxLength(MaxContentLength);

            builder
                .Property(p => p.AuthorId)
                .IsRequired();
            builder.HasOne<Post>()
                .WithMany(p => p.Comments)
                .HasForeignKey("PostId");

        }
    }
}
