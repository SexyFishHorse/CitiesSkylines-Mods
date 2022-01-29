namespace SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection
{
    public static class ServiceProviderExtensions
    {
        public static IServiceProvider Add<TAbstraction, TImplementation>(this IServiceProvider provider, ServiceLifetime lifetime)
            where TImplementation : class, TAbstraction
        {
            return provider.Add(typeof(TAbstraction), typeof(TImplementation), lifetime);
        }

        public static IServiceProvider Add<TImplementation>(this IServiceProvider provider, ServiceLifetime lifetime)
            where TImplementation : class
        {
            return provider.Add(typeof(TImplementation), typeof(TImplementation), lifetime);
        }

        public static IServiceProvider AddSingleton<TAbstraction, TImplementation>(this IServiceProvider provider)
            where TImplementation : class, TAbstraction
        {
            return provider.Add(typeof(TAbstraction), typeof(TImplementation), ServiceLifetime.Singleton);
        }

        public static IServiceProvider AddSingleton<TImplementation>(this IServiceProvider provider)
            where TImplementation : class
        {
            return provider.Add(typeof(TImplementation), typeof(TImplementation), ServiceLifetime.Singleton);
        }

        public static IServiceProvider AddTransient<TAbstraction, TImplementation>(this IServiceProvider provider)
            where TImplementation : class, TAbstraction
        {
            return provider.Add(typeof(TAbstraction), typeof(TImplementation), ServiceLifetime.Transient);
        }

        public static IServiceProvider AddTransient<TImplementation>(this IServiceProvider provider)
            where TImplementation : class
        {
            return provider.Add(typeof(TImplementation), typeof(TImplementation), ServiceLifetime.Transient);
        }
    }
}
