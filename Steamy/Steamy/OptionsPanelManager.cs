namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ColossalFramework.PlatformServices;
    using Infrastructure.UI;
    using Infrastructure.UI.Configuration;
    using Logger;

    public class OptionsPanelManager : IOptionsPanelManager
    {
        private static readonly IDictionary<string, NotificationPosition> Positions =
            new Dictionary<string, NotificationPosition>
            {
                { "Bottom right", NotificationPosition.BottomRight },
                { "Bottom left", NotificationPosition.BottomLeft },
                { "Top right", NotificationPosition.TopRight },
                { "Top left", NotificationPosition.TopLeft },
            };

        private readonly ILogger logger;

        private readonly ISteamController steamController;

        public OptionsPanelManager(ILogger logger, ISteamController steamController)
        {
            this.logger = logger;
            this.steamController = steamController;
        }

        public void ConfigureOptionsPanel(IStronglyTypedUIHelper uiHelper)
        {
            try
            {
                var appearance = uiHelper.AddGroup("Appearance");

                appearance.AddDropDown(
                    "Popup position",
                    Positions.Keys,
                    ModConfig.Instance.GetSetting<int>(SettingKeys.PopupPosition),
                    PositionChanged);

                var behaviour = uiHelper.AddGroup("Behaviour");
                behaviour.AddCheckBox("Enable achievements",
                    ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableAchievements), AchievementStatusChanged);

                var debugging = uiHelper.AddGroup("Debugging");
                debugging.AddCheckBox("Enable logging", ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableLogging),
                    EnableLoggingChanged);

                logger.Info("OnSettingsUi");
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }

        private void AchievementStatusChanged(bool isEnabled)
        {
            try
            {
                ModConfig.Instance.SaveSetting(SettingKeys.EnableAchievements, isEnabled);

                steamController.UpdateAchievementsStatus();

                logger.Info("Achievement status enabled {0}", isEnabled);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }

        private void EnableLoggingChanged(bool isLoggingEnabled)
        {
            try
            {
                ((SteamyLogger)SteamyLogger.Instance).LoggingEnabled = isLoggingEnabled;

                ModConfig.Instance.SaveSetting(SettingKeys.EnableLogging, isLoggingEnabled);

                logger.Info("Logging enabled {0}", isLoggingEnabled);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }

        private void PositionChanged(int selectedIndex)
        {
            try
            {
                var position = Positions[Positions.Keys.ToList()[selectedIndex]];

                ModConfig.Instance.SaveSetting(SettingKeys.PopupPosition, (int)position);

                steamController.UpdatePopupPosition();

                logger.Info("Position changed to {0}", position);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }
    }
}
