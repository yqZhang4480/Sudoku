﻿namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Irregular Wing</b> technique.
/// </summary>
public abstract class IrregularWingStep(Conclusion[] conclusions, View[]? views) : WingStep(conclusions, views);
