namespace Sudoku.Concepts;

/// <summary>
/// Extracts a base type that describes status table from elements of <typeparamref name="TSelf"/> type.
/// </summary>
/// <typeparam name="TSelf">The type of the instance that implements this interface type.</typeparam>
/// <typeparam name="TElement">The type of each element.</typeparam>
[LargeStructure]
[EqualityOperators(EqualityOperatorsBehavior.MakeVirtual)]
public partial interface IBitStatusMap<TSelf, TElement> :
	IAdditiveIdentity<TSelf, TSelf>,
	IBitwiseOperators<TSelf, TSelf, TSelf>,
	IEquatable<TSelf>,
	IEqualityOperators<TSelf, TSelf, bool>,
	IFormattable,
	IMinMaxValue<TSelf>,
	IModulusOperators<TSelf, TSelf, TSelf>,
	IParsable<TSelf>,
	IReadOnlySet<TElement>,
	ISet<TElement>,
	ISimpleFormattable,
	ISimpleParsable<TSelf>,
	ISubtractionOperators<TSelf, TSelf, TSelf>
	where TSelf : unmanaged, IBitStatusMap<TSelf, TElement>
	where TElement : unmanaged, IBinaryInteger<TElement>
{
	/// <summary>
	/// Indicates the error information describing the case that the number of subsets calculated by methods
	/// <see cref="GetSubsets(int)"/> and <see cref="GetAllSubsets(int)"/> is too large.
	/// </summary>
	/// <seealso cref="GetSubsets(int)"/>
	/// <seealso cref="GetAllSubsets(int)"/>
	private protected static readonly string ErrorInfo_SubsetsExceeded = """
		Both cells count and subset size is too large, which may cause potential out of memory exception. 
		This operator will throw this exception to calculate the result, in order to prevent any possible exceptions thrown.
		""".RemoveLineEndings();


	/// <summary>
	/// Indicates the size of each unit.
	/// </summary>
	int Shifting { get; }

	/// <summary>
	/// Indicates the number of the values stored in this collection.
	/// </summary>
	new int Count { get; }

	/// <summary>
	/// Gets all chunks of the current collection, meaning a list of <see cref="string"/> values that can describe
	/// all cell and candidate indices, grouped with same row/column.
	/// </summary>
	string[] StringChunks { get; }

	/// <summary>
	/// Indicates the peer intersection of the current instance.
	/// </summary>
	/// <remarks>
	/// A <b>Peer Intersection</b> is a set of cells that all cells from the base collection can be seen.
	/// For more information please visit <see href="http://sudopedia.enjoysudoku.com/Peer.html">this link</see>.
	/// </remarks>
	TSelf PeerIntersection { get; }

	/// <summary>
	/// Indicates the cell offsets in this collection.
	/// </summary>
	protected TElement[] Offsets { get; }

	/// <inheritdoc/>
	bool ICollection<TElement>.IsReadOnly => false;

	/// <inheritdoc/>
	int ICollection<TElement>.Count => Count;


	/// <summary>
	/// Indicates the empty instance.
	/// </summary>
	static abstract TSelf Empty { get; }

	/// <summary>
	/// Indicates the maximum number of elements that the collection can be reached.
	/// </summary>
	static abstract TElement MaxCount { get; }


	/// <summary>
	/// Get the offset at the specified position index.
	/// </summary>
	/// <param name="index">The index.</param>
	/// <returns>
	/// The offset at the specified position index. If the value is invalid, the return value will be <c>-1</c>.
	/// </returns>
	TElement this[int index] { get; }


	/// <inheritdoc/>
	static TSelf IAdditiveIdentity<TSelf, TSelf>.AdditiveIdentity => TSelf.Empty;


	/// <summary>
	/// Adds a new offset into the current collection.
	/// </summary>
	/// <param name="offset">The offset.</param>
	new void Add(TElement offset);

	/// <summary>
	/// Adds a list of offsets specified as the containing type <typeparamref name="TSelf"/> of this method into the current collection.
	/// This method will be called when you use C# 12 feature "Collection Expression",
	/// without a real appending operation with <see langword="foreach"/> loop.
	/// </summary>
	/// <param name="self">The instance whose type is the containing type.</param>
	void Add(scoped in TSelf self);

	/// <summary>
	/// Set the specified offsets as <see langword="true"/> value.
	/// </summary>
	/// <param name="offsets">The offsets to add.</param>
	void AddRange(IEnumerable<TElement> offsets);

	/// <summary>
	/// Set the specified offset as <see langword="false"/> value.
	/// </summary>
	/// <param name="offset">The offset.</param>
	new void Remove(TElement offset);

	/// <summary>
	/// Set the specified offsets as <see langword="false"/> value.
	/// </summary>
	/// <param name="offsets">The offsets to remove.</param>
	sealed void RemoveRange(scoped ReadOnlySpan<TElement> offsets)
	{
		foreach (var cell in offsets)
		{
			Remove(cell);
		}
	}

	/// <inheritdoc cref="RemoveRange(ReadOnlySpan{TElement})"/>
	void RemoveRange(IEnumerable<TElement> offsets);

	/// <summary>
	/// Clear all bits.
	/// </summary>
	new void Clear();

	/// <summary>
	/// Copies the current instance to the target array specified as an <typeparamref name="TElement"/>*.
	/// </summary>
	/// <param name="arr">The pointer that points to an array of type <typeparamref name="TElement"/>.</param>
	/// <param name="length">The length of that array.</param>
	/// <exception cref="ArgumentNullException">
	/// Throws when the argument <paramref name="arr"/> is <see langword="null"/>.
	/// </exception>
	/// <exception cref="InvalidOperationException">
	/// Throws when the capacity isn't enough to store all values.
	/// </exception>
	unsafe void CopyTo(TElement* arr, int length);

	/// <inheritdoc cref="ICollection{T}.CopyTo(T[], int)"/>
	new unsafe void CopyTo(TElement[] array, int arrayIndex)
	{
		fixed (TElement* pArray = array)
		{
			CopyTo(pArray + arrayIndex, Count - arrayIndex);
		}
	}

	/// <summary>
	/// Copies the current instance to the target <see cref="Span{T}"/> instance.
	/// </summary>
	/// <param name="span">
	/// The target <see cref="Span{T}"/> instance.
	/// </param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	sealed unsafe void CopyTo(scoped Span<TElement> span)
	{
		fixed (TElement* ptr = span)
		{
			CopyTo(ptr, span.Length);
		}
	}

	/// <summary>
	/// Iterates on each element in this collection.
	/// </summary>
	/// <param name="action">The visitor that handles for each element in this collection.</param>
	void ForEach(Action<TElement> action);

	/// <inheritdoc cref="ISet{T}.ExceptWith(IEnumerable{T})"/>
	new void ExceptWith(IEnumerable<TElement> other);

	/// <inheritdoc cref="ISet{T}.IntersectWith(IEnumerable{T})"/>
	new void IntersectWith(IEnumerable<TElement> other);

	/// <inheritdoc cref="ISet{T}.SymmetricExceptWith(IEnumerable{T})"/>
	new void SymmetricExceptWith(IEnumerable<TElement> other);

	/// <inheritdoc cref="ISet{T}.UnionWith(IEnumerable{T})"/>
	new void UnionWith(IEnumerable<TElement> other);

	/// <summary>
	/// Determine whether the map contains the specified offset.
	/// </summary>
	/// <param name="offset">The offset.</param>
	/// <returns>A <see cref="bool"/> value indicating that.</returns>
	new bool Contains(TElement offset);

	/// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
	bool Equals(scoped in TSelf other);

	/// <summary>
	/// Get all offsets whose bits are set <see langword="true"/>.
	/// </summary>
	/// <returns>An array of offsets.</returns>
	TElement[] ToArray();

	/// <summary>
	/// Slices the current instance, and get the new instance with some of elements between two indices.
	/// </summary>
	/// <param name="start">The start index.</param>
	/// <param name="count">The number of elements.</param>
	/// <returns>The target instance.</returns>
	TSelf Slice(int start, int count);

	/// <summary>
	/// Gets the subsets of the current collection via the specified size indicating the number of elements of the each subset.
	/// </summary>
	/// <param name="subsetSize">The size to get.</param>
	/// <returns>
	/// All possible subsets. If:
	/// <list type="table">
	/// <listheader>
	/// <term>Condition</term>
	/// <description>Meaning</description>
	/// </listheader>
	/// <item>
	/// <term><c><paramref name="subsetSize"/> &gt; Count</c></term>
	/// <description>Will return an empty array</description>
	/// </item>
	/// <item>
	/// <term><c><paramref name="subsetSize"/> == Count</c></term>
	/// <description>
	/// Will return an array that contains only one element, same as the current instance.
	/// </description>
	/// </item>
	/// <item>
	/// <term>Other cases</term>
	/// <description>The valid combinations.</description>
	/// </item>
	/// </list>
	/// </returns>
	/// <exception cref="NotSupportedException">
	/// Throws when both the count of the current instance and <paramref name="subsetSize"/> are greater than 30.
	/// </exception>
	/// <remarks>
	/// For example, if the current instance is <c>r1c1</c>, <c>r1c2</c> and <c>r1c3</c>
	/// and the argument <paramref name="subsetSize"/> is 2, the expression <c><![CDATA[cells & 2]]></c>
	/// will be an array of 3 elements given below: <c>r1c12</c>, <c>r1c13</c> and <c>r1c23</c>.
	/// </remarks>
	TSelf[] GetSubsets(int subsetSize);

	/// <summary>
	/// Equivalent to <c>GetAllSubsets(Count)</c>.
	/// </summary>
	/// <returns>All subsets of the current instance.</returns>
	/// <seealso cref="GetAllSubsets(int)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	TSelf[] GetAllSubsets() => GetAllSubsets(Count);

	/// <summary>
	/// Gets all subsets of the current collection via the specified size
	/// indicating the <b>maximum</b> number of elements of the each subset.
	/// </summary>
	/// <param name="limitSubsetSize">The size to get.</param>
	/// <returns>
	/// All possible subsets. If:
	/// <list type="table">
	/// <listheader>
	/// <term>Condition</term>
	/// <description>Meaning</description>
	/// </listheader>
	/// <item>
	/// <term><c><paramref name="limitSubsetSize"/> &gt; Count</c></term>
	/// <description>Will return an empty array</description>
	/// </item>
	/// <item>
	/// <term>Other cases</term>
	/// <description>The valid combinations.</description>
	/// </item>
	/// </list>
	/// </returns>
	/// <remarks>
	/// For example, the expression <c>cells | 3</c> is equivalent to all possible cases
	/// coming from <c><![CDATA[cells & 1]]></c>,
	/// <c><![CDATA[cells & 2]]></c> and <c><![CDATA[cells & 3]]></c>.
	/// </remarks>
	TSelf[] GetAllSubsets(int limitSubsetSize)
	{
		if (limitSubsetSize == 0 || Count == 0)
		{
			return [];
		}

		var (n, desiredSize) = (Count, 0);
		var length = Min(n, limitSubsetSize);
		for (var i = 1; i <= length; i++)
		{
			var target = Combinatorial[n - 1, i - 1];
			desiredSize += target;
		}

		var result = new List<TSelf>(desiredSize);
		for (var i = 1; i <= length; i++)
		{
			result.AddRange(GetSubsets(i));
		}

		return [.. result];
	}

	/// <summary>
	/// Gets the enumerator of the current instance in order to use <see langword="foreach"/> loop.
	/// </summary>
	/// <returns>The enumerator instance.</returns>
	new OneDimensionalArrayEnumerator<TElement> GetEnumerator();

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void ICollection<TElement>.Clear() => Clear();

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void ICollection<TElement>.CopyTo(TElement[] array, int arrayIndex) => CopyTo(array, arrayIndex);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void ISet<TElement>.ExceptWith(IEnumerable<TElement> other) => ExceptWith(other);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void ISet<TElement>.IntersectWith(IEnumerable<TElement> other) => IntersectWith(other);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void ISet<TElement>.SymmetricExceptWith(IEnumerable<TElement> other) => SymmetricExceptWith(other);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void ISet<TElement>.UnionWith(IEnumerable<TElement> other) => UnionWith(other);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	bool ISet<TElement>.Add(TElement item)
	{
		Add(item);
		return true;
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	bool ICollection<TElement>.Remove(TElement item)
	{
		Remove(item);
		return true;
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	bool IReadOnlySet<TElement>.Contains(TElement item) => Contains(item);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	bool IEquatable<TSelf>.Equals(TSelf other) => Equals(other);

	#region Not fully tested
	/// <inheritdoc cref="ISet{T}.IsProperSubsetOf(IEnumerable{T})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	new sealed bool IsProperSubsetOf(IEnumerable<TElement> other)
	{
		var otherCells = TSelf.Empty + other;
		return (TSelf)this != otherCells && (otherCells & (TSelf)this) == (TSelf)this;
	}

	/// <inheritdoc cref="ISet{T}.IsProperSupersetOf(IEnumerable{T})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	new sealed bool IsProperSupersetOf(IEnumerable<TElement> other)
	{
		var otherCells = TSelf.Empty + other;
		return (TSelf)this != otherCells && ((TSelf)this & otherCells) == otherCells;
	}

	/// <inheritdoc cref="ISet{T}.IsSubsetOf(IEnumerable{T})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	new sealed bool IsSubsetOf(IEnumerable<TElement> other) => ((TSelf.Empty + other) & (TSelf)this) == (TSelf)this;

	/// <inheritdoc cref="ISet{T}.IsSupersetOf(IEnumerable{T})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	new sealed bool IsSupersetOf(IEnumerable<TElement> other)
	{
		var otherCells = TSelf.Empty + other;
		return ((TSelf)this & otherCells) == otherCells;
	}

	/// <inheritdoc cref="ISet{T}.Overlaps(IEnumerable{T})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	new sealed bool Overlaps(IEnumerable<TElement> other) => !!((TSelf)this & (TSelf.Empty + other));

	/// <inheritdoc cref="ISet{T}.SetEquals(IEnumerable{T})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	new sealed bool SetEquals(IEnumerable<TElement> other) => (TSelf)this == TSelf.Empty + other;
	#endregion

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	bool ISet<TElement>.IsProperSubsetOf(IEnumerable<TElement> other) => IsProperSubsetOf(other);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	bool ISet<TElement>.IsProperSupersetOf(IEnumerable<TElement> other) => IsProperSupersetOf(other);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	bool ISet<TElement>.IsSubsetOf(IEnumerable<TElement> other) => IsSubsetOf(other);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	bool ISet<TElement>.IsSupersetOf(IEnumerable<TElement> other) => IsSupersetOf(other);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	bool ISet<TElement>.Overlaps(IEnumerable<TElement> other) => Overlaps(other);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	bool ISet<TElement>.SetEquals(IEnumerable<TElement> other) => SetEquals(other);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	bool IReadOnlySet<TElement>.IsProperSubsetOf(IEnumerable<TElement> other) => IsProperSubsetOf(other);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	bool IReadOnlySet<TElement>.IsProperSupersetOf(IEnumerable<TElement> other) => IsProperSupersetOf(other);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	bool IReadOnlySet<TElement>.IsSubsetOf(IEnumerable<TElement> other) => IsSubsetOf(other);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	bool IReadOnlySet<TElement>.IsSupersetOf(IEnumerable<TElement> other) => IsSupersetOf(other);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	bool IReadOnlySet<TElement>.Overlaps(IEnumerable<TElement> other) => Overlaps(other);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	bool IReadOnlySet<TElement>.SetEquals(IEnumerable<TElement> other) => SetEquals(other);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TElement>)this).GetEnumerator();

	/// <inheritdoc/>
	IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator()
	{
		var collection = new List<TElement>();
		foreach (var element in this)
		{
			collection.Add(element);
		}

		return collection.GetEnumerator();
	}


	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static TSelf IParsable<TSelf>.Parse(string s, IFormatProvider? provider) => TSelf.Parse(s);


	/// <summary>
	/// Determines whether the current collection is empty.
	/// </summary>
	/// <param name="offsets">The cells to be checked.</param>
	/// <returns>A <see cref="bool"/> value indicating that.</returns>
	/// <remarks>
	/// The type of the current collection supports using <see cref="bool"/>-like expression to determine whether the collection is not empty,
	/// for example:
	/// <code><![CDATA[
	/// if (collection)
	///     // ...
	/// ]]></code>
	/// The statement <c>collection</c> will be expanded to <c>collection.Count != 0</c>. Therefore, the negation operator <c>!</c>
	/// will invert the result of above expression. This is why I use <see langword="operator"/> <c>!</c> to determine on this.
	/// </remarks>
	static abstract bool operator !(scoped in TSelf offsets);

	/// <summary>
	/// Reverse status for all offsets, which means all <see langword="true"/> bits
	/// will be set <see langword="false"/>, and all <see langword="false"/> bits
	/// will be set <see langword="true"/>.
	/// </summary>
	/// <param name="offsets">The instance to negate.</param>
	/// <returns>The negative result.</returns>
	static abstract TSelf operator ~(scoped in TSelf offsets);

	/// <summary>
	/// Determines whether the specified <typeparamref name="TSelf"/> collection is not empty.
	/// </summary>
	/// <param name="cells">The collection.</param>
	/// <returns>A <see cref="bool"/> result indicating that.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static virtual bool operator true(scoped in TSelf cells) => cells.Count != 0;

	/// <summary>
	/// Determines whether the specified <typeparamref name="TSelf"/> collection is empty.
	/// </summary>
	/// <param name="cells">The collection.</param>
	/// <returns>A <see cref="bool"/> result indicating that.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static virtual bool operator false(scoped in TSelf cells) => cells.Count == 0;

	/// <summary>
	/// Adds the specified <paramref name="offset"/> to the <paramref name="collection"/>,
	/// and returns the added result.
	/// </summary>
	/// <param name="collection">The collection.</param>
	/// <param name="offset">The offset to be added.</param>
	/// <returns>The result collection.</returns>
	static abstract TSelf operator +(scoped in TSelf collection, TElement offset);

	/// <summary>
	/// Adds the specified list of <paramref name="offsets"/> to the <paramref name="collection"/>,
	/// and returns the added result.
	/// </summary>
	/// <param name="collection">The collection.</param>
	/// <param name="offsets">A list of cells to be added.</param>
	/// <returns>The result collection.</returns>
	static abstract TSelf operator +(scoped in TSelf collection, IEnumerable<TElement> offsets);

	/// <summary>
	/// Removes the specified <paramref name="offset"/> from the <paramref name="collection"/>,
	/// and returns the removed result.
	/// </summary>
	/// <param name="collection">The collection.</param>
	/// <param name="offset">The offset to be removed.</param>
	/// <returns>The result collection.</returns>
	static abstract TSelf operator -(scoped in TSelf collection, TElement offset);

	/// <summary>
	/// Get a <typeparamref name="TSelf"/> that contains all <paramref name="collection"/> instance
	/// but not in <paramref name="offsets"/> instance.
	/// </summary>
	/// <param name="collection">The left instance.</param>
	/// <param name="offsets">The right instance.</param>
	/// <returns>The result.</returns>
	static abstract TSelf operator -(scoped in TSelf collection, IEnumerable<TElement> offsets);

	/// <summary>
	/// Get a <typeparamref name="TSelf"/> that contains all <paramref name="left"/> instance
	/// but not in <paramref name="right"/> instance.
	/// </summary>
	/// <param name="left">The left instance.</param>
	/// <param name="right">The right instance.</param>
	/// <returns>The result.</returns>
	static virtual TSelf operator -(scoped in TSelf left, scoped in TSelf right) => left & ~right;

	/// <summary>
	/// Get the elements that both <paramref name="left"/> and <paramref name="right"/> contain.
	/// </summary>
	/// <param name="left">The left instance.</param>
	/// <param name="right">The right instance.</param>
	/// <returns>The result.</returns>
	static abstract TSelf operator &(scoped in TSelf left, scoped in TSelf right);

	/// <summary>
	/// Combine the elements from <paramref name="left"/> and <paramref name="right"/>,
	/// and return the merged result.
	/// </summary>
	/// <param name="left">The left instance.</param>
	/// <param name="right">The right instance.</param>
	/// <returns>The result.</returns>
	static abstract TSelf operator |(scoped in TSelf left, scoped in TSelf right);

	/// <summary>
	/// Get the elements that either <paramref name="left"/> or <paramref name="right"/> contains.
	/// </summary>
	/// <param name="left">The left instance.</param>
	/// <param name="right">The right instance.</param>
	/// <returns>The result.</returns>
	static abstract TSelf operator ^(scoped in TSelf left, scoped in TSelf right);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static bool IEqualityOperators<TSelf, TSelf, bool>.operator ==(TSelf left, TSelf right) => left == right;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static bool IEqualityOperators<TSelf, TSelf, bool>.operator !=(TSelf left, TSelf right) => left != right;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static TSelf IBitwiseOperators<TSelf, TSelf, TSelf>.operator ~(TSelf value) => ~value;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static TSelf ISubtractionOperators<TSelf, TSelf, TSelf>.operator -(TSelf left, TSelf right) => left - right;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static TSelf IBitwiseOperators<TSelf, TSelf, TSelf>.operator &(TSelf left, TSelf right) => left & right;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static TSelf IBitwiseOperators<TSelf, TSelf, TSelf>.operator |(TSelf left, TSelf right) => left | right;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static TSelf IBitwiseOperators<TSelf, TSelf, TSelf>.operator ^(TSelf left, TSelf right) => left ^ right;

	/// <summary>
	/// Expands the operator to <c><![CDATA[(a & b).PeerIntersection & b]]></c>.
	/// </summary>
	/// <param name="left">The base map.</param>
	/// <param name="right">The template map that the base map to check and cover.</param>
	/// <returns>The result map.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static TSelf IModulusOperators<TSelf, TSelf, TSelf>.operator %(TSelf left, TSelf right) => (left & right).PeerIntersection & right;


	/// <summary>
	/// Converts an array of element type <typeparamref name="TElement"/> to a <typeparamref name="TSelf"/> instance.
	/// </summary>
	/// <param name="array">An array of element type <typeparamref name="TElement"/>.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static virtual explicit operator TSelf(TElement[] array) => [.. array];

	/// <summary>
	/// Converts an array of element type <typeparamref name="TElement"/> to a <typeparamref name="TSelf"/> instance, with boundary checks.
	/// </summary>
	/// <param name="array">An array of element type <typeparamref name="TElement"/>.</param>
	/// <exception cref="OverflowException">
	/// Throws when at least one element in argument <paramref name="array"/> is greater than <see cref="MaxCount"/>.
	/// </exception>
	/// <seealso cref="MaxCount"/>
	static virtual explicit operator checked TSelf(TElement[] array)
	{
		var result = TSelf.Empty;
		foreach (var element in array)
		{
			if (element < default(TElement) || element >= TSelf.MaxCount)
			{
				throw new OverflowException($"The element value '{element}' is less than {default(TElement)} or greater than {TSelf.MaxCount}.");
			}

			result.Add(element);
		}

		return result;
	}

	/// <summary>
	/// Converts an <see cref="ReadOnlySpan{T}"/> of element type <typeparamref name="TElement"/> to a <typeparamref name="TSelf"/> instance.
	/// </summary>
	/// <param name="values">An array of element type <typeparamref name="TElement"/>.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static virtual explicit operator TSelf(scoped ReadOnlySpan<TElement> values) => [.. values];

	/// <summary>
	/// Converts an <see cref="ReadOnlySpan{T}"/> of element type <typeparamref name="TElement"/> to a <typeparamref name="TSelf"/> instance, 
	/// with boundary checks.
	/// </summary>
	/// <param name="values">An array of element type <typeparamref name="TElement"/>.</param>
	/// <exception cref="OverflowException">
	/// Throws when at least one element in argument <paramref name="values"/> is greater than <see cref="MaxCount"/>.
	/// </exception>
	/// <seealso cref="MaxCount"/>
	static virtual explicit operator checked TSelf(scoped ReadOnlySpan<TElement> values)
	{
		var result = TSelf.Empty;
		foreach (var element in values)
		{
			if (element < default(TElement) || element >= TSelf.MaxCount)
			{
				throw new OverflowException($"The element value '{element}' is less than {default(TElement)} or greater than {TSelf.MaxCount}.");
			}

			result.Add(element);
		}

		return result;
	}
}
