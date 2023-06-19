using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Insightify.Framework.Automapper
{
    public static class CoreServicesBuilderExtensions
    {
        public static CoreServicesBuilder AddAutomapper(this CoreServicesBuilder builder, Assembly assebly)
        {
            builder.Services.AddAutoMapper(assebly);
            return builder;
        }
    }
}
