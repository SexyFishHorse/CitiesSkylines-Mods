namespace SexyFishHorse.CitiesSkylines.Logger.Models
{
    using System;
    using System.IO;
    using ColossalFramework.Plugins;

    public class FileOutput : LogOutputBase
    {
        private readonly StreamWriter streamWriter;

        public FileOutput(string path)
        {
            streamWriter = File.CreateText(path);
            streamWriter.AutoFlush = true;
        }

        public override void Dispose()
        {
            streamWriter.Dispose();
        }

        public override void LogException(Exception ex)
        {
            LogMessage(PluginManager.MessageType.Error, ex.Message);
            LogMessage(PluginManager.MessageType.Error, ex.StackTrace);
        }

        protected override void LogMessage(PluginManager.MessageType messageType, string message)
        {
            message = string.Format("[{0}][{1}]: {2}", DateTime.Now, messageType, message);

            streamWriter.WriteLine(message);
        }
    }
}
