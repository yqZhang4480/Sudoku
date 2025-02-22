namespace Sudoku.Analytics.Patterns;

/// <summary>
/// Defines a node used in chaining.
/// </summary>
/// <param name="mask">The internal mask.</param>
/// <remarks>
/// This type corresponds to the concept of Sudoku Explainer's logic
/// "<see href="http://sudopedia.enjoysudoku.com/Node.html">Potential</see>".
/// </remarks>
[DebuggerDisplay($$"""{{{nameof(DebuggerDisplayString)}},nq}""")]
[StructLayout(LayoutKind.Auto)]
[Equals]
[GetHashCode]
[ToString]
[EqualityOperators]
[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly partial struct ChainNode([PrimaryConstructorParameter(MemberKinds.Field), HashCodeMember] Mask mask) :
	IEquatable<ChainNode>,
	IEqualityOperators<ChainNode, ChainNode, bool>
{
	/// <summary>
	/// Initializes a <see cref="ChainNode"/> instance via the specified data.
	/// </summary>
	/// <param name="candidate">The candidate.</param>
	/// <param name="isOn">Indicates whether the node is on.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ChainNode(Candidate candidate, bool isOn) : this((byte)(candidate / 9), (byte)(candidate % 9), isOn)
	{
	}

	/// <summary>
	/// <inheritdoc cref="ChainNode(Candidate, bool)" path="/summary"/>
	/// </summary>
	/// <param name="cell">The cell.</param>
	/// <param name="digit">The digit.</param>
	/// <param name="isOn"><inheritdoc cref="ChainNode(Candidate, bool)" path="/param[@name='isOn']"/></param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ChainNode(byte cell, byte digit, bool isOn) : this((Mask)((isOn ? 1 : 0) << 10 | cell * 9 + digit))
	{
	}

	/// <summary>
	/// <inheritdoc cref="ChainNode(Candidate, bool)" path="/summary"/>
	/// </summary>
	/// <param name="base">The base potential instance.</param>
	/// <param name="isOn"><inheritdoc cref="ChainNode(Candidate, bool)" path="/param[@name='isOn']"/></param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ChainNode(ChainNode @base, bool isOn) : this(@base.Cell, @base.Digit, isOn)
	{
	}


	/// <summary>
	/// Indicates whether the node is on.
	/// </summary>
	[StringMember]
	public bool IsOn => _mask >= 729;

	/// <summary>
	/// Indicates the cell used.
	/// </summary>
	public byte Cell => (byte)(Candidate / 9);

	/// <summary>
	/// Indicates the digit used.
	/// </summary>
	public byte Digit => (byte)(Candidate % 9);

	/// <summary>
	/// Indicates the candidate.
	/// </summary>
	public Candidate Candidate => _mask & (1 << 10) - 1;

	/// <summary>
	/// Defines an accessor that allows user assigning a singleton parent node into the current data structure on instantiation phase.
	/// </summary>
	public ChainNode SingletonParent
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		init => Parents.Add(value);
	}

	/// <summary>
	/// Indicates all <see cref="ChainNode"/>s in a single chain. This property should only be used in normal AICs.
	/// </summary>
	public ChainNode[] ChainPotentials
	{
		get
		{
			var result = new List<ChainNode>();
			for (var p = this; p.Parents is [var parent]; p = parent)
			{
				result.Add(p);
			}

			return [.. result];
		}
	}

	/// <summary>
	/// Gets the chain of all <see cref="ChainNode"/>s from the current <see cref="ChainNode"/> as the target node.
	/// </summary>
	public ChainNode[] FullChainPotentials
	{
		get
		{
			var result = new List<ChainNode>();
			var done = new NodeSet();
			var todo = new List<ChainNode> { this };
			while (todo.Count > 0)
			{
				var next = new List<ChainNode>();
				foreach (var p in todo)
				{
					if (!done.Contains(p))
					{
						done.Add(p);
						result.Add(p);
						next.AddRange(p.Parents);
					}
				}

				todo = next;
			}

			return [.. result];
		}
	}

	/// <summary>
	/// Indicates the step detail of the nested chain.
	/// </summary>
	public ChainingStep? NestedChainDetails { get; init; }

	/// <summary>
	/// <para>Indicates the parents of the current instance.</para>
	/// <para>
	/// The result always returns a list of length 1 if the chain is not dynamic.
	/// In addition, if a <see cref="ChainNode"/> instance has no available parent (i.e. the return collection is empty),
	/// it must the head of a chain.
	/// </para>
	/// <para>
	/// If you want to append more parent nodes into the current <see cref="ChainNode"/> instance,
	/// just call <see cref="List{T}.Add(T)"/> to add it using this property: <c>p.Parents.Add(parent);</c>.
	/// </para>
	/// </summary>
	public List<ChainNode> Parents { get; } = new(1);

	/// <summary>
	/// Indicates the candidate string representation.
	/// </summary>
	[StringMember(nameof(Candidate))]
	private string CandidateString => $"{CellsMap[Cell]}({Digit + 1})";

	/// <summary>
	/// Indicates the string that is used for display on debugger.
	/// </summary>
	private string DebuggerDisplayString => $"{CandidateString} is {(IsOn ? bool.TrueString : bool.FalseString).ToLower()}";


	[DeconstructionMethod]
	public partial void Deconstruct(out Candidate candidate, out bool isOn);

	[DeconstructionMethod]
	public partial void Deconstruct(out byte cell, out byte digit, out bool isOn);

	/// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(ChainNode other) => _mask == other._mask;
}
