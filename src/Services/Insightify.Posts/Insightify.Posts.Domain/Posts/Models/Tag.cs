using System.Runtime.CompilerServices;
using FluentAssertions;

namespace Insightify.Posts.Domain.Posts.Models
{
    using Insightify.Posts.Domain.Common.Models;
    using Insightify.Posts.Domain.Posts.Exceptions;

    using static ModelConstants.Tag;

    public class Tag : Entity<int>
    {
        private readonly HashSet<Post> posts;
        public Tag(string name)
        {
            this.ValidateName(name);
            this.Name = name;

            this.posts = new HashSet<Post>();
        }
        public string Name { get; private set; }

        public IReadOnlyCollection<Post> Posts => posts.ToList().AsReadOnly();

        public int TotalPosts => posts.Count;

        private void ValidateName(string name)
            => Guard.ForStringLength<InvalidTagException>(
                name, 
                MinTagLength,
                MaxTagLength, 
                nameof(this.Name));
    }

   
}
