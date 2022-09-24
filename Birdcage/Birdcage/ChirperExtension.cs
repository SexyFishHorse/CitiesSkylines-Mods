namespace SexyFishHorse.CitiesSkylines.Birdcage
{
    using System;
    using System.Linq;
    using ColossalFramework.UI;
    using ICities;
    using SexyFishHorse.CitiesSkylines.Birdcage.Helpers;
    using SexyFishHorse.CitiesSkylines.Birdcage.Services;
    using SexyFishHorse.CitiesSkylines.Birdcage.Settings;
    using UnityEngine;
    using ILogger = SexyFishHorse.CitiesSkylines.Infrastructure.Logging.ILogger;
    using Object = UnityEngine.Object;

    public class ChirperExtension : IChirperExtension
    {
        private readonly FilterService _filterService;

        private readonly InputService _inputService;

        private readonly ILogger _logger;

        private readonly PositionService _positionService;

        private UIButton _chirpButton;

        private IChirper _chirperWrapper;

        private bool _dragging;

        private bool _initialized;

        public ChirperExtension()
        {
            _logger = UserMod.Services.GetService<ILogger>();
            _filterService = UserMod.Services.GetService<FilterService>();
            _inputService = UserMod.Services.GetService<InputService>();
            _positionService = UserMod.Services.GetService<PositionService>();
        }

        public AudioClip NotificationSound { get; set; }

        public void OnCreated(IChirper chirper)
        {
            try
            {
                _chirperWrapper = chirper;
                OptionsPanelManager.Chirper = chirper;

                _chirpButton = Object.FindObjectsOfType<UIButton>().FirstOrDefault(x => x.name == "Zone");

                var counterLabel = Object.FindObjectsOfType<UILabel>().FirstOrDefault(x => x.name == "Counter");
                _filterService.SetCounter(counterLabel);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

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
                    _filterService.HandleNewMessage(message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

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

                _inputService.Update();
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
                    _filterService.RemovePendingMessages(NotificationSound);
                }

                if (ModConfig.Get(SettingKeys.Draggable))
                {
                    ProcessDragging();
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                throw;
            }
        }

        // TODO: Move this to OnCreated when CO fixes the initial state position mismatch
        private void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            _positionService.Chirper = _chirperWrapper;
            _positionService.DefaultPosition = _chirperWrapper.builtinChirperPosition;
            _positionService.UiView = ChirpPanel.instance.component.GetUIView();

            NotificationSound = ChirpPanel.instance.m_NotificationSound;

            if (ModConfig.Get(SettingKeys.Draggable))
            {
                _chirperWrapper.SetBuiltinChirperFree(true);

                if (ModConfig.Get<int>(SettingKeys.ChirperPositionX) > 0)
                {
                    var chirperX = ModConfig.Instance.GetSetting<int>(SettingKeys.ChirperPositionX);
                    var chirperY = ModConfig.Instance.GetSetting<int>(SettingKeys.ChirperPositionY);
                    var chirperPosition = new Vector2(chirperX, chirperY);

                    _positionService.UpdateChirperPosition(chirperPosition);
                }
            }

            var hideChirper = ModConfig.Instance.GetSetting<bool>(SettingKeys.HideChirper);
            _chirperWrapper.ShowBuiltinChirper(!hideChirper);

            _initialized = true;
        }

        private void ProcessDragging()
        {
            if (_inputService.PrimaryMouseButtonDownState && _inputService.AnyControlDown)
            {
                if (_dragging)
                {
                    _positionService.Dragging();
                }
                else
                {
                    if (_positionService.IsMouseOnChirper())
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
            if (_dragging)
            {
                return;
            }

            _logger.Info("Start dragging");

            ChirperUtils.CollapseChirperInstantly();
            _dragging = true;
            if (_chirpButton != null)
            {
                _chirpButton.isEnabled = false;
            }
        }

        private void StopDragging()
        {
            if (_dragging == false)
            {
                return;
            }

            _logger.Info("Stop dragging");

            _dragging = false;
            _positionService.UpdateChirperAnchor();
            _positionService.SaveChirperPosition();

            if (_chirpButton != null)
            {
                _chirpButton.isEnabled = true;
            }
        }
    }
}
