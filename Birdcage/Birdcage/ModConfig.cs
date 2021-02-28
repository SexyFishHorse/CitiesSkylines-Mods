namespace SexyFishHorse.CitiesSkylines.Birdcage
{
    using Infrastructure.Configuration;

    public static class ModConfig
    {
        private static IConfigurationManager instance;

        public static IConfigurationManager Instance
        {
            get
            {
                return instance ?? (instance = ConfigurationManagerFactory.GetOrCreateConfigurationManager(BirdcageUserMod.ModName));
            }
        }
    }
}
