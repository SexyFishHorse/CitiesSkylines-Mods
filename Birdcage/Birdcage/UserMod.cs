namespace SexyFishHorse.CitiesSkylines.Birdcage
{
    using SexyFishHorse.CitiesSkylines.Infrastructure;
    using SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection;
    using SexyFishHorse.CitiesSkylines.Infrastructure.UI.Configuration;

    public class UserMod : UserModBase
    {
        public const string ModName = "Birdcage";

        public static readonly IServiceProvider Services = new ServiceProvider().AddBirdcage();

        public UserMod()
        {
            OptionsPanelManager = Services.GetService<IOptionsPanelManager>();
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
