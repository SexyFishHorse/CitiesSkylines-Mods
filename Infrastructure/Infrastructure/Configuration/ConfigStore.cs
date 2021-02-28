namespace SexyFishHorse.CitiesSkylines.Infrastructure.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using SexyFishHorse.CitiesSkylines.Infrastructure.IO;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments;

    public class ConfigStore : IConfigStore
    {
        public const string DefaultConfigFileName = "ModConfiguration.xml";

        private readonly IXmlFileSystem<ModConfiguration> fileSystemWrapper;

        private readonly ModConfiguration cachedConfigs;

        public ConfigStore(
            string modFolderName,
            string configFileName = DefaultConfigFileName,
            IXmlFileSystem<ModConfiguration> fileSystemWrapper = null)
        {
            this.fileSystemWrapper = fileSystemWrapper ?? new XmlFileSystem<ModConfiguration>(new FileSystemWrapper());

            var modFolderPath = GamePaths.GetModFolderPath(GameFolder.Configs);
            modFolderPath = Path.Combine(modFolderPath, modFolderName);

            this.fileSystemWrapper.CreateDirectory(modFolderPath);

            ConfigFileInfo = new FileInfo(Path.Combine(modFolderPath, configFileName));

            if (!this.fileSystemWrapper.FileExists(ConfigFileInfo))
            {
                SaveConfigToFile(new ModConfiguration());
            }

            cachedConfigs = LoadConfigFromFile();
        }

        public FileInfo ConfigFileInfo { get; private set; }

        public T GetSetting<T>(string key)
        {
            key.ShouldNotBeNull("key");

            if (cachedConfigs.Settings.Any(x => x.Key == key))
            {
                var value = cachedConfigs.Settings.Single(x => x.Key == key).Value;

                try
                {
                    return (T)value;
                }
                catch (InvalidCastException ex)
                {
                    throw new InvalidOperationException(
                        string.Format(
                            "Unable to cast setting value \"{0}\" from type \"{1}\" to type \"{2}\".",
                            value,
                            value.GetType().Name,
                            typeof(T).Name),
                        ex);
                }
            }

            return default(T);
        }

        public bool HasSetting(string key)
        {
            key.ShouldNotBeNullOrEmpty("key");

            return cachedConfigs.Settings.Any(x => x.Key == key);
        }

        public ModConfiguration LoadConfigFromFile()
        {
            if (fileSystemWrapper.FileExists(ConfigFileInfo) == false)
            {
                return new ModConfiguration();
            }

            return fileSystemWrapper.GetFileAsObject(ConfigFileInfo) ?? new ModConfiguration();
        }

        public void RemoveSetting(string key)
        {
            key.ShouldNotBeNull("key");

            cachedConfigs.Settings.Remove(cachedConfigs.Settings.Find(x => x.Key == key));

            SaveConfigToFile(cachedConfigs);
        }

        public void SaveConfigToFile(ModConfiguration modConfiguration)
        {
            fileSystemWrapper.SaveObjectToFile(ConfigFileInfo, modConfiguration);
        }

        public void SaveSetting<T>(string key, T value)
        {
            key.ShouldNotBeNull("key");
            value.ShouldNotBeNull(
                "value",
                string.Format("The configuration value for the key {0} is null. Use RemoveSetting to remove a value", key));

            var modConfiguration = LoadConfigFromFile();
            if (modConfiguration.Settings.Any(x => x.Key == key))
            {
                modConfiguration.Settings.Remove(modConfiguration.Settings.Single(x => x.Key == key));
            }

            modConfiguration.Settings.Add(new KeyValuePair<string, object>(key, value));

            SaveConfigToFile(modConfiguration);
        }
    }
}
