namespace SexyFishHorse.CitiesSkylines.Logger.Models
{
    using System;
    using ColossalFramework.Plugins;
    using UnityEngine;

    public class DebugOutput : LogOutputBase
    {
        public override void Dispose()
        {
        }

        public override void LogException(Exception ex)
        {
            Debug.LogException(ex);
        }

        protected override void LogMessage(PluginManager.MessageType messageType, string message)
        {
            switch (messageType)
            {
                case PluginManager.MessageType.Error:
                    Debug.LogError(message);
                    break;
                case PluginManager.MessageType.Warning:
                    Debug.LogWarning(message);
                    break;
                case PluginManager.MessageType.Message:
                    Debug.Log(message);
                    break;
            }
        }
    }
}
