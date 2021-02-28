namespace SexyFishHorse.CitiesSkylines.Logger
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class LogManager
    {
        private static LogManager instance;

        private readonly IDictionary<string, ILogger> loggers;

        public LogManager()
        {
            loggers = new Dictionary<string, ILogger>();
        }

        public static LogManager Instance
        {
            get
            {
                return instance ?? (instance = new LogManager());
            }
        }

        public void SetLogger(string loggerName, ILogger logger)
        {
            ValidateLoggerName(loggerName);

            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            loggers[loggerName] = logger;
        }

        public void RemoveLogger(string loggerName)
        {
            ValidateLoggerName(loggerName);

            loggers.Remove(loggerName);
        }

        public ILogger GetLogger(string loggerName)
        {
            ValidateLoggerName(loggerName);

            if (loggers.ContainsKey(loggerName))
            {
                return loggers[loggerName];
            }

            return null;
        }

        public ILogger GetOrCreateLogger(string loggerName)
        {
            ValidateLoggerName(loggerName);

            if (loggers.ContainsKey(loggerName))
            {
                return loggers[loggerName];
            }

            var logger = new Logger(loggerName);

            loggers.Add(loggerName, logger);

            return logger;
        }

        private static void ValidateLoggerName(string loggerName)
        {
            if (string.IsNullOrEmpty(loggerName))
            {
                throw new ArgumentNullException("loggerName");
            }
        }
    }
}
