using System.Reflection;
using Insightify.Posts.Application.Common;
using Insightify.Posts.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Insightify.Posts.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApplicationSettings>(
                a => a = (ApplicationSettings)configuration.GetSection(nameof(ApplicationSettings)));
            services
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMediatR(cfg => 
                    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
            
    }
}
