namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.Validation.Arguments
{
    using System;
    using AutoFixture;
    using FluentAssertions;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments;
    using Xunit;

    public class FloatExtensionsClass
    {
        private readonly IFixture fixture;

        protected FloatExtensionsClass()
        {
            fixture = new Fixture();
        }

        public class ShouldBeGreaterThanOrEqualToMethod : FloatExtensionsClass
        {
            [Theory]
            [InlineData(float.MinValue, float.MinValue)]
            [InlineData(-1f, -1f)]
            [InlineData(0f, 0f)]
            [InlineData(0.0000001f, 0.0000001f)]
            [InlineData(0.000001f, 0.000001f)]
            [InlineData(0.00001f, 0.00001f)]
            [InlineData(0.0001f, 0.0001f)]
            [InlineData(0.001f, 0.001f)]
            [InlineData(0.01f, 0.01f)]
            [InlineData(0.1f, 0.1f)]
            [InlineData(1f, 1f)]
            [InlineData(float.MaxValue, float.MaxValue)]
            public void ShouldNotThrowExceptionIfValueIsEqualToSpecified(float value, float otherValue)
            {
                value.Invoking(x => x.ShouldBeGreaterThanOrEqualTo(otherValue, fixture.Create<string>())).Should().NotThrow();
            }

            [Theory]
            [InlineData(float.MaxValue, float.MinValue)]
            [InlineData(-1f, -2f)]
            [InlineData(0f, -1f)]
            [InlineData(1f, -1f)]
            [InlineData(0.1f, -0.1f)]
            [InlineData(0.0000001f, 0f)]
            [InlineData(0.000001f, 0f)]
            [InlineData(0.00001f, 0f)]
            [InlineData(0.0001f, 0f)]
            [InlineData(0.001f, 0f)]
            [InlineData(0.01f, 0f)]
            [InlineData(0.1f, 0f)]
            [InlineData(0.0000002f, 0.0000001f)]
            [InlineData(0.000002f, 0.000001f)]
            [InlineData(0.00002f, 0.00001f)]
            [InlineData(0.0002f, 0.0001f)]
            [InlineData(0.002f, 0.001f)]
            [InlineData(0.02f, 0.01f)]
            [InlineData(0.2f, 0.1f)]
            [InlineData(1f, 0f)]
            [InlineData(2f, 1f)]
            public void ShouldNotThrowExceptionIfValueIsGreaterThanSpecified(float value, float otherValue)
            {
                value.Invoking(x => x.ShouldBeGreaterThanOrEqualTo(otherValue, fixture.Create<string>())).Should().NotThrow();
            }

            [Theory]
            [InlineData(float.MinValue, float.MaxValue)]
            [InlineData(1f, 2f)]
            [InlineData(0f, 1f)]
            [InlineData(0.0000001f, 0.0000002f)]
            [InlineData(0.000001f, 0.000002f)]
            [InlineData(0.00001f, 0.00002f)]
            [InlineData(0.0001f, 0.0002f)]
            [InlineData(0.001f, 0.002f)]
            [InlineData(0.01f, 0.02f)]
            [InlineData(0.1f, 0.2f)]
            [InlineData(-1f, 0f)]
            [InlineData(-0.0000002f, -0.0000001f)]
            [InlineData(-0.000002f, -0.000001f)]
            [InlineData(-0.00002f, -0.00001f)]
            [InlineData(-0.0002f, -0.0001f)]
            [InlineData(-0.002f, -0.001f)]
            [InlineData(-0.02f, -0.01f)]
            [InlineData(-0.2f, -0.1f)]
            [InlineData(-2f, -1f)]
            public void ShouldThrowArgumentOutOfRangeExceptionWhenValueIsGreaterThanSpecified(float value, float otherValue)
            {
                var parameterName = fixture.Create<string>();

                value.Invoking(x => x.ShouldBeGreaterThanOrEqualTo(otherValue, parameterName))
                     .Should()
                     .Throw<ArgumentOutOfRangeException>()
                     .And.ParamName.Should()
                     .Be(parameterName);
            }
        }

        public class ShouldBeLessThanMethod : FloatExtensionsClass
        {
            [Theory]
            [InlineData(float.MinValue, float.MaxValue)]
            [InlineData(1f, 2f)]
            [InlineData(0.1f, 0.2f)]
            [InlineData(0.01f, 0.02f)]
            [InlineData(0.001f, 0.002f)]
            [InlineData(0.0001f, 0.0002f)]
            [InlineData(0.00001f, 0.00002f)]
            [InlineData(0.000001f, 0.000002f)]
            [InlineData(0f, 1f)]
            [InlineData(0f, 0.1f)]
            [InlineData(0f, 0.01f)]
            [InlineData(0f, 0.001f)]
            [InlineData(0f, 0.0001f)]
            [InlineData(0f, 0.00001f)]
            [InlineData(0f, 0.000001f)]
            [InlineData(-0.0000001f, 0f)]
            [InlineData(-0.000001f, 0f)]
            [InlineData(-0.00001f, 0f)]
            [InlineData(-0.0001f, 0f)]
            [InlineData(-0.001f, 0f)]
            [InlineData(-0.01f, 0f)]
            [InlineData(-0.1f, 0f)]
            [InlineData(-1f, 0f)]
            [InlineData(-2f, -1f)]
            public void ShouldNotThrowExceptionWhenValueIsLessThanSpecified(float value, float otherValue)
            {
                value.Invoking(x => x.ShouldBeLessThan(otherValue, fixture.Create<string>())).Should().NotThrow();
            }

            [Theory]
            [InlineData(float.MaxValue, float.MaxValue)]
            [InlineData(1f, 1f)]
            [InlineData(0.1f, 0.1f)]
            [InlineData(0.01f, 0.01f)]
            [InlineData(0.001f, 0.001f)]
            [InlineData(0.0001f, 0.0001f)]
            [InlineData(0.00001f, 0.00001f)]
            [InlineData(0.000001f, 0.000001f)]
            [InlineData(0f, 0f)]
            [InlineData(-0.000001f, -0.000001f)]
            [InlineData(-0.00001f, -0.00001f)]
            [InlineData(-0.0001f, -0.0001f)]
            [InlineData(-0.001f, -0.001f)]
            [InlineData(-0.01f, -0.01f)]
            [InlineData(-0.1f, -0.1f)]
            [InlineData(-1f, -1f)]
            [InlineData(float.MinValue, float.MinValue)]
            public void ShouldThrowArgumentOutOfRangeExceptionWhenValueIsEqualToSpecified(float value, float otherValue)
            {
                var parameterName = fixture.Freeze<string>();

                value.Invoking(x => x.ShouldBeLessThan(otherValue, parameterName))
                     .Should()
                     .Throw<ArgumentOutOfRangeException>()
                     .And.ParamName.Should()
                     .Be(parameterName);
            }

            [Theory]
            [InlineData(float.MaxValue, float.MinValue)]
            [InlineData(2f, 1f)]
            [InlineData(0.2f, 0.1f)]
            [InlineData(0.02f, 0.01f)]
            [InlineData(0.002f, 0.001f)]
            [InlineData(0.0002f, 0.0001f)]
            [InlineData(0.00002f, 0.00001f)]
            [InlineData(0.000002f, 0.000001f)]
            [InlineData(1f, 0f)]
            [InlineData(0.1f, 0f)]
            [InlineData(0.01f, 0f)]
            [InlineData(0.001f, 0f)]
            [InlineData(0.0001f, 0f)]
            [InlineData(0.00001f, 0f)]
            [InlineData(0.000001f, 0f)]
            [InlineData(0f, -0.0000001f)]
            [InlineData(0f, -0.000001f)]
            [InlineData(0f, -0.00001f)]
            [InlineData(0f, -0.0001f)]
            [InlineData(0f, -0.001f)]
            [InlineData(0f, -0.01f)]
            [InlineData(0f, -0.1f)]
            [InlineData(0f, -1f)]
            [InlineData(-1f, -2f)]
            public void ShouldThrowArgumentOutOfRangeExceptionWhenValueIsLessThanSpecified(float value, float otherValue)
            {
                var parameterName = fixture.Freeze<string>();

                value.Invoking(x => x.ShouldBeLessThan(otherValue, parameterName))
                     .Should()
                     .Throw<ArgumentOutOfRangeException>()
                     .And.ParamName.Should()
                     .Be(parameterName);
            }
        }

        public class ShouldBeLessThanOrEqualTo : FloatExtensionsClass
        {
            [Theory]
            [InlineData(float.MinValue, float.MinValue)]
            [InlineData(-1f, -1f)]
            [InlineData(0f, 0f)]
            [InlineData(0.0000001f, 0.0000001f)]
            [InlineData(0.000001f, 0.000001f)]
            [InlineData(0.00001f, 0.00001f)]
            [InlineData(0.0001f, 0.0001f)]
            [InlineData(0.001f, 0.001f)]
            [InlineData(0.01f, 0.01f)]
            [InlineData(0.1f, 0.1f)]
            [InlineData(1f, 1f)]
            [InlineData(float.MaxValue, float.MaxValue)]
            public void ShouldNotThrowExceptionIfValueIsEqualToSpecified(float value, float otherValue)
            {
                value.Invoking(x => x.ShouldBeLessThanOrEqualTo(otherValue, fixture.Create<string>())).Should().NotThrow();
            }

            [Theory]
            [InlineData(float.MinValue, float.MaxValue)]
            [InlineData(-2f, -1f)]
            [InlineData(-1f, 0f)]
            [InlineData(-1f, 1f)]
            [InlineData(-0.1f, 0.1f)]
            [InlineData(0f, 0.0000001f)]
            [InlineData(0f, 0.000001f)]
            [InlineData(0f, 0.00001f)]
            [InlineData(0f, 0.0001f)]
            [InlineData(0f, 0.001f)]
            [InlineData(0f, 0.01f)]
            [InlineData(0f, 0.1f)]
            [InlineData(0.0000001f, 0.0000002f)]
            [InlineData(0.000001f, 0.000002f)]
            [InlineData(0.00001f, 0.00002f)]
            [InlineData(0.0001f, 0.0002f)]
            [InlineData(0.001f, 0.002f)]
            [InlineData(0.01f, 0.02f)]
            [InlineData(0.1f, 0.2f)]
            [InlineData(0f, 1f)]
            [InlineData(1f, 2f)]
            public void ShouldNotThrowExceptionIfValueIsLessThanSpecified(float value, float otherValue)
            {
                value.Invoking(x => x.ShouldBeLessThanOrEqualTo(otherValue, fixture.Create<string>())).Should().NotThrow();
            }

            [Theory]
            [InlineData(2f, 1f)]
            [InlineData(1f, 0f)]
            [InlineData(0.0000002f, 0.0000001f)]
            [InlineData(0.000002f, 0.000001f)]
            [InlineData(0.00002f, 0.00001f)]
            [InlineData(0.0002f, 0.0001f)]
            [InlineData(0.002f, 0.001f)]
            [InlineData(0.02f, 0.01f)]
            [InlineData(0.2f, 0.1f)]
            [InlineData(0f, -1f)]
            [InlineData(-0.0000001f, -0.0000002f)]
            [InlineData(-0.000001f, -0.000002f)]
            [InlineData(-0.00001f, -0.00002f)]
            [InlineData(-0.0001f, -0.0002f)]
            [InlineData(-0.001f, -0.002f)]
            [InlineData(-0.01f, -0.02f)]
            [InlineData(-0.1f, -0.2f)]
            [InlineData(-1f, -2f)]
            [InlineData(float.MaxValue, float.MinValue)]
            public void ShouldThrowArgumentOutOfRangeExceptionWhenValueIsGreaterThanSpecified(float value, float otherValue)
            {
                var parameterName = fixture.Create<string>();

                value.Invoking(x => x.ShouldBeLessThanOrEqualTo(otherValue, parameterName))
                     .Should()
                     .Throw<ArgumentOutOfRangeException>()
                     .And.ParamName.Should()
                     .Be(parameterName);
            }
        }
    }
}
