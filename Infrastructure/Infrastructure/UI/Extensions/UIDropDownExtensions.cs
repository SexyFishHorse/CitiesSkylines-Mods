namespace SexyFishHorse.CitiesSkylines.Infrastructure.UI.Extensions
{
    using System;
    using ColossalFramework.UI;

    public static class UIDropDownExtensions
    {
        public static UIDropDown WithWidthFactor(this UIDropDown dropDown, float factor)
        {
            dropDown.width = (int)Math.Round(dropDown.width * factor);

            return dropDown;
        }
    }
}
