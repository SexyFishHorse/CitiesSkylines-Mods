namespace SexyFishHorse.CitiesSkylines.Birdcage.Wrappers
{
    using ColossalFramework.UI;
    using UnityEngine;

    public interface IChirpPanelWrapper
    {
        void CollapsePanel();

        void RemoveNotificationSound();

        void SetCounter(UITextComponent counterLabel);

        void SetNotificationSound(AudioClip notificationSound);

        void SynchronizeMessages(int numberOfRemovedMessages);
    }
}
