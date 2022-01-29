namespace SexyFishHorse.CitiesSkylines.Infrastructure.Logging.Outputs
{
    using System;
    using System.Linq;
    using ColossalFramework.Plugins;
    using UnityEngine;

    public class DebugOutput : LogOutputBase
    {
        public override void Dispose()
        {
        }

        public override void LogException(Exception ex, string message = null, params object[] args)
        {
            if(string.IsNullOrEmpty(message) == false)
            {
                Debug.LogErrorFormat(message, args ?? Enumerable.Empty<object>());
            }

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
