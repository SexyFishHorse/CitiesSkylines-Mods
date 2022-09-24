namespace SexyFishHorse.CitiesSkylines.Steamy.Adapters
{
    using ColossalFramework;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;

    public class SimulationManagerAdapter : ISimulationManagerAdapter
    {
        private readonly ILogger _logger;

        public SimulationManagerAdapter(ILogger logger)
        {
            _logger = logger;
        }

        public void SetAchievementsEnabled(bool enableAchievements)
        {
            var disableAchievements = enableAchievements ? SimulationMetaData.MetaBool.False : SimulationMetaData.MetaBool.True;

            if (Singleton<SimulationManager>.exists && Singleton<SimulationManager>.instance.m_metaData != null)
            {
                Singleton<SimulationManager>.instance.m_metaData.m_disableAchievements = disableAchievements;
                _logger.Info("Changed achievements disabled to {0}", disableAchievements);
            }
            else
            {
                _logger.Warn("Simulation manager or metadata not available (This warning can be safely ignored if you're in the main menu)");
            }
        }
    }
}
