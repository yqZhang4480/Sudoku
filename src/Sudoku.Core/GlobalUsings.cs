global using System.Algorithm;
global using System.Buffers;
global using System.ComponentModel;
global using System.Globalization;
global using System.Runtime.Intrinsics;
global using System.Runtime.Messages;
global using System.SourceGeneration;
global using Sudoku.Algorithm.Collections;
global using Sudoku.Algorithm.Solving;
global using Sudoku.Analytics;
global using Sudoku.Text.Formatting;
global using Sudoku.Text.Notations;
global using Sudoku.Text.Parsing;
global using static System.Algorithm.Sequences;
global using static System.Algorithm.Sorting;
global using static System.Math;
global using static Sudoku.Resources.MergedResources;
global using static Sudoku.Runtime.MaskServices.MaskOperations;
global using Chute = (Sudoku.Concepts.CellMap Cells, bool IsRow, /*Mask*/ short HousesMask);
global using MatrixRow = (Sudoku.Algorithm.Collections.DancingLinkNode Cell, Sudoku.Algorithm.Collections.DancingLinkNode Row, Sudoku.Algorithm.Collections.DancingLinkNode Column, Sudoku.Algorithm.Collections.DancingLinkNode Block);
global using unsafe RefreshingCandidatesMethodPtr = delegate*<ref Sudoku.Concepts.Grid, void>;
global using unsafe ValueChangedMethodPtr = delegate*<ref Sudoku.Concepts.Grid, int, short, short, int, void>;
global using unsafe ParserMethodPtr = delegate*<ref Sudoku.Text.Parsing.GridParser, Sudoku.Concepts.Grid>;
global using unsafe GridCellFilter = delegate*<in Sudoku.Concepts.Grid, /*Cell*/ int, bool>;
global using unsafe GridCellDigitFilter = delegate*<in Sudoku.Concepts.Grid, /*Cell*/ int, /*Digit*/ int, bool>;
