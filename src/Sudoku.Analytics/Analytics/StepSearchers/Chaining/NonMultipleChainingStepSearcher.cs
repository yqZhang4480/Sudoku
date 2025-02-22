namespace Sudoku.Analytics.StepSearchers;

/// <summary>
/// Provides with a <b>Chain</b> step searcher using same algorithm with <b>Chaining</b> used by a program called Sudoku Explainer.
/// The step searcher will include the following techniques:
/// <list type="bullet">
/// <item>Alternating Inference Chains (Cycles)</item>
/// </list>
/// </summary>
/// <remarks>
/// The type is special: it uses source code from another project called Sudoku Explainer.
/// However unfortunately, I cannot find any sites available of the project.
/// One of the original website is <see href="https://diuf.unifr.ch/pai/people/juillera/Sudoku/Sudoku.html">this link</see> (A broken link).
/// </remarks>
[StepSearcher(
	Technique.XChain, Technique.YChain, Technique.AlternatingInferenceChain, Technique.ContinuousNiceLoop, Technique.DiscontinuousNiceLoop,
	Technique.XyXChain, Technique.XyChain, Technique.FishyCycle, Technique.MWing, Technique.LocalWing, Technique.SplitWing,
	Technique.HybridWing, Technique.PurpleCow)]
public sealed partial class NonMultipleChainingStepSearcher : ChainingStepSearcher
{
	/// <inheritdoc/>
	protected internal override Step? Collect(scoped ref AnalysisContext context)
	{
		scoped ref readonly var grid = ref context.Grid;
		var result = getNonMultipleChains(grid);
		if (result.Count == 0)
		{
			return null;
		}

		result.Order();

		if (context.OnlyFindOne)
		{
			return result[0];
		}

		context.Accumulator.AddRange(result);
		return null;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		List<ChainingStep> getNonMultipleChains(scoped in Grid grid)
		{
			var result = Collect(grid, true, false);
			result.AddRange(Collect(grid, false, true));
			result.AddRange(Collect(grid, true, true));

			return result;
		}
	}

	/// <summary>
	/// Checks whether hte specified <paramref name="child"/> is the real child node of <paramref name="parent"/>.
	/// </summary>
	/// <param name="child">The child node to be checked.</param>
	/// <param name="parent">The parent node to be checked.</param>
	/// <returns>A <see cref="bool"/> result.</returns>
	private bool IsParent(ChainNode child, ChainNode parent)
	{
		var pTest = child;
		while (pTest.Parents is [var first, ..])
		{
			if ((pTest = first) == parent)
			{
				return true;
			}
		}

		return false;
	}

	/// <summary>
	/// Try to search for all AICs and continuous nice loops.
	/// </summary>
	/// <param name="grid">The grid.</param>
	/// <param name="isX">Indicates whether the chain allows X element (strong links in a house for a single digit).</param>
	/// <param name="isY">Indicates whether the chain allows Y element (strong links in a cell).</param>
	/// <returns>All possible found <see cref="ChainingStep"/>s.</returns>
	private List<ChainingStep> Collect(scoped in Grid grid, bool isX, bool isY)
	{
		var result = new List<ChainingStep>();

		foreach (byte cell in EmptyCells)
		{
			for (byte digit = 0; digit < 9; digit++)
			{
				if (CandidatesMap[digit].Contains(cell))
				{
					var pOn = new ChainNode(cell, digit, true);
					DoUnaryChaining(grid, pOn, result, isX, isY);
				}
			}
		}

		return result;
	}

	/// <summary>
	/// Look for, and add single forcing chains, and bidirectional cycles.
	/// </summary>
	/// <param name="grid"><inheritdoc cref="Collect(in Grid, bool, bool)" path="/param[@name='grid']"/></param>
	/// <param name="pOn">The start potential.</param>
	/// <param name="result">The result steps found.</param>
	/// <param name="isX"><inheritdoc cref="Collect(in Grid, bool, bool)" path="/param[@name='isX']"/></param>
	/// <param name="isY"><inheritdoc cref="Collect(in Grid, bool, bool)" path="/param[@name='isY']"/></param>
	private void DoUnaryChaining(scoped in Grid grid, ChainNode pOn, List<ChainingStep> result, bool isX, bool isY)
	{
		if (BivalueCells.Contains(pOn.Cell) && !isX)
		{
			// Y-Cycles can only start if cell has 2 potential values.
			return;
		}

		var (cycles, chains) = (new NodeList(), new NodeList());
		var (onToOn, onToOff) = (new NodeSet { pOn }, new NodeSet());

		DoCycles(grid, onToOn, onToOff, isX, isY, cycles, pOn);

		if (isX)
		{
			// Forcing Y-Chains do not exist (length must be both odd and even).

			// Forcing chain with "off" implication.
			onToOn = [pOn];
			onToOff = [];
			DoForcingChains(grid, onToOn, onToOff, isY, chains, pOn);

			// Forcing chain with "on" implication.
			var pOff = new ChainNode(pOn, false);
			onToOn = [];
			onToOff = [pOff];
			DoForcingChains(grid, onToOn, onToOff, isY, chains, pOff);
		}

		foreach (var dstOn in cycles)
		{
			// Cycle found.
			if (CreateCycleStep(grid, dstOn, isX, isY) is { } step)
			{
				result.Add(step);
			}
		}

		foreach (var target in chains)
		{
			result.Add(CreateAicStep(grid, target, isX, isY));
		}
	}

