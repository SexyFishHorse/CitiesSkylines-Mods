namespace SexyFishHorse.CitiesSkylines.Birdcage.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using ColossalFramework.UI;
    using ICities;
    using SexyFishHorse.CitiesSkylines.Birdcage.Wrappers;
    using UnityEngine;
    using ILogger = SexyFishHorse.CitiesSkylines.Logger.ILogger;

    public class FilterService
    {
        private readonly IChirpPanelWrapper chirpPanel;

        private readonly HashSet<string> filteredMessages = new HashSet<string>();

        private readonly ILogger logger;

        private readonly IMessageManagerWrapper messageManager;

        private readonly ICollection<IChirperMessage> messagesToRemove = new HashSet<IChirperMessage>();

        public FilterService(IChirpPanelWrapper chirpPanel, ILogger logger, IMessageManagerWrapper messageManager)
        {
            this.chirpPanel = chirpPanel;
            this.logger = logger;
            this.messageManager = messageManager;

            GetChirpsForSetting(SettingKeys.FilterFirstTypeOfServiceBuilt, Chirps.FirstTypeOfServiceBuilt);
            GetChirpsForSetting(SettingKeys.FilterServiceBuilt, Chirps.ServiceBuilt);
            GetChirpsForSetting(SettingKeys.FilterCelebrations, Chirps.Celebrations);
            GetChirpsForSetting(SettingKeys.FilterConcerts, Chirps.Concerts);
            GetChirpsForSetting(SettingKeys.FilterChirpXLaunches, Chirps.ChirpX);
            GetChirpsForSetting(SettingKeys.FilterCityProblems, Chirps.Problems);
            GetChirpsForSetting(SettingKeys.FilterFootballMatches, Chirps.Football);
            GetChirpsForSetting(SettingKeys.FilterPointlessChirps, Chirps.Random);
            GetChirpsForSetting(SettingKeys.FilterVarsitySportsMatches, Chirps.VarsitySports);
            GetChirpsForSetting(SettingKeys.FilterFishingBuildingUnlocked, Chirps.UnlockedFishingBuildings);
        }

        public ICollection<IChirperMessage> MessagesToRemove
        {
            get
            {
                return messagesToRemove;
            }
        }

        public void HandleNewMessage(IChirperMessage message)
        {
            logger.Info("New message: {0}", message.text);

            var citizenMessage = message as CitizenMessage;
            if (citizenMessage == null)
            {
                return;
            }

            logger.Info("Is citizen message: Id: {0} tag: {1}, key: {2}, sender id: {3}, sender name: {4}",
                citizenMessage.m_messageID, citizenMessage.m_tag, citizenMessage.m_keyID, citizenMessage.senderID,
                citizenMessage.senderName);

            if (filteredMessages.Contains(citizenMessage.m_messageID))
            {
                logger.Info("Message marked for removal");

                MessagesToRemove.Add(message);
                chirpPanel.RemoveNotificationSound();
            }
        }

        public void RemovePendingMessages(AudioClip notificationSound)
        {
            if (MessagesToRemove.Any() == false)
            {
                return;
            }

            logger.Info("Removing pending messages");

            foreach (var chirperMessage in MessagesToRemove)
            {
                messageManager.DeleteMessage(chirperMessage);
            }

            chirpPanel.SynchronizeMessages(messagesToRemove.Count);
            chirpPanel.SetNotificationSound(notificationSound);

            MessagesToRemove.Clear();
        }

        public void SetCounter(UILabel counterLabel)
        {
            chirpPanel.SetCounter(counterLabel);
        }

        private void GetChirpsForSetting(string settingKeys, IEnumerable<string> messages)
        {
            if (ModConfig.Get(settingKeys))
            {
                foreach (var message in messages)
                {
                    filteredMessages.Add(message);
                }
            }
        }
    }
}
