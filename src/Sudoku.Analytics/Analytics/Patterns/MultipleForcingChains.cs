namespace Sudoku.Analytics.Patterns;

/// <summary>
/// Defines a multiple forcing chains.
/// </summary>
public sealed partial class MultipleForcingChains : IReadOnlyList<(byte CellOrDigit, ChainNode Potential)>
{
	/// <summary>
	/// Indicates the internal dictionary.
	/// </summary>
	private readonly SortedDictionary<byte, ChainNode> _internalDictionary = new(new CellOrDigitComparer());

	/// <summary>
	/// Indicates the keys.
	/// </summary>
	private readonly List<byte> _keys = new(7);

	/// <summary>
	/// Indicates the values.
	/// </summary>
	private readonly List<ChainNode> _values = new(7);


	/// <summary>
	/// Indicates the number of elements stored in this collection.
	/// </summary>
	public int Count => _internalDictionary.Count;

	/// <summary>
	/// Indicates the cells or digits.
	/// </summary>
	public IReadOnlyList<byte> CellsOrDigits => _keys;

	/// <summary>
	/// Indicates the potentials.
	/// </summary>
	public IReadOnlyList<ChainNode> Potentials => _values;


	/// <summary>
	/// Gets the element at the specified index.
	/// </summary>
	/// <param name="index">The desired index.</param>
	/// <returns>The pair of data.</returns>
	/// <exception cref="IndexOutOfRangeException">Throws when the index is out of range.</exception>
	public (byte CellOrDigit, ChainNode Potential) this[int index]
	{
		get
		{
			var enumerator = _internalDictionary.GetEnumerator();
			var lastIndex = index;
			while (enumerator.MoveNext())
			{
				if (--lastIndex == -1)
				{
					var (a, b) = enumerator.Current;
					return (a, b);
				}
			}

			throw new IndexOutOfRangeException();
		}
	}


	/// <summary>
	/// Adds a pair of data into the collection.
	/// </summary>
	/// <param name="cellOrDigit">The cell or digit.</param>
	/// <param name="potential">The potential.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Add(byte cellOrDigit, ChainNode potential)
	{
		_internalDictionary.Add(cellOrDigit, potential);
		_keys.Add(cellOrDigit);
		_values.Add(potential);
	}

	/// <inheritdoc/>
	public IEnumerator<(byte CellOrDigit, ChainNode Potential)> GetEnumerator()
	{
		foreach (var (a, b) in this)
		{
			yield return (a, b);
		}
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


	/// <summary>
	/// Implicit cast from <see cref="Dictionary{TKey, TValue}"/> to <see cref="MultipleForcingChains"/>.
	/// </summary>
	/// <param name="dictionary">The dictionary.</param>
	public static implicit operator MultipleForcingChains(Dictionary<byte, ChainNode> dictionary)
	{
		var result = new MultipleForcingChains();
		foreach (var (key, value) in dictionary)
		{
			result.Add(key, value);
		}

		return result;
	}
}

/// <summary>
/// Defines a comparer that compares with inner key.
/// </summary>
file sealed class CellOrDigitComparer : IComparer<byte>
{
	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int Compare(byte x, byte y) => Sign(x - y);
}
