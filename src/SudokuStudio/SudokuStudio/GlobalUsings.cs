﻿#pragma warning disable CS8981

global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.Diagnostics;
global using System.Diagnostics.CodeAnalysis;
global using System.Diagnostics.CodeGen;
global using System.IO;
global using System.Linq;
global using System.Numerics;
global using System.Reflection;
global using System.Runtime.CompilerServices;
global using System.Runtime.InteropServices;
global using System.Runtime.InteropServices.WindowsRuntime;
global using System.Runtime.Versioning;
global using System.Text.Json;
global using System.Threading.Tasks;
global using Microsoft.UI;
global using Microsoft.UI.Composition;
global using Microsoft.UI.Composition.SystemBackdrops;
global using Microsoft.UI.Input;
global using Microsoft.UI.Windowing;
global using Microsoft.UI.Xaml;
global using Microsoft.UI.Xaml.Controls;
global using Microsoft.UI.Xaml.Controls.Primitives;
global using Microsoft.UI.Xaml.Input;
global using Microsoft.UI.Xaml.Markup;
global using Microsoft.UI.Xaml.Media;
global using Microsoft.UI.Xaml.Media.Animation;
global using Microsoft.UI.Xaml.Media.Imaging;
global using Microsoft.UI.Xaml.Navigation;
global using Sudoku.Checking;
global using Sudoku.Concepts;
global using Sudoku.Generating.Puzzlers;
global using Sudoku.Solving.Algorithms.Bitwise;
global using Sudoku.Text;
global using Sudoku.Text.Formatting;
global using Sudoku.Text.Notations;
global using SudokuStudio.AppLifecycle;
global using SudokuStudio.Input;
global using SudokuStudio.Interop;
global using SudokuStudio.Models;
global using SudokuStudio.Storage;
global using SudokuStudio.Views.Controls;
global using SudokuStudio.Views.Interaction;
global using SudokuStudio.Views.Pages;
global using SudokuStudio.Views.Pages.Operation;
global using SudokuStudio.Views.Windows;
global using Windows.ApplicationModel.DataTransfer;
global using Windows.Graphics;
global using Windows.Graphics.Display;
global using Windows.Graphics.Imaging;
global using Windows.Storage;
global using Windows.Storage.Pickers;
global using Windows.Storage.Search;
global using Windows.Storage.Streams;
global using Windows.UI;
global using Windows.UI.Core;
global using Windows.UI.ViewManagement;
global using WinRT;
global using WinRT.Interop;
global using static System.Numerics.BitOperations;
global using static System.Text.Json.JsonSerializer;
global using static Sudoku.Resources.MergedResources;
global using static Sudoku.SolutionWideReadOnlyFields;
global using static Sudoku.Solving.ConclusionType;
global using static SudokuStudio.Resources.ResourceDictionary;
global using mxaml = Microsoft.UI.Xaml;
global using GridLayout = Microsoft.UI.Xaml.Controls.Grid;
global using Grid = Sudoku.Concepts.Grid;
global using winsys = Windows.System;
