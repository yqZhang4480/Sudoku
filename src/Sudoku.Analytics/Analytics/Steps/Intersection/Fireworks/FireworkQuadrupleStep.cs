namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Firework Quadruple</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="cells">Indicates the cells used.</param>
/// <param name="digitsMask">Indicates the mask of digits used.</param>
public sealed partial class FireworkQuadrupleStep(
	Conclusion[] conclusions,
	View[]? views,
	[PrimaryConstructorParameter] scoped in CellMap cells,
	[PrimaryConstructorParameter] Mask digitsMask
) : FireworkStep(conclusions, views)
{
	/// <inheritdoc/>
	public override decimal BaseDifficulty => base.BaseDifficulty + .4M;

	/// <inheritdoc/>
	public override Technique Code => Technique.FireworkQuadruple;

	/// <inheritdoc/>
	public override FormatInterpolation[] FormatInterpolationParts
		=> [new(EnglishLanguage, [CellsStr, DigitsStr]), new(ChineseLanguage, [CellsStr, DigitsStr])];

	private string CellsStr => Cells.ToString();

	private string DigitsStr => DigitNotation.ToString(DigitsMask);
}
