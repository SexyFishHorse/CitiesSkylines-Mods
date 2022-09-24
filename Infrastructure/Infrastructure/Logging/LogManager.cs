namespace SexyFishHorse.CitiesSkylines.Infrastructure.Logging
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class LogManager
    {
        private static LogManager s_Instance;

        private readonly IDictionary<string, ILogger> _loggers;

        public LogManager()
        {
            _loggers = new Dictionary<string, ILogger>();
        }

        public static LogManager Instance
        {
            get
            {
                return s_Instance ?? (s_Instance = new LogManager());
            }
        }

        public void SetLogger(string loggerName, ILogger logger)
        {
            ValidateLoggerName(loggerName);

            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            _loggers[loggerName] = logger;
        }

        public void RemoveLogger(string loggerName)
        {
            ValidateLoggerName(loggerName);

            _loggers.Remove(loggerName);
        }

        public ILogger GetLogger(string loggerName)
        {
            ValidateLoggerName(loggerName);

            if (_loggers.ContainsKey(loggerName))
            {
                return _loggers[loggerName];
            }

            return null;
        }

        public ILogger GetOrCreate(string loggerName)
        {
            ValidateLoggerName(loggerName);

            if (_loggers.ContainsKey(loggerName))
            {
                return _loggers[loggerName];
            }

            var logger = new Logger(loggerName);

            _loggers.Add(loggerName, logger);

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
