namespace SexyFishHorse.CitiesSkylines.Birdcage.Wrappers
{
    using System;
    using System.Reflection;
    using ColossalFramework.UI;
    using UnityEngine;

    public class ChirpPanelWrapper : IChirpPanelWrapper
    {
        private readonly FieldInfo newMessageCountFieldInfo;

        private ChirpPanel chirpPanel;

        private UITextComponent counter;

        public ChirpPanelWrapper()
        {
            newMessageCountFieldInfo = typeof(ChirpPanel).GetField("m_NewMessageCount", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private ChirpPanel Panel
        {
            get
            {
                return chirpPanel ?? (chirpPanel = ChirpPanel.instance);
            }
        }

        public void CollapsePanel()
        {
            ChirperUtils.CollapseChirperInstantly();
        }

        public void RemoveNotificationSound()
        {
            Panel.m_NotificationSound = null;
        }

        public void SetCounter(UITextComponent counterLabel)
        {
            counter = counterLabel;
        }

        public void SetNotificationSound(AudioClip notificationSound)
        {
            Panel.m_NotificationSound = notificationSound;
        }

        public void SynchronizeMessages(int numberOfRemovedMessages)
        {
            if (Panel == null)
            {
                BirdcageLogger.Instance.Error("Panel is null");

                return;
            }

            Panel.SynchronizeMessages();

            if (counter == null)
            {
                return;
            }

            var numberOfNewMessages = GetNumberOfNewMessages() - numberOfRemovedMessages;
            counter.text = numberOfNewMessages.ToString();

            if (numberOfNewMessages < 1)
            {
                counter.isVisible = false;
            }
        }

        private int GetNumberOfNewMessages()
        {
            if (newMessageCountFieldInfo == null)
            {
                return 0;
            }

            return Convert.ToInt32(newMessageCountFieldInfo.GetValue(Panel));
        }
    }
}
