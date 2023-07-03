using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insightify.Posts.Application.Posts.Commands.Create
{
    public class CreatePostOutputModel
    {
        public CreatePostOutputModel(int postId)
            => this.PostId = postId;
       
        public int PostId { get; set; }
    }
}
