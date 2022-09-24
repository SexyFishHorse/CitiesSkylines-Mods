namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ColossalFramework.PlatformServices;
    using Infrastructure.UI;
    using Infrastructure.UI.Configuration;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;

    public class OptionsPanelManager : IOptionsPanelManager
    {
        private static readonly IDictionary<string, NotificationPosition> s_positions =
            new Dictionary<string, NotificationPosition>
            {
                { "Bottom right", NotificationPosition.BottomRight },
                { "Bottom left", NotificationPosition.BottomLeft },
                { "Top right", NotificationPosition.TopRight },
                { "Top left", NotificationPosition.TopLeft },
            };

        private readonly ILogger _logger;

        private readonly ISteamController _steamController;

        public OptionsPanelManager(ILogger logger, ISteamController steamController)
        {
            _logger = logger;
            _steamController = steamController;
        }

        public void ConfigureOptionsPanel(IStronglyTypedUIHelper uiHelper)
        {
            try
            {
                var appearance = uiHelper.AddGroup("Appearance");

                appearance.AddDropDown(
                    "Popup position",
                    s_positions.Keys,
                    ModConfig.Instance.GetSetting<int>(SettingKeys.PopupPosition),
                    PositionChanged);

                var behaviour = uiHelper.AddGroup("Behaviour");
                behaviour.AddCheckBox("Enable achievements",
                    ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableAchievements), AchievementStatusChanged);

                var debugging = uiHelper.AddGroup("Debugging");
                debugging.AddCheckBox("Enable logging", ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableLogging),
                    EnableLoggingChanged);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                throw;
            }
        }

        private void AchievementStatusChanged(bool isEnabled)
        {
            try
            {
                ModConfig.Instance.SaveSetting(SettingKeys.EnableAchievements, isEnabled);

                _steamController.UpdateAchievementsStatus();

                _logger.Info("Achievement status enabled {0}", isEnabled);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                throw;
            }
        }

        private void EnableLoggingChanged(bool isLoggingEnabled)
        {
            try
            {
                ((SteamyLogger)_logger).LoggingEnabled = isLoggingEnabled;

                ModConfig.Instance.SaveSetting(SettingKeys.EnableLogging, isLoggingEnabled);

                _logger.Info("Logging enabled {0}", isLoggingEnabled);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                throw;
            }
        }

        private void PositionChanged(int selectedIndex)
        {
            try
            {
                var position = s_positions[s_positions.Keys.ToList()[selectedIndex]];

                ModConfig.Instance.SaveSetting(SettingKeys.PopupPosition, (int)position);

                _steamController.UpdatePopupPosition();

                _logger.Info("Position changed to {0}", position);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                throw;
            }
        }
    }
}
