namespace SexyFishHorse.CitiesSkylines.Birdcage
{
    using SexyFishHorse.CitiesSkylines.Infrastructure;
    using SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection;

    public class UserMod : UserModWithOptionsBase
    {
        public const string ModName = "Birdcage";

        public static readonly IServiceProvider Services = new ServiceProvider().AddBirdcage();

        public UserMod() : base(ModName, "More Chirper controls", Services)
        {
        }
    }
}
