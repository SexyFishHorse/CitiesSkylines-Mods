namespace SexyFishHorse.CitiesSkylines.Infrastructure.UI.Extensions
{
    using System.Linq;
    using ColossalFramework.UI;
    using JetBrains.Annotations;
    using Validation.Arguments;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class UiSliderExtensions
    {
        [NotNull]
        public static UILabel GetLabel(this UISlider slider)
        {
            var panel = slider.GetComponentInParent<UIPanel>();

            return panel.Find<UILabel>("Label");
        }

        [CanBeNull]
        public static string GetLabelText(this UISlider slider)
        {
            var uiLabel = slider.GetLabel();

            return uiLabel.text;
        }

        [StringFormatMethod("label")]
        public static void SetLabelText(this UISlider slider, [NotNull] string label, params object[] args)
        {
            label.ShouldNotBeNullOrEmpty("label");

            var uiLabel = slider.GetLabel();

            if (args != null && args.Any())
            {
                label = string.Format(label, args);
            }

            uiLabel.text = label;
        }
    }
}
