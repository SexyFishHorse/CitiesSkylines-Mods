namespace SexyFishHorse.CitiesSkylines.Logger.UnitTest.Logger
{
    using System;
    using System.Collections.Generic;
    using ColossalFramework.Plugins;
    using Models;

    public class LogOutputStub : LogOutputBase
    {
        public LogOutputStub()
        {
            DisposeCount = 0;
            LogMessages = new Dictionary<PluginManager.MessageType, string>();
            LogExceptions = new List<Exception>();
        }

        public int DisposeCount { get; private set; }

        public int LogMessageCount
        {
            get
            {
                return LogMessages.Count;
            }
        }

        public int LogExceptionsCount
        {
            get
            {
                return LogExceptions.Count;
            }
        }

        public IList<Exception> LogExceptions { get; private set; }

        public IDictionary<PluginManager.MessageType, string> LogMessages { get; private set; }

        public override void Dispose()
        {
            DisposeCount++;
        }

        protected override void LogMessage(PluginManager.MessageType messageType, string message)
        {
            LogMessages.Add(messageType, message);
        }

        public override void LogException(Exception ex)
        {
            LogExceptions.Add(ex);
        }
    }
}
