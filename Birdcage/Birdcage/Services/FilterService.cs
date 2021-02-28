namespace SexyFishHorse.CitiesSkylines.Birdcage.Services
{
    using ColossalFramework.UI;
    using ICities;
    using SexyFishHorse.CitiesSkylines.Birdcage.Wrappers;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using ILogger = SexyFishHorse.CitiesSkylines.Logger.ILogger;

    public class FilterService
    {
        private static readonly HashSet<string> AllowedMessages = new HashSet<string>
        {
            LocaleID.CHIRP_ABANDONED_BUILDINGS,
            LocaleID.CHIRP_COMMERCIAL_DEMAND,
            LocaleID.CHIRP_DEAD_PILING_UP,
            LocaleID.CHIRP_DISASTER,
            LocaleID.CHIRP_FIRE_HAZARD,
            LocaleID.CHIRP_HIGH_CRIME,
            LocaleID.CHIRP_INDUSTRIAL_DEMAND,
            LocaleID.CHIRP_LAUNCH,
            LocaleID.CHIRP_LAUNCH_PREPARATION,
            LocaleID.CHIRP_LOW_HAPPINESS,
            LocaleID.CHIRP_LOW_HEALTH,
            LocaleID.CHIRP_MILESTONE_REACHED,
            LocaleID.CHIRP_NEED_MORE_PARKS,
            LocaleID.CHIRP_NOISEPOLLUTION,
            LocaleID.CHIRP_NO_ELECTRICITY,
            LocaleID.CHIRP_NO_HEALTHCARE,
            LocaleID.CHIRP_NO_PRISONS,
            LocaleID.CHIRP_NO_SCHOOLS,
            LocaleID.CHIRP_NO_WATER,
            LocaleID.CHIRP_POISONED,
            LocaleID.CHIRP_POLLUTION,
            LocaleID.CHIRP_RESIDENTIAL_DEMAND,
            LocaleID.CHIRP_SEWAGE,
            LocaleID.CHIRP_SURVIVORFOUND,
            LocaleID.CHIRP_TRASH_PILING_UP,
            LocaleID.CHIRP_UPGRADE_FESTIVALAREA,
            LocaleID.EDITORCHIRPER_REQUIREMENTS,
            LocaleID.EDITORCHIRPER_REQUIREMENTS_FERTILITY,
            LocaleID.EDITORCHIRPER_REQUIREMENTS_FOREST,
            LocaleID.EDITORCHIRPER_REQUIREMENTS_OIL,
            LocaleID.EDITORCHIRPER_REQUIREMENTS_ORE,
            LocaleID.EDITORCHIRPER_REQUIREMENTS_PLANE,
            LocaleID.EDITORCHIRPER_REQUIREMENTS_RECOMMENDED,
            LocaleID.EDITORCHIRPER_REQUIREMENTS_ROADIN,
            LocaleID.EDITORCHIRPER_REQUIREMENTS_ROADOUT,
            LocaleID.EDITORCHIRPER_REQUIREMENTS_SHIP,
            LocaleID.EDITORCHIRPER_REQUIREMENTS_TRAIN,
            LocaleID.EDITORCHIRPER_REQUIREMENTS_WATER,
            LocaleID.FOOTBALLCHIRP_LOSE,
            LocaleID.FOOTBALLCHIRP_WIN,
        };

        private readonly IChirpPanelWrapper chirpPanel;

        private readonly ILogger logger;

        private readonly IMessageManagerWrapper messageManager;

        private readonly ICollection<IChirperMessage> messagesToRemove = new HashSet<IChirperMessage>();

        public FilterService(IChirpPanelWrapper chirpPanel, ILogger logger, IMessageManagerWrapper messageManager)
        {
            this.chirpPanel = chirpPanel;
            this.logger = logger;
            this.messageManager = messageManager;
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
            logger.Info("New message: " + message.text);

            var citizenMessage = message as CitizenMessage;
            if (citizenMessage == null)
            {
                return;
            }

            logger.Info("Message id: " + citizenMessage.m_messageID);

            if (AllowedMessages.Contains(citizenMessage.m_messageID))
            {
                return;
            }

            logger.Info("Message marked for removal");

            MessagesToRemove.Add(message);
            chirpPanel.RemoveNotificationSound();
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
    }
}
