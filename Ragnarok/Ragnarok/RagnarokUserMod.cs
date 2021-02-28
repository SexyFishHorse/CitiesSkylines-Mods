namespace SexyFishHorse.CitiesSkylines.Ragnarok
{
    using System;
    using Configuration;
    using Infrastructure;
    using JetBrains.Annotations;
    using Logger;
    using Logging;

    [UsedImplicitly]
    public class RagnarokUserMod : UserModBase
    {
        public const string ModName = "Ragnarok";

        private readonly ILogger logger;

        public RagnarokUserMod()
        {
            try
            {
                logger = RagnarokLogger.Instance;
                logger.Info("Ragnarok created");

                ModConfig.Instance.Logger = logger;
                MigrateOldSettings();

                OptionsPanelManager = new OptionsPanelManager(logger);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }

        public override string Description
        {
            get
            {
                return "More disaster controls";
            }
        }

        public override string Name
        {
            get
            {
                return ModName;
            }
        }

        private void MigrateOldSettings()
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
    }
}
