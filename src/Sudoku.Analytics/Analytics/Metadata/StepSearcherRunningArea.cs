namespace Sudoku.Analytics.Metadata;

/// <summary>
/// Represents a mode that describes an area where a step searcher can be invoked.
/// </summary>
[Flags]
public enum StepSearcherRunningArea
{
	/// <summary>
	/// Indicates the step searcher is disabled.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates the mode is normal searching.
	/// </summary>
	Searching = 1,

	/// <summary>
	/// Indicates the mode is gathering.
	/// </summary>
	Gathering = 1 << 1
}
