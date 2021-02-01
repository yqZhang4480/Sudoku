﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Sudoku.Data;
using Sudoku.Painting;
using Sudoku.Painting.Extensions;
using Sudoku.UI.Data;

namespace Sudoku.UI.Controls
{
	/// <summary>
	/// Indicates a sudoku panel that used for drawing a sudoku grid.
	/// </summary>
	public sealed partial class SudokuPanel : UserControl
	{
		/// <summary>
		/// Indicates the stack that uses for undoing or redoing a step.
		/// </summary>
		private readonly Stack<SudokuGrid> _undoStack = new(), _redoStack = new();


		/// <summary>
		/// Initializes a <see cref="SudokuPanel"/> instance with the default instantiation behavior.
		/// </summary>
		public SudokuPanel() => InitializeComponent();


		/// <summary>
		/// Indicates the preferences used.
		/// </summary>
		public PagePreferences Preferences => ProgramData.BaseWindow.Preferences;

		/// <summary>
		/// Indicates the grid painter instance. This property can be <see langword="null"/>
		/// when the panel doesn't finish its initialization.
		/// </summary>
		public GridPainter GridPainter { get; set; } = null!;


		/// <summary>
		/// Indicates an event triggered when undid a step.
		/// </summary>
		public event UndoEventHandler? Undo;

		/// <summary>
		/// Indicates an event triggered when redid a step.
		/// </summary>
		public event RedoEventHandler? Redo;


		/// <summary>
		/// Raises <see cref="Undo"/>.
		/// </summary>
		/// <param name="grid">The grid.</param>
		/// <seealso cref="Undo"/>
		private void OnUndoing(SudokuGrid grid)
		{
			// TODO: Update icons.

			Repaint();

			Undo?.Invoke(grid);
		}

		/// <summary>
		/// Raises <see cref="Redo"/>.
		/// </summary>
		/// <param name="grid">The grid.</param>
		/// <seealso cref="Redo"/>
		private void OnRedoing(SudokuGrid grid)
		{
			// TODO: Update icons.

			Repaint();

			Redo?.Invoke(grid);
		}


		/// <summary>
		/// Raises the event <see cref="Undo"/>.
		/// </summary>
		/// <seealso cref="Undo"/>
		private void UndoGrid()
		{
			if (_undoStack.Count == 0)
			{
				return;
			}

			var grid = _undoStack.Pop();
			_redoStack.Push(grid);

			OnUndoing(grid);
		}

		/// <summary>
		/// Raises the event <see cref="Redo"/>
		/// </summary>
		/// <see cref="Redo"/>
		private void RedoGrid()
		{
			if (_redoStack.Count == 0)
			{
				return;
			}

			var grid = _redoStack.Pop();
			_undoStack.Push(grid);

			OnRedoing(grid);
		}

		/// <summary>
		/// Initialize <see cref="GridPainter"/>.
		/// </summary>
		/// <seealso cref="GridPainter"/>
		[MemberNotNull(nameof(GridPainter))]
		private void InitializeGridPainter()
		{
			GridPainter = new(new((float)Width, (float)Height), Preferences) { Grid = SudokuGrid.Empty };
			Repaint();
		}

		/// <summary>
		/// Repaint the grid using the <see cref="Shape"/> controls.
		/// </summary>
		private void Repaint() => ImageGrid.Source = GridPainter.Paint().ToImageSource();


		/// <summary>
		/// Triggers when the sudoku panel is loaded.
		/// </summary>
		/// <param name="sender">The object to trigger the event.</param>
		/// <param name="e">The event arguments provided.</param>
		private void SudokuPanel_Loaded(object sender, RoutedEventArgs e) => InitializeGridPainter();
	}
}
