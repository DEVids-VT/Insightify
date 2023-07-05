using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Application.Common;

namespace Insightify.Posts.Application.Posts.Commands.Common
{
    public class CommentCommand<TCommand> : EntityCommand<int> where TCommand : EntityCommand<int>
    {
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public string Content { get; set; } = default!;
    }
}
