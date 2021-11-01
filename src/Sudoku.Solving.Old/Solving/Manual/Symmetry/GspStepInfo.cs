﻿namespace Sudoku.Solving.Manual.Symmetry;

/// <summary>
/// Provides a usage of <b>Gurth's symmetrical placement</b> (GSP) technique.
/// </summary>
/// <param name="Conclusions">All conclusions.</param>
/// <param name="Views">All views.</param>
/// <param name="SymmetryType">The symmetry type used.</param>
/// <param name="MappingTable">
/// The mapping table. The value is always not <see langword="null"/> unless the current instance
/// contains multiple different symmetry types.
/// </param>
public sealed record GspStepInfo(
	IReadOnlyList<Conclusion> Conclusions, IReadOnlyList<View> Views,
	SymmetryType SymmetryType, int?[]? MappingTable
) : SymmetryStepInfo(Conclusions, Views)
{
	/// <inheritdoc/>
	public override decimal Difficulty => 7.0M;

	/// <inheritdoc/>
	public override DifficultyLevel DifficultyLevel => DifficultyLevel.Fiendish;

	/// <inheritdoc/>
	public override Technique TechniqueCode => Technique.Gsp;

	[FormatItem]
	private string SymmetryTypeSnippet
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => TextResources.Current.SymmetryTypeSnippet;
	}

	[FormatItem]
	private string SymmetryTypeName
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => SymmetryType switch
		{
			SymmetryType.Central => TextResources.Current.SymmetryTypeCentral,
			SymmetryType.Diagonal => TextResources.Current.SymmetryTypeDiagnoal,
			SymmetryType.AntiDiagonal => TextResources.Current.SymmetryTypeAntiDiagonal
		};
	}

	[FormatItem]
	[NotNullIfNotNull(nameof(MappingTable))]
	private string? MappingRelations
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			if (MappingTable is null)
			{
				return null;
			}
			else
			{
				const string separator = ", ";

				var sb = new StringHandler(initialCapacity: 100);
				for (int i = 0; i < 9; i++)
				{
					int? value = MappingTable[i];

					sb.AppendFormatted(i + 1);

					if (value is { } v && value != i)
					{
						sb.AppendFormatted($" -> {v + 1}");
					}

					sb.AppendFormatted(separator);
				}

				sb.RemoveFromEnd(separator.Length);
				return $"{(string)TextResources.Current.MappingRelationSnippet}{sb.ToStringAndClear()}";
			}
		}
	}


	/// <summary>
	/// Merge two information, and reserve all conclusions from them two.
	/// </summary>
	/// <param name="left">The left information to merge.</param>
	/// <param name="right">The right information to merge.</param>
	/// <returns>The merge result.</returns>
	[return: NotNullIfNotNull("left"), NotNullIfNotNull("right")]
	public static GspStepInfo? operator |(GspStepInfo? left, GspStepInfo? right)
	{
		switch ((Left: left, Right: right))
		{
			case (Left: null, Right: null):
			{
				return null;
			}
			case (Left: not null, Right: not null):
			{
				var results = new List<Conclusion>(left.Conclusions);
				results.AddRange(right.Conclusions);

				var candidateOffsets = new List<DrawingInfo>(left.Views[0].Candidates!);
				candidateOffsets.AddRange(right.Views[0].Candidates!);
				return new(
					results,
					new View[] { new() { Candidates = candidateOffsets } },
					left.SymmetryType | right.SymmetryType,
					null
				);
			}
			default:
			{
				return new((left ?? right)!);
			}
		}
	}
}
