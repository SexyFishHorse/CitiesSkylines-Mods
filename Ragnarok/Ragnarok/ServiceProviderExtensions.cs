namespace SexyFishHorse.CitiesSkylines.Ragnarok
{
    using SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection;
    using SexyFishHorse.CitiesSkylines.Infrastructure.UI.Configuration;
    using SexyFishHorse.CitiesSkylines.Ragnarok.Configuration;
    using SexyFishHorse.CitiesSkylines.Ragnarok.Logging;

    public static class ServiceProviderExtensions
    {
        public static IServiceProvider AddRagnarok(this IServiceProvider services)
        {
            return services
                   .Add(RagnarokLogger.Instance)
                   .AddSingleton<IOptionsPanelManager, OptionsPanelManager>();
        }
    }
}
