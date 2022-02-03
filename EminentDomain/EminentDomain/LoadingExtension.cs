namespace EminentDomain
{
    using ICities;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Extensions;

    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode.IsGameOrScenario())
            {
                GameAreaManager.instance.m_maxAreaCount = 25;
            }
        }
    }
}
