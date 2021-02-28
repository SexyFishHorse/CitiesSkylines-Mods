namespace SexyFishHorse.CitiesSkylines.Infrastructure.Configuration
{
    using System.IO;
    using JetBrains.Annotations;

    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public interface IConfigStore
    {
        [NotNull]
        FileInfo ConfigFileInfo { get; }

        [CanBeNull]
        T GetSetting<T>([NotNull] string key);

        void RemoveSetting([NotNull] string key);

        void SaveSetting<T>([NotNull] string key, [NotNull] T value);

        bool HasSetting([NotNull] string key);

        [NotNull]
        ModConfiguration LoadConfigFromFile();

        void SaveConfigToFile([NotNull] ModConfiguration modConfiguration);
    }
}
