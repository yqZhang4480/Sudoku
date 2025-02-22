namespace Sudoku.Analytics;

/// <summary>
/// Represents an instance that can collect all possible <see cref="Step"/>s in a grid for one status.
/// </summary>
public sealed partial class StepCollector : AnalyzerOrCollector
{
	/// <summary>
	/// Indicates whether the solver only displays the techniques with the same displaying level.
	/// </summary>
	/// <remarks>
	/// The default value is <see langword="true"/>.
	/// </remarks>
	public bool OnlyShowSameLevelTechniquesInFindAllSteps { get; internal set; } = true;

	/// <summary>
	/// Indicates the maximum steps can be gathered.
	/// </summary>
	/// <remarks>
	/// The default value is 1000.
	/// </remarks>
	public int MaxStepsGathered { get; internal set; } = 1000;

	/// <inheritdoc/>
	[DisallowNull]
	[ImplicitField(RequiredReadOnlyModifier = false)]
	public override StepSearcher[]? StepSearchers
	{
		get => _stepSearchers;

		protected internal set => ResultStepSearchers = FilterStepSearchers(_stepSearchers = value, StepSearcherRunningArea.Gathering);
	}

	/// <inheritdoc/>
	public override StepSearcher[] ResultStepSearchers { get; protected internal set; } =
		from searcher in StepSearcherPool.Default()
		where searcher.RunningArea.Flags(StepSearcherRunningArea.Gathering)
		select searcher;


	/// <summary>
	/// Search for all possible steps in a grid.
	/// </summary>
	/// <param name="puzzle">The puzzle grid.</param>
	/// <param name="progress">The progress instance that is used for reporting the status.</param>
	/// <param name="cancellationToken">The cancellation token used for canceling an operation.</param>
	/// <returns>
	/// The result. If cancelled, the return value will be <see langword="null"/>; otherwise, a real list even though it may be empty.
	/// </returns>
	public IEnumerable<Step>? Search(scoped in Grid puzzle, IProgress<AnalyzerProgress>? progress = null, CancellationToken cancellationToken = default)
	{
		if (puzzle.IsSolved || !puzzle.ExactlyValidate(out _, out var isSukaku) || isSukaku is not { } sukaku)
		{
			return [];
		}

		try
		{
			return searchInternal(sukaku, progress, puzzle, cancellationToken);
		}
		catch (Exception ex)
		{
			if (ex is OperationCanceledException { CancellationToken: var c } && c == cancellationToken)
			{
				return null;
			}

			throw;
		}


		List<Step> searchInternal(bool sukaku, IProgress<AnalyzerProgress>? progress, scoped in Grid puzzle, CancellationToken cancellationToken)
		{
			const int defaultLevelValue = int.MaxValue;

			var possibleStepSearchers = ResultStepSearchers;
			var totalSearchersCount = possibleStepSearchers.Length;

			Initialize(puzzle, puzzle.SolutionGrid);

			var (i, bag, currentSearcherIndex) = (defaultLevelValue, new List<Step>(), 0);
			foreach (var searcher in possibleStepSearchers)
			{
				switch (searcher)
				{
					case { RunningArea: var runningArea } when !runningArea.Flags(StepSearcherRunningArea.Gathering):
					case { IsNotSupportedForSukaku: true } when sukaku:
					{
						goto ReportProgress;
					}
					case { Level: var currentLevel }:
					{
						// If a searcher contains the upper level, it will be skipped.
						if (OnlyShowSameLevelTechniquesInFindAllSteps && i != defaultLevelValue && i != currentLevel)
						{
							goto ReportProgress;
						}

						cancellationToken.ThrowIfCancellationRequested();

						// Searching.
						var accumulator = new List<Step>();
						scoped var context = new AnalysisContext(accumulator, puzzle, false);
						searcher.Collect(ref context);

						switch (accumulator.Count)
						{
							case 0:
							{
								goto ReportProgress;
							}
							case var count:
							{
								if (OnlyShowSameLevelTechniquesInFindAllSteps)
								{
									i = currentLevel;
								}

								bag.AddRange(count > MaxStepsGathered ? accumulator[..MaxStepsGathered] : accumulator);

								break;
							}
						}

						break;
					}
				}

			// Report the progress if worth.
			ReportProgress:
				progress?.Report(new(searcher.ToString(), ++currentSearcherIndex / (double)totalSearchersCount));
			}

			// Return the result.
			return bag;
		}
	}
}
