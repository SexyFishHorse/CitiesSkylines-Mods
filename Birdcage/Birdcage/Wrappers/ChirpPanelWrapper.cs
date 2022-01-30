namespace SexyFishHorse.CitiesSkylines.Birdcage.Wrappers
{
    using System;
    using System.Reflection;
    using ColossalFramework.UI;
    using SexyFishHorse.CitiesSkylines.Birdcage.Helpers;
    using UnityEngine;
    using ILogger = SexyFishHorse.CitiesSkylines.Infrastructure.Logging.ILogger;

    public class ChirpPanelWrapper : IChirpPanelWrapper
    {
        private readonly FieldInfo newMessageCountFieldInfo;

        private readonly ILogger logger;

        private ChirpPanel chirpPanel;

        private UITextComponent counter;

        public ChirpPanelWrapper(ILogger logger)
        {
            this.logger = logger;

            newMessageCountFieldInfo = typeof(ChirpPanel).GetField("m_NewMessageCount", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private ChirpPanel Panel
        {
            get
            {
                return chirpPanel
                    ? chirpPanel
                    : chirpPanel = ChirpPanel.instance;
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
                logger.Error("Panel is null");

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
