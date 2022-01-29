namespace SexyFishHorse.CitiesSkylines.Birdcage
{
    using SexyFishHorse.CitiesSkylines.Birdcage.Services;
    using SexyFishHorse.CitiesSkylines.Infrastructure;
    using UnityObject = UnityEngine.Object;

    public class UserMod : UserModBase
    {
        public const string ModName = "Birdcage";

        public UserMod()
        {
            OptionsPanelManager = new OptionsPanelManager(BirdcageLogger.Instance, PositionService.Instance);
        }

        public override string Description
        {
            get
            {
                return "More Chirper controls";
            }
        }

        public override string Name
        {
            get
            {
                return ModName;
            }
        }
    }
}
