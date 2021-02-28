namespace SexyFishHorse.CitiesSkylines.Infrastructure.Configuration
{
    using System;
    using JetBrains.Annotations;
    using Logger;

    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public interface IConfigurationManager
    {
        ILogger Logger { get; set; }

        T GetSetting<T>(string settingKey);

        bool HasSetting(string settingKey);

        void MigrateKey<T>(string settingKey, string newSettingKey);

        void MigrateType<TOrigin, TTarget>(string settingKey, Func<TOrigin, TTarget> typeConversionFunction);

        void SaveSetting(string settingKey, object value);
    }
}
