namespace Sudoku.Analytics.Patterns;

/// <summary>
/// Defines a <see cref="ChainNode"/> collection using <see cref="HashSet{T}"/> as backing implementation.
/// </summary>
/// <seealso cref="ChainNode"/>
public sealed class NodeSet : HashSet<ChainNode>
{
	/// <summary>
	/// Initializes a <see cref="NodeSet"/> instance.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public NodeSet() : base(new EqualityComparer())
	{
	}

	/// <summary>
	/// Initializes a <see cref="NodeSet"/> instance via the specified <see cref="ChainNode"/> collection to be added.
	/// </summary>
	/// <param name="base">The collection of <see cref="ChainNode"/> instances.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public NodeSet(NodeSet @base) : base(@base, new EqualityComparer())
	{
	}


	/// <summary>
	/// Determines whether a <see cref="NodeSet"/> object contains the specified element,
	/// comparing for properties <see cref="ChainNode.Candidate"/> and <see cref="ChainNode.IsOn"/>.
	/// </summary>
	/// <param name="base">The element to locate in the <see cref="NodeSet"/> object.</param>
	/// <returns>
	/// <see langword="true"/> if the <see cref="NodeSet"/> object contains the specified element; otherwise, <see langword="false"/>.
	/// </returns>
	/// <seealso cref="ChainNode.Candidate"/>
	/// <seealso cref="ChainNode.IsOn"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public new bool Contains(ChainNode @base) => GetNullable(@base) is not null;

	/// <summary>
	/// <para>
	/// Try to get the target <see cref="ChainNode"/> instance whose internal value
	/// (i.e. properties <see cref="ChainNode.Candidate"/> and <see cref="ChainNode.IsOn"/>) are same as
	/// the specified one.
	/// </para>
	/// <para>
	/// Please note that this method will return an instance inside the current collection
	/// whose value equals to the specified one; however, property <see cref="ChainNode.Parents"/> may not be equal.
	/// </para>
	/// </summary>
	/// <param name="base">The value to be checked.</param>
	/// <returns>
	/// <para>
	/// The found value whose value is equal to <paramref name="base"/>; without checking for property <see cref="ChainNode.Parents"/>.
	/// </para>
	/// <para>If none found, <see langword="null"/> will be returned.</para>
	/// </returns>
	/// <seealso cref="ChainNode.Candidate"/>
	/// <seealso cref="ChainNode.IsOn"/>
	public ChainNode? GetNullable(ChainNode @base)
	{
		foreach (var potential in this)
		{
			if (potential == @base)
			{
				return potential;
			}
		}

		return null;
	}


	/// <summary>
	/// Gets the <see cref="ChainNode"/> instances that both <paramref name="left"/> and <paramref name="right"/> contain,
	/// and modifies the argument <paramref name="left"/>, replacing it with <see cref="ChainNode"/>s mentioned above,
	/// then returns it.
	/// </summary>
	/// <param name="left">The first collection to be participated in merging operation.</param>
	/// <param name="right">The second collection to be participated in merging operation.</param>
	/// <returns>Modified collection <paramref name="left"/>.</returns>
	/// <remarks>
	/// <include file="../../global-doc-comments.xml" path="/g/requires-compound-invocation"/>
	/// </remarks>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static NodeSet operator &(NodeSet left, NodeSet right)
	{
		left.IntersectWith(right);
		return left;
	}

	/// <summary>
	/// Gets the <see cref="ChainNode"/> instances that comes from both collections <paramref name="left"/> and <paramref name="right"/>,
	/// and modifies the argument <paramref name="left"/>, replacing it with <see cref="ChainNode"/>s mentioned above,
	/// then returns it.
	/// </summary>
	/// <param name="left">The first collection to be participated in merging operation.</param>
	/// <param name="right">The second collection to be participated in merging operation.</param>
	/// <returns>Modified collection <paramref name="left"/>.</returns>
	/// <remarks>
	/// <include file="../../global-doc-comments.xml" path="/g/requires-compound-invocation"/>
	/// </remarks>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static NodeSet operator |(NodeSet left, NodeSet right)
	{
		left.UnionWith(right);
		return left;
	}

	/// <summary>
	/// Gets the <see cref="ChainNode"/> instances that only one collection in <paramref name="left"/> and <paramref name="right"/> contains,
	/// and modifies the argument <paramref name="left"/>, replacing it with <see cref="ChainNode"/>s mentioned above,
	/// then returns it.
	/// </summary>
	/// <param name="left">The first collection to be participated in merging operation.</param>
	/// <param name="right">The second collection to be participated in merging operation.</param>
	/// <returns>Modified collection <paramref name="left"/>.</returns>
	/// <remarks>
	/// <include file="../../global-doc-comments.xml" path="/g/requires-compound-invocation"/>
	/// </remarks>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static NodeSet operator ^(NodeSet left, NodeSet right)
	{
		left.SymmetricExceptWith(right);
		return left;
	}
}

/// <summary>
/// Defines an equality comparer that compares to <see cref="ChainNode"/> instances.
/// </summary>
/// <seealso cref="ChainNode"/>
file sealed class EqualityComparer : IEqualityComparer<ChainNode>
{
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(ChainNode x, ChainNode y) => x == y;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int GetHashCode(ChainNode obj) => obj.GetHashCode();
}
