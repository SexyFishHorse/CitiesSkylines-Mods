namespace SexyFishHorse.CitiesSkylines.Birdcage
{
    using ICities;
    using SexyFishHorse.CitiesSkylines.Birdcage.Settings;

    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnLevelUnloading()
        {
            OptionsPanelManager.Chirper = null;
        }
    }
}
