namespace SexyFishHorse.CitiesSkylines.Ragnarok.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using ICities;
    using SexyFishHorse.CitiesSkylines.Logger;
    using SexyFishHorse.CitiesSkylines.Ragnarok.Configuration;
    using SexyFishHorse.CitiesSkylines.Ragnarok.Model;

    public static class GenericDisasterServices
    {
        private const int SecondsBeforePausing = 5;

        public static IEnumerable<DisasterInfo> GetDisasterInfos()
        {
            var numPrefabs = PrefabCollection<DisasterInfo>.PrefabCount();
            for (var i = 0; i < numPrefabs; i++)
            {
                var disasterInfo = PrefabCollection<DisasterInfo>.GetPrefab((uint)i);
                if (disasterInfo == null || disasterInfo.name == DisasterInfoNames.GenericFlood)
                {
                    continue;
                }

                yield return disasterInfo;
            }
        }

        public static void HandlePauseOnDisaster(ILogger logger, DisasterType disasterType)
        {
            logger.Info("Handle pause: " + disasterType);
            var autoPauseDisasters = new HashSet<DisasterType>
            {
                DisasterType.Earthquake,
                DisasterType.MeteorStrike,
                DisasterType.Sinkhole,
                DisasterType.Tornado,
                DisasterType.Tsunami,
                DisasterType.ThunderStorm,
            };

            if (ModConfig.Instance.GetSetting<bool>(SettingKeys.PauseOnDisasterStart) && autoPauseDisasters.Contains(disasterType))
            {
                new Thread(
                    () =>
                    {
                        try
                        {
                            var pauseStart = DateTime.UtcNow + TimeSpan.FromSeconds(SecondsBeforePausing);

                            while (DateTime.UtcNow < pauseStart)
                            {
                            }

                            logger.Info("Pausing game");
                            SimulationManager.instance.SimulationPaused = true;
                        }
                        catch (Exception ex)
                        {
                            logger.LogException(ex);

                            throw;
                        }
                    }).Start();
            }
        }

        public static void UpdateAutoFollowDisaster(ILogger logger)
        {
            try
            {
                if (DisasterManager.exists)
                {
                    DisasterManager.instance.m_disableAutomaticFollow =
                        !ModConfig.Instance.GetSetting<bool>(SettingKeys.DisableAutofocusDisaster);

                    logger.Info("disableAutomaticFollow is {0}", DisasterManager.instance.m_disableAutomaticFollow);
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }
    }
}
