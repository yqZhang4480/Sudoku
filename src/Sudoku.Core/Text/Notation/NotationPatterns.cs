namespace Sudoku.Text.Notation;

/// <summary>
/// The extracted type that contains a list of methods that provides with patterns of the notations to be parsed.
/// </summary>
internal static partial class NotationPatterns
{
	[GeneratedRegex("""(R[1-9]{1,9}C[1-9]{1,9}|r[1-9]{1,9}c[1-9]{1,9})""", RegexOptions.Compiled, 5000)]
	public static partial Regex CellOrCellListPattern_RxCy();

	[GeneratedRegex("""[A-IKa-ik]{1,9}[1-9]{1,9}""", RegexOptions.Compiled, 5000)]
	public static partial Regex CellOrCellListPattern_K9();

	[GeneratedRegex("""\{\s*(R[1-9]{1,9}C[1-9]{1,9}|r[1-9]{1,9}c[1-9]{1,9})(,\s*(R[1-9]{1,9}C[1-9]{1,9}|r[1-9]{1,9}c[1-9]{1,9}))?\s*\}""", RegexOptions.Compiled, 5000)]
	public static partial Regex ComplexCellOrCellListPattern_RxCy();

	[GeneratedRegex("""[1-9]{1,9}(R[1-9]{1,9}C[1-9]{1,9}|r[1-9]{1,9}c[1-9]{1,9})""", RegexOptions.Compiled, 5000)]
	public static partial Regex Candidates_PrepositionalFormPattern();

	[GeneratedRegex("""(R[1-9]{1,9}C[1-9]{1,9}|r[1-9]{1,9}c[1-9]{1,9})\([1-9]{1,9}\)""", RegexOptions.Compiled, 5000)]
	public static partial Regex Candidates_PostpositionalFormPattern();

	[GeneratedRegex("""[1-9]{1,9}\{\s*(R[1-9]{1,9}C[1-9]{1,9}|r[1-9]{1,9}c[1-9]{1,9})(,\s*(R[1-9]{1,9}C[1-9]{1,9}|r[1-9]{1,9}c[1-9]{1,9}))?\s*\}""", RegexOptions.Compiled, 5000)]
	public static partial Regex Candidates_ComplexPrepositionalFormPattern();

	[GeneratedRegex("""\{\s*(R[1-9]{1,9}C[1-9]{1,9}|r[1-9]{1,9}c[1-9]{1,9})(,\s*(R[1-9]{1,9}C[1-9]{1,9}|r[1-9]{1,9}c[1-9]{1,9}))?\s*\}\([1-9]{1,9}\)""", RegexOptions.Compiled, 5000)]
	public static partial Regex Candidates_ComplexPostpositionalFormPattern();

	[GeneratedRegex("""[Rr]([1-9]+)[Cc]([1-9]+)\s*(=|!=|<>)\s*([1-9]+)""", RegexOptions.Compiled, 5000)]
	public static partial Regex ConclusionPattern_RxCy();

	[GeneratedRegex("""([A-IKa-ik]+)([1-9]+)\s*(=|!=|<>)\s*([1-9]+)""", RegexOptions.Compiled, 5000)]
	public static partial Regex ConclusionPattern_K9();

	[GeneratedRegex("""(\+?\d|\.)""", RegexOptions.Compiled, 5000)]
	public static partial Regex GridSusserDigitPattern();

	[GeneratedRegex("""\d(\|\d){242}""", RegexOptions.Compiled, 5000)]
	public static partial Regex GridOpenSudokuPattern();

	[GeneratedRegex("""(\<\d\>|\*\d\*|\d*[\+\-]?\d+)""", RegexOptions.Compiled, 5000)]
	public static partial Regex GridPencilmarkedPattern();

	[GeneratedRegex("""([\d\.\+]{9}(\r|\n|\r\n)){8}[\d\.\+]{9}""", RegexOptions.Compiled, 5000)]
	public static partial Regex GridSimpleMultilinePattern();

	[GeneratedRegex("""[\d\.\+]{80,}(\:(\d{3}\s+)*\d{3})?""", RegexOptions.Compiled, 5000)]
	public static partial Regex GridSusserPattern();

	[GeneratedRegex("""[\d\.\*]{1,9}(,[\d\.\*]{1,9}){8}""", RegexOptions.Compiled, 5000)]
	public static partial Regex GridShortenedSusserPattern();

	[GeneratedRegex("""\d*[\-\+]?\d+""", RegexOptions.Compiled, 5000)]
	public static partial Regex GridSukakuSegmentPattern();
}
