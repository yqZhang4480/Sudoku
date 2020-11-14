﻿using System.Collections.Generic;
using Sudoku.Data;
using Sudoku.Data.Extensions;
using Sudoku.Solving.Manual.Intersections;
using Sudoku.Solving.Manual.Singles;
using Sudoku.Solving.Manual.Subsets;

namespace Sudoku.Solving.Extensions
{
	/// <summary>
	/// Provides extension methods on <see cref="SudokuGrid"/>.
	/// </summary>
	/// <seealso cref="SudokuGrid"/>
	public static class SudokuGridEx
	{
		/// <summary>
		/// All available SSTS searchers.
		/// </summary>
		private static readonly TechniqueSearcher[] SstsSearchers =
		{
			new SingleTechniqueSearcher(false, false, false),
			new LcTechniqueSearcher(),
			new SubsetTechniqueSearcher()
		};


		/// <summary>
		/// To clean the grid.
		/// </summary>
		/// <param name="this">(<see langword="this ref"/> parameter) The grid.</param>
		/// <remarks>
		/// "To clean a grid" means we process this grid to fill with singles that is found
		/// in <see cref="SingleTechniqueSearcher"/>, and remove eliminations that is found
		/// in <see cref="LcTechniqueSearcher"/> and <see cref="SubsetTechniqueSearcher"/>.
		/// The process won't stop until the puzzle cannot use these techniques.
		/// </remarks>
		/// <seealso cref="SingleTechniqueSearcher"/>
		/// <seealso cref="LcTechniqueSearcher"/>
		/// <seealso cref="SubsetTechniqueSearcher"/>
		public static void Clean(this ref SudokuGrid @this)
		{
		AtStart:
			for (int i = 0; i < SstsSearchers.Length; i++)
			{
				var searcher = SstsSearchers[i];
				var steps = new List<TechniqueInfo>();
				searcher.GetAll(steps, @this);
				if (steps.Count == 0)
				{
					continue;
				}

				foreach (var step in steps)
				{
					bool needAdd = false;
					foreach (var (t, c, d) in step.Conclusions)
					{
						switch (t)
						{
							case ConclusionType.Assignment when @this.GetStatus(c) == CellStatus.Empty:
							case ConclusionType.Elimination when @this.Exists(c, d) is true:
							{
								needAdd = true;

								goto FinalCheck;
							}
						}
					}

				FinalCheck:
					if (needAdd)
					{
						step.ApplyTo(ref @this);
						goto AtStart;
					}
				}
			}
		}

		/// <summary>
		/// Get the mask that is a result after the bitwise and operation processed all cells
		/// in the specified map.
		/// </summary>
		/// <param name="grid">(<see langword="this in"/> parameter) The grid.</param>
		/// <param name="map">(<see langword="in"/> parameter) The map.</param>
		/// <returns>The result.</returns>
		public static short BitwiseAndMasks(this in SudokuGrid grid, in GridMap map)
		{
			short mask = SudokuGrid.MaxCandidatesMask;
			foreach (int cell in map)
			{
				mask &= grid.GetCandidateMask(cell);
			}

			return mask;
		}

		/// <summary>
		/// Get the mask that is a result after the bitwise or operation processed all cells
		/// in the specified map.
		/// </summary>
		/// <param name="grid">(<see langword="this in"/> parameter) The grid.</param>
		/// <param name="map">(<see langword="in"/> parameter) The map.</param>
		/// <returns>The result.</returns>
		public static short BitwiseOrMasks(this in SudokuGrid grid, in GridMap map)
		{
			short mask = 0;
			foreach (int cell in map)
			{
				mask |= grid.GetCandidateMask(cell);
			}

			return mask;
		}
	}
}
