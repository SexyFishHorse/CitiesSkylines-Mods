namespace SexyFishHorse.CitiesSkylines.Birdcage.Wrappers
{
    using ICities;

    public class MessageManagerWrapper : IMessageManagerWrapper
    {
        public void DeleteMessage(IChirperMessage chirperMessage)
        {
            MessageManager.instance.DeleteMessage(chirperMessage);
        }
    }
}
