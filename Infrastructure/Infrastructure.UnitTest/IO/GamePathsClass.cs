namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.IO
{
    using System;
    using System.IO;
    using FluentAssertions;
    using SexyFishHorse.CitiesSkylines.Infrastructure.IO;
    using Xunit;

    public class GamePathsClass
    {
        public class GetModFolderPathMethod : GamePathsClass
        {
            [Theory]
            [InlineData(GameFolder.Root, @"Colossal Order\Cities_Skylines")]
            [InlineData(GameFolder.Addons, @"Colossal Order\Cities_Skylines\Addons")]
            [InlineData(GameFolder.Assets, @"Colossal Order\Cities_Skylines\Addons\Assets")]
            [InlineData(GameFolder.ColorCorrections, @"Colossal Order\Cities_Skylines\Addons\ColorCorrections")]
            [InlineData(GameFolder.Import, @"Colossal Order\Cities_Skylines\Addons\Import")]
            [InlineData(GameFolder.Mods, @"Colossal Order\Cities_Skylines\Addons\Mods")]
            [InlineData(GameFolder.MapEditor, @"Colossal Order\Cities_Skylines\Addons\MapEditor")]
            [InlineData(GameFolder.Brushes, @"Colossal Order\Cities_Skylines\Addons\MapEditor\Brushes")]
            [InlineData(GameFolder.Heightmaps, @"Colossal Order\Cities_Skylines\Addons\MapEditor\Heightmaps")]
            [InlineData(GameFolder.Maps, @"Colossal Order\Cities_Skylines\Maps")]
            [InlineData(GameFolder.Saves, @"Colossal Order\Cities_Skylines\Saves")]
            [InlineData(GameFolder.Snapshots, @"Colossal Order\Cities_Skylines\Snapshots")]
            [InlineData(GameFolder.WorkshopStagingArea, @"Colossal Order\Cities_Skylines\WorkshopStagingArea")]
            [InlineData(GameFolder.Configs, @"Colossal Order\Cities_Skylines\Configs")]
            public void ShouldBuildStringCorrectly(GameFolder folder, string expectedEndPart)
            {
                var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var expectedResult = Path.Combine(localAppDataPath, expectedEndPart);

                var result = GamePaths.GetModFolderPath(folder);

                result.Should().Be(expectedResult);
            }
        }
    }
}
