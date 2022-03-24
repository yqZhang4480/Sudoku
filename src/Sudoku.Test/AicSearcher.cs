﻿using System.Buffers;
using Sudoku.Collections;
using Xunit.Abstractions;

namespace Sudoku.Test;

internal sealed partial class AicSearcher
{
	/// <summary>
	/// Indicates the field that stores the temporary strong inferences during the searching.
	/// </summary>
	/// <remarks>
	/// The value uses a <see cref="Dictionary{TKey, TValue}"/> to store the table of strong inferences, where:
	/// <list type="table">
	/// <listheader>
	/// <term>Item</term>
	/// <description>Meaning</description>
	/// </listheader>
	/// <item>
	/// <term>Key</term>
	/// <description>The ID of a node.</description>
	/// </item>
	/// <item>
	/// <term>Value</term>
	/// <description>
	/// All possible IDs that corresponds to their own node respectively,
	/// one of which can form a strong inference with the <b>Key</b> node.
	/// </description>
	/// </item>
	/// </list>
	/// </remarks>
	/// <seealso cref="Dictionary{TKey, TValue}"/>
	private readonly Dictionary<int, HashSet<int>?> _strongInferences = new();

	/// <summary>
	/// Indicates the field that stores the temporary weak inferences during the searching.
	/// </summary>
	/// <remarks>
	/// The value uses a <see cref="Dictionary{TKey, TValue}"/> to store the table of weak inferences, where:
	/// <list type="table">
	/// <listheader>
	/// <term>Item</term>
	/// <description>Meaning</description>
	/// </listheader>
	/// <item>
	/// <term>Key</term>
	/// <description>The ID of a node.</description>
	/// </item>
	/// <item>
	/// <term>Value</term>
	/// <description>
	/// All possible IDs that corresponds to their own node respectively,
	/// one of which can form a weak inference with the <b>Key</b> node.
	/// </description>
	/// </item>
	/// </list>
	/// </remarks>
	/// <seealso cref="Dictionary{TKey, TValue}"/>
	private readonly Dictionary<int, HashSet<int>?> _weakInferences = new();

	/// <summary>
	/// Indicates the lookup table that can get the target ID value
	/// via the corresponding <see cref="Node"/> instance.
	/// </summary>
	/// <seealso cref="Node"/>
	private readonly Dictionary<Node, int> _idLookup = new();

	/// <summary>
	/// Indicates all possible found chains, that stores the IDs of the each node.
	/// </summary>
	private readonly List<(int[] ChainIds, bool StartsWithWeakInference)> _foundChains = new();

	/// <summary>
	/// Indicates the output instance that can allow displaying the customized items onto the test explorer.
	/// </summary>
	private readonly ITestOutputHelper _output;

	/// <summary>
	/// Indicates the global ID value.
	/// </summary>
	private int _globalId;

	/// <summary>
	/// Indicates the lookup table that can get the target <see cref="Node"/> instance
	/// via the corresponding ID value specified as the index.
	/// </summary>
	private Node?[] _nodeLookup = null!;


	/// <summary>
	/// Initializes a <see cref="AicSearcher"/> instance via the <see cref="ITestOutputHelper"/> instance
	/// to allow displaying the customized items onto the test explorer.
	/// </summary>
	/// <param name="output">
	/// The <see cref="ITestOutputHelper"/> instance
	/// that allows displaying the customized items onto the test explorer.
	/// </param>
	/// <seealso cref="ITestOutputHelper"/>
	public AicSearcher(ITestOutputHelper output) => _output = output;


	/// <summary>
	/// Indicates whether the searcher uses DFS algorithm to search for chains.
	/// </summary>
	/// <remarks>
	/// The default value is <see langword="false"/>.
	/// </remarks>
	public bool DepthFirstSearching { get; set; } = false;

	/// <summary>
	/// Indicates the maximum capacity used for the allocation on shared memory.
	/// </summary>
	/// <remarks>
	/// The default value is <c>3000</c>.
	/// </remarks>
	public int MaxCapacity { get; set; } = 3000;

