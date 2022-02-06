namespace SexyFishHorse.CitiesSkylines.Birdcage.Settings
{
    using System;
    using ColossalFramework.UI;
    using ICities;
    using SexyFishHorse.CitiesSkylines.Birdcage.Services;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;
    using SexyFishHorse.CitiesSkylines.Infrastructure.UI;
    using SexyFishHorse.CitiesSkylines.Infrastructure.UI.Configuration;
    using SexyFishHorse.CitiesSkylines.Infrastructure.UI.Extensions;

    public class OptionsPanelManager : IOptionsPanelManager
    {
        private const string ResetPositionButtonTooltip = "Only available while in game";

        private static UIButton resetPositionButton;

        private static IChirper chirper;

        private readonly FilterService filterService;

        private readonly ILogger logger;

        private readonly PositionService positionService;

        public OptionsPanelManager(ILogger logger, PositionService positionService, FilterService filterService)
        {
            this.logger = logger;
            this.positionService = positionService;
            this.filterService = filterService;
        }

        public static IChirper Chirper
        {
            get { return chirper; }

            set
            {
                chirper = value;

                if (resetPositionButton != null)
                {
                    var enable = chirper != null;

                    resetPositionButton.isEnabled = enable;
                    resetPositionButton.tooltip = enable ? ResetPositionButtonTooltip : null;
                }
            }
        }

        public void ConfigureOptionsPanel(IStronglyTypedUIHelper uiHelper)
        {
            try
            {
                AddAppearanceSettings(uiHelper);
                AddBehaviourSettings(uiHelper);
                AddDebugSettings(uiHelper);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
        }

        private void AddCheckBox(IStronglyTypedUIHelper group, string label, string settingKey, string localeId)
        {
            group.AddCheckBox(
                     label,
                     ModConfig.Get(settingKey),
                     isChecked =>
                     {
                         ModConfig.Save(settingKey, isChecked);
                         filterService.UpdateFilters();
                     })
                 .WithTooltipLocaleId(localeId);
        }

        private void AddBehaviourSettings(IStronglyTypedUIHelper uiHelper)
        {
            var group = uiHelper.AddGroup("Filter chirps");
            group.AddLabel("Mouse over each setting to see an example of each chirp");

            group.AddSpace(25);

            group.AddLabel("Random \"filler\" chirps");
            AddCheckBox(
                group,
                "Citizen chatter",
                SettingKeys.FilterPointlessChirps,
                LocaleID.CHIRP_RANDOM_EXP8);
            AddCheckBox(
                group,
                "Enacted policies and district themes",
                SettingKeys.FilterPoliciesAndThemes,
                LocaleID.CHIRP_ORGANIC_FARMING);
            group.AddSpace();

            group.AddLabel("When you build, unlock or achieve something");
            AddCheckBox(
                group,
                "First time you build  a service building (eg. city's first hospital)",
                SettingKeys.FilterFirstTypeOfServiceBuilt,
                LocaleID.CHIRP_FIRST_FIRE_STATION);
            AddCheckBox(
                group,
                "When subsequent service buildings are built (eg. city's second hospital)",
                SettingKeys.FilterServiceBuilt,
                LocaleID.CHIRP_NEW_FIRE_STATION);
            AddCheckBox(
                group,
                "When you unlock fishery buildings",
                SettingKeys.FilterFishingBuildingUnlocked,
                LocaleID.CHIRP_FISHING_BOAT_HARBOR_04_UNLOCKED);
            AddCheckBox(
                group,
                "Celebrations (high attractiveness, milestone reached etc.)",
                SettingKeys.FilterCelebrations,
                LocaleID.CHIRP_ATTRACTIVE_CITY);
            group.AddSpace();

            group.AddLabel("Citizens reacting to events that happen in your city");
            AddCheckBox(
                group,
                "Varsity sports matches",
                SettingKeys.FilterVarsitySportsMatches,
                LocaleID.VARSITYSPORTSCHIRP_WIN);
            AddCheckBox(group, "Football matches", SettingKeys.FilterFootballMatches, LocaleID.FOOTBALLCHIRP_LOSE);
            AddCheckBox(group, "Concerts", SettingKeys.FilterConcerts, LocaleID.CHIRP_BAND_MOTI);
            AddCheckBox(group, "ChirpX launches", SettingKeys.FilterChirpXLaunches, LocaleID.CHIRP_LAUNCH);
            AddCheckBox(
                group,
                "Toga parties and graduations",
                SettingKeys.FilterTogaPartiesAndGraduations,
                LocaleID.GRADUATIONCHIRP_GENERIC);
            group.AddSpace();

            AddCheckBox(
                group,
                "Actual city problems (high crime, no power etc.)",
                SettingKeys.FilterCityProblems,
                LocaleID.CHIRP_NO_WATER);
            AddCheckBox(
                group,
                "Reactions to disasters",
                SettingKeys.FilterDisasters,
                LocaleID.CHIRP_RANDOM_DISASTERS);

            group.AddSpace();
            AddCheckBox(
                group,
                "Uncategorized chirps (chirps I haven't yet figured out how are triggered)",
                SettingKeys.FilterUncategorized,
                LocaleID.CHIRP_ASSISTIVE_TECHNOLOGIES);

            // foreach (var chirp in Chirps.Uncategorized)
            // {
            //     group.AddTextField(chirp, Locale.Get(chirp), text => { }, text => { })
            //          .AsWideAsGroup(group)
            //          .readOnly = true;
            // }
        }

        private void AddAppearanceSettings(IStronglyTypedUIHelper uiHelper)
        {
            var group = uiHelper.AddGroup("Appearance");
            group.AddCheckBox(
                "Hide Chirper",
                ModConfig.Instance.GetSetting<bool>(SettingKeys.HideChirper),
                ToggleChirper);
            group.AddCheckBox(
                "Make Chirper draggable (hold ctrl + left mouse button)",
                ModConfig.Instance.GetSetting<bool>(SettingKeys.Draggable),
                ToggleDraggable);

            group.AddSpace();
            resetPositionButton = group.AddButton("Reset Chirper position", ResetPosition)
                                       .WithTooltip(ResetPositionButtonTooltip);
            resetPositionButton.isEnabled = false;
        }

        private void AddDebugSettings(IStronglyTypedUIHelper uiHelper)
        {
            var group = uiHelper.AddGroup("Debugging");
            group.AddCheckBox(
                "Enable logging",
                ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableLogging),
                ToggleLogging);
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

        private void ToggleLogging(bool loggingEnabled)
        {
            ((BirdcageLogger)logger).LoggingEnabled = loggingEnabled;
            ModConfig.Instance.SaveSetting(SettingKeys.EnableLogging, loggingEnabled);
        }
    }
}
