﻿global using System;
global using System.Collections.Generic;
global using System.CommandLine;
global using System.CommandLine.Annotations;
global using System.ComponentModel;
global using System.Linq;
global using System.Reflection;
global using System.Runtime.CompilerServices;
global using CommandLine;
global using CommandLine.Text;
global using Sudoku.CommandLine;
global using Sudoku.CommandLine.Commands;
global using Sudoku.Concepts.Collections;
global using Sudoku.Concepts.Parsing;
global using Sudoku.Generating;
global using Sudoku.Solving;
global using Sudoku.Solving.Manual;

[assembly: GlobalConfiguration(FullCommandNamePrefix = "/")]