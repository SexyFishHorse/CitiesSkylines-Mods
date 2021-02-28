namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.UI.Extensions
{
    using System.Reflection;
    using FluentAssertions;
    using Xunit;

    public class UiHelperBaseExtensions
    {
        [Fact]
        public void ShouldHaveMRootField()
        {
            var field = typeof(UIHelper).GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);

            field.Should().NotBeNull();
        }
    }
}
