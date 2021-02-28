namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.Validation.Arguments
{
    using System;
    using System.Collections.Generic;
    using AutoFixture;
    using FluentAssertions;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments;
    using Xunit;

    public class EnumerableExtensionsClass
    {
        public class ShouldNotBeNullOrEmptyMethod : EnumerableExtensionsClass
        {
            private readonly IFixture fixture;

            public ShouldNotBeNullOrEmptyMethod()
            {
                fixture = new Fixture();
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenCollectionIsNotEmpty()
            {
                var list = fixture.CreateMany<string>();

                list.Invoking(x => x.ShouldNotBeNullOrEmpty(fixture.Create<string>())).Should().NotThrow();
            }

            [Fact]
            public void ShouldThrowArgumentExceptionWhenCollectionIsEmpty()
            {
                var parameterName = fixture.Create<string>();
                var list = new List<string>();

                list.Invoking(x => x.ShouldNotBeNullOrEmpty(parameterName))
                    .Should()
                    .Throw<ArgumentException>()
                    .And.ParamName.Should()
                    .Be(parameterName);
            }

            [Fact]
            public void ShouldThrowArgumentNullExceptionWhenCollectionIsNull()
            {
                var parameterName = fixture.Create<string>();
                List<string> list = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                list.Invoking(x => x.ShouldNotBeNullOrEmpty(parameterName))
                    .Should()
                    .Throw<ArgumentNullException>()
                    .And.ParamName.Should()
                    .Be(parameterName);
            }
        }
    }
}
