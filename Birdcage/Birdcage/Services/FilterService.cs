namespace SexyFishHorse.CitiesSkylines.Birdcage.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using ColossalFramework.UI;
    using ICities;
    using SexyFishHorse.CitiesSkylines.Birdcage.Helpers;
    using SexyFishHorse.CitiesSkylines.Birdcage.Settings;
    using SexyFishHorse.CitiesSkylines.Birdcage.Wrappers;
    using UnityEngine;
    using ILogger = SexyFishHorse.CitiesSkylines.Infrastructure.Logging.ILogger;

    public class FilterService
    {
        private readonly IChirpPanelWrapper _chirpPanel;

        private readonly HashSet<string> _filteredMessages = new HashSet<string>();

        private readonly ILogger _logger;

        private readonly IMessageManagerWrapper _messageManager;

        private readonly ICollection<IChirperMessage> _messagesToRemove = new HashSet<IChirperMessage>();

        public FilterService(IChirpPanelWrapper chirpPanel, ILogger logger, IMessageManagerWrapper messageManager)
        {
            _chirpPanel = chirpPanel;
            _logger = logger;
            _messageManager = messageManager;

            UpdateFilters();
        }

        public void UpdateFilters()
        {
            _messagesToRemove.Clear();
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
            GetChirpsForSetting(SettingKeys.FilterTogaPartiesAndGraduations, Chirps.TogaPartiesAndGraduations);
            GetChirpsForSetting(SettingKeys.FilterPoliciesAndThemes, Chirps.PoliciesAndThemes);
            GetChirpsForSetting(SettingKeys.FilterDisasters, Chirps.Disasters);
            GetChirpsForSetting(SettingKeys.FilterUncategorized, Chirps.Uncategorized);
        }

        public ICollection<IChirperMessage> MessagesToRemove
        {
            get
            {
                return _messagesToRemove;
            }
        }

        public void HandleNewMessage(IChirperMessage message)
        {
            _logger.Info("New message: {0}", message.text);

            var citizenMessage = message as CitizenMessage;
            if (citizenMessage == null)
            {
                return;
            }

            _logger.Info("Is citizen message: Id: {0} tag: {1}, key: {2}, sender id: {3}, sender name: {4}",
                citizenMessage.m_messageID, citizenMessage.m_tag, citizenMessage.m_keyID, citizenMessage.senderID,
                citizenMessage.senderName);

            if (_filteredMessages.Contains(citizenMessage.m_messageID))
            {
                _logger.Info("Message marked for removal");

                MessagesToRemove.Add(message);
                _chirpPanel.RemoveNotificationSound();
            }
        }

        public void RemovePendingMessages(AudioClip notificationSound)
        {
            if (MessagesToRemove.Any() == false)
            {
                return;
            }

            _logger.Info("Removing pending messages");

            foreach (var chirperMessage in MessagesToRemove)
            {
                _messageManager.DeleteMessage(chirperMessage);
            }

            _chirpPanel.SynchronizeMessages(_messagesToRemove.Count);
            _chirpPanel.SetNotificationSound(notificationSound);

            MessagesToRemove.Clear();
        }

        public void SetCounter(UILabel counterLabel)
        {
            _chirpPanel.SetCounter(counterLabel);
        }

        private void GetChirpsForSetting(string settingKeys, IEnumerable<string> messages)
        {
            if (ModConfig.Get(settingKeys))
            {
                foreach (var message in messages)
                {
                    _filteredMessages.Add(message);
                }
            }
        }
    }
}
