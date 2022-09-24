namespace SexyFishHorse.CitiesSkylines.Infrastructure.Logging
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using ColossalFramework.Plugins;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging.Outputs;

    public class Logger : ILogger
    {
        private readonly IList<LogOutputBase> _logOutputs;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class which logs to both the in-game console and to a <see cref="loggerName"/>.log
        /// file in the %LocalAppData%\Colossal Order\Cities_Skylines\Logs\ folder (The folder will be automatically created).
        /// </summary>
        /// <param name="loggerName">The name of the logger. This will be used for the filename for the log as well.</param>
        public Logger(string loggerName)
        {
            Directory.CreateDirectory(GetLogFolderPath());
            var path = Path.Combine(GetLogFolderPath(), string.Format("{0}.log", loggerName));

            _logOutputs = new List<LogOutputBase> { new DebugPanelOutput(), new FileOutput(path), new DebugOutput() };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class which logs to the specified outputs
        /// </summary>
        /// <param name="logOutputs">The outputs that log statements are outputted to.</param>
        public Logger(IList<LogOutputBase> logOutputs)
        {
            _logOutputs = logOutputs;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class which logs to the specified output
        /// </summary>
        /// <param name="logOutput">The output that log statements are outputted to.</param>
        public Logger(LogOutputBase logOutput)
            : this(new List<LogOutputBase> { logOutput })
        {
        }

        public void Dispose()
        {
            foreach (var output in _logOutputs)
            {
                output.Dispose();
            }
        }

        public void Error(string message, params object[] args)
        {
            Log(PluginManager.MessageType.Error, message, args);
        }

        public void Info(string message, params object[] args)
        {
            Log(PluginManager.MessageType.Message, message, args);
        }

        public void Log(PluginManager.MessageType messageType, string message, params object[] args)
        {
            foreach (var output in _logOutputs)
            {
                output.Log(messageType, message, args);
            }
        }

        public void LogException(Exception ex, string message = null, params object[] args)
        {
            foreach (var output in _logOutputs)
            {
                output.LogException(ex, message, args);
            }
        }

        public void Warn(string message, params object[] args)
        {
            Log(PluginManager.MessageType.Warning, message, args);
        }

        private static string GetLogFolderPath()
        {
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            var path = Path.Combine(localAppData, "Colossal Order");
            path = Path.Combine(path, "Cities_Skylines");
            path = Path.Combine(path, "Logs");

            return path;
        }
    }
}
