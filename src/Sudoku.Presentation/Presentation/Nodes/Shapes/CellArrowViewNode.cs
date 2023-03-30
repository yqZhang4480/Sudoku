namespace Sudoku.Presentation.Nodes.Shapes;

/// <summary>
/// Defines a cell arrow view node.
/// </summary>
public sealed partial class CellArrowViewNode(int cell, Direction directions) : SingleCellMarkViewNode(DisplayColorKind.Normal, cell, directions)
{
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override bool Equals([NotNullWhen(true)] ViewNode? other) => other is CellArrowViewNode comparer && Cell == comparer.Cell;

	[GeneratedOverriddingMember(GeneratedGetHashCodeBehavior.CallingHashCodeCombine, nameof(TypeIdentifier), nameof(Cell))]
	public override partial int GetHashCode();

	[GeneratedOverriddingMember(GeneratedToStringBehavior.RecordLike, nameof(Identifier), nameof(Cell), nameof(Directions))]
	public override partial string ToString();

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override CellArrowViewNode Clone() => new(Cell, Directions);
}
