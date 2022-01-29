namespace SexyFishHorse.CitiesSkylines.Birdcage
{
    using System;
    using SexyFishHorse.CitiesSkylines.Birdcage.Services;
    using SexyFishHorse.CitiesSkylines.Birdcage.Wrappers;
    using SexyFishHorse.CitiesSkylines.Infrastructure;
    using SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection;
    using SexyFishHorse.CitiesSkylines.Infrastructure.UI.Configuration;
    using SexyFishHorse.CitiesSkylines.Logger;
    using UnityObject = UnityEngine.Object;

    public class UserMod : UserModBase
    {
        public const string ModName = "Birdcage";

        public UserMod()
        {
            var logger = new BirdcageLogger();

            try
            {
                var provider = new ServiceProvider()
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
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
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
