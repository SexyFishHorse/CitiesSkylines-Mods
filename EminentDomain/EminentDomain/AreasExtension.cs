namespace EminentDomain
{
    using ICities;

    public class AreasExtension : AreasExtensionBase
    {
        public override void OnCreated(IAreas areas)
        {
            areas.maxAreaCount = 25;
        }
    }
}
