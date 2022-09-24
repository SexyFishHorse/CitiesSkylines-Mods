namespace SexyFishHorse.CitiesSkylines.Infrastructure.Configuration
{
    using System.Collections.Generic;
    using JetBrains.Annotations;

    [PublicAPI]
    public static class ConfigurationManagerFactory
    {
        private static readonly IDictionary<string, IConfigurationManager> s_managers =
            new Dictionary<string, IConfigurationManager>();

        [PublicAPI]
        public static IConfigurationManager GetOrCreateConfigurationManager(string modName)
        {
            if (s_managers.ContainsKey(modName) == false)
            {
                s_managers.Add(modName, ConfigurationManager.Create(modName));
            }

            return s_managers[modName];
        }
    }
}
