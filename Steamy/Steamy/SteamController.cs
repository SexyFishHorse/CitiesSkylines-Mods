namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using Adapters;

    public class SteamController : ISteamController
    {
        private readonly IPlatformServiceAdapter platformService;

        private readonly ISimulationManagerAdapter simulationManager;

        public SteamController(IPlatformServiceAdapter platformService, ISimulationManagerAdapter simulationManager)
        {
            this.platformService = platformService;
            this.simulationManager = simulationManager;
        }

        public void UpdateAchievementsStatus()
        {
            var enableAchievements = ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableAchievements);

            simulationManager.SetAchievementsEnabled(enableAchievements);
        }

        public void UpdatePopupPosition()
        {
            var popupPosition = ModConfig.Instance.GetSetting<int>(SettingKeys.PopupPosition);

            platformService.SetPopupPosition(popupPosition);
        }
    }
}