	/// <summary>
	/// Construct cycles and return them, by recording them into argument <paramref name="cycles"/>.
	/// </summary>
	/// <param name="grid"><inheritdoc cref="Collect(in Grid, bool, bool)" path="/param[@name='grid']"/></param>
	/// <param name="toOn">The potentials that are assumed to be "on".</param>
	/// <param name="toOff">The potentials that are assumed to be "off".</param>
	/// <param name="isX"><inheritdoc cref="Collect(in Grid, bool, bool)" path="/param[@name='isX']"/></param>
	/// <param name="isY"><inheritdoc cref="Collect(in Grid, bool, bool)" path="/param[@name='isY']"/></param>
	/// <param name="cycles">
	/// <para>All found cycles, represented as their final <see cref="ChainNode"/> node.</para>
	/// <para>By using <see cref="ChainNode.ChainPotentials"/>, we can get the whole chain.</para>
	/// </param>
	/// <param name="source">The source node.</param>
	private void DoCycles(scoped in Grid grid, NodeSet toOn, NodeSet toOff, bool isX, bool isY, NodeList cycles, ChainNode source)
	{
		var pendingOn = new NodeList(toOn);
		var pendingOff = new NodeList(toOff);

		// Mind why this is a BFS and works. I learned that cycles are only found by DFS
		// Maybe we are missing loops.

		var length = 0; // Cycle length.
		while (pendingOn.Count > 0 || pendingOff.Count > 0)
		{
			length++;
			while (pendingOn.Count > 0)
			{
				var p = pendingOn.RemoveFirst();
				var makeOff = GetOnToOff(grid, p, isY);
				foreach (var pOff in makeOff)
				{
					if (!IsParent(p, pOff))
					{
						// Not processed yet.
						pendingOff.AddLast(pOff);

						toOff.Add(pOff);
					}
				}
			}

			length++;
			while (pendingOff.Count > 0)
			{
				var p = pendingOff.RemoveFirst();
				var makeOn = GetOffToOn(grid, p, null, toOff, isX, isY, false);
				foreach (var pOn in makeOn)
				{
					if (length >= 4 && pOn == source)
					{
						// Cycle found.
						cycles.AddLast(pOn);
					}

					if (!toOn.Contains(pOn))
					{
						// Not processed yet.
						pendingOn.AddLast(pOn);

						toOn.Add(pOn);
					}
				}
			}
		}
	}

