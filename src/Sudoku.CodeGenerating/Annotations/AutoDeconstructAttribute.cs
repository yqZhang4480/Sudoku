﻿namespace Sudoku.CodeGenerating;

/// <summary>
/// Allows the type can be deconstructed to multiple elements.
/// </summary>
[AttributeUsage(Class | Struct, AllowMultiple = true, Inherited = false)]
public sealed class AutoDeconstructAttribute : Attribute
{
	/// <summary>
	/// Initializes an instance with the specified member list.
	/// </summary>
	/// <param name="members">The members.</param>
	public AutoDeconstructAttribute(params string[] members) => FieldOrPropertyList = members;


	/// <summary>
	/// All members to deconstruct.
	/// </summary>
	public string[] FieldOrPropertyList { get; }
}
