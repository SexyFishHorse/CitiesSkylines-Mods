namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using System;
    using ICities;
    using SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;

    public class LoadingExtension : LoadingExtensionBase
    {
        private readonly ILogger logger;

        private readonly ISteamController steamController;

        public LoadingExtension() : this(
            ServiceProvider.Instance.GetService<ILogger>(),
            ServiceProvider.Instance.GetService<ISteamController>())
        {
        }

        public LoadingExtension(ILogger logger, ISteamController steamController)
        {
            this.logger = logger;
            this.steamController = steamController;

            try
            {
                steamController.UpdateAchievementsStatus();
                steamController.UpdatePopupPosition();

                logger.Info("SteamyUserMod");
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }


        public override void OnCreated(ILoading loading)
        {
            try
            {
                logger.Info("On created");

                steamController.UpdatePopupPosition();
                steamController.UpdateAchievementsStatus();
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            try
            {
                logger.Info("On level loaded");

                steamController.UpdatePopupPosition();
                steamController.UpdateAchievementsStatus();
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }
    }
}
