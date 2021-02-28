namespace SexyFishHorse.CitiesSkylines.Birdcage
{
    using System;
    using System.Linq;
    using ColossalFramework.UI;
    using ICities;
    using SexyFishHorse.CitiesSkylines.Birdcage.Services;
    using SexyFishHorse.CitiesSkylines.Birdcage.Wrappers;
    using SexyFishHorse.CitiesSkylines.Infrastructure;
    using UnityEngine;
    using ILogger = SexyFishHorse.CitiesSkylines.Logger.ILogger;
    using UnityObject = UnityEngine.Object;

    public class BirdcageUserMod : UserModBase, IChirperExtension
    {
        public const string ModName = "Birdcage";

        private readonly FilterService filterService;

        private readonly InputService inputService;

        private readonly ILogger logger;

        private readonly PositionService positionService;

        private UIButton chirpButton;

        private IChirper chirperWrapper;

        private bool dragging;

        private bool initialized;

        public BirdcageUserMod()
        {
            logger = BirdcageLogger.Instance;

            filterService = new FilterService(new ChirpPanelWrapper(), logger, new MessageManagerWrapper());
            inputService = new InputService();
            positionService = new PositionService();

            OptionsPanelManager = new OptionsPanelManager(logger, positionService);
        }

        public override string Description
        {
            get
            {
                return "More Chirper controls";
            }
        }

        public override string Name
        {
            get
            {
                return ModName;
            }
        }

        public AudioClip NotificationSound { get; set; }

        public void OnCreated(IChirper chirper)
        {
            try
            {
                chirperWrapper = chirper;
                ((OptionsPanelManager)OptionsPanelManager).Chirper = chirper;
                chirpButton = UnityObject.FindObjectsOfType<UIButton>().FirstOrDefault(x => x.name == "Zone");

                var counterLabel = UnityObject.FindObjectsOfType<UILabel>().FirstOrDefault(x => x.name == "Counter");
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
                if (ModConfig.Instance.GetSetting<bool>(SettingKeys.FilterMessages))
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

                if (ModConfig.Instance.GetSetting<bool>(SettingKeys.HideChirper))
                {
                    return;
                }

                if (ModConfig.Instance.GetSetting<bool>(SettingKeys.FilterMessages))
                {
                    filterService.RemovePendingMessages(NotificationSound);
                }

                if (ModConfig.Instance.GetSetting<bool>(SettingKeys.Draggable))
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

            if (ModConfig.Instance.GetSetting<bool>(SettingKeys.Draggable))
            {
                chirperWrapper.SetBuiltinChirperFree(true);

                if (ModConfig.Instance.GetSetting<int>(SettingKeys.ChirperPositionX) > 0)
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
