namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.Validation.Arguments
{
    using System;
    using AutoFixture;
    using FluentAssertions;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments;
    using Xunit;

    public class IntExtensionsClass
    {
        public class ShouldBeGreaterThanZeroMethod : IntExtensionsClass
        {
            private readonly IFixture fixture;

            public ShouldBeGreaterThanZeroMethod()
            {
                fixture = new Fixture();
            }

            [Theory]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(100)]
            [InlineData(int.MaxValue)]
            public void ShouldNotThrowExceptionIfValueIsGreaterThanZero(int value)
            {
                value.Invoking(x => x.ShouldBeGreaterThanZero(fixture.Create<string>())).Should().NotThrow();
            }

            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            [InlineData(-2)]
            [InlineData(-3)]
            [InlineData(-100)]
            [InlineData(int.MinValue)]
            public void ShouldThrowArgumentOutOfRangeExceptionWhenValueIsLessThanOne(int value)
            {
                var parameterName = fixture.Create<string>();
                value.Invoking(x => x.ShouldBeGreaterThanZero(parameterName))
                     .Should()
                     .Throw<ArgumentOutOfRangeException>()
                     .And.ParamName.Should()
                     .Be(parameterName);
            }
        }
    }
}
