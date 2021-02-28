namespace SexyFishHorse.CitiesSkylines.Logger.UnitTest.Logger
{
    using System;
    using AutoFixture;
    using Moq;
    using Xunit;
    using XunitShouldExtension;

    public class LogManagerTests
    {
        [Fact]
        public void GetLogger_LoggerDoesNotExist_ShouldReturnNull()
        {
            var fixture = new Fixture();

            LogManager.Instance.GetLogger(fixture.Create<string>()).ShouldBeNull();
        }

        [Fact]
        public void GetLogger_LoggerExist_ShouldReturnLogger()
        {
            var fixture = new Fixture();

            var loggerName = fixture.Create<string>();
            var logger = fixture.Create<Mock<ILogger>>();

            LogManager.Instance.SetLogger(loggerName, logger.Object);

            LogManager.Instance.GetLogger(loggerName).ShouldBe(logger.Object);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GetLogger_LoggerNameIsNull_ShouldThrowArgumentNullException(string loggerName)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => LogManager.Instance.GetLogger(loggerName));

            exception.ParamName.ShouldBe("loggerName");
        }

        [Fact]
        public void GetOrCreateLogger_CalledMultipleTimes_ShouldReturnSameLogger()
        {
            var fixture = new Fixture();

            var loggerName = fixture.Create<string>();

            var result1 = LogManager.Instance.GetOrCreateLogger(loggerName);
            var result2 = LogManager.Instance.GetOrCreateLogger(loggerName);

            result1.ShouldBe(result2);
            result2.ShouldBe(result1);
        }

        [Fact]
        public void GetOrCreateLogger_LoggerDoesNotExist_ShouldCreateLogger()
        {
            var fixture = new Fixture();

            var result = LogManager.Instance.GetOrCreateLogger(fixture.Create<string>());

            result.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GetOrCreateLogger_LoggerNameIsNull_ShouldThrowArgumentNullException(string loggerName)
        {
            var exception =
                Assert.Throws<ArgumentNullException>(() => LogManager.Instance.GetOrCreateLogger(loggerName));

            exception.ParamName.ShouldBe("loggerName");
        }

        [Fact]
        public void Instance_CalledMultipleTimes_ShouldReturnSameInstance()
        {
            var instance1 = LogManager.Instance;
            var instance2 = LogManager.Instance;

            instance1.ShouldBe(instance2);
            instance2.ShouldBe(instance1);
        }

        [Fact]
        public void Instance_InstanceNotSet_ShouldReturnInstance()
        {
            LogManager.Instance.ShouldNotBeNull();
        }

        [Fact]
        public void RemoveLogger_LoggerExists_LoggerIsRemoved()
        {
            var fixture = new Fixture();

            var loggerName = fixture.Create<string>();

            LogManager.Instance.SetLogger(loggerName, fixture.Create<Mock<ILogger>>().Object);

            LogManager.Instance.RemoveLogger(loggerName);

            LogManager.Instance.GetLogger(loggerName).ShouldBeNull();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void RemoveLogger_LoggerNameIsNull_ShouldThrowArgumentNullException(string loggerName)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => LogManager.Instance.RemoveLogger(loggerName));

            exception.ParamName.ShouldBe("loggerName");
        }

        [Fact]
        public void SetLogger_LoggerAndLoggerNameSet_ShouldSetLogger()
        {
            var fixture = new Fixture();

            var loggerName = fixture.Create<string>();
            var logger = fixture.Create<Mock<ILogger>>();

            LogManager.Instance.SetLogger(loggerName, logger.Object);

            var result = LogManager.Instance.GetOrCreateLogger(loggerName);

            result.ShouldBe(logger.Object);
        }

        [Fact]
        public void SetLogger_LoggerIsNull_ShouldThrowArgumentNullException()
        {
            var fixture = new Fixture();

            var exception =
                Assert.Throws<ArgumentNullException>(
                    () => LogManager.Instance.SetLogger(fixture.Create<string>(), null));

            exception.ParamName.ShouldBe("logger");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void SetLogger_LoggerNameIsNull_ShouldThrowArgumentNullException(string loggerName)
        {
            var fixture = new Fixture();

            var exception = Assert.Throws<ArgumentNullException>(
                () => LogManager.Instance.SetLogger(loggerName, fixture.Create<Mock<ILogger>>().Object));

            exception.ParamName.ShouldBe("loggerName");
        }
    }
}
