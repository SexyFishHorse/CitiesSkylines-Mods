namespace SexyFishHorse.CitiesSkylines.Birdcage
{
    using System;
    using System.Linq;
    using ColossalFramework.UI;
    using ICities;
    using SexyFishHorse.CitiesSkylines.Birdcage.Services;
    using SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection;
    using UnityEngine;
    using ILogger = SexyFishHorse.CitiesSkylines.Logger.ILogger;
    using Object = UnityEngine.Object;

    public class ChirperExtension : IChirperExtension
    {
        private readonly FilterService filterService;

        private readonly InputService inputService;

        private readonly ILogger logger;

        private readonly PositionService positionService;

        private UIButton chirpButton;

        private IChirper chirperWrapper;

        private bool dragging;

        private bool initialized;

        public ChirperExtension()
        {
            logger = ServiceProvider.Instance.GetService<ILogger>();
            filterService = ServiceProvider.Instance.GetService<FilterService>();
            inputService = ServiceProvider.Instance.GetService<InputService>();
            positionService = ServiceProvider.Instance.GetService<PositionService>();
        }

        public AudioClip NotificationSound { get; set; }

        public void OnCreated(IChirper chirper)
        {
            try
            {
                chirperWrapper = chirper;
                OptionsPanelManager.Chirper = chirper;

                chirpButton = Object.FindObjectsOfType<UIButton>().FirstOrDefault(x => x.name == "Zone");

                var counterLabel = Object.FindObjectsOfType<UILabel>().FirstOrDefault(x => x.name == "Counter");
                filterService.SetCounter(counterLabel);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }

        public void OnMessagesUpdated()
        {
        }

        public void OnNewMessage(IChirperMessage message)
        {
            try
            {
                if (ModConfig.FilterMessages)
                {
                    filterService.HandleNewMessage(message);
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }

        public void OnReleased()
        {
        }

        public void OnUpdate()
        {
            try
            {
                Initialize();

                inputService.Update();
                if (ChirpPanel.instance == null)
                {
                    return;
                }

                if (ModConfig.Get(SettingKeys.HideChirper))
                {
                    return;
                }

                if (ModConfig.FilterMessages)
                {
                    filterService.RemovePendingMessages(NotificationSound);
                }

                if (ModConfig.Get(SettingKeys.Draggable))
                {
                    ProcessDragging();
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);

                throw;
            }
        }

        // TODO: Move this to OnCreated when CO fixes the initial state position mismatch
        private void Initialize()
        {
            if (initialized)
            {
                return;
            }

            positionService.Chirper = chirperWrapper;
            positionService.DefaultPosition = chirperWrapper.builtinChirperPosition;
            positionService.UiView = ChirpPanel.instance.component.GetUIView();

            NotificationSound = ChirpPanel.instance.m_NotificationSound;

            if (ModConfig.Get(SettingKeys.Draggable))
            {
                chirperWrapper.SetBuiltinChirperFree(true);

                if (ModConfig.Get<int>(SettingKeys.ChirperPositionX) > 0)
                {
                    var chirperX = ModConfig.Instance.GetSetting<int>(SettingKeys.ChirperPositionX);
                    var chirperY = ModConfig.Instance.GetSetting<int>(SettingKeys.ChirperPositionY);
                    var chirperPosition = new Vector2(chirperX, chirperY);

                    positionService.UpdateChirperPosition(chirperPosition);
                }
            }

            var hideChirper = ModConfig.Instance.GetSetting<bool>(SettingKeys.HideChirper);
            chirperWrapper.ShowBuiltinChirper(!hideChirper);

            initialized = true;
        }

        private void ProcessDragging()
        {
            if (inputService.PrimaryMouseButtonDownState && inputService.AnyControlDown)
            {
                if (dragging)
                {
                    positionService.Dragging();
                }
                else
                {
                    if (positionService.IsMouseOnChirper())
                    {
                        StartDragging();
                    }
                }
            }
            else
            {
                StopDragging();
            }
        }

        private void StartDragging()
        {
            if (dragging)
            {
                return;
            }

            logger.Info("Start dragging");

            ChirperUtils.CollapseChirperInstantly();
            dragging = true;
            if (chirpButton != null)
            {
                chirpButton.isEnabled = false;
            }
        }

        private void StopDragging()
        {
            if (dragging == false)
            {
                return;
            }

            logger.Info("Stop dragging");

            dragging = false;
            positionService.UpdateChirperAnchor();
            positionService.SaveChirperPosition();

            if (chirpButton != null)
            {
                chirpButton.isEnabled = true;
            }
        }
    }
}
