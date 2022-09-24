namespace SexyFishHorse.CitiesSkylines.Ragnarok.Configuration
{
    using Infrastructure.Configuration;

    public static class ModConfig
    {
        private static IConfigurationManager s_Instance;

        public static IConfigurationManager Instance
        {
            get
            {
                return s_Instance ?? (s_Instance = ConfigurationManager.Create(UserMod.ModName));
            }
        }
    }
}
