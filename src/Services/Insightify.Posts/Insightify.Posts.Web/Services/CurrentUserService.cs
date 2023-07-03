using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Application.Common.Gateways;
using Microsoft.AspNetCore.Http;

namespace Insightify.Posts.Web.Services
{
    public class CurrentUserService : ICurrentUser
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user == null)
            {
                throw new InvalidOperationException("This request does not have an authenticated user.");
            }

            this.UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }
    }
}
