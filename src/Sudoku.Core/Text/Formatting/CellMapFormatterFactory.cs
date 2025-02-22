namespace Sudoku.Text.Formatting;

/// <summary>
/// Indicates the factory that creates the cell map formatter.
/// </summary>
internal static class CellMapFormatterFactory
{
	/// <summary>
	/// Get a built-in <see cref="ICellMapFormatter"/> instance according to the specified format.
	/// </summary>
	/// <param name="format">The format.</param>
	/// <returns>The grid formatter.</returns>
	/// <exception cref="FormatException">Throws when the format string is invalid.</exception>
	public static ICellMapFormatter? GetBuiltInFormatter(string? format)
		=> format switch
		{
			null or "N" or "n" => new RxCyFormat(),
			"K" or "k" => new K9Format(),
			"T" or "t" => new CellMapTableFormat(),
			"B" or "b" or "B+" or "b+" => new CellMapBinaryFormat(),
			"B-" or "b-" => new CellMapBinaryFormat(false),
			_ => null
		};
}
