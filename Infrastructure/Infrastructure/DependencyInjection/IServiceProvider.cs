namespace SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection
{
    using System;
    using SexyFishHorse.CitiesSkylines.Logger;

    public interface IServiceProvider
    {
        /// <summary>
        ///     If the logger is set any errors during adding or requesting services are logged. Exceptions will still be thrown.
        /// </summary>
        ILogger Logger { get; set; }

        IServiceProvider Add(Type abstraction, Type implementation, ServiceLifetime lifetime);

        TImplementation GetService<TImplementation>();

        IServiceProvider Add(Type abstraction, object implementation);
    }
}
