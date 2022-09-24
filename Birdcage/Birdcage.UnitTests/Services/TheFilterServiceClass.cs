namespace SexyFishHorse.CitiesSkylines.Birdcage.UnitTests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using FluentAssertions;
    using Helpers;
    using ICities;
    using Moq;
    using SexyFishHorse.CitiesSkylines.Birdcage.Services;
    using SexyFishHorse.CitiesSkylines.Birdcage.Wrappers;
    using UnityEngine;
    using Xunit;

    [Trait("Category", "UnitTest")]
    public class TheFilterServiceClass
    {
        private readonly Fixture _fixture;

        protected TheFilterServiceClass()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        public class TheHandleNewMessageMethod : TheFilterServiceClass
        {
            [Fact]
            public void ShouldMapAllMessageTypes()
            {
                var chirpsInGame = typeof(LocaleID).GetFields().Where(x => x.Name.Contains("CHIRP")).Select(x => x.Name).Where(FieldIsValidChirp);
                var mappedChirps = typeof(Chirps).GetFields(BindingFlags.Static | BindingFlags.Public).Where(x => x.FieldType == typeof(HashSet<string>)).Select(x => x.GetValue(null)).Cast<HashSet<string>>().SelectMany(x => x);

                var notMappedChirps = chirpsInGame.Except(mappedChirps);

                notMappedChirps.Should().BeEmpty("because all fields should be mapped");
            }

            private bool FieldIsValidChirp(string name)
            {
                string[] excludedFields = {
                    "BUILDING_STATUS_CHIRPYBIRTHDAY",
                    "WNP_CUPCAKE_MAPSCHIRPERHATS",
                    "CHIRPER_SELECTIMAGE",
                    "DISASTERNAME_CHIRPYNADO",
                    "TRIGGERPANEL_DEFAULTCHIRP",
                    "OPTIONS_CHIRPERVOLUME",
                    "OPTIONS_AUTOEXPAND_CHIRPER",
                    "CHIRPER_NAME",
                    "CHIRP_DEFAULT",
                };

                string[] excludedPrefixes = {
                    "CHIRPXPANEL_",
                    "EDITORCHIRPER_",
                    "CHIRPHEADER_",
                };

                return excludedFields.Contains(name) == false && excludedPrefixes.All(prefix => name.StartsWith(prefix) == false);
            }

            [Theory(Skip = "Rewrite test due to change in filter service")]
            [InlineData(LocaleID.CHIRP_CHEAP_FLOWERS)]
            [InlineData(LocaleID.CHIRP_BAND_LILY)]
            [InlineData(LocaleID.CHIRP_FIRST_BUS_DEPOT)]
            [InlineData(LocaleID.CHIRP_RANDOM)]
            public void ShouldFilterUnimportantMessages(string messageId)
            {
                var instance = _fixture.Create<FilterService>();

                var message = _fixture.Build<CitizenMessage>().With(x => x.m_messageID, messageId).Create();

                instance.HandleNewMessage(message);

                instance.MessagesToRemove.Should()
                    .HaveCount(1, "because one message has to be removed")
                    .And.Contain(message, "because this is the message to remove");
            }

            [Theory(Skip = "Rewrite test due to change in filter service")]
            [InlineData(LocaleID.CHIRP_ABANDONED_BUILDINGS)]
            [InlineData(LocaleID.CHIRP_DISASTER)]
            [InlineData(LocaleID.CHIRP_MILESTONE_REACHED)]
            [InlineData(LocaleID.CHIRP_TRASH_PILING_UP)]
            public void ShouldNotFilterImportantMessages(string messageId)
            {
                var instance = _fixture.Create<FilterService>();

                var message = _fixture.Build<CitizenMessage>().With(x => x.m_messageID, messageId).Create();

                instance.HandleNewMessage(message);

                instance.MessagesToRemove.Should().HaveCount(0, "because this message should not be removed");
            }

            [Fact(Skip = "Rewrite test due to change in filter service")]
            public void ShouldNotHandleGenericMessages()
            {
                var instance = _fixture.Create<FilterService>();

                instance.HandleNewMessage(_fixture.Create<GenericMessage>());

                instance.MessagesToRemove.Should().HaveCount(0, "because generic messages should not be removed");
            }

            [Fact(Skip = "Rewrite test due to change in filter service")]
            public void ShouldNotRemoveNotificationSoundForUnfilteredMessages()
            {
                var chirpPanel = _fixture.Freeze<Mock<IChirpPanelWrapper>>();

                var instance = _fixture.Create<FilterService>();

                var message = _fixture.Build<CitizenMessage>().With(x => x.m_messageID, LocaleID.CHIRP_DISASTER).Create();

                instance.HandleNewMessage(message);

                chirpPanel.Verify(x => x.RemoveNotificationSound(), Times.Never);
            }

            [Fact(Skip = "Rewrite test due to change in filter service")]
            public void ShouldRemoveNotificationSoundForFilteredMessages()
            {
                var chirpPanel = _fixture.Freeze<Mock<IChirpPanelWrapper>>();

                var instance = _fixture.Create<FilterService>();

                var message = _fixture.Build<CitizenMessage>().With(x => x.m_messageID, LocaleID.CHIRP_RANDOM).Create();

                instance.HandleNewMessage(message);

                chirpPanel.Verify(x => x.RemoveNotificationSound(), Times.Once);
            }
        }

        public class TheRemovePendingMessagesMethod : TheFilterServiceClass
        {
            [Fact]
            public void ShouldClearPendingMessages()
            {
                var instance = _fixture.Create<FilterService>();
                instance.MessagesToRemove.Add(_fixture.Create<IChirperMessage>());

                instance.RemovePendingMessages(new AudioClip());

                instance.MessagesToRemove.Should().HaveCount(0, "because all messages should have been removed");
            }

            [Fact]
            public void ShouldNotCollapsePanelIfThereAreNoMessagesToRemove()
            {
                var chirpPanel = _fixture.Freeze<Mock<IChirpPanelWrapper>>();
                var instance = _fixture.Create<FilterService>();

                instance.RemovePendingMessages(new AudioClip());

                chirpPanel.Verify(x => x.CollapsePanel(), Times.Never);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            public void ShouldRemoveAllPendingMessages(int numberOfMessages)
            {
                var messageManager = _fixture.Freeze<Mock<IMessageManagerWrapper>>();

                var instance = _fixture.Create<FilterService>();
                instance.MessagesToRemove.AddMany(_fixture.Create<IChirperMessage>, numberOfMessages);

                instance.RemovePendingMessages(new AudioClip());

                messageManager.Verify(x => x.DeleteMessage(It.IsAny<IChirperMessage>()), Times.Exactly(numberOfMessages));
            }

            [Theory(Skip = "Rewrite test due to change in filter service")]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            public void ShouldSetTheNotificationSoundOnce(int numberOfMessages)
            {
                var chirpPanel = _fixture.Freeze<Mock<IChirpPanelWrapper>>();

                var instance = _fixture.Create<FilterService>();
                instance.MessagesToRemove.AddMany(_fixture.Create<IChirperMessage>, numberOfMessages);

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
                var chirpPanel = _fixture.Freeze<Mock<IChirpPanelWrapper>>();

                var instance = _fixture.Create<FilterService>();
                instance.MessagesToRemove.AddMany(_fixture.Create<IChirperMessage>, numberOfMessages);

                instance.RemovePendingMessages(new AudioClip());

                chirpPanel.Verify(x => x.SynchronizeMessages(numberOfMessages), Times.Once);
            }
        }
    }
}
