namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Locked Candidates</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="digit">Indicates the digit used.</param>
/// <param name="baseSet">Indicates the house that the current locked candidates forms.</param>
/// <param name="coverSet">Indicates the house that the current locked candidates influences.</param>
public sealed partial class LockedCandidatesStep(
	Conclusion[] conclusions,
	View[]? views,
	[PrimaryConstructorParameter] Digit digit,
	[PrimaryConstructorParameter] House baseSet,
	[PrimaryConstructorParameter] House coverSet
) : IntersectionStep(conclusions, views)
{
	/// <inheritdoc/>
	public override decimal BaseDifficulty => BaseSet < 9 ? 2.6M : 2.8M;

	/// <inheritdoc/>
	public override Technique Code => BaseSet < 9 ? Technique.Pointing : Technique.Claiming;

	/// <inheritdoc/>
	public override FormatInterpolation[] FormatInterpolationParts
		=> [new(EnglishLanguage, [DigitStr, BaseSetStr, CoverSetStr]), new(ChineseLanguage, [DigitStr, BaseSetStr, CoverSetStr])];

	private string DigitStr => (Digit + 1).ToString();

	private string BaseSetStr => HouseNotation.ToString(BaseSet);

	private string CoverSetStr => HouseNotation.ToString(CoverSet);
}
