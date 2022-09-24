namespace SexyFishHorse.CitiesSkylines.Infrastructure
{
    using ICities;
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Infrastructure.DependencyInjection;
    using SexyFishHorse.CitiesSkylines.Infrastructure.UI.Configuration;
    using SexyFishHorse.CitiesSkylines.Infrastructure.UI.Extensions;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public abstract class UserModWithOptionsBase : IUserModWithOptions
    {
        private readonly IOptionsPanelManager _optionsPanel;

        protected UserModWithOptionsBase(string name, string description, IServiceProvider services)
        {
            Name = name;
            Description = description;
            _optionsPanel = services.GetService<IOptionsPanelManager>();
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public void OnSettingsUI(UIHelperBase uiHelperBase)
        {
            var uiHelper = uiHelperBase.AsStronglyTyped();

            _optionsPanel.ConfigureOptionsPanel(uiHelper);
        }
    }
}
