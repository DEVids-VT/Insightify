using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Application.Posts.Commands.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insightify.Posts.Web.Features
{
    [AllowAnonymous]
    public class PostsController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<CreatePostOutputModel>> Create([FromBody] CreatePostCommand command) 
            => await this.Send(command);
    }
}
