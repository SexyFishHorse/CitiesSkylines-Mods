namespace SexyFishHorse.CitiesSkylines.Ragnarok
{
    using Infrastructure;
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection;
    using IServiceProvider = SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection.IServiceProvider;

    [UsedImplicitly]
    public class UserMod : UserModWithOptionsBase
    {
        public const string ModName = "Ragnarok";

        public static readonly IServiceProvider Services = new ServiceProvider().AddRagnarok();

        public UserMod() : base(ModName, "More disaster controls", Services)
        {
        }
    }
}
