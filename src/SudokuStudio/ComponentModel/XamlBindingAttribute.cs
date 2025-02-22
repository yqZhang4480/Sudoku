#pragma warning disable CS9113
namespace SudokuStudio.ComponentModel;

/// <summary>
/// Defines a binding base attribute type.
/// </summary>
/// <typeparam name="T">The type of the property.</typeparam>
/// <param name="propertyName">The property name.</param>
public abstract class XamlBindingAttribute<T>(string propertyName) : Attribute
{
	/// <summary>
	/// Indicates whether the property type is nullable if the member is reference type; for value type, this property has no effect.
	/// </summary>
	public bool IsNullable { get; init; }

	/// <summary>
	/// Indicates the referenced member name that will be used for displaying <c>inheritdoc</c> part.
	/// </summary>
	public string? DocReferencedMemberName { get; init; }

	/// <summary>
	/// Indicates the referenced path that will be used for displaying <c>inheritdoc</c> part.
	/// </summary>
	public string? DocReferencedPath { get; init; }

	/// <summary>
	/// Indicates the <c>summary</c> content of the dependency property displayed in XML documentation comment.
	/// </summary>
	public string? DocSummary { get; init; }

	/// <summary>
	/// Indicates the <c>remarks</c> content of the dependency property displayed in XML documentation comment.
	/// </summary>
	public string? DocRemarks { get; init; }

	/// <summary>
	/// Indicates the referenced member name that points to a member that can create a default value of the current dependency property.
	/// </summary>
	/// <remarks>
	/// <para>This property will be used if the property <see cref="DefaultValue"/> cannot be assigned due to not being a constant.</para>
	/// <para>
	/// <i>This property is being deprecated. Please use type <see cref="DefaultAttribute"/> instead:</i>
	/// <code><![CDATA[
	/// [DefaultValue]
	/// private static readonly object DependencyPropertyNameDefaultValue = ...;
	/// ]]></code>
	/// </para>
	/// </remarks>
	/// <seealso cref="DefaultValue"/>
	[Obsolete($"Use attribute type '{nameof(DefaultAttribute)}' to mark target callback method instead.", false)]
	public string? DefaultValueGeneratingMemberName { get; init; }

	/// <summary>
	/// Indicates the callback method name.
	/// </summary>
	/// <remarks>
	/// <i>This property is being deprecated. Please use type <see cref="CallbackAttribute"/> instead:</i>
	/// <code><![CDATA[
	/// [Callback]
	/// private static void PropertyNamePropertyCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
	/// {
	///     // ...
	/// }
	/// ]]></code>
	/// </remarks>
	[Obsolete($"Use attribute type '{nameof(CallbackAttribute)}' to mark target callback method instead.", false)]
	public string? CallbackMethodName { get; init; }

	/// <summary>
	/// Indicates the default value.
	/// </summary>
	public T? DefaultValue { get; init; }
}
