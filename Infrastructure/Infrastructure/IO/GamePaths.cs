namespace SexyFishHorse.CitiesSkylines.Infrastructure.IO
{
    using System;
    using System.IO;
    using System.Linq;
    using JetBrains.Annotations;

    public static class GamePaths
    {
        [UsedImplicitly]
        public const string CitiesSkylinesFolderName = "Cities_Skylines";

        [UsedImplicitly]
        public const string ColossalOrderFolderName = "Colossal Order";

        public static string GetModFolderPath(GameFolder gameFolder)
        {
            var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var colossalOrderPath = Path.Combine(localAppDataPath, ColossalOrderFolderName);
            var citiesSkylinesPath = Path.Combine(colossalOrderPath, CitiesSkylinesFolderName);

            var pathAttribute =
                gameFolder.GetType()
                          .GetMember(gameFolder.ToString())
                          .Single()
                          .GetCustomAttributes(typeof(FolderPathAttribute), false)
                          .Cast<FolderPathAttribute>()
                          .Single();

            return pathAttribute.Paths.Aggregate(citiesSkylinesPath, Path.Combine);
        }
    }
}
