namespace SudokuStudio.Views.Pages.Operation;

/// <summary>
/// Attribute checking operation page.
/// </summary>
public sealed partial class AttributeCheckingOperation : Page, IOperationProviderPage
{
	/// <summary>
	/// Initializes an <see cref="AttributeCheckingOperation"/> instance.
	/// </summary>
	public AttributeCheckingOperation() => InitializeComponent();


	/// <inheritdoc/>
	public AnalyzePage BasePage { get; set; } = null!;


	private async void BackdoorButton_ClickAsync(object sender, RoutedEventArgs e)
	{
		var puzzle = BasePage.SudokuPane.Puzzle;
		if (!puzzle.IsValid)
		{
			ErrorDialog_PuzzleIsInvalid.Target = BackdoorButton;
			ErrorDialog_PuzzleIsInvalid.IsOpen = true;

			return;
		}

		var backdoors = await Task.Run(getBackdoors);
		var view = View.Empty;
		foreach (var (type, candidate) in backdoors)
		{
			view.Add(new CandidateViewNode(type == Assignment ? WellKnownColorIdentifierKind.Assignment : WellKnownColorIdentifierKind.Elimination, candidate));
		}

		var visualUnit = new BackdoorVisualUnit(view);
		BasePage.VisualUnit = visualUnit;


		Conclusion[] getBackdoors()
		{
			lock (AnalyzingRelatedSyncRoot)
			{
				return BackdoorSearcher.GetBackdoors(puzzle);
			}
		}
	}

	private async void TrueCandidateButton_ClickAsync(object sender, RoutedEventArgs e)
	{
		var puzzle = BasePage.SudokuPane.Puzzle;
		if (!puzzle.IsValid)
		{
			ErrorDialog_PuzzleIsInvalid.Target = TrueCandidateButton;
			ErrorDialog_PuzzleIsInvalid.IsOpen = true;

			return;
		}

		var trueCandidates = await Task.Run(getTrueCandidates);
		if (trueCandidates.Length == 0)
		{
			ErrorDialog_PuzzleIsNotBugMultipleGrid.IsOpen = true;

			return;
		}

		var view = View.Empty;
		Array.ForEach(trueCandidates, candidate => view.Add(new CandidateViewNode(WellKnownColorIdentifierKind.Assignment, candidate)));

		var visualUnit = new TrueCandidateVisualUnit(view);
		BasePage.VisualUnit = visualUnit;


		Candidate[] getTrueCandidates()
		{
			lock (AnalyzingRelatedSyncRoot)
			{
				return TrueCandidatesSearcher.GetAllTrueCandidates(puzzle);
			}
		}
	}
}

/// <summary>
/// Defines a backdoor visual unit.
/// </summary>
/// <param name="view">The view.</param>
file sealed class BackdoorVisualUnit(View view) : IRenderable
{
	/// <inheritdoc/>
	public View[]? Views { get; } = [view];

	/// <inheritdoc/>
	Conclusion[] IRenderable.Conclusions { get; } = Array.Empty<Conclusion>();
}

/// <summary>
/// Defines a true-candidate visual unit.
/// </summary>
/// <param name="view">The view.</param>
file sealed class TrueCandidateVisualUnit(View view) : IRenderable
{
	/// <inheritdoc/>
	public View[]? Views { get; } = [view];

	/// <inheritdoc/>
	Conclusion[] IRenderable.Conclusions { get; } = Array.Empty<Conclusion>();
}
