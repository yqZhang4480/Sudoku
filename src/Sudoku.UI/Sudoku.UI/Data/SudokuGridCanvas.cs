﻿namespace Sudoku.UI.Data;

/// <summary>
/// Provides the canvas that stores the sudoku grid details.
/// </summary>
/// <param name="BaseGrid">
/// The <see cref="Grid"/> instance that stores and displays the <see cref="Shape"/>s.
/// </param>
/// <param name="CandidateTextBlockPool">Indicates the pool of candidate <see cref="TextBlock"/>s.</param>
/// <param name="CellTextBlockPool">Indicates the pool of cell <see cref="TextBlock"/>s.</param>
/// <param name="HighlightCellPool">
/// Indicates the pool of highlight cells displaying via <see cref="Border"/>s.
/// </param>
/// <param name="HighlightCandidatePool">
/// Indicates the pool of highlight candidates displaying via <see cref="Ellipse"/>s.
/// </param>
/// <param name="HighlightRegionPool">
/// Indicates the pool of highlight regions displaying via <see cref="Border"/>s.
/// </param>
public sealed record class SudokuGridCanvas(
	Grid BaseGrid,
	TextBlock[] CellTextBlockPool,
	TextBlock[] CandidateTextBlockPool,
	Border[] HighlightCellPool,
	Ellipse[] HighlightCandidatePool,
	Border[] HighlightRegionPool
)
{
	/// <summary>
	/// Indicates the solver that checks the uniqueness of sudoku puzzles.
	/// </summary>
	private static readonly FastSolver Solver = new();


	/// <summary>
	/// Initializes a <see cref="SudokuGridCanvas"/> instance with the specified <see cref="Grid"/> instance
	/// indicating the base control stores those values.
	/// </summary>
	/// <param name="baseGrid">The base <see cref="Grid"/> instance.</param>
	public SudokuGridCanvas(Grid baseGrid)
	: this(baseGrid, new TextBlock[81], new TextBlock[729], new Border[81], new Ellipse[729], new Border[27])
	{
		createMainGridOutlines();

		createCellControls();
		createCandidateControls();
		createCellBorders();
		createCandidateEllipses();
		createRegionBorders();


		void createMainGridOutlines()
		{
			const int size = 27;
			for (int i = 0; i < size; i++)
			{
				BaseGrid.RowDefinitions.Add(new());
				BaseGrid.ColumnDefinitions.Add(new());
			}

			for (int i = 0; i <= size; i += 3)
			{
				switch (i)
				{
					case 0:
					case 9:
					case 18:
					{
						f(top: 3, row: i, columnSpan: size);
						f(left: 3, column: i, rowSpan: size);

						break;
					}
					case 27:
					{
						f(bottom: 3, row: 26, columnSpan: size);
						f(right: 3, column: 26, rowSpan: size);

						break;
					}
					default:
					{
						f(top: 1, row: i, columnSpan: size);
						f(left: 1, column: i, rowSpan: size);

						break;
					}
				}
			}


			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			void f(
				double left = 0, double top = 0, double right = 0, double bottom = 0,
				int row = 0, int column = 0, int rowSpan = 1, int columnSpan = 1
			)
			{
				var border = new Border
				{
					BorderThickness = new(left, top, right, bottom),
					BorderBrush = new SolidColorBrush(Colors.White)
				};

				Grid.SetRow(border, row);
				Grid.SetRowSpan(border, rowSpan);
				Grid.SetColumn(border, column);
				Grid.SetColumnSpan(border, columnSpan);

				BaseGrid.Children.Add(border);
			}
		}

		void createCellControls()
		{
			for (int cell = 0; cell < 81; cell++)
			{
				var tb = new TextBlock
				{
					Text = (cell % 9 + 1).ToString(),
					Visibility = Visibility.Collapsed,
					Foreground = new SolidColorBrush(Colors.WhiteSmoke),
					FontSize = 40,
					FontFamily = new("Fira Code"),
					HorizontalAlignment = HorizontalAlignment.Center,
					VerticalAlignment = VerticalAlignment.Center,
					HorizontalTextAlignment = TextAlignment.Center
				};

				int row = cell / 9, column = cell % 9;
				Grid.SetRow(tb, row * 3);
				Grid.SetRowSpan(tb, 3);
				Grid.SetColumn(tb, column * 3);
				Grid.SetColumnSpan(tb, 3);

				BaseGrid.Children.Add(tb);

				CellTextBlockPool[cell] = tb;
			}
		}

		void createCandidateControls()
		{
			for (int candidate = 0; candidate < 729; candidate++)
			{
				int cell = candidate / 9, digit = candidate % 9;
				var tb = new TextBlock
				{
					Text = (digit + 1).ToString(),
					Visibility = Visibility.Visible,
					Foreground = new SolidColorBrush(Colors.Gray),
					FontSize = 14,
					FontFamily = new("Times New Roman"),
					HorizontalAlignment = HorizontalAlignment.Center,
					VerticalAlignment = VerticalAlignment.Center,
					HorizontalTextAlignment = TextAlignment.Center
				};

				int row = cell / 9, column = cell % 9;
				Grid.SetRow(tb, row * 3 + digit / 3);
				Grid.SetColumn(tb, column * 3 + digit % 3);

				BaseGrid.Children.Add(tb);

				CandidateTextBlockPool[cell * 9 + digit] = tb;
			}
		}

		void createCellBorders()
		{
			for (int cell = 0; cell < 81; cell++)
			{
				var border = new Border
				{
					BorderThickness = new(1.5),
					Visibility = Visibility.Collapsed,
					BorderBrush = new SolidColorBrush(Colors.Blue),
					Background = new SolidColorBrush(Color.FromArgb(64, 0, 0, 255))
				};

				int row = cell / 9, column = cell % 9;
				Grid.SetRow(border, row * 3);
				Grid.SetRowSpan(border, 3);
				Grid.SetColumn(border, column * 3);
				Grid.SetColumnSpan(border, 3);

				BaseGrid.Children.Add(border);

				HighlightCellPool[cell] = border;
			}
		}

		void createCandidateEllipses()
		{
			for (int candidate = 0; candidate < 729; candidate++)
			{
				int cell = candidate / 9, digit = candidate % 9;
				var ellipse = new Ellipse
				{
					StrokeThickness = 1.5,
					Visibility = Visibility.Collapsed,
					Stroke = new SolidColorBrush(Colors.Blue),
					Fill = new SolidColorBrush(Color.FromArgb(64, 0, 0, 255))
				};

				int row = cell / 9, column = cell % 9;
				Grid.SetRow(ellipse, row / 3 + digit / 3);
				Grid.SetColumn(ellipse, column / 3 + digit % 3);

				BaseGrid.Children.Add(ellipse);

				HighlightCandidatePool[candidate] = ellipse;
			}
		}

		void createRegionBorders()
		{
			int[] blockRowFactor = { 0, 9, 18, 0, 9, 18, 0, 9, 18 };
			int[] blockColumnFactor = { 0, 0, 0, 9, 9, 9, 18, 18, 18 };
			double uniformBorderThickness = 1.5;
			var borderBrush = new SolidColorBrush(Colors.Blue);
			var background = new SolidColorBrush(Color.FromArgb(64, 0, 0, 255));

			f(0, uniformBorderThickness, borderBrush, background);
			f(9, uniformBorderThickness, borderBrush, background);
			f(18, uniformBorderThickness, borderBrush, background);


			void f(int start, double uniformBorderThickness, Brush borderBrush, Brush background)
			{
				for (int region = start; region < start + 9; region++)
				{
					var (row, column, rowSpan, columnSpan) = start switch
					{
						0 => (blockRowFactor[region], blockColumnFactor[region], 9, 9),
						9 => (region % 9 * 3, 0, 1, 9),
						18 => (0, region % 9 * 3, 9, 1)
					};

					var border = new Border
					{
						BorderThickness = new(uniformBorderThickness),
						Visibility = Visibility.Collapsed,
						BorderBrush = borderBrush,
						Background = background
					};

					Grid.SetRow(border, row);
					Grid.SetColumn(border, column);
					Grid.SetRowSpan(border, rowSpan);
					Grid.SetColumnSpan(border, columnSpan);

					BaseGrid.Children.Add(border);

					HighlightRegionPool[region] = border;
				}
			}
		}
	}


	/// <summary>
	/// Load the sudoku puzzle.
	/// </summary>
	/// <param name="sudoku">The sudoku.</param>
	/// <exception cref="InvalidPuzzleException">Throws when the load opertion is failed.</exception>
	public void LoadSudoku(in SudokuGrid sudoku)
	{
		if (
			sudoku switch
			{
#if DEBUG
				{ IsDebuggerUndefined: true } =>
					UiResources.Current.ContentDialog_FailedDragPuzzleFile_Content_DebuggerUndefinedFailed1,
#endif
				{ IsUndefined: true } =>
					UiResources.Current.ContentDialog_FailedDragPuzzleFile_Content_UndefinedFailed,
				_ when !Solver.CheckValidity($"{sudoku:0}") =>
					UiResources.Current.ContentDialog_FailedDragPuzzleFile_Content_UniquenessFailed,
				_ => null
			} is string errorInfo
		)
		{
			throw new InvalidPuzzleException(sudoku, errorInfo);
		}

		for (int cell = 0; cell < 81; cell++)
		{
			switch (sudoku.GetStatus(cell))
			{
				case CellStatus.Empty:
				{
					// Show or hide the candidate text block.
					short candidates = sudoku.GetCandidates(cell);
					foreach (int digit in candidates)
					{
						CandidateTextBlockPool[cell * 9 + digit].Visibility = Visibility.Visible;
					}
					foreach (int digit in (short)(SudokuGrid.MaxCandidatesMask & ~candidates))
					{
						CandidateTextBlockPool[cell * 9 + digit].Visibility = Visibility.Collapsed;
					}

					// Hide the cell text block.
					CellTextBlockPool[cell].Visibility = Visibility.Collapsed;

					break;
				}
				case CellStatus.Modifiable:
				{
					// Show the cell text block.
					CellTextBlockPool[cell].Foreground = new SolidColorBrush(Colors.LightGray);

					// Hide the candidate text block.
					for (int digit = 0; digit < 9; digit++)
					{
						CandidateTextBlockPool[cell * 9 + digit].Visibility = Visibility.Collapsed;
					}

					goto ModifiableOrGiven;
				}
				case CellStatus.Given:
				{
					// Show the cell text block.
					CellTextBlockPool[cell].Foreground = new SolidColorBrush(Colors.White);

					// Hide the candidate text block.
					for (int digit = 0; digit < 9; digit++)
					{
						CandidateTextBlockPool[cell * 9 + digit].Visibility = Visibility.Collapsed;
					}

					goto ModifiableOrGiven;
				}

			ModifiableOrGiven:
				{
					ref var element = ref CellTextBlockPool[cell];
					element.Visibility = Visibility.Visible;
					element.Text = (sudoku[cell] + 1).ToString();

					break;
				}
			}
		}
	}
}
