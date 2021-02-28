namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.Validation.Arguments
{
    using System;
    using AutoFixture;
    using FluentAssertions;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments;
    using Xunit;

    public class StringExtensionsClass
    {
        public class ShouldNotBeNullOrEmptyMethod : StringExtensionsClass
        {
            private readonly IFixture fixture;

            public ShouldNotBeNullOrEmptyMethod()
            {
                fixture = new Fixture();
            }

            [Fact]
            public void ShouldNotThrowExceptionIfStringIsNotEmpty()
            {
                var value = fixture.Create<string>();

                value.Invoking(x => x.ShouldNotBeNullOrEmpty(fixture.Create<string>())).Should().NotThrow();
            }

            [Fact]
            public void ShouldThrowArgumentExceptionIfStringIsEmpty()
            {
                var parameterName = fixture.Create<string>();
                var value = string.Empty;

                value.Invoking(x => x.ShouldNotBeNullOrEmpty(parameterName))
                     .Should().Throw<ArgumentException>()
                     .And.ParamName.Should()
                     .Be(parameterName);
            }

            [Fact]
            public void ShouldThrowArgumentNullExceptionIfValueIsNull()
            {
                var parameterName = fixture.Create<string>();
                string value = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                value.Invoking(x => x.ShouldNotBeNullOrEmpty(parameterName))
                     .Should().Throw<ArgumentNullException>()
                     .And.ParamName.Should()
                     .Be(parameterName);
            }
        }
    }
}
