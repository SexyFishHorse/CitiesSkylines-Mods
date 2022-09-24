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
        private readonly FieldInfo _newMessageCountFieldInfo;

        private readonly ILogger _logger;

        private ChirpPanel _chirpPanel;

        private UITextComponent _counter;

        public ChirpPanelWrapper(ILogger logger)
        {
            _logger = logger;

            _newMessageCountFieldInfo = typeof(ChirpPanel).GetField("m_NewMessageCount", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private ChirpPanel Panel
        {
            get
            {
                return _chirpPanel
                    ? _chirpPanel
                    : _chirpPanel = ChirpPanel.instance;
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
            _counter = counterLabel;
        }

        public void SetNotificationSound(AudioClip notificationSound)
        {
            Panel.m_NotificationSound = notificationSound;
        }

        public void SynchronizeMessages(int numberOfRemovedMessages)
        {
            if (Panel == null)
            {
                _logger.Error("Panel is null");

                return;
            }

            Panel.SynchronizeMessages();

            if (_counter == null)
            {
                return;
            }

            var numberOfNewMessages = GetNumberOfNewMessages() - numberOfRemovedMessages;
            _counter.text = numberOfNewMessages.ToString();

            if (numberOfNewMessages < 1)
            {
                _counter.isVisible = false;
            }
        }

        private int GetNumberOfNewMessages()
        {
            if (_newMessageCountFieldInfo == null)
            {
                return 0;
            }

            return Convert.ToInt32(_newMessageCountFieldInfo.GetValue(Panel));
        }
    }
}
