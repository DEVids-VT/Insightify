using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Application.Common;

namespace Insightify.Posts.Application.Posts.Commands.Common
{
    public abstract class PostCommand<TCommand> : EntityCommand<Guid>
        where TCommand : EntityCommand<Guid>
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? ImageUrl { get; set; }
    }
}
