namespace SexyFishHorse.CitiesSkylines.Infrastructure.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;
    using Logger;

    public class ConfigurationManager : IConfigurationManager
    {
        private readonly IConfigStore configStore;

        private ModConfiguration configuration;

        public ConfigurationManager(IConfigStore configStore)
        {
            this.configStore = configStore;
        }

        public ILogger Logger { get; set; }

        public static IConfigurationManager Create(string modName)
        {
            return new ConfigurationManager(new ConfigStore(modName, modName + ".xml"));
        }

        public T GetSetting<T>(string settingKey)
        {
            EnsureConfigLoaded();

            if (configuration.Settings.Any(x => x.Key == settingKey))
            {
                var value = configuration.Settings.Single(x => x.Key == settingKey).Value;
                try
                {
                    return (T)value;
                }
                catch (InvalidCastException ex)
                {
                    var message = string.Format(
                        "Tried to cast value '{0}' of type {1} to {2}.",
                        value,
                        value.GetType().Name,
                        typeof(T).Name);

                    TryLogError(message);

                    throw new InvalidCastException(message, ex);
                }
            }

            TryLog("No setting found for {0}", settingKey);

            return default(T);
        }

        public bool HasSetting(string settingKey)
        {
            EnsureConfigLoaded();

            return configuration.Settings.Any(x => x.Key == settingKey);
        }

        public void MigrateKey<T>(string settingKey, string newSettingKey)
        {
            EnsureConfigLoaded();

            if (configuration.Settings.All(x => x.Key != settingKey))
            {
                return;
            }

            var settingValue = configuration.Settings.Single(x => x.Key == settingKey);

            RemoveSetting(settingKey);
            SaveSetting(newSettingKey, (T)settingValue.Value);

            TryLog("Migrated setting {0} to {1}", settingKey, newSettingKey);
        }

        public void MigrateType<TOrigin, TTarget>(string settingKey, Func<TOrigin, TTarget> typeConversionFunction)
        {
            EnsureConfigLoaded();

            if (configuration.Settings.All(x => x.Key != settingKey))
            {
                return;
            }

            var settingValue = configuration.Settings.Single(x => x.Key == settingKey).Value;
            if (settingValue.GetType().FullName == typeof(TTarget).FullName)
            {
                return;
            }

            var newValue = typeConversionFunction.Invoke((TOrigin)settingValue);

            SaveSetting(settingKey, newValue);

            TryLog("Converted value '{0}' from type {1} to {2}", settingKey, typeof(TOrigin).Name, typeof(TTarget).Name);
        }

        public void SaveSetting(string settingKey, object value)
        {
            EnsureConfigLoaded();

            if (configuration.Settings.Any(x => x.Key == settingKey))
            {
                configuration.Settings.Remove(configuration.Settings.Single(x => x.Key == settingKey));
            }

            var newKeyValuePair = new KeyValuePair<string, object>(settingKey, value);
            configuration.Settings.Add(newKeyValuePair);

            configStore.SaveConfigToFile(configuration);

            TryLog("Saved setting {0} with value {1}", settingKey, value);
        }

        private void EnsureConfigLoaded()
        {
            if (configuration == null)
            {
                configuration = configStore.LoadConfigFromFile();

                TryLog("Loaded config from file");
            }
        }

        private void RemoveSetting(string settingKey)
        {
            var setting = configuration.Settings.FirstOrDefault(x => x.Key == settingKey);
            configuration.Settings.Remove(setting);

            configStore.SaveConfigToFile(configuration);

            TryLog("Removed setting for {0}", settingKey);
        }

        [StringFormatMethod("format")]
        private void TryLog(string format, params object[] args)
        {
            if (Logger != null)
            {
                Logger.Info(format, args);
            }
        }

        private void TryLogError(string message)
        {
            if (Logger != null)
            {
                Logger.Error(message);
            }
        }
    }
}
