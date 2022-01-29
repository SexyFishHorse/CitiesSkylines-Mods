namespace SexyFishHorse.CitiesSkylines.Birdcage
{
    using SexyFishHorse.CitiesSkylines.Infrastructure.Configuration;

    public static class ModConfig
    {
        private static IConfigurationManager instance;

        public static IConfigurationManager Instance
        {
            get
            {
                return instance ??
                       (instance = ConfigurationManagerFactory.GetOrCreateConfigurationManager(UserMod.ModName));
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
