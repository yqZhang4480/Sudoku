namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is an <b>Almost Locked Sets XY-Wing</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="als1">Indicates the first ALS used in this pattern.</param>
/// <param name="als2">Indicates the second ALS used in this pattern.</param>
/// <param name="bridge">Indicates the ALS that is as a bridge.</param>
/// <param name="xDigitsMask">Indicates the mask that holds the digits for the X value.</param>
/// <param name="yDigitsMask">Indicates the mask that holds the digits for the Y value.</param>
/// <param name="zDigitsMask">Indicates the mask that holds the digits for the Z value.</param>
public sealed partial class AlmostLockedSetsXyWingStep(
	Conclusion[] conclusions,
	View[]? views,
	[PrimaryConstructorParameter(GeneratedMemberName = "FirstAls")] AlmostLockedSet als1,
	[PrimaryConstructorParameter(GeneratedMemberName = "SecondAls")] AlmostLockedSet als2,
	[PrimaryConstructorParameter(GeneratedMemberName = "BridgeAls")] AlmostLockedSet bridge,
	[PrimaryConstructorParameter] Mask xDigitsMask,
	[PrimaryConstructorParameter] Mask yDigitsMask,
	[PrimaryConstructorParameter] Mask zDigitsMask
) : AlmostLockedSetsStep(conclusions, views)
{
	/// <inheritdoc/>
	public override decimal BaseDifficulty => 6.0M;

	/// <inheritdoc/>
	public override Technique Code => Technique.AlmostLockedSetsXyWing;

	/// <inheritdoc/>
	public override FormatInterpolation[] FormatInterpolationParts
		=> [
			new(EnglishLanguage, [Als1Str, BridgeStr, Als2Str, XStr, YStr, ZStr]),
			new(ChineseLanguage, [Als1Str, BridgeStr, Als2Str, XStr, YStr, ZStr])
		];

	private string Als1Str => FirstAls.ToString();

	private string BridgeStr => BridgeAls.ToString();

	private string Als2Str => SecondAls.ToString();

	private string XStr => DigitNotation.ToString(XDigitsMask);

	private string YStr => DigitNotation.ToString(YDigitsMask);

	private string ZStr => DigitNotation.ToString(ZDigitsMask);
}
