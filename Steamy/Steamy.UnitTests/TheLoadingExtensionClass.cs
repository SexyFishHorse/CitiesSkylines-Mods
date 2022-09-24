namespace SexyFishHorse.CitiesSkylines.Steamy.UnitTests
{
    using System;
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using AutoFixture.Kernel;
    using FluentAssertions;
    using ICities;
    using Moq;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Logging;
    using Xunit;

    [Trait("Category", "UnitTest")]
    public class TheLoadingExtensionClass
    {
        private readonly IFixture _fixture;

        public TheLoadingExtensionClass()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Customize<LoadingExtension>(
                c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));
        }

        public class TheConstructor : TheLoadingExtensionClass
        {
            [Fact]
            public void ShouldLogExceptionAndRethrow()
            {
                var logger = _fixture.Freeze<Mock<ILogger>>();
                var steamController = _fixture.Freeze<Mock<ISteamController>>();

                steamController.Setup(x => x.UpdateAchievementsStatus()).Throws<Exception>();

                Action act = () => _fixture.Create<LoadingExtension>();

                act.Should().Throw<Exception>();

                logger.Verify(x => x.LogException(It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Once);
            }

            [Fact]
            public void ShouldUpdateAchievementStatus()
            {
                var steamController = _fixture.Freeze<Mock<ISteamController>>();

                _fixture.Create<LoadingExtension>();

                steamController.Verify(x => x.UpdateAchievementsStatus(), Times.Once);
            }

            [Fact]
            public void ShouldUpdatePopupPosition()
            {
                var steamController = _fixture.Freeze<Mock<ISteamController>>();

                _fixture.Create<LoadingExtension>();

                steamController.Verify(x => x.UpdatePopupPosition(), Times.Once);
            }
        }

        public class TheOnCreatedMethod : TheLoadingExtensionClass
        {
            [Fact]
            public void ShouldUpdateAchievementStatus()
            {
                var steamController = _fixture.Freeze<Mock<ISteamController>>();

                var instance = _fixture.Freeze<LoadingExtension>();

                instance.OnCreated(_fixture.Create<ILoading>());

                steamController.Verify(x => x.UpdateAchievementsStatus(),
                    Times.Exactly(2)); // Constructor and OnCreated
            }

            [Fact]
            public void ShouldUpdatePopupPosition()
            {
                var steamController = _fixture.Freeze<Mock<ISteamController>>();

                var instance = _fixture.Freeze<LoadingExtension>();

                instance.OnCreated(_fixture.Create<ILoading>());

                steamController.Verify(x => x.UpdatePopupPosition(), Times.Exactly(2)); // Constructor and OnCreated
            }
        }

        public class TheOnLevelLoadedMethod : TheLoadingExtensionClass
        {
            [Fact]
            public void ShouldUpdateAchievementStatus()
            {
                var steamController = _fixture.Freeze<Mock<ISteamController>>();

                var instance = _fixture.Freeze<LoadingExtension>();

                instance.OnLevelLoaded(_fixture.Create<LoadMode>());

                steamController.Verify(x => x.UpdateAchievementsStatus(),
                    Times.Exactly(2)); // Constructor and OnCreated
            }

            [Fact]
            public void ShouldUpdatePopupPosition()
            {
                var steamController = _fixture.Freeze<Mock<ISteamController>>();

                var instance = _fixture.Freeze<LoadingExtension>();

                instance.OnLevelLoaded(_fixture.Create<LoadMode>());

                steamController.Verify(x => x.UpdatePopupPosition(), Times.Exactly(2)); // Constructor and OnCreated
            }
        }
    }
}
