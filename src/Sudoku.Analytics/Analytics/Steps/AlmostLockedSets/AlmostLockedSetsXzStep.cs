namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is an <b>Almost Locked Sets XZ</b> or <b>Extended Subset Principle</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="als1">Indicates the first ALS used.</param>
/// <param name="als2">Indicates the second ALS used.</param>
/// <param name="xDigitsMask">Indicates the mask of X digits used.</param>
/// <param name="zDigitsMask">Indicates the mask of Z digits used.</param>
/// <param name="isDoublyLinked">
/// <para>Indicates whether ALS-XZ is doubly-linked.</para>
/// <para>
/// All possible values are <see langword="true"/>, <see langword="false"/> and <see langword="null"/>.
/// If the value is <see langword="true"/> or <see langword="false"/>, the ALS-XZ is a Doubly- or Singly- Linked ALS-XZ;
/// otherwise, an Extended Subset Principle technique.
/// </para>
/// </param>
public sealed partial class AlmostLockedSetsXzStep(
	Conclusion[] conclusions,
	View[]? views,
	[PrimaryConstructorParameter(GeneratedMemberName = "FirstAls")] AlmostLockedSet als1,
	[PrimaryConstructorParameter(GeneratedMemberName = "SecondAls")] AlmostLockedSet als2,
	[PrimaryConstructorParameter] Mask xDigitsMask,
	[PrimaryConstructorParameter] Mask zDigitsMask,
	[PrimaryConstructorParameter] bool? isDoublyLinked
) : AlmostLockedSetsStep(conclusions, views)
{
	/// <inheritdoc/>
	public override decimal BaseDifficulty => IsDoublyLinked is true ? 5.7M : 5.5M;

	/// <inheritdoc/>
	public override string? Format
		=> GetString(
			(IsDoublyLinked, ZDigitsMask) switch
			{
				(null, 0) => "TechniqueFormat_ExtendedSubsetPrincipleWithoutDuplicate",
				(null, _) => "TechniqueFormat_ExtendedSubsetPrincipleWithDuplicate",
				_ => "TechniqueFormat_AlmostLockedSetsXzRule"
			}
		);

	/// <inheritdoc/>
	public override Technique Code
		=> IsDoublyLinked switch
		{
			true => Technique.DoublyLinkedAlmostLockedSetsXzRule,
			false => Technique.SinglyLinkedAlmostLockedSetsXzRule,
			_ => Technique.ExtendedSubsetPrinciple
		};

	/// <inheritdoc/>
	public override FormatInterpolation[] FormatInterpolationParts
		=> [
			new(
				EnglishLanguage,
				IsDoublyLinked is null ? ZDigitsMask == 0 ? [CellsStr] : [EspDigitStr, CellsStr] : [Als1Str, Als2Str, XStr, ZResultStr]
			),
			new(
				ChineseLanguage,
				IsDoublyLinked is null ? ZDigitsMask == 0 ? [CellsStr] : [EspDigitStr, CellsStr] : [Als1Str, Als2Str, XStr, ZResultStr]
			)
		];

	private string CellsStr => (FirstAls.Cells | SecondAls.Cells).ToString();

	private string EspDigitStr => (TrailingZeroCount(ZDigitsMask) + 1).ToString();

	private string Als1Str => FirstAls.ToString();

	private string Als2Str => SecondAls.ToString();

	private string XStr => DigitNotation.ToString(XDigitsMask);

	private string ZResultStr => ZDigitsMask == 0 ? string.Empty : $"{GetString("Comma")!}Z = {DigitNotation.ToString(ZDigitsMask)}";
}
