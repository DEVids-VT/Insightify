using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Domain.Posts;
using Insightify.Posts.Domain.Posts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Insightify.Posts.Domain.Posts.ModelConstants.Post;
using static Insightify.Posts.Domain.Posts.ModelConstants.Common;


namespace Insightify.Posts.Infrastructure.Posts.Configuration
{
    internal class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(MaxTitleLength);

            builder
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(MaxDescriptionLength);

            builder
                .Property(p => p.ImageUrl)
                .IsRequired()
                .HasMaxLength(MaxUrlLength);

            builder
                .Property(p => p.AuthorId)
                .IsRequired();

            builder
                .HasMany(p => p.Likes)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("likes");
            builder
                .HasMany(p => p.Saves)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("saves");
            builder
                .HasMany(p => p.Comments)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("comments");

        }
    }
}
