namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.Validation.Arguments
{
    using System;
    using AutoFixture;
    using FluentAssertions;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments;
    using Xunit;

    public class ObjectExtensionsClass
    {
        public class ShouldNotBeNullMethod : ObjectExtensionsClass
        {
            private readonly IFixture _fixture;

            public ShouldNotBeNullMethod()
            {
                _fixture = new Fixture();
            }

            [Fact]
            public void ShouldNotThrowExceptionIfParameterIsNotNull()
            {
                var value = _fixture.Create<string>();

                // ReSharper disable once ExpressionIsAlwaysNull
                value.Invoking(x => x.ShouldNotBeNull(_fixture.Create<string>()))
                     .Should()
                     .NotThrow();
            }

            [Fact]
            public void ShouldThrowArgumentNullExceptionWhenParameterIsNull()
            {
                var parameterName = _fixture.Create<string>();
                string value = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Action act = () => value.ShouldNotBeNullOrEmpty(parameterName);

                act
                    .Should()
                    .Throw<ArgumentNullException>()
                    .And.ParamName.Should()
                    .Be(parameterName);
            }
        }
    }
}
