namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Firework Pair Type 2</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="digitsMask">Indicates the mask of digits used.</param>
/// <param name="pattern1">Indicates the first firework pattern used.</param>
/// <param name="pattern2">Indicates the second firework pattern used.</param>
/// <param name="extraCell">Indicates the extra cell used.</param>
public sealed partial class FireworkPairType2Step(
	Conclusion[] conclusions,
	View[]? views,
	[PrimaryConstructorParameter] Mask digitsMask,
	[PrimaryConstructorParameter] scoped in Firework pattern1,
	[PrimaryConstructorParameter] scoped in Firework pattern2,
	[PrimaryConstructorParameter] Cell extraCell
) : FireworkStep(conclusions, views)
{
	/// <inheritdoc/>
	public override decimal BaseDifficulty => base.BaseDifficulty + .3M;

	/// <inheritdoc/>
	public override Technique Code => Technique.FireworkPairType2;

	/// <inheritdoc/>
	public override FormatInterpolation[] FormatInterpolationParts
		=> [
			new(EnglishLanguage, [Firework1Str, Firework2Str, DigitsStr, ExtraCellStr]),
			new(ChineseLanguage, [Firework1Str, Firework2Str, DigitsStr, ExtraCellStr])
		];

	private string ExtraCellStr => CellNotation.ToString(ExtraCell);

	private string DigitsStr => DigitNotation.ToString(DigitsMask);

	private string Firework1Str => $"{Pattern1.Map}({DigitNotation.ToString(DigitsMask)})";

	private string Firework2Str => $"{Pattern2.Map}({DigitNotation.ToString(DigitsMask)})";
}
