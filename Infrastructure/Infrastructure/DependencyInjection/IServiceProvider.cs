namespace SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection
{
    using System;

    public interface IServiceProvider
    {
        IServiceProvider Add(Type abstraction, Type implementation, ServiceLifetime lifetime);

        TImplementation GetService<TImplementation>();

        IServiceProvider Add(Type abstraction, object implementation);
    }
}
