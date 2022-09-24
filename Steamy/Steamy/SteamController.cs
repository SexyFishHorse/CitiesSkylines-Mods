namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using Adapters;

    public class SteamController : ISteamController
    {
        private readonly IPlatformServiceAdapter _platformService;

        private readonly ISimulationManagerAdapter _simulationManager;

        public SteamController(IPlatformServiceAdapter platformService, ISimulationManagerAdapter simulationManager)
        {
            _platformService = platformService;
            _simulationManager = simulationManager;
        }

        public void UpdateAchievementsStatus()
        {
            var enableAchievements = ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableAchievements);

            _simulationManager.SetAchievementsEnabled(enableAchievements);
        }

        public void UpdatePopupPosition()
        {
            var popupPosition = ModConfig.Instance.GetSetting<int>(SettingKeys.PopupPosition);

            _platformService.SetPopupPosition(popupPosition);
        }
    }
}
