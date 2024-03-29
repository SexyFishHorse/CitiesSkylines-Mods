﻿namespace SexyFishHorse.CitiesSkylines.Infrastructure.UI
{
    using System.Collections.Generic;
    using ColossalFramework.UI;
    using ICities;
    using JetBrains.Annotations;

    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public interface IStronglyTypedUIHelper
    {
        [NotNull]
        UIHelperBase UiHelperBase { get; }

        UIComponent Component { get; }

        [NotNull]
        UIButton AddButton([NotNull] string label, [NotNull] OnButtonClicked buttonClickedEvent);

        [NotNull]
        UICheckBox AddCheckBox([NotNull] string label, bool isChecked, [NotNull] OnCheckChanged checkChangedEvent);

        [NotNull]
        UIDropDown AddDropDown([NotNull] string label,
            [NotNull] ICollection<string> options,
            int selectedIndex,
            [NotNull] OnDropdownSelectionChanged selectionChangedEvent);

        [NotNull]
        StronglyTypedUIHelper AddGroup(string label);

        [NotNull]
        UISlider AddSlider(
            [NotNull] string label,
            float minimumValue,
            float maximumValue,
            float step,
            float value,
            [NotNull] OnValueChanged valueChangedEvent);

        [NotNull]
        UIPanel AddSpace(int height);

        [NotNull]
        UITextField AddTextField([NotNull] string label,
            string value,
            OnTextChanged textChangedEvent,
            OnTextSubmitted textSubmittedEvent);
    }
}
