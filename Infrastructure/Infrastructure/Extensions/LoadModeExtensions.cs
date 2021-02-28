namespace SexyFishHorse.CitiesSkylines.Infrastructure.Extensions
{
    using System.Collections.Generic;
    using ICities;

    public static class LoadModeExtensions
    {
        private static readonly ICollection<SimulationManager.UpdateMode> GameModes = new HashSet<SimulationManager.UpdateMode>
        {
            SimulationManager.UpdateMode.LoadGame,
            SimulationManager.UpdateMode.NewGameFromMap,
            SimulationManager.UpdateMode.NewGameFromScenario,
            SimulationManager.UpdateMode.LoadScenario
        };

        public static bool IsGameOrScenario(this LoadMode mode)
        {
            var updateMode = (SimulationManager.UpdateMode)mode;

            return GameModes.Contains(updateMode);
        }

        public static bool IsScenario(this LoadMode mode)
        {
            var updateMode = (SimulationManager.UpdateMode)mode;

            return (updateMode == SimulationManager.UpdateMode.NewGameFromScenario) ||
                   (updateMode == SimulationManager.UpdateMode.LoadScenario);
        }
    }
}
