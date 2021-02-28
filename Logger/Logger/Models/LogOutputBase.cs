namespace SexyFishHorse.CitiesSkylines.Logger.Models
{
    using System;
    using System.Linq;
    using ColossalFramework.Plugins;

    public abstract class LogOutputBase : IDisposable
    {
        public abstract void Dispose();

        public void Log(PluginManager.MessageType messageType, string message, object[] args)
        {
            if (args.Any())
            {
                message = string.Format(message, args);
            }

            LogMessage(messageType, message);
        }

        protected abstract void LogMessage(PluginManager.MessageType messageType, string message);

        public abstract void LogException(Exception ex);
    }
}
