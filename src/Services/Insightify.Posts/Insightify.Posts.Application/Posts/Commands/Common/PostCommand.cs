using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Application.Common;

namespace Insightify.Posts.Application.Posts.Commands.Common
{
    public abstract class PostCommand<TCommand> : EntityCommand<int>
        where TCommand : EntityCommand<int>
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? ImageUrl { get; set; }

        public IEnumerable<string> Tags { get; set; } = new HashSet<string>();
    }
}
