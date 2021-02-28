namespace SexyFishHorse.CitiesSkylines.Infrastructure
{
    using System.Diagnostics.CodeAnalysis;
    using ICities;
    using JetBrains.Annotations;

    /// <summary>
    /// This interface can only be applied to IUserMod classes. It provides the method signature for the settings UI.
    /// </summary>
    /// <typeparam name="T">Your IUserMod implementation. This type must implement or inherit from <see cref="IUserMod"/></typeparam>
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [SuppressMessage("ReSharper", "UnusedTypeParameter", Justification = "Used only for making sure the interface is being used on the correct class.")]
    public interface IUserModWithOptionsPanel<T> where T : IUserMod
    {
        // ReSharper disable once InconsistentNaming
        void OnSettingsUI(UIHelperBase uiHelperBase);
    }
}
