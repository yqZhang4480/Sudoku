namespace Sudoku.Analytics.InternalHelpers;

/// <summary>
/// Defines a type that stores some fields as shared one.
/// </summary>
internal static class SharedFields
{
	/// <summary>
	/// <para>
	/// Indicates all maps that forms the each intersection. The pattern will be like:
	/// <code><![CDATA[
	/// .-------.-------.-------.
	/// | C C C | A A A | A A A |
	/// | B B B | . . . | . . . |
	/// | B B B | . . . | . . . |
	/// '-------'-------'-------'
	/// ]]></code>
	/// </para>
	/// <para>
	/// In addition, in this data structure, a <b>CoverSet</b> is a block and a <b>BaseSet</b> is a line.
	/// </para>
	/// </summary>
	public static readonly IReadOnlyDictionary<IntersectionBase, IntersectionResult> IntersectionMaps;

	/// <summary>
	/// <para>The table of all blocks to iterate for each blocks.</para>
	/// <para>
	/// This field is only used for providing the data for another field <see cref="IntersectionMaps"/>.
	/// </para>
	/// </summary>
	/// <seealso cref="IntersectionMaps"/>
	private static readonly byte[][] IntersectionBlockTable = [
		[1, 2], [0, 2], [0, 1],
		[1, 2], [0, 2], [0, 1],
		[1, 2], [0, 2], [0, 1],
		[4, 5], [3, 5], [3, 4],
		[4, 5], [3, 5], [3, 4],
		[4, 5], [3, 5], [3, 4],
		[7, 8], [6, 8], [6, 7],
		[7, 8], [6, 8], [6, 7],
		[7, 8], [6, 8], [6, 7],
		[3, 6], [0, 6], [0, 3],
		[3, 6], [0, 6], [0, 3],
		[3, 6], [0, 6], [0, 3],
		[4, 7], [1, 7], [1, 4],
		[4, 7], [1, 7], [1, 4],
		[4, 7], [1, 7], [1, 4],
		[5, 8], [2, 8], [2, 5],
		[5, 8], [2, 8], [2, 5],
		[5, 8], [2, 8], [2, 5]
	];


	/// <include file='../../global-doc-comments.xml' path='g/static-constructor' />
	static SharedFields()
	{
		scoped var r = (ReadOnlySpan<byte>)[0, 1, 2, 3, 4, 5, 6, 7, 8];
		scoped var c = (ReadOnlySpan<byte>)[0, 3, 6, 1, 4, 7, 2, 5, 8];
		var dic = new Dictionary<IntersectionBase, IntersectionResult>(new IntersectionBaseComparer());
		for (var bs = (byte)9; bs < 27; bs++)
		{
			for (var j = (byte)0; j < 3; j++)
			{
				var cs = bs < 18 ? r[(bs - 9) / 3 * 3 + j] : c[(bs - 18) / 3 * 3 + j];
				scoped ref readonly var bm = ref HousesMap[bs];
				scoped ref readonly var cm = ref HousesMap[cs];
				var i = bm & cm;
				dic.Add(new(bs, cs), new(bm - i, cm - i, i, IntersectionBlockTable[(bs - 9) * 3 + j]));
			}
		}

		IntersectionMaps = dic;
	}
}

/// <summary>
/// Represents a comparer instance that compares two tuples.
/// </summary>
file sealed class IntersectionBaseComparer : IEqualityComparer<IntersectionBase>
{
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(IntersectionBase x, IntersectionBase y) => x == y;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int GetHashCode(IntersectionBase obj) => obj.Line << 5 | obj.Block;
}
