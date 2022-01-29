namespace SexyFishHorse.CitiesSkylines.Infrastructure.UI
{
    using System;
    using System.Reflection;
    using ColossalFramework.UI;
    using ICities;
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class StronglyTypedUIHelper : IStronglyTypedUIHelper
    {
        public StronglyTypedUIHelper([NotNull] UIHelperBase uiHelperBase)
        {
            UiHelperBase = uiHelperBase;
            var field = uiHelperBase.GetType().GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
            {
                throw new InvalidOperationException("The field \"m_Root\" was not found on the UIHelperBase.");
            }

            Component = (UIComponent)field.GetValue(uiHelperBase);
        }

        public UIHelperBase UiHelperBase { get; private set; }

        public UIComponent Component { get; private set; }

        public UIButton AddButton(string label, OnButtonClicked buttonClickedEvent)
        {
            label.ShouldNotBeNullOrEmpty("label");
            buttonClickedEvent.ShouldNotBeNull("buttonClickedEvent");

            return (UIButton)UiHelperBase.AddButton(label, buttonClickedEvent);
        }

        public UICheckBox AddCheckBox(string label, bool isChecked, OnCheckChanged checkChangedEvent)
        {
            label.ShouldNotBeNullOrEmpty("label");
            checkChangedEvent.ShouldNotBeNull("checkChangedEvent");

            return (UICheckBox)UiHelperBase.AddCheckbox(label, isChecked, checkChangedEvent);
        }

        public UIDropDown AddDropDown(string label,
            string[] options,
            int selectedIndex,
            OnDropdownSelectionChanged selectionChangedEvent)
        {
            label.ShouldNotBeNullOrEmpty("label");
            options.ShouldNotBeNullOrEmpty("options");
            selectionChangedEvent.ShouldNotBeNull("selectionChangedEvent");

            return (UIDropDown)UiHelperBase.AddDropdown(label, options, selectedIndex, selectionChangedEvent);
        }

        public StronglyTypedUIHelper AddGroup(string label)
        {
            return new StronglyTypedUIHelper(UiHelperBase.AddGroup(label));
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

            return (UISlider)UiHelperBase.AddSlider(label, minimumValue, maximumValue, step, value, valueChangedEvent);
        }

        public UIPanel AddSpace(int height = 10)
        {
            height.ShouldBeGreaterThanZero("height");

            return (UIPanel)UiHelperBase.AddSpace(height);
        }

        public UITextField AddTextField(string label,
            string value,
            OnTextChanged textChangedEvent,
            OnTextSubmitted textSubmittedEvent)
        {
            label.ShouldNotBeNullOrEmpty("label");

            return (UITextField)UiHelperBase.AddTextfield(label, value, textChangedEvent, textSubmittedEvent);
        }
    }
}
