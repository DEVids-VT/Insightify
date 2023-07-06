using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Application.Common;
using Insightify.Posts.Web.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Insightify.Posts.Web
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
        public const string PathSeparator = "/";
        public const string Id = "{id}";

        private IMediator? mediator;

        protected IMediator Mediator
            => this.mediator ??= this.HttpContext
                .RequestServices
                .GetService<IMediator>();

        protected Task<ActionResult<TResult>> Send<TResult>(IRequest<TResult> request)
            => this.Mediator.Send(request).ToActionResult();

        protected Task<ActionResult> Send(IRequest<Result> request)
            => this.Mediator.Send(request).ToActionResult();

        protected Task<ActionResult<TResult>> Send<TResult>(IRequest<Result<TResult>> request)
            => this.Mediator.Send(request).ToActionResult();
    }
}
