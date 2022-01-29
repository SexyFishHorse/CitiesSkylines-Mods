namespace SexyFishHorse.CitiesSkylines.Infrastructure.UI.Extensions
{
    using ColossalFramework.UI;
    using UnityEngine;

    public static class UILabelExtensions
    {
        public static UILabel WithFontSizeScale(this UILabel label, float scale)
        {
            label.textScale = scale;

            return label;
        }

        public static UILabel WithFontColor(this UILabel label, Color32 color)
        {
            label.textColor = color;

            return label;
        }

        public static UILabel AlignedTo(this UILabel label, UIHorizontalAlignment alignment)
        {
            label.textAlignment = alignment;

            return label;
        }

        public static UILabel PrefixedWith(this UILabel label, string prefix)
        {
            label.prefix = prefix;

            return label;
        }

        public static UILabel SuffixedWith(this UILabel label, string suffix)
        {
            label.suffix = suffix;

            return label;
        }
    }
}
