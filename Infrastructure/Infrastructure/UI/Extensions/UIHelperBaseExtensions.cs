namespace SexyFishHorse.CitiesSkylines.Infrastructure.UI.Extensions
{
    using ColossalFramework.UI;
    using ICities;
    using JetBrains.Annotations;

    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public static class UIHelperBaseExtensions
    {
        [NotNull]
        public static IStronglyTypedUIHelper AsStronglyTyped([NotNull] this UIHelperBase uiHelper)
        {
            return new StronglyTypedUIHelper(uiHelper);
        }

        [NotNull]
        public static UILabel AddLabel(this IStronglyTypedUIHelper uiHelper, string text)
        {
            var label = uiHelper.Component.AddUIComponent<UILabel>();
            label.text = text;

            return label;
        }
    }
}
