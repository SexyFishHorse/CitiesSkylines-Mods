namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using Infrastructure.Configuration;

    public static class ModConfig
    {
        private static IConfigurationManager instance;

        public static IConfigurationManager Instance
        {
            get
            {
                return instance ?? (instance = ConfigurationManagerFactory.GetOrCreateConfigurationManager(SteamyUserMod.ModName));
            }
        }
    }
}
