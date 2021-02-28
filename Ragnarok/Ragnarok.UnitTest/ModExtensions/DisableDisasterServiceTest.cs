namespace SexyFishHorse.CitiesSkylines.Ragnarok.UnitTest.ModExtensions
{
    using System.Reflection;
    using FluentAssertions;
    using JetBrains.Annotations;
    using Xunit;

    [UsedImplicitly]
    public class DisableDisasterServiceTest
    {
        [Fact]
        public void DisasterWrapperShouldHavePrivateFieldForDisasterTypeInfoConversion()
        {
            var conversionField = typeof(DisasterWrapper).GetField(
                "m_DisasterTypeToInfoConversion",
                BindingFlags.NonPublic | BindingFlags.Instance);

            conversionField.Should().NotBeNull();
        }
    }
}
