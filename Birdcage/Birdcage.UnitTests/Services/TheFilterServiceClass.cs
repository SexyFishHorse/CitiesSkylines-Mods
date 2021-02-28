namespace SexyFishHorse.CitiesSkylines.Birdcage.UnitTests.Services
{
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using FluentAssertions;
    using ICities;
    using Moq;
    using SexyFishHorse.CitiesSkylines.Birdcage.Services;
    using SexyFishHorse.CitiesSkylines.Birdcage.Wrappers;
    using UnityEngine;
    using Xunit;

    public class TheFilterServiceClass
    {
        private readonly Fixture fixture;

        protected TheFilterServiceClass()
        {
            fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
        }

        public class TheHandleNewMessageMethod : TheFilterServiceClass
        {
            [Theory]
            [InlineData(LocaleID.CHIRP_CHEAP_FLOWERS)]
            [InlineData(LocaleID.CHIRP_BAND_LILY)]
            [InlineData(LocaleID.CHIRP_FIRST_BUS_DEPOT)]
            [InlineData(LocaleID.CHIRP_RANDOM)]
            public void ShouldFilterUnimportantMessages(string messageId)
            {
                var instance = fixture.Create<FilterService>();

                var message = fixture.Build<CitizenMessage>().With(x => x.m_messageID, messageId).Create();

                instance.HandleNewMessage(message);

                instance.MessagesToRemove.Should()
                        .HaveCount(1, "because one message has to be removed")
                        .And.Contain(message, "because this is the message to remove");
            }

            [Theory]
            [InlineData(LocaleID.CHIRP_ABANDONED_BUILDINGS)]
            [InlineData(LocaleID.CHIRP_DISASTER)]
            [InlineData(LocaleID.CHIRP_MILESTONE_REACHED)]
            [InlineData(LocaleID.CHIRP_TRASH_PILING_UP)]
            public void ShouldNotFilterImportantMessages(string messageId)
            {
                var instance = fixture.Create<FilterService>();

                var message = fixture.Build<CitizenMessage>().With(x => x.m_messageID, messageId).Create();

                instance.HandleNewMessage(message);

                instance.MessagesToRemove.Should().HaveCount(0, "because this message should not be removed");
            }

            [Fact]
            public void ShouldNotHandleGenericMessages()
            {
                var instance = fixture.Create<FilterService>();

                instance.HandleNewMessage(fixture.Create<GenericMessage>());

                instance.MessagesToRemove.Should().HaveCount(0, "because generic messages should not be removed");
            }

            [Fact]
            public void ShouldNotRemoveNotificationSoundForUnfilteredMessages()
            {
                var chirpPanel = fixture.Freeze<Mock<IChirpPanelWrapper>>();

                var instance = fixture.Create<FilterService>();

                var message = fixture.Build<CitizenMessage>().With(x => x.m_messageID, LocaleID.CHIRP_DISASTER).Create();

                instance.HandleNewMessage(message);

                chirpPanel.Verify(x => x.RemoveNotificationSound(), Times.Never);
            }

            [Fact]
            public void ShouldRemoveNotificationSoundForFilteredMessages()
            {
                var chirpPanel = fixture.Freeze<Mock<IChirpPanelWrapper>>();

                var instance = fixture.Create<FilterService>();

                var message = fixture.Build<CitizenMessage>().With(x => x.m_messageID, LocaleID.CHIRP_RANDOM).Create();

                instance.HandleNewMessage(message);

                chirpPanel.Verify(x => x.RemoveNotificationSound(), Times.Once);
            }
        }

        public class TheRemovePendingMessagesMethod : TheFilterServiceClass
        {
            [Fact]
            public void ShouldClearPendingMessages()
            {
                var instance = fixture.Create<FilterService>();
                instance.MessagesToRemove.Add(fixture.Create<IChirperMessage>());

                instance.RemovePendingMessages(new AudioClip());

                instance.MessagesToRemove.Should().HaveCount(0, "because all messages should have been removed");
            }

            [Fact]
            public void ShouldNotCollapsePanelIfThereAreNoMessagesToRemove()
            {
                var chirpPanel = fixture.Freeze<Mock<IChirpPanelWrapper>>();
                var instance = fixture.Create<FilterService>();

                instance.RemovePendingMessages(new AudioClip());

                chirpPanel.Verify(x => x.CollapsePanel(), Times.Never);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            public void ShouldRemoveAllPendingMessages(int numberOfMessages)
            {
                var messageManager = fixture.Freeze<Mock<IMessageManagerWrapper>>();

                var instance = fixture.Create<FilterService>();
                instance.MessagesToRemove.AddMany(fixture.Create<IChirperMessage>, numberOfMessages);

                instance.RemovePendingMessages(new AudioClip());

                messageManager.Verify(x => x.DeleteMessage(It.IsAny<IChirperMessage>()), Times.Exactly(numberOfMessages));
            }

            [Theory]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            public void ShouldSetTheNotificationSoundOnce(int numberOfMessages)
            {
                var chirpPanel = fixture.Freeze<Mock<IChirpPanelWrapper>>();

                var instance = fixture.Create<FilterService>();
                instance.MessagesToRemove.AddMany(fixture.Create<IChirperMessage>, numberOfMessages);

                var notificationSound = new AudioClip();

                instance.RemovePendingMessages(notificationSound);

                chirpPanel.Verify(x => x.SetNotificationSound(notificationSound), Times.Once);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            public void ShouldSynchronizeMessagesOnce(int numberOfMessages)
            {
                var chirpPanel = fixture.Freeze<Mock<IChirpPanelWrapper>>();

                var instance = fixture.Create<FilterService>();
                instance.MessagesToRemove.AddMany(fixture.Create<IChirperMessage>, numberOfMessages);

                instance.RemovePendingMessages(new AudioClip());

                chirpPanel.Verify(x => x.SynchronizeMessages(numberOfMessages), Times.Once);
            }
        }
    }
}
