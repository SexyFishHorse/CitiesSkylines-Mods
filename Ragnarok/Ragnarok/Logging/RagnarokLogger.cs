namespace SexyFishHorse.CitiesSkylines.Ragnarok.Logging
{
    using System;
    using ColossalFramework.Plugins;
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;
    using SexyFishHorse.CitiesSkylines.Ragnarok.Configuration;

    public class RagnarokLogger : ILogger
    {
        private static ILogger s_InternalLogger;

        private readonly ILogger _logger;

        private RagnarokLogger()
        {
            try
            {
                _logger = LogManager.Instance.GetOrCreate(UserMod.ModName);
                Enabled = ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableLogging);
            }
            catch (Exception ex)
            {
                DebugOutputPanel.AddMessage(
                    PluginManager.MessageType.Error, "[Ragnarok][RagnarokLogger]: " + ex.Message);
            }
        }

        public static bool Enabled { get; set; }

        public static ILogger Instance
        {
            get
            {
                return s_InternalLogger ?? (s_InternalLogger = new RagnarokLogger());
            }
        }

        public void Dispose()
        {
            _logger.Dispose();
        }

        [StringFormatMethod("message")]
        public void Error(string message, params object[] args)
        {
            try
            {
                _logger.Error(message, args);
            }
            catch (Exception ex)
            {
                DebugOutputPanel.AddMessage(
                    PluginManager.MessageType.Error, "[Ragnarok][RagnarokLogger.Error] ERROR: " + ex.Message);
            }
        }

        public void Info(string message, params object[] args)
        {
            if (!Enabled)
            {
                return;
            }

            try
            {
                _logger.Info(message, args);
            }
            catch (Exception ex)
            {
                DebugOutputPanel.AddMessage(
                    PluginManager.MessageType.Error, "[Ragnarok][RagnarokLogger.Info] ERROR: " + ex.Message);
            }
        }

        [StringFormatMethod("message")]
        public void Log(PluginManager.MessageType messageType, string message, params object[] args)
        {
            if (!Enabled && (messageType != PluginManager.MessageType.Error))
            {
                return;
            }

            try
            {
                _logger.Log(messageType, message, args);
            }
            catch (Exception ex)
            {
                DebugOutputPanel.AddMessage(
                    PluginManager.MessageType.Error, "[Ragnarok][RagnarokLogger.Log] ERROR: " + ex.Message);
            }
        }

        public void LogException(Exception exception, string message = null, params object[] args)
        {
            try
            {
                _logger.LogException(exception, message, args);
            }
            catch (Exception ex)
            {
                DebugOutputPanel.AddMessage(
                    PluginManager.MessageType.Error, "[Ragnarok][RagnarokLogger.LogException] ERROR: " + ex.Message);
            }
        }

        [StringFormatMethod("message")]
        public void Warn(string message, params object[] args)
        {
            if (!Enabled)
            {
                return;
            }

            try
            {
                _logger.Warn(message, args);
            }
            catch (Exception ex)
            {
                DebugOutputPanel.AddMessage(
                    PluginManager.MessageType.Error, "[Ragnarok][RagnarokLogger.Warn] ERROR: " + ex.Message);
            }
        }
    }
}
