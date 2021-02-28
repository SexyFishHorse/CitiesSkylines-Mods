namespace SexyFishHorse.CitiesSkylines.Infrastructure.IO
{
    using JetBrains.Annotations;

    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public enum GameFolder
    {
        [FolderPath("")]
        Root = 0,

        [FolderPath("Addons")]
        Addons = 1,

        [FolderPath(new[] { "Addons", "Assets" })]
        Assets = 100,

        [FolderPath(new[] { "Addons", "ColorCorrections" })]
        ColorCorrections = 101,

        [FolderPath(new[] { "Addons", "Import" })]
        Import = 102,

        [FolderPath(new[] { "Addons", "Mods" })]
        Mods = 104,

        [FolderPath(new[] { "Addons", "MapEditor" })]
        MapEditor = 103,

        [FolderPath(new[] { "Addons", "MapEditor", "Brushes" })]
        Brushes = 103001,

        [FolderPath(new[] { "Addons", "MapEditor", "Heightmaps" })]
        Heightmaps = 103002,

        [FolderPath("Maps")]
        Maps = 2,

        [FolderPath("Saves")]
        Saves = 3,

        [FolderPath("Snapshots")]
        Snapshots = 4,

        [FolderPath("WorkshopStagingArea")]
        WorkshopStagingArea = 5,

        [FolderPath("Configs")]
        Configs = 6,
    }
}
