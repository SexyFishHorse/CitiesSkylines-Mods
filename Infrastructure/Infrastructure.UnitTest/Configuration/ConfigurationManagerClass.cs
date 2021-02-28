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
        private readonly IFixture fixture;

        protected ConfigurationManagerClass()
        {
            fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
        }

        public class GetSettingMethod : ConfigurationManagerClass
        {
            [Fact]
            public void ShouldLoadConfigFromFileOnFirstCall()
            {
                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                var instance = fixture.Freeze<ConfigurationManager>();

                instance.GetSetting<string>(fixture.Create<string>());

                configStore.Verify(x => x.LoadConfigFromFile(), Times.Once);
            }

            [Fact]
            public void ShouldOnlyLoadFromFileOnceOnMultipleCalls()
            {
                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                var instance = fixture.Freeze<ConfigurationManager>();

                instance.GetSetting<string>(fixture.Create<string>());
                instance.GetSetting<string>(fixture.Create<string>());

                configStore.Verify(x => x.LoadConfigFromFile(), Times.Once);
            }

            [Fact]
            public void ShouldReturnDefaultValueForNonExistentSetting()
            {
                var modConfiguration = fixture.Create<ModConfiguration>();

                var key = fixture.Create<string>();
                if (modConfiguration.Settings.Any(x => x.Key == key))
                {
                    modConfiguration.Settings.Remove(modConfiguration.Settings.Single(x => x.Key == key));
                }

                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfiguration);
                var instance = fixture.Freeze<ConfigurationManager>();

                var setting = instance.GetSetting<string>(key);

                setting.Should().Be(default(string));
            }

            [Fact]
            public void ShouldReturnValueIfExistsAndTypesMatch()
            {
                var modConfiguration = fixture.Create<ModConfiguration>();

                var key = fixture.Create<string>();
                var value = fixture.Create<string>();
                modConfiguration.Settings.Add(new KeyValuePair<string, object>(key, value));

                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfiguration);
                var instance = fixture.Freeze<ConfigurationManager>();

                var setting = instance.GetSetting<string>(key);

                setting.Should().Be(value);
            }

            [Fact]
            public void ShouldThrowInvalidCastExceptionWhenTryingToCastToInvalidType()
            {
                var modConfiguration = fixture.Create<ModConfiguration>();

                var key = fixture.Create<string>();
                modConfiguration.Settings.Add(new KeyValuePair<string, object>(key, "myValue"));

                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfiguration);
                var instance = fixture.Freeze<ConfigurationManager>();

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
                var oldSettingKey = fixture.Create<string>();
                var newSettingKey = fixture.Create<string>();
                var value = fixture.Create<string>();
                var modConfig = fixture.Create<ModConfiguration>();
                modConfig.Settings.Add(new KeyValuePair<string, object>(oldSettingKey, value));

                fixture.Freeze<Mock<IConfigStore>>().Setup(x => x.LoadConfigFromFile()).Returns(modConfig);

                var instance = fixture.Freeze<ConfigurationManager>();

                instance.MigrateKey<string>(oldSettingKey, newSettingKey);

                modConfig.Settings.Should().Contain(x => x.Key == newSettingKey && x.Value.Equals(value));
            }

            [Fact]
            public void ShouldCallSave()
            {
                var oldSettingKey = fixture.Create<string>();
                var newSettingKey = fixture.Create<string>();
                var value = fixture.Create<string>();
                var modConfig = fixture.Create<ModConfiguration>();
                modConfig.Settings.Add(new KeyValuePair<string, object>(oldSettingKey, value));

                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfig);

                var instance = fixture.Freeze<ConfigurationManager>();

                instance.MigrateKey<string>(oldSettingKey, newSettingKey);

                configStore.Verify(x => x.SaveConfigToFile(It.IsAny<ModConfiguration>()), Times.AtLeastOnce);
            }

            [Fact]
            public void ShouldLoadConfigFromDiscOnFirstCall()
            {
                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                var instance = fixture.Freeze<ConfigurationManager>();

                instance.MigrateKey<string>(fixture.Create<string>(), fixture.Create<string>());

                configStore.Verify(x => x.LoadConfigFromFile(), Times.Once);
            }

            [Fact]
            public void ShouldNotCallSaveWhenSettingIsNotFound()
            {
                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                var instance = fixture.Freeze<ConfigurationManager>();

                instance.MigrateKey<string>(fixture.Create<string>(), fixture.Create<string>());

                configStore.Verify(x => x.SaveConfigToFile(It.IsAny<ModConfiguration>()), Times.Never);
            }

            [Fact]
            public void ShouldRemoveOldSetting()
            {
                var oldSettingKey = fixture.Create<string>();
                var modConfig = fixture.Create<ModConfiguration>();
                modConfig.Settings.Add(new KeyValuePair<string, object>(oldSettingKey, fixture.Create<string>()));

                fixture.Freeze<Mock<IConfigStore>>().Setup(x => x.LoadConfigFromFile()).Returns(modConfig);

                var instance = fixture.Freeze<ConfigurationManager>();

                instance.MigrateKey<string>(oldSettingKey, fixture.Create<string>());

                modConfig.Settings.Should().NotContain(x => x.Key == oldSettingKey);
            }
        }

        public class MigrateTypeMethod : ConfigurationManagerClass
        {
            [Fact]
            public void ShouldChangeTypeOfValue()
            {
                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                var instance = fixture.Freeze<ConfigurationManager>();

                var intValue = fixture.Create<int>();
                var keyValuePair = new KeyValuePair<string, object>(fixture.Create<string>(), intValue.ToString());
                var modConfig = fixture.Build<ModConfiguration>().WithAutoProperties().Create();
                modConfig.Settings.Add(keyValuePair);
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfig);

                instance.MigrateType<string, int>(keyValuePair.Key, Convert.ToInt32);

                modConfig.Settings.Should().Contain(x => x.Key == keyValuePair.Key && (int)x.Value == intValue);
            }

            [Fact]
            public void ShouldLoadConfigFromDiscOnFirstCall()
            {
                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                var instance = fixture.Freeze<ConfigurationManager>();

                instance.MigrateType<string, int>(fixture.Create<string>(), Convert.ToInt32);

                configStore.Verify(x => x.LoadConfigFromFile(), Times.Once);
            }

            [Fact]
            public void ShouldNotCallSaveWhenSettingIsOfCorrectType()
            {
                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                var instance = fixture.Freeze<ConfigurationManager>();

                var keyValuePair = new KeyValuePair<string, object>(fixture.Create<string>(), fixture.Create<int>());
                var modConfig = fixture.Build<ModConfiguration>().WithAutoProperties().Create();
                modConfig.Settings.Add(keyValuePair);
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfig);

                instance.MigrateType<string, int>(keyValuePair.Key, Convert.ToInt32);

                configStore.Verify(x => x.SaveConfigToFile(It.IsAny<ModConfiguration>()), Times.Never);
            }

            [Fact]
            public void ShouldNotCallSaveWhenSettingNotFound()
            {
                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                var instance = fixture.Freeze<ConfigurationManager>();

                instance.MigrateType<string, int>(fixture.Create<string>(), Convert.ToInt32);

                configStore.Verify(x => x.SaveConfigToFile(It.IsAny<ModConfiguration>()), Times.Never);
            }

            [Fact]
            public void ShouldSaveSetting()
            {
                var configStore = fixture.Freeze<Mock<IConfigStore>>();
                var instance = fixture.Freeze<ConfigurationManager>();

                var intValue = fixture.Create<int>();
                var keyValuePair = new KeyValuePair<string, object>(fixture.Create<string>(), intValue.ToString());
                var modConfig = fixture.Build<ModConfiguration>().WithAutoProperties().Create();
                modConfig.Settings.Add(keyValuePair);
                configStore.Setup(x => x.LoadConfigFromFile()).Returns(modConfig);

                instance.MigrateType<string, int>(keyValuePair.Key, Convert.ToInt32);

                configStore.Verify(x => x.SaveConfigToFile(It.IsAny<ModConfiguration>()), Times.Once);
            }
        }
    }
}
