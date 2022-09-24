namespace SexyFishHorse.CitiesSkylines.Ragnarok.ModExtensions
{
    using ColossalFramework.UI;
    using ICities;
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Extensions;
    using SexyFishHorse.CitiesSkylines.Ragnarok.Logging;
    using ILogger = SexyFishHorse.CitiesSkylines.Infrastructure.Logging.ILogger;
    using UnityObject = UnityEngine.Object;

    [UsedImplicitly]
    public class GiantDisasterService : ILoadingExtension
    {
        private readonly ILogger _logger;

        public GiantDisasterService()
        {
            _logger = RagnarokLogger.Instance;
        }

        public void OnCreated(ILoading loading)
        {
        }

        public void OnLevelLoaded(LoadMode mode)
        {
            if (!mode.IsGameOrScenario())
            {
                return;
            }

            _logger.Info("Changing max disaster spawn intensity");

            var optionPanel = UnityObject.FindObjectOfType<DisastersOptionPanel>();
            var slider = optionPanel.GetComponentInChildren<UISlider>();
            slider.maxValue = byte.MaxValue;
            slider.minValue = byte.MinValue;

            _logger.Info("Max disaster spawn intensity changed to 25.5");
        }

        public void OnLevelUnloading()
        {
        }

        public void OnReleased()
        {
        }
    }
}
