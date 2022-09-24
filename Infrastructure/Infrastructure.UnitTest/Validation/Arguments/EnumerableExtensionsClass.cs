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
            private readonly IFixture _fixture;

            public ShouldNotBeNullOrEmptyMethod()
            {
                _fixture = new Fixture();
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenCollectionIsNotEmpty()
            {
                var list = _fixture.CreateMany<string>();

                list.Invoking(x => x.ShouldNotBeNullOrEmpty(_fixture.Create<string>())).Should().NotThrow();
            }

            [Fact]
            public void ShouldThrowArgumentExceptionWhenCollectionIsEmpty()
            {
                var parameterName = _fixture.Create<string>();
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
                var parameterName = _fixture.Create<string>();
                List<string> list = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Action act = () => list.ShouldNotBeNullOrEmpty(parameterName);

                act
                    .Should()
                    .Throw<ArgumentNullException>()
                    .And.ParamName.Should()
                    .Be(parameterName);
            }
        }
    }
}
