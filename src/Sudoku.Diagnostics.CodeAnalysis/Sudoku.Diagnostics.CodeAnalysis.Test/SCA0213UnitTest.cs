﻿using VerifyCS = Sudoku.Diagnostics.CodeAnalysis.Test.CSharpAnalyzerVerifier<
	Sudoku.Diagnostics.CodeAnalysis.Analyzers.SCA0212_GridFormatAnalyzer
>;

namespace Sudoku.Diagnostics.CodeAnalysis.Test;

[TestClass]
public sealed class SCA0213UnitTest
{
	[TestMethod]
	public async Task TestCase_EmptyCode() => await VerifyCS.VerifyAnalyzerAsync(@"");

	[TestMethod]
	public async Task TestCase_Normal()
		=> await VerifyCS.VerifyAnalyzerAsync(
			"""
			#nullable enable

			using System;
			using Sudoku.Concepts;

			file sealed class Program
			{
				public void TestCase()
				{
					var grid = Grid.Empty;
					Console.WriteLine(grid.ToString("#"));
					Console.WriteLine({|#0:grid.ToString("+:", null)|});
					Console.WriteLine({|#1:grid.ToString("0+:")|});
				}
			}

			namespace Sudoku.Concepts
			{
				file struct Grid
				{
					public static readonly Grid Empty;
					
					public readonly string ToString() => throw new();
					public readonly string ToString(string? format) => throw new();
					public readonly string ToString(string? format, IFormatProvider? formatProvider) => throw new();
				}
			}
			""",
			VerifyCS.Diagnostic(nameof(SCA0213)).WithLocation(0),
			VerifyCS.Diagnostic(nameof(SCA0213)).WithLocation(1)
		);
}
