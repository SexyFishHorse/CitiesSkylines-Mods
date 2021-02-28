namespace SexyFishHorse.CitiesSkylines.Logger.Models
{
    using System;
    using ColossalFramework.Plugins;

    public class ConsoleOutput : LogOutputBase
    {
        public override void Dispose()
        {
        }

        protected override void LogMessage(PluginManager.MessageType messageType, string message)
        {
            message = string.Format("[{0}]: {1}", DateTime.Now, message);

            DebugOutputPanel.AddMessage(messageType, message);
        }

        public override void LogException(Exception ex)
        {
            LogMessage(PluginManager.MessageType.Error, ex.Message);
            LogMessage(PluginManager.MessageType.Error, ex.StackTrace);
        }
    }
}
