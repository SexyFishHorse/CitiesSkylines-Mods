namespace SexyFishHorse.CitiesSkylines.Infrastructure.UnitTest.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using FluentAssertions;
    using Moq;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Configuration;
    using Xunit;

    public class ConfigurationManagerClass
    {
        private readonly IFixture _fixture;

        protected ConfigurationManagerClass()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        public class GetSettingMethod : ConfigurationManagerClass
        {
            [Fact]
            public void ShouldLoadConfigFromFileOnFirstCall()
            {
                var configStore = _fixture.Freeze<Mock<IConfigStore>>();
                var instance = _fixture.Freeze<ConfigurationManager>();

                instance.GetSetting<string>(_fixture.Create<string>());

                configStore.Verify(x => x.LoadConfigFromFile(), Times.Once);
            }

            [Fact]
            public void ShouldOnlyLoadFromFileOnceOnMultipleCalls()
            {
                var configStore = _fixture.Freeze<Mock<IConfigStore>>();
                var instance = _fixture.Freeze<ConfigurationManager>();

                instance.GetSetting<string>(_fixture.Create<string>());
                instance.GetSetting<string>(_fixture.Create<string>());

                configStore.Verify(x => x.LoadConfigFromFile(), Times.Once);
            }

            [Fact]
            public void ShouldReturnDefaultValueForNonExistentSetting()
            {
                var modConfiguration = _fixture.Create<ModConfiguration>();

                var key = _fixture.Create<string>();
                if (modConfiguration.Settings.Any(x => x.Key == key))
                {
                    modConfiguration.Settings.Remove(modConfiguration.Settings.Single(x => x.Key == key));
                }

                var configStore = _fixture.Freeze<Mock<IConfigStore>>();
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfiguration);
                var instance = _fixture.Freeze<ConfigurationManager>();

                var setting = instance.GetSetting<string>(key);

                setting.Should().Be(default(string));
            }

            [Fact]
            public void ShouldReturnValueIfExistsAndTypesMatch()
            {
                var modConfiguration = _fixture.Create<ModConfiguration>();

                var key = _fixture.Create<string>();
                var value = _fixture.Create<string>();
                modConfiguration.Settings.Add(new KeyValuePair<string, object>(key, value));

                var configStore = _fixture.Freeze<Mock<IConfigStore>>();
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfiguration);
                var instance = _fixture.Freeze<ConfigurationManager>();

                var setting = instance.GetSetting<string>(key);

                setting.Should().Be(value);
            }

            [Fact]
            public void ShouldThrowInvalidCastExceptionWhenTryingToCastToInvalidType()
            {
                var modConfiguration = _fixture.Create<ModConfiguration>();

                var key = _fixture.Create<string>();
                modConfiguration.Settings.Add(new KeyValuePair<string, object>(key, "myValue"));

                var configStore = _fixture.Freeze<Mock<IConfigStore>>();
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfiguration);
                var instance = _fixture.Freeze<ConfigurationManager>();

                instance.Invoking(x => x.GetSetting<int>(key))
                        .Should().Throw<InvalidCastException>()
                        .WithMessage("Tried to cast value 'myValue' of type string to Int32.");
            }
        }

        public class MigrateKeyMethod : ConfigurationManagerClass
        {
            [Fact]
            public void ShouldAddNewSetting()
            {
                var oldSettingKey = _fixture.Create<string>();
                var newSettingKey = _fixture.Create<string>();
                var value = _fixture.Create<string>();
                var modConfig = _fixture.Create<ModConfiguration>();
                modConfig.Settings.Add(new KeyValuePair<string, object>(oldSettingKey, value));

                _fixture.Freeze<Mock<IConfigStore>>().Setup(x => x.LoadConfigFromFile()).Returns(modConfig);

                var instance = _fixture.Freeze<ConfigurationManager>();

                instance.MigrateKey<string>(oldSettingKey, newSettingKey);

                modConfig.Settings.Should().Contain(x => x.Key == newSettingKey && x.Value.Equals(value));
            }

            [Fact]
            public void ShouldCallSave()
            {
                var oldSettingKey = _fixture.Create<string>();
                var newSettingKey = _fixture.Create<string>();
                var value = _fixture.Create<string>();
                var modConfig = _fixture.Create<ModConfiguration>();
                modConfig.Settings.Add(new KeyValuePair<string, object>(oldSettingKey, value));

                var configStore = _fixture.Freeze<Mock<IConfigStore>>();
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfig);

                var instance = _fixture.Freeze<ConfigurationManager>();

                instance.MigrateKey<string>(oldSettingKey, newSettingKey);

                configStore.Verify(x => x.SaveConfigToFile(It.IsAny<ModConfiguration>()), Times.AtLeastOnce);
            }

            [Fact]
            public void ShouldLoadConfigFromDiscOnFirstCall()
            {
                var configStore = _fixture.Freeze<Mock<IConfigStore>>();
                var instance = _fixture.Freeze<ConfigurationManager>();

                instance.MigrateKey<string>(_fixture.Create<string>(), _fixture.Create<string>());

                configStore.Verify(x => x.LoadConfigFromFile(), Times.Once);
            }

            [Fact]
            public void ShouldNotCallSaveWhenSettingIsNotFound()
            {
                var configStore = _fixture.Freeze<Mock<IConfigStore>>();
                var instance = _fixture.Freeze<ConfigurationManager>();

                instance.MigrateKey<string>(_fixture.Create<string>(), _fixture.Create<string>());

                configStore.Verify(x => x.SaveConfigToFile(It.IsAny<ModConfiguration>()), Times.Never);
            }

            [Fact]
            public void ShouldRemoveOldSetting()
            {
                var oldSettingKey = _fixture.Create<string>();
                var modConfig = _fixture.Create<ModConfiguration>();
                modConfig.Settings.Add(new KeyValuePair<string, object>(oldSettingKey, _fixture.Create<string>()));

                _fixture.Freeze<Mock<IConfigStore>>().Setup(x => x.LoadConfigFromFile()).Returns(modConfig);

                var instance = _fixture.Freeze<ConfigurationManager>();

                instance.MigrateKey<string>(oldSettingKey, _fixture.Create<string>());

                modConfig.Settings.Should().NotContain(x => x.Key == oldSettingKey);
            }
        }

        public class MigrateTypeMethod : ConfigurationManagerClass
        {
            [Fact]
            public void ShouldChangeTypeOfValue()
            {
                var configStore = _fixture.Freeze<Mock<IConfigStore>>();
                var instance = _fixture.Freeze<ConfigurationManager>();

                var intValue = _fixture.Create<int>();
                var keyValuePair = new KeyValuePair<string, object>(_fixture.Create<string>(), intValue.ToString());
                var modConfig = _fixture.Build<ModConfiguration>().WithAutoProperties().Create();
                modConfig.Settings.Add(keyValuePair);
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfig);

                instance.MigrateType<string, int>(keyValuePair.Key, Convert.ToInt32);

                modConfig.Settings.Should().Contain(x => x.Key == keyValuePair.Key && (int)x.Value == intValue);
            }

            [Fact]
            public void ShouldLoadConfigFromDiscOnFirstCall()
            {
                var configStore = _fixture.Freeze<Mock<IConfigStore>>();
                var instance = _fixture.Freeze<ConfigurationManager>();

                instance.MigrateType<string, int>(_fixture.Create<string>(), Convert.ToInt32);

                configStore.Verify(x => x.LoadConfigFromFile(), Times.Once);
            }

            [Fact]
            public void ShouldNotCallSaveWhenSettingIsOfCorrectType()
            {
                var configStore = _fixture.Freeze<Mock<IConfigStore>>();
                var instance = _fixture.Freeze<ConfigurationManager>();

                var keyValuePair = new KeyValuePair<string, object>(_fixture.Create<string>(), _fixture.Create<int>());
                var modConfig = _fixture.Build<ModConfiguration>().WithAutoProperties().Create();
                modConfig.Settings.Add(keyValuePair);
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfig);

                instance.MigrateType<string, int>(keyValuePair.Key, Convert.ToInt32);

                configStore.Verify(x => x.SaveConfigToFile(It.IsAny<ModConfiguration>()), Times.Never);
            }

            [Fact]
            public void ShouldNotCallSaveWhenSettingNotFound()
            {
                var configStore = _fixture.Freeze<Mock<IConfigStore>>();
                var instance = _fixture.Freeze<ConfigurationManager>();

                instance.MigrateType<string, int>(_fixture.Create<string>(), Convert.ToInt32);

                configStore.Verify(x => x.SaveConfigToFile(It.IsAny<ModConfiguration>()), Times.Never);
            }

            [Fact]
            public void ShouldSaveSetting()
            {
                var configStore = _fixture.Freeze<Mock<IConfigStore>>();
                var instance = _fixture.Freeze<ConfigurationManager>();

                var intValue = _fixture.Create<int>();
                var keyValuePair = new KeyValuePair<string, object>(_fixture.Create<string>(), intValue.ToString());
                var modConfig = _fixture.Build<ModConfiguration>().WithAutoProperties().Create();
                modConfig.Settings.Add(keyValuePair);
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfig);

                instance.MigrateType<string, int>(keyValuePair.Key, Convert.ToInt32);

                configStore.Verify(x => x.SaveConfigToFile(It.IsAny<ModConfiguration>()), Times.Once);
            }
        }
    }
}
