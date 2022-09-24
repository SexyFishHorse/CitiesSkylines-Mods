namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using System;
    using ColossalFramework.Plugins;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;

    public class SteamyLogger : ILogger
    {
        private readonly ILogger _logger;

        private bool _disposed;

        public SteamyLogger()
        {
            LoggingEnabled = ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableLogging);

            _logger = LogManager.Instance.GetOrCreate(UserMod.ModName);
        }

        public bool LoggingEnabled { get; set; }

        public void Dispose()
        {
            _logger.Dispose();

            _disposed = true;
        }

        public void Error(string message, params object[] args)
        {
            EnsureNotDisposed();

            if (LoggingEnabled)
            {
                _logger.Error(message, args);
            }
        }

        public void Info(string message, params object[] args)
        {
            EnsureNotDisposed();

            if (LoggingEnabled)
            {
                _logger.Info(message, args);
            }
        }

        public void Log(PluginManager.MessageType messageType, string message, params object[] args)
        {
            EnsureNotDisposed();

            if (LoggingEnabled)
            {
                _logger.Log(messageType, message, args);
            }
        }

        public void LogException(Exception exception, string message = null, params object[] args)
        {
            EnsureNotDisposed();

            if (LoggingEnabled)
            {
                _logger.LogException(exception, message, args);
            }
        }

        public void Warn(string message, params object[] args)
        {
            EnsureNotDisposed();

            if (LoggingEnabled)
            {
                _logger.Warn(message, args);
            }
        }

        private void EnsureNotDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }
}
