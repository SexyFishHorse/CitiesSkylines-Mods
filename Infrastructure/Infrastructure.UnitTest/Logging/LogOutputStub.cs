namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ColossalFramework.Plugins;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging.Outputs;

    public class LogOutputStub : LogOutputBase
    {
        public LogOutputStub()
        {
            DisposeCount = 0;
            LogMessages = new Dictionary<PluginManager.MessageType, string>();
            LogExceptions = new List<Tuple<Exception, string>>();
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

        public IList<Tuple<Exception, string>> LogExceptions { get; private set; }

        public IDictionary<PluginManager.MessageType, string> LogMessages { get; private set; }

        public override void Dispose()
        {
            DisposeCount++;
        }

        protected override void LogMessage(PluginManager.MessageType messageType, string message)
        {
            LogMessages.Add(messageType, message);
        }

        public override void LogException(Exception ex, string message = null, params object[] args)
        {
            if (string.IsNullOrEmpty(message) == false)
            {
                message = string.Format(message, args ?? Enumerable.Empty<object>());
            }

            LogExceptions.Add(Tuple.Create(ex, message));
        }
    }
}
