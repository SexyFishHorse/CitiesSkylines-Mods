﻿namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.Validation.Arguments
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
            private readonly IFixture _fixture;

            public ShouldNotBeNullOrEmptyMethod()
            {
                _fixture = new Fixture();
            }

            [Fact]
            public void ShouldNotThrowExceptionIfStringIsNotEmpty()
            {
                var value = _fixture.Create<string>();

                value.Invoking(x => x.ShouldNotBeNullOrEmpty(_fixture.Create<string>())).Should().NotThrow();
            }

            [Fact]
            public void ShouldThrowArgumentExceptionIfStringIsEmpty()
            {
                var parameterName = _fixture.Create<string>();
                var value = string.Empty;

                value.Invoking(x => x.ShouldNotBeNullOrEmpty(parameterName))
                     .Should().Throw<ArgumentException>()
                     .And.ParamName.Should()
                     .Be(parameterName);
            }

            [Fact]
            public void ShouldThrowArgumentNullExceptionIfValueIsNull()
            {
                var parameterName = _fixture.Create<string>();
                string value = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Action act = () => value.ShouldNotBeNullOrEmpty(parameterName);

                act
                     .Should().Throw<ArgumentNullException>()
                     .And.ParamName.Should()
                     .Be(parameterName);
            }
        }
    }
}
