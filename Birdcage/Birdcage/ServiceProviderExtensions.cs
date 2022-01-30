namespace SexyFishHorse.CitiesSkylines.Birdcage
{
    using SexyFishHorse.CitiesSkylines.Birdcage.Services;
    using SexyFishHorse.CitiesSkylines.Birdcage.Settings;
    using SexyFishHorse.CitiesSkylines.Birdcage.Wrappers;
    using SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;
    using SexyFishHorse.CitiesSkylines.Infrastructure.UI.Configuration;

    public static class ServiceProviderExtensions
    {
        public static IServiceProvider AddBirdcage(this IServiceProvider services)
        {
            return services
                .AddSingleton<ILogger,BirdcageLogger>()
                .AddSingleton<IChirpPanelWrapper, ChirpPanelWrapper>()
                .AddSingleton<IMessageManagerWrapper, MessageManagerWrapper>()
                .AddSingleton<FilterService>()
                .AddSingleton<InputService>()
                .AddSingleton<PositionService>()
                .AddSingleton<IOptionsPanelManager, OptionsPanelManager>();
        }
    }
}
