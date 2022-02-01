﻿using Sudoku.Collections;
using Sudoku.Resources;

namespace Sudoku.Solving.BruteForces;

/// <summary>
/// Encapsulates a solver result information that is created by a brute force solver.
/// </summary>
/// <param name="OriginalPuzzle"><inheritdoc/></param>
/// <param name="IsSolved">
/// <para><inheritdoc/></para>
/// <para>The default value is <see langword="true"/>.</para>
/// </param>
/// <param name="FailedReason">
/// <para><inheritdoc/></para>
/// <para>The default value is <see cref="FailedReason.Nothing"/>.</para>
/// </param>
/// <param name="Solution">
/// <para><inheritdoc/></para>
/// <para>The default value is <see cref="Grid.Undefined"/>.</para>
/// </param>
/// <param name="ElapsedTime">
/// <para><inheritdoc/></para>
/// <para>The default value is <see cref="TimeSpan.Zero"/>.</para>
/// </param>
public sealed record BruteForceSolverResult(
	in Grid OriginalPuzzle,
	bool IsSolved = true,
	FailedReason FailedReason = FailedReason.Nothing,
	in Grid Solution = default,
	TimeSpan ElapsedTime = default
) : ISolverResult
{
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public string ToDisplayString()
	{
		// Print header.
		var sb = new StringHandler();
		sb.Append(ResourceManager.Shared["bruteForceSolverResultPuzzle"]);
		sb.Append(OriginalPuzzle.ToString("#"));
		sb.AppendLine();

		// Print the solution (if not null).
		if (!Solution.IsUndefined)
		{
			sb.Append(ResourceManager.Shared["bruteForceSolverResultSolution"]);
			sb.Append(Solution.ToString("!"));
			sb.AppendLine();
		}

		// Print the elapsed time.
		sb.Append(ResourceManager.Shared["bruteForceSolverResultPuzzleHas"]);
		sb.AppendWhen(IsSolved, ResourceManager.Shared["bruteForceSolverResultNot"]);
		sb.Append(ResourceManager.Shared["bruteForceSolverResultBeenSolved"]);
		sb.AppendLine();
		sb.Append(ResourceManager.Shared["bruteForceSolverResultTimeElapsed"]);
		sb.Append(ElapsedTime, @"hh\:mm\:ss\.ffffff");
		sb.AppendLine();

		return sb.ToStringAndClear();
	}

	/// <inheritdoc/>
	public override string ToString() => ToDisplayString();
}
