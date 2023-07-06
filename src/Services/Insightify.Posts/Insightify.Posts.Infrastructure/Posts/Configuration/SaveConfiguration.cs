using Insightify.Posts.Domain.Posts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insightify.Posts.Infrastructure.Posts.Configuration
{
    internal class SaveConfiguration : IEntityTypeConfiguration<Save>
    {
        public void Configure(EntityTypeBuilder<Save> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(a => a.UserId)
                .IsRequired();

            builder
                .Property(a => a.Timestamp)
                .IsRequired();

            builder.Property<int>("PostId");

            builder.HasOne<Post>()
                .WithMany(p => p.Saves)
                .HasForeignKey("PostId");
        }
    }
}
