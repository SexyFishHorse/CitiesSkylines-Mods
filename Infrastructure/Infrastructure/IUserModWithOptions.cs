namespace SexyFishHorse.CitiesSkylines.Infrastructure
{
    using System.Diagnostics.CodeAnalysis;
    using ICities;
    using JetBrains.Annotations;

    /// <summary>
    /// This interface can only be applied to IUserMod classes. It provides the method signature for the settings UI.
    /// </summary>
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [SuppressMessage("ReSharper", "UnusedTypeParameter", Justification = "Used only for making sure the interface is being used on the correct class.")]
    public interface IUserModWithOptions : IUserMod
    {
        // ReSharper disable once InconsistentNaming
        void OnSettingsUI(UIHelperBase uiHelperBase);
    }
}
