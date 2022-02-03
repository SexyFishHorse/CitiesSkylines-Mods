namespace EminentDomain
{
    using SexyFishHorse.CitiesSkylines.Infrastructure;

    public class UserMod : UserModBase
    {
        public const string ModName = "Eminent Domain";

        public override string Description
        {
            get { return "Unlock all 25 tiles on the last milestone"; }
        }

        public override string Name
        {
            get { return ModName; }
        }
    }
}
