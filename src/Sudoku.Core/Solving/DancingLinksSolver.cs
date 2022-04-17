﻿namespace Sudoku.Solving;

/// <summary>
/// Defines a solver that uses the dancing links algorithm.
/// </summary>
[Algorithm("Dancing links", UriLink = "https://en.wikipedia.org/wiki/Dancing_Links")]
public sealed class DancingLinksSolver : ISimpleSolver
{
	/// <summary>
	/// Indicates the stack that stores the raw data for the solutions.
	/// </summary>
	private readonly Stack<DataNode> _answerNodesStack = new();


	/// <summary>
	/// indicates the number of all found solutions.
	/// </summary>
	private int _solutionCount;

	/// <summary>
	/// Indicates the found solution.
	/// </summary>
	private Grid _solution;

	/// <summary>
	/// Indicates the root node of the full link map.
	/// </summary>
	private ColumnNode? _root;


	/// <inheritdoc/>
	public bool? Solve(in Grid grid, out Grid result)
	{
		Unsafe.SkipInit(out result);

		int[] gridValue = grid.ToArray();
		var dlx = new DancingLink(new ColumnNode(-1));
		_root = dlx.CreateLinkedList(gridValue);

		try
		{
			Search();

			result = _solution;
			return true;
		}
		catch (InvalidOperationException ex)
		{
			return ex.Message.Contains("multiple") ? false : null;
		}
	}

	/// <summary>
	/// Try to search the full dancing link map and get the possible solution.
	/// </summary>
	/// <exception cref="InvalidOperationException">Throws when the puzzle has multiple solutions.</exception>
	private void Search()
	{
		if (_solutionCount > 1)
		{
			throw new InvalidOperationException("The puzzle has multiple solutions.");
		}

		Debug.Assert(_root is not null);
		if (_root.Right == _root)
		{
			// All columns were removed!
			_solutionCount++;
			RecordSolution(_answerNodesStack, out _solution);
		}
		else
		{
			var c = ChooseNextColumn();
			Cover(c);

			for (var r = c.Down; r != c; r = r.Down)
			{
				_answerNodesStack.Push(r);
				for (var j = r.Right; j != r; j = j.Right)
				{
					Cover(j.Column!);
				}

				Search();
				r = _answerNodesStack.Pop();
				c = r.Column!;

				for (var j = r.Left; j != r; j = j.Left)
				{
					Uncover(j.Column!);
				}
			}

			Uncover(c);
		}
	}

	/// <summary>
	/// Cover the nodes for the specified column.
	/// </summary>
	/// <param name="column">The column.</param>
	private void Cover(DataNode column)
	{
		column.Right.Left = column.Left;
		column.Left.Right = column.Right;
		for (var i = column.Down; i != column; i = i.Down)
		{
			for (var j = i.Right; j != i; j = j.Right)
			{
				j.Down.Up = j.Up;
				j.Up.Down = j.Down;
				j.Column!.Size--;
			}
		}
	}

	/// <summary>
	/// Uncover the nodes for the specified column.
	/// </summary>
	/// <param name="column">The column.</param>
	private void Uncover(DataNode column)
	{
		for (var i = column.Up; i != column; i = i.Up)
		{
			for (var j = i.Left; j != i; j = j.Left)
			{
				j.Column!.Size++;
				j.Down.Up = j;
				j.Up.Down = j;
			}
		}
		column.Right.Left = column;
		column.Left.Right = column;
	}

	/// <summary>
	/// Try to gather all possible solutions, and determine whether the puzzle is valid.
	/// </summary>
	/// <param name="answer">The answers found.</param>
	/// <param name="result">The solution if the puzzle is unique.</param>
	/// <exception cref="InvalidOperationException">
	/// Throws when the puzzle has no possible solutions.
	/// </exception>
	private void RecordSolution(Stack<DataNode> answer, out Grid result)
	{
		int[] resultArray = new int[81];
		var idList = new List<int>(from k in answer select k.Id);
		idList.Sort();

		var gridList = new List<int>(from id in idList select id % 9 + 1);
		for (int i = 0, x = 0, y = 0; i < 81; i++)
		{
			resultArray[y * 9 + x++] = gridList[i];
			if ((i + 1) % 9 == 0)
			{
				y++;
				x = 0;
			}
		}

		result = new Grid(resultArray) is { IsValid: true } grid
			? grid
			: throw new InvalidOperationException("The puzzle has no possible solutions.");
	}

	/// <summary>
	/// Try to choose the next column node.
	/// </summary>
	/// <returns>The chosen next column node.</returns>
	[MemberNotNull(nameof(_root))]
	private ColumnNode ChooseNextColumn()
	{
		Debug.Assert(_root is not null);

		int size = int.MaxValue;
		var nextColumn = new ColumnNode(-1);
		var j = _root.Right.Column;
		while (j != _root)
		{
			if (j!.Size < size)
			{
				nextColumn = j;
				size = j.Size;
			}

			j = j.Right.Column;
		}
		return nextColumn;
	}
}
