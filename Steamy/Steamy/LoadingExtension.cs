namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using System;
    using ICities;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;

    public class LoadingExtension : LoadingExtensionBase
    {
        private readonly ILogger _logger;

        private readonly ISteamController _steamController;

        public LoadingExtension() : this(
            UserMod.Services.GetService<ILogger>(),
            UserMod.Services.GetService<ISteamController>())
        {
        }

        public LoadingExtension(ILogger logger, ISteamController steamController)
        {
            _logger = logger;
            _steamController = steamController;

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
                _logger.Info("On created");

                _steamController.UpdatePopupPosition();
                _steamController.UpdateAchievementsStatus();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                throw;
            }
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            try
            {
                _logger.Info("On level loaded");

                _steamController.UpdatePopupPosition();
                _steamController.UpdateAchievementsStatus();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                throw;
            }
        }
    }
}
