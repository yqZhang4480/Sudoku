﻿namespace Sudoku.Variants.Verifiers;

/// <summary>
/// Defines a verifier that checks for consecutive border bars.
/// </summary>
/// <param name="TargetGrid"><inheritdoc path="/param[@name='TargetGrid']"/></param>
/// <param name="Identifier"><inheritdoc path="/param[@name='Identifier']"/></param>
public sealed record ConsecutiveSudoku(scoped in Grid TargetGrid, Identifier Identifier) :
	VariantGridElementVerifier<BorderBarViewNode>(TargetGrid, Identifier)
{
	/// <inheritdoc/>
	public override BorderBarViewNode[] Verify()
	{
		ThrowIfNotSolved();

		var result = new List<BorderBarViewNode>();

		for (var row = 0; row < 9; row++)
		{
			for (var column = 0; column < 9; column++)
			{
				var cell = row * 9 + column;
				var adjacent1 = column + 1 >= 9 ? -1 : row * 9 + column + 1;
				var adjacent2 = row + 1 >= 9 ? -1 : (row + 1) * 9 + column;

				var a = TargetGrid[cell];
				foreach (var adjacent in stackalloc[] { adjacent1, adjacent2 })
				{
					if (adjacent == -1)
					{
						continue;
					}

					var b = TargetGrid[adjacent];
					if (Abs(a - b) == 1)
					{
						result.Add(new(Identifier, cell, adjacent));
					}
				}
			}
		}

		return result.ToArray();
	}
}
