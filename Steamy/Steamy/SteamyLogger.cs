﻿namespace SexyFishHorse.CitiesSkylines.Steamy
{
    using System;
    using ColossalFramework.Plugins;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;

    public class SteamyLogger : ILogger
    {
        private readonly ILogger logger;

        private bool disposed;

        public SteamyLogger()
        {
            LoggingEnabled = ModConfig.Instance.GetSetting<bool>(SettingKeys.EnableLogging);

            logger = LogManager.Instance.GetOrCreate(UserMod.ModName);
        }

        public bool LoggingEnabled { get; set; }

        public void Dispose()
        {
            logger.Dispose();

            disposed = true;
        }

        public void Error(string message, params object[] args)
        {
            EnsureNotDisposed();

            if (LoggingEnabled)
            {
                logger.Error(message, args);
            }
        }

        public void Info(string message, params object[] args)
        {
            EnsureNotDisposed();

            if (LoggingEnabled)
            {
                logger.Info(message, args);
            }
        }

        public void Log(PluginManager.MessageType messageType, string message, params object[] args)
        {
            EnsureNotDisposed();

            if (LoggingEnabled)
            {
                logger.Log(messageType, message, args);
            }
        }

        public void LogException(Exception exception, string message = null, params object[] args)
        {
            EnsureNotDisposed();

            if (LoggingEnabled)
            {
                logger.LogException(exception, message, args);
            }
        }

        public void Warn(string message, params object[] args)
        {
            EnsureNotDisposed();

            if (LoggingEnabled)
            {
                logger.Warn(message, args);
            }
        }

        private void EnsureNotDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }
}
