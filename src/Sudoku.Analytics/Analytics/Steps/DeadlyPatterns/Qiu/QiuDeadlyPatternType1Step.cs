namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Qiu's Deadly Pattern Type 1</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="pattern"><inheritdoc/></param>
/// <param name="candidate">Indicates the target candidate.</param>
public sealed partial class QiuDeadlyPatternType1Step(
	Conclusion[] conclusions,
	View[]? views,
	scoped in QiuDeadlyPattern pattern,
	[PrimaryConstructorParameter] Candidate candidate
) : QiuDeadlyPatternStep(conclusions, views, pattern)
{
	/// <inheritdoc/>
	public override int Type => 1;

	/// <inheritdoc/>
	public override IReadOnlyDictionary<string, string[]?> FormatInterpolatedParts
		=> new Dictionary<string, string[]?> { { EnglishLanguage, [PatternStr, CandidateStr] }, { ChineseLanguage, [CandidateStr, PatternStr] } };

	private string CandidateStr => RxCyNotation.ToCandidateString(Candidate);
}
