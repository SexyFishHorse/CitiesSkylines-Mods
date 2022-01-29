namespace SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection
{
    public enum ServiceLifetime
    {
        /// <summary>
        /// The same instance is returned each time it is requested form the provider.
        /// </summary>
        Singleton = 1,

        /// <summary>
        /// A new instance is created each time it is requested from the provider;
        /// </summary>
        Transient = 2,
    }
}
