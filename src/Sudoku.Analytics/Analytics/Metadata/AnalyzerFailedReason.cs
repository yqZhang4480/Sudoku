namespace Sudoku.Analytics.Metadata;

/// <summary>
/// Defines a reason why the puzzle is failed to be analyzed.
/// </summary>
public enum AnalyzerFailedReason
{
	/// <summary>
	/// Indicates nothing goes wrong.
	/// </summary>
	Nothing,

	/// <summary>
	/// <para>Indicates the failed reason is that the puzzle doesn't contain a valid unique solution.</para>
	/// <para>
	/// Different with <see cref="PuzzleHasMultipleSolutions"/> and <see cref="PuzzleHasNoSolution"/>,
	/// this field will include more generic cases. If the puzzle doesn't pass the pre-process operation
	/// before solving, we should use this field.
	/// </para>
	/// </summary>
	/// <remarks>
	/// This field can also be used for false-positive on analyzing a puzzle, raising <see cref="StepSearcherProcessException{TStepSearcher}"/>.
	/// </remarks>
	/// <seealso cref="PuzzleHasNoSolution"/>
	/// <seealso cref="PuzzleHasMultipleSolutions"/>
	/// <seealso cref="StepSearcherProcessException{TStepSearcher}"/>
	PuzzleIsInvalid,

	/// <summary>
	/// Indicates the failed reason is that the puzzle doesn't contain a valid solution.
	/// </summary>
	PuzzleHasNoSolution,

	/// <summary>
	/// Indicates the failed reason is that the puzzle contains multiple valid solutions.
	/// </summary>
	PuzzleHasMultipleSolutions,

	/// <summary>
	/// Indicates the failed reason is that the user has canceled the task.
	/// </summary>
	UserCancelled,

	/// <summary>
	/// Indicates the failed reason is that the solver or some searching module isn't implemented.
	/// </summary>
	NotImplemented,

	/// <summary>
	/// Indicates the failed reason is that the solver has encountered an error and couldn't solve
	/// that will cause an exception thrown.
	/// </summary>
	ExceptionThrown,

	/// <summary>
	/// Indicates the failed reason is that the solver has found a wrong step that cause the grid become invalid.
	/// </summary>
	WrongStep,

	/// <summary>
	/// Indicates the failed reason is that the puzzle is too hard to solve. The solver gave up.
	/// </summary>
	/// <remarks><i>
	/// This option becomes deprecated at present because we has already implemented a step searcher
	/// that always produces a valid step, guaranteeing that the solver will never give up at all time.
	/// </i></remarks>
	PuzzleIsTooHard
}
