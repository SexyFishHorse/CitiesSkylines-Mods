namespace SexyFishHorse.CitiesSkylines.Steamy.UnitTests
{
    using System;
    using FluentAssertions;
    using ICities;
    using Moq;
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using AutoFixture.Kernel;
    using SexyFishHorse.CitiesSkylines.Logger;
    using Xunit;

    [Trait("Category", "UnitTest")]
    public class TheSteamyUserModClass
    {
        private readonly IFixture fixture;

        protected TheSteamyUserModClass()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());
            fixture.Customize<SteamyUserMod>(c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));
        }

        public class TheConstructor : TheSteamyUserModClass
        {
            [Fact]
            public void ShouldLogExceptionAndRethrow()
            {
                var logger = fixture.Freeze<Mock<ILogger>>();
                var steamController = fixture.Freeze<Mock<ISteamController>>();

                steamController.Setup(x => x.UpdateAchievementsStatus()).Throws<Exception>();

                Action act = () => fixture.Create<SteamyUserMod>();

                act.Should().Throw<Exception>();

                logger.Verify(x => x.LogException(It.IsAny<Exception>()), Times.Once);
            }

            [Fact]
            public void ShouldUpdateAchievementStatus()
            {
                var steamController = fixture.Freeze<Mock<ISteamController>>();

                fixture.Create<SteamyUserMod>();

                steamController.Verify(x => x.UpdateAchievementsStatus(), Times.Once);
            }

            [Fact]
            public void ShouldUpdatePopupPosition()
            {
                var steamController = fixture.Freeze<Mock<ISteamController>>();

                fixture.Create<SteamyUserMod>();

                steamController.Verify(x => x.UpdatePopupPosition(), Times.Once);
            }
        }

        public class TheOnCreatedMethod : TheSteamyUserModClass
        {
            [Fact]
            public void ShouldUpdateAchievementStatus()
            {
                var steamController = fixture.Freeze<Mock<ISteamController>>();

                var instance = fixture.Freeze<SteamyUserMod>();

                instance.OnCreated(fixture.Create<ILoading>());

                steamController.Verify(x => x.UpdateAchievementsStatus(), Times.Exactly(2)); // Constructor and OnCreated
            }

            [Fact]
            public void ShouldUpdatePopupPosition()
            {
                var steamController = fixture.Freeze<Mock<ISteamController>>();

                var instance = fixture.Freeze<SteamyUserMod>();

                instance.OnCreated(fixture.Create<ILoading>());

                steamController.Verify(x => x.UpdatePopupPosition(), Times.Exactly(2)); // Constructor and OnCreated
            }
        }

        public class TheOnLevelLoadedMethod : TheSteamyUserModClass
        {
            [Fact]
            public void ShouldUpdateAchievementStatus()
            {
                var steamController = fixture.Freeze<Mock<ISteamController>>();

                var instance = fixture.Freeze<SteamyUserMod>();

                instance.OnLevelLoaded(fixture.Create<LoadMode>());

                steamController.Verify(x => x.UpdateAchievementsStatus(), Times.Exactly(2)); // Constructor and OnCreated
            }

            [Fact]
            public void ShouldUpdatePopupPosition()
            {
                var steamController = fixture.Freeze<Mock<ISteamController>>();

                var instance = fixture.Freeze<SteamyUserMod>();

                instance.OnLevelLoaded(fixture.Create<LoadMode>());

                steamController.Verify(x => x.UpdatePopupPosition(), Times.Exactly(2)); // Constructor and OnCreated
            }
        }
    }
}
