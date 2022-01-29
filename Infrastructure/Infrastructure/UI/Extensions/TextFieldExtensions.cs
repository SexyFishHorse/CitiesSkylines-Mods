namespace SexyFishHorse.CitiesSkylines.Infrastructure.UI.Extensions
{
    using ColossalFramework.UI;

    public static class TextFieldExtensions
    {

        public static UITextField AsWideAsGroup(this UITextField textField, IStronglyTypedUIHelper group)
        {
            textField.width = group.Component.width - 45;

            return textField;
        }
    }
}
