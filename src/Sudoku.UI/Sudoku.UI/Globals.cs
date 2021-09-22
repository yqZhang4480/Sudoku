﻿global using System;
global using System.Collections.Generic;
global using System.Diagnostics.CodeAnalysis;
global using System.Dynamic;
global using System.IO;
global using System.Linq;
global using System.Numerics;
global using System.Reflection;
global using System.Runtime.CompilerServices;
global using System.Runtime.InteropServices;
global using System.Text.RegularExpressions;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.UI;
global using Microsoft.UI.Xaml;
global using Microsoft.UI.Xaml.Controls;
global using Microsoft.UI.Xaml.Media;
global using Microsoft.UI.Xaml.Navigation;
global using Microsoft.UI.Xaml.Shapes;
global using Sudoku.CodeGenerating;
global using Sudoku.Data;
global using Sudoku.Models;
global using Sudoku.Solving.BruteForces;
global using Sudoku.UI.Data;
global using Sudoku.UI.Pages.MainWindow;
global using Sudoku.UI.Resources;
global using Windows.ApplicationModel.DataTransfer;
global using Windows.Storage;
global using Windows.UI;
global using Windows.UI.Text;
global using static Sudoku.UI.Constants;
global using static Sudoku.UI.Constants.Formats;
global using Grid = Microsoft.UI.Xaml.Controls.Grid;
global using SudokuGrid = Sudoku.Data.Grid;

[assembly: AutoDeconstructExtension<CornerRadius>(nameof(CornerRadius.TopLeft), nameof(CornerRadius.TopRight), nameof(CornerRadius.BottomLeft), nameof(CornerRadius.BottomRight))]