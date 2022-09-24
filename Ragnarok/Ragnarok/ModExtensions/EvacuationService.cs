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
        private readonly ILogger _logger;

        private readonly HashSet<ushort> _manualReleaseDisasters = new HashSet<ushort>();

        private DisasterWrapper _disasterWrapper;

        private FieldInfo _evacuatingField;

        private WarningPhasePanel _phasePanel;

        public EvacuationService()
        {
            _logger = RagnarokLogger.Instance;

            _logger.Info("EvacuationService created");
        }

        public override void OnCreated(IDisaster disasters)
        {
            _logger.Info("EvacuationService: OnCreated");
            _disasterWrapper = (DisasterWrapper)disasters;
        }

        public override void OnDisasterDeactivated(ushort disasterId)
        {
            try
            {
                var disasterInfo = _disasterWrapper.GetDisasterSettings(disasterId);

                _logger.Info(
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
                    _logger.Info("Not evacuating. Clear list of active manual release disasters");
                    _manualReleaseDisasters.Clear();
                    return;
                }

                if (ShouldAutoRelease(disasterInfo.type) && !_manualReleaseDisasters.Any())
                {
                    _logger.Info("Auto releasing citizens");
                    DisasterManager.instance.EvacuateAll(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                throw;
            }
        }

        public override void OnDisasterDetected(ushort disasterId)
        {
            try
            {
                var disasterInfo = _disasterWrapper.GetDisasterSettings(disasterId);

                _logger.Info(
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
                    _logger.Info("Is auto-evacuate disaster");
                    if (!IsEvacuating())
                    {
                        _logger.Info("Starting evacuation");
                        DisasterManager.instance.EvacuateAll(false);
                    }
                    else
                    {
                        _logger.Info("Already evacuating");
                    }

                    if (ShouldManualRelease(disasterInfo.type))
                    {
                        _logger.Info("Should be manually released");
                        _manualReleaseDisasters.Add(disasterId);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                throw;
            }
        }

        private void FindPhasePanel()
        {
            _logger.Info("ES: Find Phase Panel");

            if (_phasePanel != null)
            {
                return;
            }

            _phasePanel = UnityObject.FindObjectOfType<WarningPhasePanel>();
            _evacuatingField = _phasePanel.GetType().GetField("m_isEvacuating", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private bool IsEvacuating()
        {
            FindPhasePanel();

            var isEvacuating = (bool)_evacuatingField.GetValue(_phasePanel);

            _logger.Info("Is evacuating: " + isEvacuating);

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