	/// <summary>
	/// Construct forcing chains (in Sudoku Explainer, AICs will be treated as forcing chains).
	/// In other words, this method does find for AICs.
	/// </summary>
	/// <param name="grid"><inheritdoc cref="Collect(in Grid, bool, bool)" path="/param[@name='grid']"/></param>
	/// <param name="toOn">
	/// <inheritdoc cref="DoCycles(in Grid, NodeSet, NodeSet, bool, bool, NodeList, ChainNode)" path="/param[@name='toOn']"/>
	/// </param>
	/// <param name="toOff">
	/// <inheritdoc cref="DoCycles(in Grid, NodeSet, NodeSet, bool, bool, NodeList, ChainNode)" path="/param[@name='toOff']"/>
	/// </param>
	/// <param name="isY"><inheritdoc cref="Collect(in Grid, bool, bool)" path="/param[@name='isY']"/></param>
	/// <param name="chains">
	/// <para>All found chains, represented as their final <see cref="ChainNode"/> node.</para>
	/// <para>
	/// <inheritdoc
	///     cref="DoCycles(in Grid, NodeSet, NodeSet, bool, bool, NodeList, ChainNode)"
	///     path="//param[@name='cycles']/para[2]"/>
	/// </para>
	/// </param>
	/// <param name="source">
	/// <inheritdoc cref="DoCycles(in Grid, NodeSet, NodeSet, bool, bool, NodeList, ChainNode)" path="/param[@name='source']"/>
	/// </param>
	private void DoForcingChains(scoped in Grid grid, NodeSet toOn, NodeSet toOff, bool isY, NodeList chains, ChainNode source)
	{
		var pendingOn = new NodeList(toOn);
		var pendingOff = new NodeList(toOff);
		while (pendingOn.Count > 0 || pendingOff.Count > 0)
		{
			while (pendingOn.Count > 0)
			{
				var p = pendingOn.RemoveFirst();
				var makeOff = GetOnToOff(grid, p, isY);
				foreach (var pOff in makeOff)
				{
					var pOn = new ChainNode(pOff, true); // Conjugate.
					if (source == pOn)
					{
						// Cyclic contradiction (forcing chain) found.
						if (!chains.Contains(pOff))
						{
							chains.AddLast(pOff);
						}
					}

					if (!IsParent(p, pOff))
					{
						// Why this filter? (seems useless).
						if (!toOff.Contains(pOff))
						{
							// Not processed yet.
							pendingOff.AddLast(pOff);
							toOff.Add(pOff);
						}
					}
				}
			}

			while (pendingOff.Count > 0)
			{
				var p = pendingOff.RemoveFirst();
				var makeOn = GetOffToOn(grid, p, null, toOff, true, isY, false);
				foreach (var pOn in makeOn)
				{
					var pOff = new ChainNode(pOn, false); // Conjugate.
					if (source == pOff)
					{
						// Cyclic contradiction (forcing chain) found.
						if (!chains.Contains(pOn))
						{
							chains.AddLast(pOn);
						}
					}

					if (!toOn.Contains(pOn))
					{
						// Not processed yet.
						pendingOn.AddLast(pOn);
						toOn.Add(pOn);
					}
				}
			}
		}
	}

	/// <summary>
	/// Try to create a cycle hint. If any conclusion (elimination, assignment) found and is available,
	/// the method will return a <see cref="BidirectionalCycleStep"/> instance with a non-<see langword="null"/> value.
	/// </summary>
	/// <param name="grid">
	/// <inheritdoc cref="Collect(in Grid, bool, bool)" path="/param[@name='grid']"/>
	/// </param>
	/// <param name="dstOn">Indicates the destination node that is at the state "on".</param>
	/// <param name="isX">
	/// <inheritdoc cref="DoCycles(in Grid, NodeSet, NodeSet, bool, bool, NodeList, ChainNode)" path="/param[@name='isX']"/>
	/// </param>
	/// <param name="isY">
	/// <inheritdoc cref="DoCycles(in Grid, NodeSet, NodeSet, bool, bool, NodeList, ChainNode)" path="/param[@name='isY']"/>
	/// </param>
	/// <returns>
	/// A valid <see cref="BidirectionalCycleStep"/> instance, or <see langword="null"/> if no available eliminations found.
	/// </returns>
	private BidirectionalCycleStep? CreateCycleStep(scoped in Grid grid, ChainNode dstOn, bool isX, bool isY)
	{
		var nodes = dstOn.ChainPotentials;

		Debug.Assert((nodes.Length & 1) == 0);

		var conclusions = new List<Conclusion>();
		for (var i = 0; i < nodes.Length; i += 2)
		{
			var (c1, d1, _) = nodes[i];
			var (c2, d2, _) = nodes[i + 1];
			if (c1 == c2)
			{
				foreach (var digit in (Mask)(grid.GetCandidates(c1) & ~(1 << d1 | 1 << d2)))
				{
					conclusions.Add(new(Elimination, c1, digit));
				}
			}
			else if (d1 == d2)
			{
				foreach (var house in (CellsMap[c1] + c2).CoveredHouses)
				{
					foreach (var cell in (HousesMap[house] & CandidatesMap[d1]) - c1 - c2)
					{
						conclusions.Add(new(Elimination, cell, d1));
					}
				}
			}
		}

		if (conclusions.Count == 0)
		{
			return null;
		}

		var result = new BidirectionalCycleStep([.. conclusions], dstOn, isX, isY);
		return new(result, result.CreateViews(grid));
	}

	/// <summary>
	/// Try to create an AIC hint.
	/// </summary>
	private ForcingChainStep CreateAicStep(scoped in Grid grid, ChainNode target, bool isX, bool isY)
	{
		var (candidate, isOn) = target;
		var result = new ForcingChainStep([new(isOn ? Assignment : Elimination, candidate)], target, isX, isY);
		return new(result, result.CreateViews(grid));
	}
}
