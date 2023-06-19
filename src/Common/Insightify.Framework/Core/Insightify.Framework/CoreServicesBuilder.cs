using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Insightify.Framework
{
    public class CoreServicesBuilder
    {
        public IServiceCollection Services { get; set; }

        public CoreServicesBuilder(IServiceCollection services)
        {
            this.Services = services;

            services.AddLogging();
            services.AddOptions();
            services.AddHttpClient();
            services.AddHttpContextAccessor();

            services.Configure<RouteOptions>(x => x.LowercaseUrls = true);
        }
    }
}
