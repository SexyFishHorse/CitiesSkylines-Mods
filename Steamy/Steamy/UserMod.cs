namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Infrastructure;
    using SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection;

    [UsedImplicitly]
    public class UserMod : UserModWithOptionsBase
    {
        public const string ModName = "Steamy";

        public static readonly IServiceProvider Services = new ServiceProvider().AddSteamy();

        public UserMod() : base(ModName, "Configure how Steam works", Services)
        {
        }
    }
}
