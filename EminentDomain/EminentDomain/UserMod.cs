namespace EminentDomain
{
    using ICities;

    public class UserMod : IUserMod
    {
        public const string ModName = "Eminent Domain";

        public string Description
        {
            get { return "Unlock all 25 tiles on the last milestone"; }
        }

        public string Name
        {
            get { return ModName; }
        }
    }
}
