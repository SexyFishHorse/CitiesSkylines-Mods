namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using Infrastructure;
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Steamy.Adapters;

    [UsedImplicitly]
    public class UserMod : UserModBase
    {
        public const string ModName = "Steamy";

        public UserMod()
        {
            OptionsPanelManager = new OptionsPanelManager(
                SteamyLogger.Instance,
                new SteamController(
                    new PlatformServiceAdapter(SteamyLogger.Instance),
                    new SimulationManagerAdapter(SteamyLogger.Instance)));
        }

        public override string Description
        {
            get
            {
                return "Configure how Steam integrates with the game";
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
