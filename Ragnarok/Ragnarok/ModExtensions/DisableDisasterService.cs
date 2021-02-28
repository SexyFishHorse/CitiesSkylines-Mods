namespace SexyFishHorse.CitiesSkylines.Ragnarok.ModExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using ICities;
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Extensions;
    using SexyFishHorse.CitiesSkylines.Logger;
    using SexyFishHorse.CitiesSkylines.Ragnarok.Configuration;
    using SexyFishHorse.CitiesSkylines.Ragnarok.Logging;
    using SexyFishHorse.CitiesSkylines.Ragnarok.Services;

    [UsedImplicitly]
    public class DisableDisasterService : IDisasterBase, ILoadingExtension
    {
        private static readonly Dictionary<DisasterType, DisasterSettingKeys> DisasterSettingDictionary =
            new Dictionary<DisasterType, DisasterSettingKeys>
            {
                { DisasterType.Earthquake, SettingKeys.Earthquakes },
                { DisasterType.ForestFire, SettingKeys.ForestFires },
                { DisasterType.MeteorStrike, SettingKeys.Meteors },
                { DisasterType.Sinkhole, SettingKeys.Sinkholes },
                { DisasterType.StructureCollapse, SettingKeys.StructureCollapses },
                { DisasterType.StructureFire, SettingKeys.StructureFires },
                { DisasterType.ThunderStorm, SettingKeys.Thunderstorms },
                { DisasterType.Tornado, SettingKeys.Tornadoes },
                { DisasterType.Tsunami, SettingKeys.Tsunamis }
            };

        private readonly ILogger logger;

        private FieldInfo conversionField;

        private DisasterWrapper disasterWrapper;

        public DisableDisasterService()
        {
            logger = RagnarokLogger.Instance;
            logger.Info("DisableDisasterService created");
        }

        public override void OnCreated(IDisaster disaster)
        {
            try
            {
                logger.Info("DDS: OnCreated {0}", disaster);

                disasterWrapper = (DisasterWrapper)disaster;

                conversionField = disasterWrapper.GetType()
                                                 .GetField(
                                                     "m_DisasterTypeToInfoConversion",
                                                     BindingFlags.NonPublic | BindingFlags.Instance);

                SetConversionTable();
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }

        public void OnCreated(ILoading loading)
        {
        }

        public override void OnDisasterActivated(ushort disasterId)
        {
            try
            {
                var info = disasterWrapper.GetDisasterSettings(disasterId);

                logger.Info(
                    "DDS: OnDisasterActivated. Id: {0}, Name: {1}, Type: {2}, Intensity: {3}",
                    disasterId,
                    info.name,
                    info.type,
                    info.intensity);

                if (TryDisableDisaster(disasterId, info))
                {
                    return;
                }

                GenericDisasterServices.HandlePauseOnDisaster(logger, info.type);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }

        public override void OnDisasterCreated(ushort disasterId)
        {
            SetConversionTable();

            var info = disasterWrapper.GetDisasterSettings(disasterId);

            logger.Info(
                "DDS: OnDisasterCreated. Id: {0}, Name: {1}, Type: {2}, Intensity: {3}",
                disasterId,
                info.name,
                info.type,
                info.intensity);

            try
            {
                var disasterInfo = disasterWrapper.GetDisasterSettings(disasterId);
                logger.Info(
                    "DDS: Created disaster type {0} with name {1} and intensity {2}",
                    disasterInfo.type,
                    disasterInfo.name,
                    disasterInfo.intensity);

                // TODO: Figure out why intensity is always 55 here.
                if (TryDisableDisaster(disasterId, disasterInfo))
                {
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }

        public override void OnDisasterStarted(ushort disasterId)
        {
            SetConversionTable();

            var info = disasterWrapper.GetDisasterSettings(disasterId);

            logger.Info(
                "DDS: OnDisasterStarted. Id: {0}, Name: {1}, Type: {2}, Intensity: {3}",
                disasterId,
                info.name,
                info.type,
                info.intensity);

            var settingKeys = GetSettingKeysForDisasterType(info.type);
            if (settingKeys == null)
            {
                logger.Info("DDS: No setting keys found");
                return;
            }

            if (ModConfig.Instance.GetSetting<bool>(settingKeys.Disable))
            {
                logger.Info("DDS: Deactivating disaster");
                disasterWrapper.EndDisaster(disasterId);
            }

            var maxIntensity = ModConfig.Instance.GetSetting<byte>(settingKeys.MaxIntensity);
            if (maxIntensity > 0)
            {
                logger.Info("DDS: Disable when over intensity {0}", maxIntensity);
                if (info.intensity > maxIntensity)
                {
                    logger.Info("DDS: Deactivating disaster");
                    disasterWrapper.EndDisaster(disasterId);
                }
            }
        }

        public void OnLevelLoaded(LoadMode mode)
        {
            try
            {
                if (!mode.IsGameOrScenario())
                {
                    return;
                }

                if (mode.IsScenario())
                {
                    if (ModConfig.Instance.GetSetting<bool>(SettingKeys.DisableScenarioDisasters))
                    {
                        DisasterManager.instance.ClearAll();
                    }
                }

                BuildingManager.instance.m_firesDisabled = ModConfig.Instance.GetSetting<bool>(SettingKeys.DisableNonDisasterFires);

                GenericDisasterServices.UpdateAutoFollowDisaster(logger);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }

        public void OnLevelUnloading()
        {
        }

        private static DisasterSettingKeys GetSettingKeysForDisasterType(DisasterType type)
        {
            return DisasterSettingDictionary.ContainsKey(type) ? DisasterSettingDictionary[type] : null;
        }

        private void SetConversionTable()
        {
            var fieldValue = (Dictionary<DisasterType, DisasterInfo>)conversionField.GetValue(disasterWrapper);

            if (fieldValue == null || !fieldValue.Any() || fieldValue.Any(x => x.Value == null))
            {
                logger.Info("DDS: Rebuilding conversion table");
                var conversionDictionary = new Dictionary<DisasterType, DisasterInfo>();
                conversionDictionary[DisasterType.Earthquake] = DisasterManager.FindDisasterInfo<EarthquakeAI>();
                conversionDictionary[DisasterType.ForestFire] = DisasterManager.FindDisasterInfo<ForestFireAI>();
                conversionDictionary[DisasterType.MeteorStrike] = DisasterManager.FindDisasterInfo<MeteorStrikeAI>();
                conversionDictionary[DisasterType.ThunderStorm] = DisasterManager.FindDisasterInfo<ThunderStormAI>();
                conversionDictionary[DisasterType.Tornado] = DisasterManager.FindDisasterInfo<TornadoAI>();
                conversionDictionary[DisasterType.Tsunami] = DisasterManager.FindDisasterInfo<TsunamiAI>();
                conversionDictionary[DisasterType.StructureCollapse] = DisasterManager.FindDisasterInfo<StructureCollapseAI>();
                conversionDictionary[DisasterType.StructureFire] = DisasterManager.FindDisasterInfo<StructureFireAI>();
                conversionDictionary[DisasterType.Sinkhole] = DisasterManager.FindDisasterInfo<SinkholeAI>();

                if (conversionDictionary.Any(x => x.Value == null))
                {
                    logger.Info("DDS: Contains null values");
                }

                conversionField.SetValue(disasterWrapper, conversionDictionary);
            }
        }

        private bool TryDisableDisaster(ushort disasterId, DisasterSettings disasterInfo)
        {
            var settingKeys = GetSettingKeysForDisasterType(disasterInfo.type);

            if (settingKeys == null)
            {
                logger.Info("DDS: No setting keys found");
                return true;
            }

            if (ModConfig.Instance.GetSetting<bool>(settingKeys.Disable))
            {
                logger.Info("DDS: Deactivating disaster");
                disasterWrapper.EndDisaster(disasterId);
            }

            return false;
        }
    }
}
