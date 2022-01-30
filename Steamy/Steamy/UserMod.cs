namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Infrastructure;
    using SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection;
    using SexyFishHorse.CitiesSkylines.Infrastructure.UI.Configuration;

    [UsedImplicitly]
    public class UserMod : UserModBase
    {
        public const string ModName = "Steamy";

        public static readonly IServiceProvider Services = new ServiceProvider().AddSteamy();

        public UserMod()
        {
            OptionsPanelManager = Services.GetService<IOptionsPanelManager>();
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
