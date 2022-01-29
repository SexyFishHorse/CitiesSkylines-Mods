namespace SexyFishHorse.CitiesSkylines.Birdcage
{
    using SexyFishHorse.CitiesSkylines.Birdcage.Services;
    using SexyFishHorse.CitiesSkylines.Birdcage.Wrappers;
    using SexyFishHorse.CitiesSkylines.Infrastructure;
    using SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;
    using SexyFishHorse.CitiesSkylines.Infrastructure.UI.Configuration;

    public class UserMod : UserModBase
    {
        public const string ModName = "Birdcage";

        public UserMod()
        {
            var logger = new BirdcageLogger();

            var provider = new ServiceProvider { Logger = logger }
                .Add<ILogger>(logger)
                .AddSingleton<IChirpPanelWrapper, ChirpPanelWrapper>()
                .AddSingleton<IMessageManagerWrapper, MessageManagerWrapper>()
                .AddSingleton<FilterService>()
                .AddSingleton<InputService>()
                .AddSingleton<PositionService>()
                .AddSingleton<IOptionsPanelManager, OptionsPanelManager>();
            ServiceProvider.Instance = provider;

            OptionsPanelManager = ServiceProvider.Instance.GetService<IOptionsPanelManager>();
        }

        public override string Description
        {
            get
            {
                return "More Chirper controls";
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
