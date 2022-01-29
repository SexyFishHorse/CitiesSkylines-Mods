namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Infrastructure;
    using SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;
    using SexyFishHorse.CitiesSkylines.Infrastructure.UI.Configuration;
    using SexyFishHorse.CitiesSkylines.Steamy.Adapters;

    [UsedImplicitly]
    public class UserMod : UserModBase
    {
        public const string ModName = "Steamy";

        public UserMod()
        {
            var logger = new SteamyLogger();

            var provider = ServiceProvider.Instance = new ServiceProvider { Logger = logger };

            provider
                .Add<ILogger>(logger)
                .AddSingleton<IPlatformServiceAdapter, PlatformServiceAdapter>()
                .AddSingleton<ISimulationManagerAdapter, SimulationManagerAdapter>()
                .AddSingleton<IOptionsPanelManager, OptionsPanelManager>()
                .AddSingleton<ISteamController, SteamController>();

            OptionsPanelManager = provider.GetService<IOptionsPanelManager>();
        }

        public override string Description
        {
            get
            {
                return "Configure how Steam integrates with the game";
            }
        }

        public override string Name
        {
            get
            {
                return ModName;
            }
        }
    }
}
