﻿namespace Sudoku.UI.DataConversion.DataTemplateSelectors;

/// <summary>
/// Defines a data template selector that determines which data template can be used on a setting item.
/// </summary>
public sealed class SettingItemDataTemplateSelector : ModelDataTemplateSelector
{
	/// <summary>
	/// Indicates the template that is used for a toggle switch.
	/// </summary>
	[DataTemplateModelType<ToggleSwitchSettingItem>]
	public DataTemplate ToggleSwitchTemplate { get; set; } = null!;

	/// <summary>
	/// Indicates the template that is used for a slider.
	/// </summary>
	[DataTemplateModelType<SliderSettingItem>]
	public DataTemplate SliderTemplate { get; set; } = null!;

	/// <summary>
	/// Indicates the template that is used for a slider.
	/// </summary>
	[DataTemplateModelType<Int32SliderSettingItem>]
	public DataTemplate Int32SliderTemplate { get; set; } = null!;

	/// <summary>
	/// Indicates the template that is used for a color picker.
	/// </summary>
	[DataTemplateModelType<ColorPickerSettingItem>]
	public DataTemplate ColorPickerTemplate { get; set; } = null!;

	/// <summary>
	/// Indicates the template that is used for a group of color selectors.
	/// </summary>
	[DataTemplateModelType<ColorSelectorGroupSettingItem>]
	public DataTemplate ColorSelectorGroupTemplate { get; set; } = null!;

	/// <summary>
	/// Indicates the template that is used for a font picker.
	/// </summary>
	[DataTemplateModelType<FontPickerSettingItem>]
	public DataTemplate FontPickerTemplate { get; set; } = null!;

	/// <summary>
	/// Indicates the template that is used for a combo box group that applied
	/// to a <see cref="PeerFocusingMode"/> instance.
	/// </summary>
	/// <seealso cref="PeerFocusingMode"/>
	[DataTemplateModelType<PeerFocusingModeComboBoxSettingItem>]
	public DataTemplate PeerFocusingModeComboBoxGroupTemplate { get; set; } = null!;
}
