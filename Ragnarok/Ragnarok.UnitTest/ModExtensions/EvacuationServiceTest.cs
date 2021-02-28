namespace SexyFishHorse.CitiesSkylines.Ragnarok.UnitTest.ModExtensions
{
    using System.Reflection;
    using FluentAssertions;
    using Xunit;

    public class EvacuationServiceTest
    {
        [Fact]
        public void WarningPhasePanelShouldHavePrivateFieldForIsEvacuating()
        {
            var evacuatingField = typeof(WarningPhasePanel).GetField("m_isEvacuating", BindingFlags.NonPublic | BindingFlags.Instance);

            evacuatingField.Should().NotBeNull();
        }
    }
}
