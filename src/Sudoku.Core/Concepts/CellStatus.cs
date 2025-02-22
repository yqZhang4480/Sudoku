﻿namespace Sudoku.Concepts;

/// <summary>
/// Represents a cell status.
/// </summary>
[Flags]
public enum CellStatus : byte
{
	/// <summary>
	/// Indicates the cell status is invalid.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	Undefined = 0,

	/// <summary>
	/// Indicates that the cell is empty.
	/// </summary>
	Empty = 1,

	/// <summary>
	/// Indicates the current cell has been filled a value that is not given from initial grid.
	/// </summary>
	Modifiable = 1 << 1,

	/// <summary>
	/// Indicates the current cell has been filled a value that cannot be modified because it exists in initial grid.
	/// </summary>
	Given = 1 << 2
}
