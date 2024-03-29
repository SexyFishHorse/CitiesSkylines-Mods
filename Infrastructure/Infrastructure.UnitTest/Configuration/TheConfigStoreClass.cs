﻿namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using FluentAssertions;
    using Moq;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Configuration;
    using SexyFishHorse.CitiesSkylines.Infrastructure.IO;
    using Xunit;

    public class TheConfigStoreClass
    {
        private readonly Fixture _fixture;

        protected TheConfigStoreClass()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        private Mock<IXmlFileSystem<ModConfiguration>> SetModConfig(ModConfiguration modConfig)
        {
            var fileSystemWrapper = _fixture.Freeze<Mock<IXmlFileSystem<ModConfiguration>>>();
            fileSystemWrapper.Setup(x => x.FileExists(It.IsAny<FileInfo>())).Returns(true);
            fileSystemWrapper.Setup(x => x.GetFileAsObject(It.IsAny<FileInfo>())).Returns(modConfig);

            return fileSystemWrapper;
        }

        public class TheGetSettingMethod : TheConfigStoreClass
        {
            [Fact]
            public void ShouldLoadFileFromDisk()
            {
                var fileSystemWrapper = _fixture.Freeze<Mock<IXmlFileSystem<ModConfiguration>>>();
                fileSystemWrapper.Setup(x => x.FileExists(It.IsAny<FileInfo>())).Returns(true);

                var instance = _fixture.Create<ConfigStore>();

                instance.GetSetting<object>(_fixture.Create<string>());

                fileSystemWrapper.Verify(x => x.GetFileAsObject(It.IsAny<FileInfo>()), Times.Once);
            }

            [Fact]
            public void ShouldReturnDefaultValueIfFileIsMissing()
            {
                var instance = _fixture.Create<ConfigStore>();

                var setting = instance.GetSetting<string>(_fixture.Create<string>());

                setting.Should().BeNull();
            }

            [Fact]
            public void ShouldReturnValueFromFile()
            {
                var setting = _fixture.Create<KeyValuePair<string, object>>();
                var modConfig = _fixture.Create<ModConfiguration>();
                modConfig.Settings.Add(setting);

                SetModConfig(modConfig);

                var instance = _fixture.Create<ConfigStore>();

                var settingValue = instance.GetSetting<object>(setting.Key);

                settingValue.Should().Be(setting.Value);
            }

            [Fact]
            [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "For testing purposes.")]
            public void ShouldThrowExceptionIfKeyIsMissing()
            {
                var instance = _fixture.Create<ConfigStore>();

                Action act = () => instance.GetSetting<object>(null);

                act.Should().Throw<ArgumentNullException>();
            }
        }

        public class TheHasSettingMethod : TheConfigStoreClass
        {
            [Fact]
            public void ShouldReturnFalseIfSettingDoesNotExist()
            {
                var instance = _fixture.Create<ConfigStore>();

                instance.HasSetting(_fixture.Create<string>()).Should().BeFalse("because the setting does not exist");
            }

            [Fact]
            public void ShouldReturnTrueIfSettingExist()
            {
                var key = _fixture.Create<string>();

                var modConfig = _fixture.Create<ModConfiguration>();
                modConfig.Settings.Add(new KeyValuePair<string, object>(key, _fixture.Create<object>()));

                SetModConfig(modConfig);

                var instance = _fixture.Create<ConfigStore>();

                instance.HasSetting(key).Should().BeTrue("because the setting exist");
            }

            [Fact]
            [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "For testing purposes.")]
            public void ShouldThrowExceptionIfKeyIsNull()
            {
                var instance = _fixture.Create<ConfigStore>();

                Action act = () => instance.HasSetting(null);

                act.Should().Throw<ArgumentNullException>();
            }
        }

        public class TheRemoveSettingMethod : TheConfigStoreClass
        {
            [Fact]
            public void ShouldRemoveSettingFromFile()
            {
                var key = _fixture.Create<string>();

                var modConfig = _fixture.Create<ModConfiguration>();
                modConfig.Settings.Add(new KeyValuePair<string, object>(key, _fixture.Create<object>()));

                var fileSystemWrapper = SetModConfig(modConfig);

                var instance = _fixture.Create<ConfigStore>();

                instance.RemoveSetting(key);

                fileSystemWrapper.Verify(
                    x => x.SaveObjectToFile(
                        It.IsAny<FileInfo>(),
                        It.Is<ModConfiguration>(config => config.Settings.Any(s => s.Key == key) == false)),
                    Times.Once);
            }

            [Fact]
            [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "For testing purposes.")]
            public void ShouldThrowExceptionIfKeyIsNull()
            {
                var instance = _fixture.Create<ConfigStore>();

                Action act = () => instance.RemoveSetting(null);

                act.Should().Throw<ArgumentNullException>();
            }
        }

        public class TheSaveSettingMethod : TheConfigStoreClass
        {
            [Fact]
            public void ShouldOverwriteExistingSettings()
            {
                var setting = _fixture.Create<KeyValuePair<string, object>>();
                var newSetting = new KeyValuePair<string, object>(setting.Key, _fixture.Create<object>());
                var modConfig = _fixture.Create<ModConfiguration>();
                modConfig.Settings.Add(setting);

                var fileSystemWrapper = SetModConfig(modConfig);
                var instance = _fixture.Create<ConfigStore>();

                instance.SaveSetting(newSetting.Key, newSetting.Value);

                fileSystemWrapper.Verify(
                    x => x.SaveObjectToFile(
                        It.IsAny<FileInfo>(),
                        It.Is<ModConfiguration>(
                            config => config.Settings.Contains(setting) == false && config.Settings.Contains(newSetting))),
                    Times.Once);
            }

            [Fact]
            public void ShouldSaveSettingsToDisk()
            {
                var setting = _fixture.Create<KeyValuePair<string, object>>();

                var fileSystemWrapper = _fixture.Freeze<Mock<IXmlFileSystem<ModConfiguration>>>();
                var instance = _fixture.Create<ConfigStore>();

                instance.SaveSetting(setting.Key, setting.Value);

                fileSystemWrapper.Verify(
                    x => x.SaveObjectToFile(It.IsAny<FileInfo>(), It.Is<ModConfiguration>(config => config.Settings.Contains(setting))),
                    Times.Once);
            }

            [Fact]
            [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "For testing purposes.")]
            public void ShouldThrowExceptionIfKeyIsNull()
            {
                var instance = _fixture.Create<ConfigStore>();

                Action act = () => instance.SaveSetting(null, _fixture.Create<object>());

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("key");
            }

            [Fact]
            [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "For testing purposes.")]
            public void ShouldThrowExceptionIfValueIsNull()
            {
                var instance = _fixture.Create<ConfigStore>();

                Action act = () => instance.SaveSetting<object>(_fixture.Create<string>(), null);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("value");
            }
        }
    }
}
