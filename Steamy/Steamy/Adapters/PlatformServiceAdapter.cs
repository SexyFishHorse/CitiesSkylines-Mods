namespace SexyFishHorse.CitiesSkylines.Steamy.Adapters
{
    using ColossalFramework.PlatformServices;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;

    public class PlatformServiceAdapter : IPlatformServiceAdapter
    {
        private readonly ILogger _logger;

        public PlatformServiceAdapter(ILogger logger)
        {
            _logger = logger;
        }

        public void SetPopupPosition(int position)
        {
            var notificationPosition = (NotificationPosition)position;

            PlatformService.SetOverlayNotificationPosition(notificationPosition);

            _logger.Info("Changed popup position to {0}", position);
        }
    }
}
