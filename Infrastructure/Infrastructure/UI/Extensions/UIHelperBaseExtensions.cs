namespace SexyFishHorse.CitiesSkylines.Infrastructure.UI.Extensions
{
    using System;
    using System.Reflection;
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
            var field = uiHelper.UiHelper.GetType().GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);

            if (field == null)
            {
                throw new InvalidOperationException("m_Root field not found on the UI helper.");
            }

            var root = (UIComponent)field.GetValue(uiHelper.UiHelper);

            var label = root.AddUIComponent<UILabel>();
            label.text = text;

            return label;
        }
    }
}
