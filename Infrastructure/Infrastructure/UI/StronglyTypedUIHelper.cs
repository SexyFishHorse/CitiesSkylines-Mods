namespace SexyFishHorse.CitiesSkylines.Infrastructure.UI
{
    using ColossalFramework.UI;
    using ICities;
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class StronglyTypedUIHelper : IStronglyTypedUIHelper
    {
        public StronglyTypedUIHelper([NotNull] UIHelperBase uiHelper)
        {
            UiHelper = uiHelper;
        }

        public UIHelperBase UiHelper { get; private set; }

        public UIButton AddButton(string label, OnButtonClicked buttonClickedEvent)
        {
            label.ShouldNotBeNullOrEmpty("label");
            buttonClickedEvent.ShouldNotBeNull("buttonClickedEvent");

            return (UIButton)UiHelper.AddButton(label, buttonClickedEvent);
        }

        public UICheckBox AddCheckBox(string label, bool isChecked, OnCheckChanged checkChangedEvent)
        {
            label.ShouldNotBeNullOrEmpty("label");
            checkChangedEvent.ShouldNotBeNull("checkChangedEvent");

            return (UICheckBox)UiHelper.AddCheckbox(label, isChecked, checkChangedEvent);
        }

        public UIDropDown AddDropDown(
            string label,
            string[] values,
            int selectedIndex,
            OnDropdownSelectionChanged selectionChangedEvent)
        {
            label.ShouldNotBeNullOrEmpty("label");
            values.ShouldNotBeNullOrEmpty("values");
            selectionChangedEvent.ShouldNotBeNull("selectionChangedEvent");

            return (UIDropDown)UiHelper.AddDropdown(label, values, selectedIndex, selectionChangedEvent);
        }

        public StronglyTypedUIHelper AddGroup(string label)
        {
            return new StronglyTypedUIHelper(UiHelper.AddGroup(label));
        }

        public UISlider AddSlider(
            string label,
            float minimumValue,
            float maximumValue,
            float step,
            float value,
            OnValueChanged valueChangedEvent)
        {
            label.ShouldNotBeNullOrEmpty("label");
            minimumValue.ShouldBeLessThan(maximumValue, "minimumValue");
            step.ShouldBeLessThanOrEqualTo(maximumValue, "step");
            value.ShouldBeGreaterThanOrEqualTo(minimumValue, "value");
            value.ShouldBeLessThanOrEqualTo(maximumValue, "value");
            valueChangedEvent.ShouldNotBeNull("valueChangedEvent");

            return (UISlider)UiHelper.AddSlider(label, minimumValue, maximumValue, step, value, valueChangedEvent);
        }

        public UIPanel AddSpace(int height)
        {
            height.ShouldBeGreaterThanZero("height");

            return (UIPanel)UiHelper.AddSpace(height);
        }

        public UITextField AddTextField(
            string label,
            string value,
            OnTextChanged textChangedEvent,
            OnTextSubmitted textSubmittedEvent)
        {
            label.ShouldNotBeNullOrEmpty("label");

            return (UITextField)UiHelper.AddTextfield(label, value, textChangedEvent, textSubmittedEvent);
        }
    }
}
