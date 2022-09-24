namespace SexyFishHorse.CitiesSkylines.Birdcage.Settings
{
    using SexyFishHorse.CitiesSkylines.Infrastructure.Configuration;

    public static class ModConfig
    {
        private static IConfigurationManager s_Instance;

        public static IConfigurationManager Instance
        {
            get
            {
                return s_Instance ??
                       (s_Instance = ConfigurationManagerFactory.GetOrCreateConfigurationManager(UserMod.ModName));
            }
        }

        public static bool FilterMessages
        {
            get
            {
                return Get(SettingKeys.FilterFirstTypeOfServiceBuilt)
                       || Get(SettingKeys.FilterServiceBuilt)
                       || Get(SettingKeys.FilterCelebrations)
                       || Get(SettingKeys.FilterConcerts)
                       || Get(SettingKeys.FilterChirpXLaunches)
                       || Get(SettingKeys.FilterFootballMatches)
                       || Get(SettingKeys.FilterCityProblems)
                       || Get(SettingKeys.FilterPointlessChirps)
                       || Get(SettingKeys.FilterFishingBuildingUnlocked)
                       || Get(SettingKeys.FilterVarsitySportsMatches);
            }
        }

        public static bool Get(string settingKey)
        {
            return Instance.GetSetting<bool>(settingKey);
        }

        public static T Get<T>(string settingKey)
        {
            return Instance.GetSetting<T>(settingKey);
        }

        public static void Save(string settingKey, object value)
        {
            Instance.SaveSetting(settingKey, value);
        }
    }
}
