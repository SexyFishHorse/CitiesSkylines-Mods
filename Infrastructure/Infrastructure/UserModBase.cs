namespace SexyFishHorse.CitiesSkylines.Infrastructure
{
    using ICities;
    using JetBrains.Annotations;
    using UI;
    using UI.Configuration;
    using UI.Extensions;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public abstract class UserModBase : IUserMod
    {
        public abstract string Description { get; }

        public abstract string Name { get; }

        protected IOptionsPanelManager OptionsPanelManager { get; set; }

        public void OnSettingsUI(UIHelperBase uiHelperBase)
        {
            var uiHelper = uiHelperBase.AsStronglyTyped();

            ConfigureOptionsPanel(uiHelper);
        }

        protected void ConfigureOptionsPanel(IStronglyTypedUIHelper uiHelper)
        {
            if (OptionsPanelManager != null)
            {
                OptionsPanelManager.ConfigureOptionsPanel(uiHelper);
            }
        }
    }
}
