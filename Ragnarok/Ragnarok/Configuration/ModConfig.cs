namespace SexyFishHorse.CitiesSkylines.Ragnarok.Configuration
{
    using Infrastructure.Configuration;

    public static class ModConfig
    {
        private static IConfigurationManager instance;

        public static IConfigurationManager Instance
        {
            get
            {
                return instance ?? (instance = ConfigurationManager.Create(UserMod.ModName));
            }
        }
    }
}
