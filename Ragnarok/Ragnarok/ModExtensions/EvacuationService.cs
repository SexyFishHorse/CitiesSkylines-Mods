namespace SexyFishHorse.CitiesSkylines.Ragnarok.ModExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Configuration;
    using ICities;
    using JetBrains.Annotations;
    using Logging;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;
    using UnityObject = UnityEngine.Object;

    [UsedImplicitly]
    public class EvacuationService : IDisasterBase
    {
        private readonly ILogger logger;

        private readonly HashSet<ushort> manualReleaseDisasters = new HashSet<ushort>();

        private DisasterWrapper disasterWrapper;

        private FieldInfo evacuatingField;

        private WarningPhasePanel phasePanel;

        public EvacuationService()
        {
            logger = RagnarokLogger.Instance;

            logger.Info("EvacuationService created");
        }

        public override void OnCreated(IDisaster disasters)
        {
            logger.Info("EvacuationService: OnCreated");
            disasterWrapper = (DisasterWrapper)disasters;
        }

        public override void OnDisasterDeactivated(ushort disasterId)
        {
            try
            {
                var disasterInfo = disasterWrapper.GetDisasterSettings(disasterId);

                logger.Info(
                    "EvacuationService.OnDisasterDeactivated. Id: {0}, Name: {1}, Type: {2}, Intensity: {3}",
                    disasterId,
                    disasterInfo.name,
                    disasterInfo.type,
                    disasterInfo.intensity);

                if (disasterInfo.type == DisasterType.Empty)
                {
                    return;
                }

                if (!IsEvacuating())
                {
                    logger.Info("Not evacuating. Clear list of active manual release disasters");
                    manualReleaseDisasters.Clear();
                    return;
                }

                if (ShouldAutoRelease(disasterInfo.type) && !manualReleaseDisasters.Any())
                {
                    logger.Info("Auto releasing citizens");
                    DisasterManager.instance.EvacuateAll(true);
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }

        public override void OnDisasterDetected(ushort disasterId)
        {
            try
            {
                var disasterInfo = disasterWrapper.GetDisasterSettings(disasterId);

                logger.Info(
                    "OnDisasterDetected. Id: {0}, Name: {1}, Type: {2}, Intensity: {3}",
                    disasterId,
                    disasterInfo.name,
                    disasterInfo.type,
                    disasterInfo.intensity);

                if (disasterInfo.type == DisasterType.Empty)
                {
                    return;
                }

                if (ShouldAutoEvacuate(disasterInfo.type))
                {
                    logger.Info("Is auto-evacuate disaster");
                    if (!IsEvacuating())
                    {
                        logger.Info("Starting evacuation");
                        DisasterManager.instance.EvacuateAll(false);
                    }
                    else
                    {
                        logger.Info("Already evacuating");
                    }

                    if (ShouldManualRelease(disasterInfo.type))
                    {
                        logger.Info("Should be manually released");
                        manualReleaseDisasters.Add(disasterId);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }

        private void FindPhasePanel()
        {
            logger.Info("ES: Find Phase Panel");

            if (phasePanel != null)
            {
                return;
            }

            phasePanel = UnityObject.FindObjectOfType<WarningPhasePanel>();
            evacuatingField = phasePanel.GetType().GetField("m_isEvacuating", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private bool IsEvacuating()
        {
            FindPhasePanel();

            var isEvacuating = (bool)evacuatingField.GetValue(phasePanel);

            logger.Info("Is evacuating: " + isEvacuating);

            return isEvacuating;
        }

        private bool ShouldAutoEvacuate(DisasterType disasterType)
        {
            var settingKey = SettingKeys.AutoEvacuateSettingKeyMapping.Single(x => x.Key == disasterType).Value;

            return ModConfig.Instance.GetSetting<int>(settingKey) > 0;
        }

        private bool ShouldAutoRelease(DisasterType disasterType)
        {
            var settingKey = SettingKeys.AutoEvacuateSettingKeyMapping.Single(x => x.Key == disasterType).Value;

            return ModConfig.Instance.GetSetting<int>(settingKey) > 1;
        }

        private bool ShouldManualRelease(DisasterType disasterType)
        {
            var settingKey = SettingKeys.AutoEvacuateSettingKeyMapping.Single(x => x.Key == disasterType).Value;

            return ModConfig.Instance.GetSetting<int>(settingKey) == 1;
        }
    }
}