	/// <summary>
	/// Indicates the extended nodes to be searched for. Please note that the type of the property
	/// is an enumeration type with bit-fields attribute, which means you can add multiple choices
	/// into the value.
	/// </summary>
	/// <remarks>
	/// <para>
	/// You can set the value as a bit-field mask to define your own types to be searched for, where:
	/// <list type="table">
	/// <listheader>
	/// <term>Field name</term>
	/// <description>Description (What kind of nodes can be searched)</description>
	/// </listheader>
	/// <item>
	/// <term><see cref="SearcherNodeTypes.SoleDigit"/></term>
	/// <description>
	/// The strong and weak inferences between 2 sole candidate nodes of a same digit
	/// (i.e. X-Chain).
	/// </description>
	/// </item>
	/// <item>
	/// <term><see cref="SearcherNodeTypes.SoleCell"/></term>
	/// <description>
	/// The strong and weak inferences between 2 sole candidate nodes of a same cell
	/// (i.e. Y-Chain).
	/// </description>
	/// </item>
	/// <item>
	/// <term><see cref="SearcherNodeTypes.LockedCandidates"/></term>
	/// <description>
	/// The strong and weak inferences between 2 nodes, where at least one node is a locked candidates node.
	/// </description>
	/// </item>
	/// <item>
	/// <term><see cref="SearcherNodeTypes.LockedSet"/></term>
	/// <description>
	/// The strong inferences between 2 nodes, where at least one node is an almost locked set node.
	/// </description>
	/// </item>
	/// <item>
	/// <term><see cref="SearcherNodeTypes.HiddenSet"/></term>
	/// <description>
	/// The weak inferences between 2 nodes, where at least one node is an almost hidden set node.
	/// </description>
	/// </item>
	/// <item>
	/// <term><see cref="SearcherNodeTypes.UniqueRectangle"/></term>
	/// <description>
	/// The strong and weak inferences between 2 nodes, where at least one node
	/// is an almost unique rectangle node.
	/// </description>
	/// </item>
	/// <item>
	/// <term><see cref="SearcherNodeTypes.Kraken"/></term>
	/// <description>
	/// The strong and weak inferences between 2 nodes, where at least one node
	/// is a kraken fish node.
	/// </description>
	/// </item>
	/// </list>
	/// Other typed inferences are being considered, such as an XYZ-Wing node, etc.
	/// </para>
	/// <para>
	/// The default value is <c>
	/// <see cref="SearcherNodeTypes.SoleCell"/>
	/// | <see cref="SearcherNodeTypes.SoleDigit"/>
	/// | <see cref="SearcherNodeTypes.LockedCandidates"/>
	/// | <see cref="SearcherNodeTypes.LockedSet"/>
	/// | <see cref="SearcherNodeTypes.HiddenSet"/>
	/// </c>.
	/// </para>
	/// </remarks>
	public SearcherNodeTypes NodeTypes { get; set; } =
		SearcherNodeTypes.SoleDigit | SearcherNodeTypes.SoleCell
			| SearcherNodeTypes.LockedCandidates
			| SearcherNodeTypes.LockedSet | SearcherNodeTypes.HiddenSet;


	/// <summary>
	/// Get all possible chain steps.
	/// </summary>
	/// <param name="grid">The grid used.</param>
	public void GetAll(in Grid grid)
	{
		try
		{
			// Clear all possible lists.
			_strongInferences.Clear();
			_weakInferences.Clear();
			_nodeLookup = ArrayPool<Node?>.Shared.Rent(MaxCapacity);
			_idLookup.Clear();
			_foundChains.Clear();

			// Gather strong and weak links.
			GatherInferences_SoleCandidate(grid);
			GatherInferences_LockedCandidates(grid);
			GatherInferences_AlmostLockedSet(grid);
			GatherInferences_AlmostHiddenSet(grid);
			GatherInferences_UniqueRectangle(grid);
			GatherInferences_BasicFish(grid);

			// Remove IDs if they don't appear in the lookup table.
			RemoveIdsNotAppearingInLookupDictionary(_weakInferences);
			RemoveIdsNotAppearingInLookupDictionary(_strongInferences);

#if false
			// Display the inferences found.
			PrintInferences(_strongInferences);
			PrintInferences(_weakInferences);
#else
			// Construct chains.
			if (DepthFirstSearching)
			{
				Dfs_StartWithWeak();
				Dfs_StartWithStrong();
			}
			else
			{
				Bfs();
			}

			// Output the result.
			var tempList = new Dictionary<AlternatingInferenceChain, Conclusion[]>();
			foreach (var (nids, startsWithWeak) in _foundChains)
			{
				var chain = new AlternatingInferenceChain(from nid in nids select _nodeLookup[nid], startsWithWeak);
				if (chain.GetConclusions(grid) is { Length: not 0 } conclusions && !tempList.ContainsKey(chain))
				{
					tempList.Add(chain, conclusions);
				}
			}

			foreach (var (chain, conclusions) in tempList)
			{
				_output.WriteLine($"{chain} => {new ConclusionCollection(conclusions).ToString()}");
			}
#endif
		}
		finally
		{
			// Clears the memory.
			ArrayPool<Node?>.Shared.Return(_nodeLookup);
		}
	}


	partial void GatherInferences_SoleCandidate(in Grid grid);
	partial void GatherInferences_LockedCandidates(in Grid grid);
	partial void GatherInferences_AlmostLockedSet(in Grid grid);
	partial void GatherInferences_AlmostHiddenSet(in Grid grid);
	partial void GatherInferences_UniqueRectangle(in Grid grid);
	partial void GatherInferences_BasicFish(in Grid grid);
}
