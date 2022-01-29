namespace SexyFishHorse.CitiesSkylines.Birdcage
{
    using ICities;

    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnLevelUnloading()
        {
            OptionsPanelManager.Chirper = null;
        }
    }
}
