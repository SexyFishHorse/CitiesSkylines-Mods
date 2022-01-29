namespace SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection
{
    using System;

    internal class ServiceDefinition
    {
        /// <summary>
        /// The type that will be returned from the provider.
        /// </summary>
        public Type InstanceType { get; set; }

        /// <summary>
        /// The lifetime of the service.
        /// </summary>
        public ServiceLifetime Lifetime { get; set; }

        /// <summary>
        /// Create a new instance of <see cref="ServiceDefinition"/>.
        /// </summary>
        /// <param name="instanceType">The type that will be returned from the provider.</param>
        /// <param name="lifetime">The lifetime of the service.</param>
        public ServiceDefinition(Type instanceType, ServiceLifetime lifetime)
        {
            InstanceType = instanceType;
            Lifetime = lifetime;
        }
    }
}
