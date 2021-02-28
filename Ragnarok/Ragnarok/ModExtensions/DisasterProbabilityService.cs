namespace SexyFishHorse.CitiesSkylines.Ragnarok.ModExtensions
{
    using Configuration;
    using ICities;
    using Infrastructure.Extensions;
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Ragnarok.Services;

    [UsedImplicitly]
    public class DisasterProbabilityService : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            if (!mode.IsGameOrScenario())
            {
                return;
            }

            var disasterInfos = GenericDisasterServices.GetDisasterInfos();

            foreach (var disasterInfo in disasterInfos)
            {
                var settingKey = string.Format("{0}Probability", disasterInfo.name.Replace(" ", string.Empty));

                if (ModConfig.Instance.HasSetting(settingKey))
                {
                    disasterInfo.m_finalRandomProbability = ModConfig.Instance.GetSetting<int>(settingKey);
                }
            }
        }
    }
}
