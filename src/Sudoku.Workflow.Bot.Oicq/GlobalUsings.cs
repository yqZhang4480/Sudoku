﻿global using System;
global using System.Collections.Concurrent;
global using System.Collections.Generic;
global using System.CommandLine;
global using System.Diagnostics.CodeAnalysis;
global using System.Drawing;
global using System.IO;
global using System.Linq;
global using System.Net.NetworkInformation;
global using System.Numerics;
global using System.Reflection;
global using System.Runtime.CompilerServices;
global using System.Runtime.Versioning;
global using System.Text.Json.Serialization;
global using System.Text.RegularExpressions;
global using System.Threading;
global using System.Threading.Tasks;
global using Mirai.Net.Data.Messages;
global using Mirai.Net.Data.Messages.Concretes;
global using Mirai.Net.Data.Messages.Receivers;
global using Mirai.Net.Data.Shared;
global using Mirai.Net.Modules;
global using Mirai.Net.Sessions;
global using Mirai.Net.Sessions.Http.Managers;
global using Mirai.Net.Utils.Scaffolds;
global using Sudoku.Concepts;
global using Sudoku.Drawing;
global using Sudoku.Generating;
global using Sudoku.Preprocessing.AutoFiller;
global using Sudoku.Presentation;
global using Sudoku.Presentation.Nodes;
global using Sudoku.Runtime.AnalysisServices;
global using Sudoku.Solving.Logical;
global using Sudoku.Solving.Logical.Techniques;
global using Sudoku.Workflow.Bot.Oicq.Annotations;
global using Sudoku.Workflow.Bot.Oicq.ComponentModel;
global using Sudoku.Workflow.Bot.Oicq.Handlers;
global using Sudoku.Workflow.Bot.Oicq.Lifecycle;
global using Sudoku.Workflow.Bot.Oicq.RootCommands;
global using Sudoku.Workflow.Bot.Oicq.RootCommands.Periodic;
global using Sudoku.Workflow.Bot.Oicq.ValueConverters;
global using static System.Algorithm.Sequences;
global using static System.Math;
global using static System.Text.Json.JsonSerializer;
global using static Sudoku.Workflow.Bot.Oicq.Constants;
global using static Sudoku.Workflow.Bot.Oicq.Lifecycle.EnvironmentVariables;
global using Group = Mirai.Net.Data.Shared.Group;
global using SpecialFolder = System.Environment.SpecialFolder;
global using File = System.IO.File;
