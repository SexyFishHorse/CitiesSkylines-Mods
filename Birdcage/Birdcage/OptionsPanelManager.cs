namespace SexyFishHorse.CitiesSkylines.Birdcage
{
    using System;
    using ICities;
    using SexyFishHorse.CitiesSkylines.Birdcage.Services;
    using SexyFishHorse.CitiesSkylines.Infrastructure.UI;
    using SexyFishHorse.CitiesSkylines.Infrastructure.UI.Configuration;
    using SexyFishHorse.CitiesSkylines.Logger;

    public class OptionsPanelManager : IOptionsPanelManager
    {
        private readonly ILogger logger;

        private readonly PositionService positionService;

        public OptionsPanelManager(ILogger logger, PositionService positionService)
        {
            this.logger = logger;
            this.positionService = positionService;
        }

        public IChirper Chirper { get; set; }

        public void ConfigureOptionsPanel(IStronglyTypedUIHelper uiHelper)
        {
            try
            {
                var appearanceGroup = uiHelper.AddGroup("Appearance");
                var behaviourGroup = uiHelper.AddGroup("Behaviour");
                var debugGroup = uiHelper.AddGroup("Debugging");

                appearanceGroup.AddCheckBox("Hide chirper", ModConfig.Instance.GetSetting<bool>(SettingKeys.HideChirper), ToggleChirper);
                appearanceGroup.AddCheckBox(
                    "Make Chirper draggable (hold ctrl + left mouse button)",
                    ModConfig.Instance.GetSetting<bool>(SettingKeys.Draggable),
                    ToggleDraggable);
                appearanceGroup.AddSpace(10);
                appearanceGroup.AddButton("Reset Chirper position", ResetPosition);

                behaviourGroup.AddCheckBox(
                    "Filter non-important messages",
                    ModConfig.Instance.GetSetting<bool>(SettingKeys.FilterMessages),
                    ToggleFilter);

                debugGroup.AddCheckBox("Enable logging", ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableLogging), ToggleLogging);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
        }

        private void ResetPosition()
        {
            if (Chirper == null)
            {
                return;
            }

            positionService.ResetPosition();

            positionService.SaveChirperPosition();
        }

        private void ToggleChirper(bool hideChirper)
        {
            try
            {
                ModConfig.Instance.SaveSetting(SettingKeys.HideChirper, hideChirper);

                if (ChirpPanel.instance != null)
                {
                    ChirpPanel.instance.gameObject.SetActive(!hideChirper);
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
        }

        private void ToggleDraggable(bool isDraggable)
        {
            if (Chirper != null)
            {
                Chirper.SetBuiltinChirperFree(true);
            }

            ModConfig.Instance.SaveSetting(SettingKeys.Draggable, isDraggable);
        }

        private void ToggleFilter(bool shouldFilter)
        {
            try
            {
                ModConfig.Instance.SaveSetting(SettingKeys.FilterMessages, shouldFilter);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
        }

        private void ToggleLogging(bool loggingEnabled)
        {
            ((BirdcageLogger)BirdcageLogger.Instance).LoggingEnabled = loggingEnabled;
            ModConfig.Instance.SaveSetting(SettingKeys.EnableLogging, loggingEnabled);
        }
    }
}
