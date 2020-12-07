﻿using System;
using System.Collections.Generic;
using System.Extensions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Sudoku.Data;
using Sudoku.Data.Extensions;
using static Sudoku.Constants.Processings;

namespace Sudoku.Solving.Checking
{
	/// <summary>
	/// Encapsulates a BUG technique checker.
	/// </summary>
	public sealed class BugChecker
	{
		/// <summary>
		/// Initializes an instance with the specified grid.
		/// </summary>
		/// <param name="puzzle">(<see langword="in"/> parameter) The current puzzle grid.</param>
		public BugChecker(in SudokuGrid puzzle) =>
			Puzzle = puzzle.IsValid()
				? puzzle
				: throw new ArgumentException("The specified grid doesn't have a unique solution.", nameof(puzzle));


		/// <summary>
		/// Indicates the current grid is a BUG+n pattern.
		/// </summary>
		public bool IsBugPattern => GetAllTrueCandidates().Count != 0;

		/// <summary>
		/// The grid.
		/// </summary>
		public SudokuGrid Puzzle { get; }

		/// <summary>
		/// Indicates all true candidates (non-BUG candidates).
		/// </summary>
		public IReadOnlyList<int> TrueCandidates => GetAllTrueCandidates();


		/// <summary>
		/// Get all true candidates when the number of empty cells
		/// is below than the argument.
		/// </summary>
		/// <param name="maximumEmptyCells">The maximum number of the empty cells.</param>
		/// <returns>All true candidates.</returns>
		[SkipLocalsInit]
		public IReadOnlyList<int> GetAllTrueCandidates(int maximumEmptyCells)
		{
			TechniqueSearcher.InitializeMaps(Puzzle);

			// Get the number of multivalue cells.
			// If the number of that is greater than the specified number,
			// here will return the default list directly.
			int multivalueCellsCount = 0;
			foreach (int value in TechniqueSearcher.EmptyMap)
			{
				switch (Puzzle.GetCandidateMask(value).PopCount())
				{
					case 1:
					case > 2 when ++multivalueCellsCount > maximumEmptyCells:
					{
						return Array.Empty<int>();
					}
				}
			}

			// Store all bivalue cells and construct the relations.
			var span = (stackalloc int[3]);
			var stack = new GridMap[multivalueCellsCount + 1, 9];
			foreach (int cell in TechniqueSearcher.BivalueMap)
			{
				foreach (int digit in Puzzle.GetCandidateMask(cell))
				{
					ref var map = ref stack[0, digit];
					map.AddAnyway(cell);

					span[0] = RegionLabel.Row.GetRegion(cell);
					span[1] = RegionLabel.Column.GetRegion(cell);
					span[2] = RegionLabel.Block.GetRegion(cell);
					foreach (int region in span)
					{
						if ((map & RegionMaps[region]).Count > 2)
						{
							// The specified region contains at least three positions to fill with the digit,
							// which is invalid in any BUG + n patterns.
							return Array.Empty<int>();
						}
					}
				}
			}

			// Store all multivalue cells.
			// Suppose the pattern is the simplest BUG + 1 pattern (i.e. Only one multi-value cell).
			// The comments will help you to understand the processing.
			short mask;
			short[,] pairs = new short[multivalueCellsCount, 37]; // 37 == (1 + 8) * 8 / 2 + 1
			int[] multivalueCells = (TechniqueSearcher.EmptyMap - TechniqueSearcher.BivalueMap).ToArray();
			for (int i = 0, length = multivalueCells.Length; i < length; i++)
			{
				mask = Puzzle.GetCandidateMask(multivalueCells[i]); // eg. { 2, 4, 6 } (42)
				short[] pairList = GetAllCombinations(mask, 2); // eg. { 2, 4 }, { 4, 6 }, { 2, 6 } (10, 40, 34)

				// eg. pairs[i, ..] = { 3, { 2, 4 }, { 4, 6 }, { 2, 6 } } ({ 3, 10, 40, 34 })
				pairs[i, 0] = (short)pairList.Length;
				for (int z = 1, pairListLength = pairList.Length; z <= pairListLength; z++)
				{
					pairs[i, z] = pairList[z - 1];
				}
			}

			// Now check the pattern.
			// If the pattern is a valid BUG + n, the processing here will give you one plan of all possible
			// combinations; otherwise, none will be found.
			var playground = (stackalloc int[3]);
			int currentIndex = 1;
			int[] chosen = new int[multivalueCellsCount + 1];
			var resultMap = new GridMap[9];
			var result = new List<int>();
			do
			{
				int i;
				int currentCell = multivalueCells[currentIndex - 1];
				bool @continue = false;
				for (i = chosen[currentIndex] + 1; i <= pairs[currentIndex - 1, 0]; i++)
				{
					@continue = true;
					mask = pairs[currentIndex - 1, i];
					foreach (int digit in pairs[currentIndex - 1, i])
					{
						var temp = stack[currentIndex - 1, digit];
						temp.AddAnyway(currentCell);

						playground[0] = RegionLabel.Block.GetRegion(currentCell);
						playground[1] = RegionLabel.Row.GetRegion(currentCell);
						playground[2] = RegionLabel.Column.GetRegion(currentCell);
						foreach (int region in playground)
						{
							if ((temp & RegionMaps[region]).Count > 2)
							{
								@continue = false;
								break;
							}
						}

						if (!@continue) break;
					}

					if (@continue) break;
				}

				if (@continue)
				{
					for (int z = 0; z < stack.GetLength(1); z++)
					{
						stack[currentIndex, z] = stack[currentIndex - 1, z];
					}

					chosen[currentIndex] = i;
					int pos1;
					unsafe
					{
						pos1 = (&mask)->FindFirstSet();
					}

					stack[currentIndex, pos1].AddAnyway(currentCell);
					stack[currentIndex, mask.GetNextSet(pos1)].AddAnyway(currentCell);
					if (currentIndex == multivalueCellsCount)
					{
						// Iterate on each digit.
						for (int digit = 0; digit < 9; digit++)
						{
							// Take the cell that doesn't contain in the map above.
							// Here, the cell is the "true candidate cell".
							ref var map = ref resultMap[digit];
							map = TechniqueSearcher.CandMaps[digit] - stack[currentIndex, digit];
							foreach (int cell in map)
							{
								result.Add(cell * 9 + digit);
							}
						}

						return result;
					}
					else
					{
						currentIndex++;
					}
				}
				else
				{
					chosen[currentIndex--] = 0;
				}
			} while (currentIndex > 0);

			return result;
		}

		/// <summary>
		/// Get all true candidates when the number of empty cells
		/// is below than the argument asynchronizedly.
		/// </summary>
		/// <param name="maximumEmptyCells">The maximum number of the empty cells.</param>
		/// <returns>The task to get all true candidates.</returns>
		public async Task<IReadOnlyList<int>> GetAllTrueCandidatesAsync(int maximumEmptyCells) =>
			await Task.Run(() => GetAllTrueCandidates(maximumEmptyCells));


		/// <summary>
		/// Get all true candidates.
		/// </summary>
		/// <returns>All true candidates.</returns>
		public IReadOnlyList<int> GetAllTrueCandidates() => GetAllTrueCandidates(20);


		/// <summary>
		/// Get all combinations of a specified mask.
		/// </summary>
		/// <param name="mask">The mask.</param>
		/// <param name="oneCount">
		/// The number of <see langword="true"/> bits.
		/// </param>
		/// <returns>All combinations.</returns>
		private static short[] GetAllCombinations(short mask, int oneCount)
		{
			var result = new List<short>();
			foreach (short z in new BitSubsetsGenerator(9, oneCount))
			{
				if ((mask | z) == mask)
				{
					result.Add(z);
				}
			}

			return result.ToArray();
		}
	}
}
