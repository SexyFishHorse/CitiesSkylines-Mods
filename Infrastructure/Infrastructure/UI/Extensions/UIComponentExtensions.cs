namespace SexyFishHorse.CitiesSkylines.Infrastructure.UI.Extensions
{
    using ColossalFramework.UI;

    public static class UIComponentExtensions
    {
        public static TComponent WithTooltipLocaleId<TComponent>(this TComponent component, string localeId) where TComponent : UIComponent
        {
            component.tooltipLocaleID = localeId;

            return component;
        }

        public static TComponent WithTooltip<TComponent>(this TComponent component, string tooltip) where TComponent : UIComponent
        {
            component.tooltip = tooltip;

            return component;
        }
    }
}
