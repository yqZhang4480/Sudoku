﻿namespace Sudoku.Analytics.StepSearchers;

/// <summary>
/// Provides with a <b>Brute Force</b> step searcher.
/// The step searcher will include the following techniques:
/// <list type="bullet">
/// <item>Brute Force</item>
/// </list>
/// </summary>
[Direct]
[Fixed]
[StepSearcher]
public sealed class BruteForceStepSearcher() : StepSearcher(43, StepSearcherLevel.Hidden), IStepSearcherRequiresSolution
{
	/// <inheritdoc/>
	public Grid Solution { get; set; }


	/// <inheritdoc/>
	protected internal override Step? GetAll(scoped ref AnalysisContext context)
	{
		if (Solution.IsUndefined)
		{
			goto ReturnNull;
		}

		scoped ref readonly var grid = ref context.Grid;
		foreach (var offset in BruteForceTryAndErrorOrder)
		{
			if (grid.GetStatus(offset) == CellStatus.Empty)
			{
				var cand = offset * 9 + Solution[offset];
				var step = new BruteForceStep(
					new[] { new Conclusion(Assignment, cand) },
					new[] { View.Empty | new CandidateViewNode(DisplayColorKind.Normal, cand) }
				);
				if (context.OnlyFindOne)
				{
					return step;
				}

				context.Accumulator.Add(step);
			}
		}

	ReturnNull:
		return null;
	}
}
