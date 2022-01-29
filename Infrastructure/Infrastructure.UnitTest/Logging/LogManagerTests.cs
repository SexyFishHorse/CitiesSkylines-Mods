namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.Logging
{
    using System;
    using AutoFixture;
    using FluentAssertions;
    using Moq;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;
    using Xunit;

    public class LogManagerTests
    {
        [Fact]
        public void GetLogger_LoggerDoesNotExist_ShouldReturnNull()
        {
            var fixture = new Fixture();

            LogManager.Instance.GetLogger(fixture.Create<string>()).Should().BeNull();
        }

        [Fact]
        public void GetLogger_LoggerExist_ShouldReturnLogger()
        {
            var fixture = new Fixture();

            var loggerName = fixture.Create<string>();
            var logger = fixture.Create<Mock<ILogger>>();

            LogManager.Instance.SetLogger(loggerName, logger.Object);

            LogManager.Instance.GetLogger(loggerName).Should().Be(logger.Object);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GetLogger_LoggerNameIsNull_ShouldThrowArgumentNullException(string loggerName)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => LogManager.Instance.GetLogger(loggerName));

            exception.ParamName.Should().Be("loggerName");
        }

        [Fact]
        public void GetOrCreateLogger_CalledMultipleTimes_ShouldReturnSameLogger()
        {
            var fixture = new Fixture();

            var loggerName = fixture.Create<string>();

            var result1 = LogManager.Instance.GetOrCreate(loggerName);
            var result2 = LogManager.Instance.GetOrCreate(loggerName);

            result1.Should().Be(result2);
            result2.Should().Be(result1);
        }

        [Fact]
        public void GetOrCreateLogger_LoggerDoesNotExist_ShouldCreateLogger()
        {
            var fixture = new Fixture();

            var result = LogManager.Instance.GetOrCreate(fixture.Create<string>());

            result.Should().NotBeNull();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GetOrCreateLogger_LoggerNameIsNull_ShouldThrowArgumentNullException(string loggerName)
        {
            var exception =
                Assert.Throws<ArgumentNullException>(() => LogManager.Instance.GetOrCreate(loggerName));

            exception.ParamName.Should().Be("loggerName");
        }

        [Fact]
        public void Instance_CalledMultipleTimes_ShouldReturnSameInstance()
        {
            var instance1 = LogManager.Instance;
            var instance2 = LogManager.Instance;

            instance1.Should().Be(instance2);
            instance2.Should().Be(instance1);
        }

        [Fact]
        public void Instance_InstanceNotSet_ShouldReturnInstance()
        {
            LogManager.Instance.Should().NotBeNull();
        }

        [Fact]
        public void RemoveLogger_LoggerExists_LoggerIsRemoved()
        {
            var fixture = new Fixture();

            var loggerName = fixture.Create<string>();

            LogManager.Instance.SetLogger(loggerName, fixture.Create<Mock<ILogger>>().Object);

            LogManager.Instance.RemoveLogger(loggerName);

            LogManager.Instance.GetLogger(loggerName).Should().BeNull();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void RemoveLogger_LoggerNameIsNull_ShouldThrowArgumentNullException(string loggerName)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => LogManager.Instance.RemoveLogger(loggerName));

            exception.ParamName.Should().Be("loggerName");
        }

        [Fact]
        public void SetLogger_LoggerAndLoggerNameSet_ShouldSetLogger()
        {
            var fixture = new Fixture();

            var loggerName = fixture.Create<string>();
            var logger = fixture.Create<Mock<ILogger>>();

            LogManager.Instance.SetLogger(loggerName, logger.Object);

            var result = LogManager.Instance.GetOrCreate(loggerName);

            result.Should().Be(logger.Object);
        }

        [Fact]
        public void SetLogger_LoggerIsNull_ShouldThrowArgumentNullException()
        {
            var fixture = new Fixture();

            var exception =
                Assert.Throws<ArgumentNullException>(
                    () => LogManager.Instance.SetLogger(fixture.Create<string>(), null));

            exception.ParamName.Should().Be("logger");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void SetLogger_LoggerNameIsNull_ShouldThrowArgumentNullException(string loggerName)
        {
            var fixture = new Fixture();

            var exception = Assert.Throws<ArgumentNullException>(
                () => LogManager.Instance.SetLogger(loggerName, fixture.Create<Mock<ILogger>>().Object));

            exception.ParamName.Should().Be("loggerName");
        }
    }
}
