using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Domain.Posts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Insightify.Posts.Infrastructure.Posts.Configuration
{
    internal class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(a => a.UserId)
                .IsRequired();

            builder
                .Property(a => a.Timestamp)
                .IsRequired();
        }
    }
}
