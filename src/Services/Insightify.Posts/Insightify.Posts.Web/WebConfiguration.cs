using Insightify.Posts.Application.Common.Gateways;
using Insightify.Posts.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Insightify.Posts.Application.Common;

namespace Insightify.Posts.Web
{
    public static class WebConfiguration
    {
        public static IServiceCollection AddWebComponents(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<Result>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUserService>()
                .AddControllers();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}
