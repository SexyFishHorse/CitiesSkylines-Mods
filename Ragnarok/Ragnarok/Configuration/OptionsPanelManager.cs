namespace SexyFishHorse.CitiesSkylines.Ragnarok.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using ColossalFramework.UI;
    using Infrastructure.UI;
    using Infrastructure.UI.Configuration;
    using Infrastructure.UI.Extensions;
    using Logging;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;
    using SexyFishHorse.CitiesSkylines.Ragnarok.Services;

    public class OptionsPanelManager : IOptionsPanelManager
    {
        private const byte MaximumIntensityValue = byte.MaxValue;

        private const byte MinimumIntensityValue = byte.MinValue;

        private static readonly string[] s_autoEvacuateValues =
        {
            "Disabled",
            "Enabled - manual release",
            "Enabled - auto release"
        };

        private readonly ILogger _logger;

        private readonly IDictionary<string, UISlider> _maxIntensitySliders = new Dictionary<string, UISlider>();

        public OptionsPanelManager(ILogger logger)
        {
            _logger = logger;

            logger.Info("OptionsPanelManager created");

            MigrateOldSettings();
        }



        private static void MigrateOldSettings()
        {
            ModConfig.Instance.MigrateKey<int>("AutoEvacuateEarthquake", SettingKeys.Earthquakes.AutoEvacuate);
            ModConfig.Instance.MigrateType<int, byte>(SettingKeys.Earthquakes.MaxIntensity, Convert.ToByte);
            ModConfig.Instance.MigrateType<int, byte>(SettingKeys.ForestFires.MaxIntensity, Convert.ToByte);
            ModConfig.Instance.MigrateType<int, byte>(SettingKeys.Meteors.MaxIntensity, Convert.ToByte);
            ModConfig.Instance.MigrateType<int, byte>(SettingKeys.Sinkholes.MaxIntensity, Convert.ToByte);
            ModConfig.Instance.MigrateType<int, byte>(SettingKeys.StructureCollapses.MaxIntensity, Convert.ToByte);
            ModConfig.Instance.MigrateType<int, byte>(SettingKeys.StructureFires.MaxIntensity, Convert.ToByte);
            ModConfig.Instance.MigrateType<int, byte>(SettingKeys.Thunderstorms.MaxIntensity, Convert.ToByte);
            ModConfig.Instance.MigrateType<int, byte>(SettingKeys.Tornadoes.MaxIntensity, Convert.ToByte);
            ModConfig.Instance.MigrateType<int, byte>(SettingKeys.Tsunamis.MaxIntensity, Convert.ToByte);
        }

        public void ConfigureOptionsPanel(IStronglyTypedUIHelper uiHelper)
        {
            try
            {
                ConfigureGeneralGroup(uiHelper);
                ConfigureDisableDisastersGroup(uiHelper);
                ConfigureMaxIntensityGroup(uiHelper);
                ConfigureAutoEvacuateGroup(uiHelper);
                ConfigureProbabilityGroup(uiHelper);

                var debuggingGroup = uiHelper.AddGroup("Debugging");
                debuggingGroup.AddCheckBox(
                    "Enable logging",
                    ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableLogging),
                    isChecked =>
                    {
                        ModConfig.Instance.SaveSetting(SettingKeys.EnableLogging, isChecked);
                        RagnarokLogger.Enabled = isChecked;
                    });
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                throw;
            }
        }

        private void AddAutoEvacuateBehaviourDropDown(IStronglyTypedUIHelper autoEvacuateGroup, string disasterName, string settingKey)
        {
            var dropDown = autoEvacuateGroup.AddDropDown(
                disasterName,
                s_autoEvacuateValues,
                ModConfig.Instance.GetSetting<int>(settingKey),
                sel => SaveSetting(settingKey, sel));

            dropDown.width = (int)Math.Round(dropDown.width * 1.4f);
        }

        private void AddEnabledDisasterCheckbox(IStronglyTypedUIHelper uiGroup, string label, string settingKey)
        {
            uiGroup.AddCheckBox(
                label,
                ModConfig.Instance.GetSetting<bool>(settingKey),
                isChecked => SaveSetting(settingKey, isChecked));
        }

        private void AddMaxIntensitySlider(IStronglyTypedUIHelper group, string disasterName, string impactSettingKey)
        {
            var setting = ModConfig.Instance.GetSetting<byte>(impactSettingKey);

            var slider =
                group.AddSlider(
                    disasterName,
                    MinimumIntensityValue,
                    MaximumIntensityValue,
                    1,
                    setting,
                    val =>
                    {
                        ModConfig.Instance.SaveSetting(impactSettingKey, (byte)val);
                        UpdateMaxIntensityLabel(impactSettingKey, disasterName, val);
                    });
            slider.width = (int)Math.Round(slider.width * 1.4);
            var label = slider.GetLabel();
            label.width = (int)Math.Round(label.width * 1.4);

            if (_maxIntensitySliders.ContainsKey(impactSettingKey))
            {
                _maxIntensitySliders[impactSettingKey] = slider;
            }
            else
            {
                _maxIntensitySliders.Add(impactSettingKey, slider);
            }

            UpdateMaxIntensityLabel(impactSettingKey, disasterName, setting);
        }

        private static void AddProbabilitySlider(IStronglyTypedUIHelper disasterGroup, string settingKey, DisasterInfo disasterInfo)
        {
            var probability = disasterInfo.m_finalRandomProbability;
            if (ModConfig.Instance.HasSetting(settingKey))
            {
                probability = ModConfig.Instance.GetSetting<int>(settingKey);
            }

            var labelText = disasterInfo.name;
            var slider = disasterGroup.AddSlider(
                labelText,
                0,
                1000,
                1,
                probability,
                newProbability =>
                {
                    disasterInfo.m_finalRandomProbability = (int)newProbability;
                    ModConfig.Instance.SaveSetting(settingKey, (int)newProbability);
                });

            slider.width = (int)Math.Round(slider.width * 1.5f);
            var label = slider.GetLabel();
            label.width = (int)Math.Round(label.width * 1.5f);
        }

        private void ConfigureAutoEvacuateGroup(IStronglyTypedUIHelper uiHelper)
        {
            var group = uiHelper.AddGroup("Auto evacuate");

            foreach (var disaster in SettingKeys.DisasterSettingKeys)
            {
                AddAutoEvacuateBehaviourDropDown(group, disaster.Key, disaster.Value.AutoEvacuate);
            }
        }

        private void ConfigureDisableDisastersGroup(IStronglyTypedUIHelper uiHelper)
        {
            var group = uiHelper.AddGroup("Disable disasters");

            group.AddCheckBox(
                "Fires not caused by disasters",
                ModConfig.Instance.GetSetting<bool>(SettingKeys.DisableNonDisasterFires),
                OnDisableNonDisasterFiresChanged);
            group.AddSpace(10);

            foreach (var disableDisaster in SettingKeys.DisasterSettingKeys)
            {
                AddEnabledDisasterCheckbox(group, disableDisaster.Key, disableDisaster.Value.Disable);
            }

            group.AddSpace(10);

            AddEnabledDisasterCheckbox(group, "Disable scenario disasters (really?)", SettingKeys.DisableScenarioDisasters);
        }

        private void ConfigureGeneralGroup(IStronglyTypedUIHelper uiHelper)
        {
            var generalGroup = uiHelper.AddGroup("General");

            generalGroup.AddCheckBox(
                "Auto focus on disaster",
                ModConfig.Instance.GetSetting<bool>(SettingKeys.DisableAutofocusDisaster),
                OnAutoFocusDisasterChanged);

            generalGroup.AddCheckBox(
                "Pause on disaster start",
                ModConfig.Instance.GetSetting<bool>(SettingKeys.PauseOnDisasterStart),
                isChecked => SaveSetting(SettingKeys.PauseOnDisasterStart, isChecked));
        }

        private void ConfigureMaxIntensityGroup(IStronglyTypedUIHelper uiHelper)
        {
            var group = uiHelper.AddGroup("Disable disasters over a given intensity");
            group.AddLabel("Use the sliders to set the max intensity for a given disaster.\nSet to zero (0) to allow all intensities.");

            foreach (var disaster in SettingKeys.DisasterSettingKeys)
            {
                AddMaxIntensitySlider(group, disaster.Key, disaster.Value.MaxIntensity);
            }
        }

        private void ConfigureProbabilityGroup(IStronglyTypedUIHelper uiHelper)
        {
            var probabilityGroup = uiHelper.AddGroup("Disaster probabilities");
            if (!GenericDisasterServices.GetDisasterInfos().Any())
            {
                probabilityGroup.AddLabel("You need to be in an active game or scenario to change these settings.");

                return;
            }

            probabilityGroup.AddLabel(
                "The sliders below indicate the risk of a certain type of disaster spawning.\n\n" +
                "These are in relation to each other meaning that if two sliders are set equally\n" +
                "then they have an equal chance of occurring.");
            var disasterInfos = GenericDisasterServices.GetDisasterInfos().ToList();
            foreach (var disasterInfo in disasterInfos)
            {
                var settingKey = string.Format("{0}Probability", disasterInfo.name.Replace(" ", string.Empty));

                AddProbabilitySlider(probabilityGroup, settingKey, disasterInfo);
            }
        }

        private void OnAutoFocusDisasterChanged(bool isChecked)
        {
            SaveSetting(SettingKeys.DisableAutofocusDisaster, isChecked);

            GenericDisasterServices.UpdateAutoFollowDisaster(_logger);
        }

        private void OnDisableNonDisasterFiresChanged(bool isChecked)
        {
            SaveSetting(SettingKeys.DisableNonDisasterFires, isChecked);

            if (BuildingManager.exists)
            {
                BuildingManager.instance.m_firesDisabled = isChecked;
            }
        }

        private void SaveSetting(string settingKey, object value)
        {
            _logger.Info("Saving setting {0} with value {1}", settingKey, value);

            ModConfig.Instance.SaveSetting(settingKey, value);
        }

        private void UpdateMaxIntensityLabel(string impactSettingKey, string disasterName, float value)
        {
            string intensity;
            if (value < 1)
            {
                intensity = "Allow all";
            }
            else
            {
                intensity = (value / 10.0f).ToString("f1", CultureInfo.CurrentUICulture);
            }

            _maxIntensitySliders[impactSettingKey].SetLabelText("{0} ({1})", disasterName, intensity);
        }
    }
}
