using Insightify.Posts.Domain.Posts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static Insightify.Posts.Domain.Posts.ModelConstants.Tag;

namespace Insightify.Posts.Infrastructure.Posts.Configuration
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(MaxTagLength);

            builder
                .HasMany(p => p.Posts)
                .WithMany(p => p.Tags);

        }
    }
}
