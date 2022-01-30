namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;
    using SexyFishHorse.CitiesSkylines.Infrastructure.UI.Configuration;
    using SexyFishHorse.CitiesSkylines.Steamy.Adapters;

    public static class ServiceProviderExtensions
    {
        public static IServiceProvider AddSteamy(this IServiceProvider services)
        {
            return services.AddSingleton<ILogger, SteamyLogger>()
                .AddSingleton<IPlatformServiceAdapter, PlatformServiceAdapter>()
                .AddSingleton<ISimulationManagerAdapter, SimulationManagerAdapter>()
                .AddSingleton<IOptionsPanelManager, OptionsPanelManager>()
                .AddSingleton<ISteamController, SteamController>();
        }
    }
}
