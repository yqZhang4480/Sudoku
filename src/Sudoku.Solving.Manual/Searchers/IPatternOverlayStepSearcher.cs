﻿namespace Sudoku.Solving.Manual.Searchers;

/// <summary>
/// Provides with a <b>Pattern Overlay</b> step searcher.
/// The step searcher will include the following techniques:
/// <list type="bullet">
/// <item>Pattern Overlay</item>
/// </list>
/// </summary>
public interface IPatternOverlayStepSearcher : ILastResortStepSearcher
{
	/// <summary>
	/// Get all invalid positions.
	/// </summary>
	/// <param name="grid">The grid.</param>
	/// <returns>The 9 maps for invalid positions of each digit.</returns>
	protected static sealed CellMap[] GetInvalidPos(scoped in Grid grid)
	{
		var result = new CellMap[9];
		var invalidPos = new CellMap[9];
		var mustPos = new CellMap[9];
		for (int digit = 0; digit < 9; digit++)
		{
			for (int cell = 0; cell < 81; cell++)
			{
				if ((grid.GetCandidates(cell) >> digit & 1) == 0)
				{
					invalidPos[digit].Add(cell);
				}
				else if (grid[cell] == digit)
				{
					mustPos[digit].Add(cell);
				}
			}
		}

		for (int digit = 0; digit < 9; digit++)
		{
			foreach (var map in GetTemplates())
			{
				if (mustPos[digit] - map || invalidPos[digit] & map)
				{
					continue;
				}

				result[digit] |= map;
			}

			result[digit] = CandidatesMap[digit] - result[digit];
		}

		return result;
	}

	/// <summary>
	/// Get all possible templates. The total number of all possible pattern overlay templates is 46656.
	/// </summary>
	/// <returns>The templates.</returns>
	protected static sealed IEnumerable<CellMap> GetTemplates()
	{
		for (int i1 = 0; i1 < 9; i1++)
		{
			for (int i2 = 0; i2 < 9; i2++)
			{
				if (i2 / 3 != i1 / 3)
				{
					for (int i3 = 0; i3 < 9; i3++)
					{
						if (i3 / 3 != i1 / 3 && i3 / 3 != i2 / 3)
						{
							for (int i4 = 0; i4 < 9; i4++)
							{
								if (i4 != i1 && i4 != i2 && i4 != i3)
								{
									for (int i5 = 0; i5 < 9; i5++)
									{
										if (i5 != i1 && i5 != i2 && i5 != i3 && i5 / 3 != i4 / 3)
										{
											for (int i6 = 0; i6 < 9; i6++)
											{
												if (i6 != i1 && i6 != i2 && i6 != i3 && i6 / 3 != i4 / 3 && i6 / 3 != i5 / 3)
												{
													for (int i7 = 0; i7 < 9; i7++)
													{
														if (i7 != i1 && i7 != i2 && i7 != i3 && i7 != i4 && i7 != i5 && i7 != i6)
														{
															for (int i8 = 0; i8 < 9; i8++)
															{
																if (i8 != i1 && i8 != i2 && i8 != i3 && i8 != i4 && i8 != i5 && i8 != i6 && i8 / 3 != i7 / 3)
																{
																	for (int i9 = 0; i9 < 9; i9++)
																	{
																		if (i9 != i1 && i9 != i2 && i9 != i3 && i9 != i4 && i9 != i5 && i9 != i6 && i9 / 3 != i7 / 3 && i9 / 3 != i8 / 3)
																		{
																			yield return CellMap.CreateByBits(
																				1 << i1 | 1 << (i2 + 9) | 1 << (i3 + 18),
																				1 << i4 | 1 << (i5 + 9) | 1 << (i6 + 18),
																				1 << i7 | 1 << (i8 + 9) | 1 << (i9 + 18)
																			);
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}
}

[StepSearcher]
[StepSearcherOptions(IsDirect = true)]
internal sealed unsafe partial class PatternOverlayStepSearcher : IPatternOverlayStepSearcher
{
	/// <inheritdoc/>
	public IStep? GetAll(ICollection<IStep> accumulator, scoped in Grid grid, bool onlyFindOne)
	{
		var templates = IPatternOverlayStepSearcher.GetInvalidPos(grid);
		for (int digit = 0; digit < 9; digit++)
		{
			if (templates[digit] is not (var template and not []))
			{
				continue;
			}

			var step = new PatternOverlayStep(from cell in template select new Conclusion(Elimination, cell, digit));
			if (onlyFindOne)
			{
				return step;
			}

			accumulator.Add(step);
		}

		return null;
	}
}
