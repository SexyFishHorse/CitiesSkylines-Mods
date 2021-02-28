namespace SexyFishHorse.CitiesSkylines.Steamy.Adapters
{
    using ColossalFramework;
    using SexyFishHorse.CitiesSkylines.Logger;

    public class SimulationManagerAdapter : ISimulationManagerAdapter
    {
        private readonly ILogger logger;

        public SimulationManagerAdapter(ILogger logger)
        {
            this.logger = logger;
        }

        public void SetAchievementsEnabled(bool enableAchievements)
        {
            var disableAchievements = enableAchievements ? SimulationMetaData.MetaBool.False : SimulationMetaData.MetaBool.True;

            if (Singleton<SimulationManager>.exists && Singleton<SimulationManager>.instance.m_metaData != null)
            {
                Singleton<SimulationManager>.instance.m_metaData.m_disableAchievements = disableAchievements;
                logger.Info("Changed achievements disabled to {0}", disableAchievements);
            }
            else
            {
                logger.Warn("Simulation manager or metadata not available (This warning can be safely ignored if you're in the main menu)");
            }
        }
    }
}
