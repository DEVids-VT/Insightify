using System.Reflection;
using Insightify.Framework.DependencyInjection.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;

namespace Insightify.Framework
{

    public static class ServiceCollectionExtensions
    {
        public static CoreServicesBuilder AddCoreServices(this IServiceCollection services,
            Action<CoreServicesBuilder> builder, bool enforceAuthorization = true)
        {
            var serviceBuilder = new CoreServicesBuilder(services);
            builder(serviceBuilder);

            serviceBuilder.AddDefaultMvcOptions(enforceAuthorization);
            serviceBuilder.AddCorsWithAcceptAll();
            return serviceBuilder;
        }

        public static IMvcBuilder AddDefaultMvcOptions(this CoreServicesBuilder builder,
            bool enforceAuthorization = true)
        {
            return builder.Services.AddMvc(o =>
            {
                if (enforceAuthorization)
                {
                    o.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                        .Build()));
                }
            }).AddNewtonsoftJson(p =>
            {
                p.SerializerSettings.Formatting = Formatting.Indented;
                p.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
        }
        public static void AddCorsWithAcceptAll(this CoreServicesBuilder builder, string policyName = "CorsAllowAllOrigins")
        {
            builder.Services.AddCors(p => p.AddPolicy(policyName, b => b
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()));
        }
        public static CoreServicesBuilder ScanForServices(this CoreServicesBuilder builder, Assembly assembly)
        {
            builder.Services.ScanForServices(assembly);
            return builder;
        }

        public static void ScanForServices(this IServiceCollection services, Assembly assembly)
        {
            var typesToRegister = assembly.GetTypes().Where(p =>
                p.GetCustomAttributes(typeof(RegisterAttribute), true).Length > 0 && p.IsClass);

            foreach (var implementationType in typesToRegister)
            {
                var attribute = implementationType.GetCustomAttribute<RegisterAttribute>();
                if (attribute == null)
                {
                    continue;
                }
                var interfaceType = implementationType.GetInterfaces().FirstOrDefault();
                if (interfaceType == null)
                {
                    switch (attribute.LifeTime)
                    {
                        case ServiceLifetime.Singleton:
                            services.TryAddSingleton(implementationType);
                            continue;
                        case ServiceLifetime.Scoped:
                            services.TryAddScoped(implementationType);
                            continue;
                        case ServiceLifetime.Transient:
                            services.TryAddTransient(implementationType);
                            continue;
                    }
                }
                else
                {
                    switch (attribute.LifeTime)
                    {
                        case ServiceLifetime.Singleton:
                            services.TryAddSingleton(interfaceType, implementationType);
                            continue;
                        case ServiceLifetime.Scoped:
                            services.TryAddScoped(interfaceType, implementationType);
                            continue;
                        case ServiceLifetime.Transient:
                            services.TryAddTransient(interfaceType, implementationType);
                            continue;
                    }
                }
            }
        }
    }
}
