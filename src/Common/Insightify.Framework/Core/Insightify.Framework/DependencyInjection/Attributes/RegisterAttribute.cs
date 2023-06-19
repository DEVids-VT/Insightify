
namespace Insightify.Framework.DependencyInjection.Attributes
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Registers a Class/Interface with Dependency Injection
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterAttribute : Attribute
    {
        public ServiceLifetime LifeTime { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="RegisterAttribute"/> class
        /// </summary>
        /// <param name="lifeTime">Service Lifetime</param>
        public RegisterAttribute(ServiceLifetime lifeTime)
        {
            this.LifeTime = lifeTime;
        }
    }
}
