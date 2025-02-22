namespace Sudoku.Analytics.StepSearchers;

/// <summary>
/// Provides with a <b>Locked Subset</b> step searcher. The step searcher will include the following techniques:
/// <list type="bullet">
/// <item>
/// Naked Subsets (+):
/// <list type="bullet">
/// <item>Naked Pair (+)</item>
/// <item>Naked Triple (+)</item>
/// <item>Naked Quadruple (+)</item>
/// </list>
/// </item>
/// <item>
/// Naked Subsets:
/// <list type="bullet">
/// <item>Naked Pair</item>
/// <item>Naked Triple</item>
/// <item>Naked Quadruple</item>
/// </list>
/// </item>
/// <item>
/// Hidden Subsets:
/// <list type="bullet">
/// <item>Hidden Pair</item>
/// <item>Hidden Triple</item>
/// <item>Hidden Quadruple</item>
/// </list>
/// </item>
/// </list>
/// </summary>
[StepSearcher(
	Technique.HiddenPair, Technique.HiddenTriple, Technique.HiddenQuadruple,
	Technique.NakedPair, Technique.NakedTriple, Technique.NakedQuadruple)]
public sealed partial class NormalSubsetStepSearcher : SubsetStepSearcher
{
	/// <inheritdoc/>
	public override bool OnlySearchingForLocked => false;
}
