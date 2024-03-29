﻿namespace SexyFishHorse.CitiesSkylines.Infrastructure.Logging.Outputs
{
    using System;
    using ColossalFramework.Plugins;

    public class DebugPanelOutput : LogOutputBase
    {
        public override void Dispose()
        {
        }

        protected override void LogMessage(PluginManager.MessageType messageType, string message)
        {
            message = string.Format("[{0}]: {1}", DateTime.Now, message);

            DebugOutputPanel.AddMessage(messageType, message);
        }

        public override void LogException(Exception ex, string message = null, params object[] args)
        {
            if (string.IsNullOrEmpty(message) == false)
            {
                LogMessage(PluginManager.MessageType.Error, string.Format(message, args));
            }

            LogMessage(PluginManager.MessageType.Error, ex.Message);
            LogMessage(PluginManager.MessageType.Error, ex.StackTrace);
        }
    }
}
