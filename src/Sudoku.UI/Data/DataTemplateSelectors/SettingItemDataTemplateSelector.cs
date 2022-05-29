﻿namespace Sudoku.UI.Data.DataTemplateSelectors;

/// <summary>
/// Defines a data template selector that determines which data template can be used on a setting item.
/// </summary>
public sealed class SettingItemDataTemplateSelector : DataTemplateSelector
{
	/// <summary>
	/// Indicates the template that is used for a boolean value.
	/// </summary>
	public DataTemplate BooleanValueTemplate { get; set; } = null!;

	/// <summary>
	/// Indicates the default template.
	/// </summary>
	public DataTemplate DefaultTemplate { get; set; } = null!;


	/// <inheritdoc/>
	/// <exception cref="InvalidOperationException">Throws when data template cannot be found.</exception>
	protected override DataTemplate SelectTemplateCore(object item)
		=> item switch
		{
			SettingItem { ItemValue: var value } => value switch
			{
				bool => BooleanValueTemplate,
				_ => DefaultTemplate
			},
			_ => throw new InvalidOperationException("Cannot find possible data template due to invalid type of the item.")
		};
}
