﻿namespace SudokuStudio.Configuration;

/// <summary>
/// Defines a list of UI-related preference items. Some items in this group may not be found in settings page
/// because they are controlled by UI only, not by users.
/// </summary>
public sealed class UIPreferenceGroup : PreferenceGroup
{
	/// <inheritdoc cref="SudokuPane.DisplayCandidates"/>
	public bool DisplayCandidates { get; set; }

	/// <inheritdoc cref="SudokuPane.DisplayCursors"/>
	public bool DisplayCursors { get; set; }

	/// <inheritdoc cref="SudokuPane.UseDifferentColorToDisplayDeltaDigits"/>
	public bool DistinctWithDeltaDigits { get; set; }

	/// <inheritdoc cref="SudokuPane.HighlightCandidateCircleScale"/>
	public decimal HighlightedPencilmarkBackgroundEllipseScale { get; set; }

	/// <inheritdoc cref="SudokuPane.HighlightBackgroundOpacity"/>
	public decimal HighlightedBackgroundOpacity { get; set; }

	/// <inheritdoc cref="SudokuPane.ChainStrokeThickness"/>
	public decimal ChainStrokeThickness { get; set; }

	/// <inheritdoc cref="SudokuPane.CoordinateLabelDisplayKind"/>
	public CoordinateLabelDisplayKind CoordinateLabelDisplayKind { get; set; }

	/// <inheritdoc cref="SudokuPane.CoordinateLabelDisplayMode"/>
	public CoordinateLabelDisplayMode CoordinateLabelDisplayMode { get; set; }

	/// <inheritdoc cref="SudokuPane.DeltaCellColor"/>
	public Color DeltaValueColor { get; set; }

	/// <inheritdoc cref="SudokuPane.DeltaCandidateColor"/>
	public Color DeltaPencilmarkColor { get; set; }

	/// <inheritdoc cref="SudokuPane.BorderColor"/>
	public Color SudokuPaneBorderColor { get; set; }

	/// <inheritdoc cref="SudokuPane.CursorBackgroundColor"/>
	public Color CursorBackgroundColor { get; set; }

	/// <inheritdoc cref="SudokuPane.LinkColor"/>
	public Color ChainColor { get; set; }

	/// <inheritdoc cref="SudokuPane.NormalColor"/>
	public Color NormalColor { get; set; }

	/// <inheritdoc cref="SudokuPane.AssignmentColor"/>
	public Color AssignmentColor { get; set; }

	/// <inheritdoc cref="SudokuPane.OverlappedAssignmentColor"/>
	public Color OverlappedAssignmentColor { get; set; }

	/// <inheritdoc cref="SudokuPane.EliminationColor"/>
	public Color EliminationColor { get; set; }

	/// <inheritdoc cref="SudokuPane.CannibalismColor"/>
	public Color CannibalismColor { get; set; }

	/// <inheritdoc cref="SudokuPane.ExofinColor"/>
	public Color ExofinColor { get; set; }

	/// <inheritdoc cref="SudokuPane.EndofinColor"/>
	public Color EndofinColor { get; set; }

	/// <inheritdoc cref="SudokuPane.StrongLinkDashStyle"/>
	public DashArray StrongLinkDashStyle { get; set; }

	/// <inheritdoc cref="SudokuPane.WeakLinkDashStyle"/>
	public DashArray WeakLinkDashStyle { get; set; }

	/// <inheritdoc cref="SudokuPane.CycleLikeLinkDashStyle"/>
	public DashArray CyclingCellLinkDashStyle { get; set; }

	/// <inheritdoc cref="SudokuPane.OtherLinkDashStyle"/>
	public DashArray OtherLinkDashStyle { get; set; }

	/// <inheritdoc cref="SudokuPane.AuxiliaryColors"/>
	public ColorPalette AuxiliaryColors { get; set; } = null!;

	/// <inheritdoc cref="SudokuPane.DifficultyLevelForegrounds"/>
	public ColorPalette DifficultyLevelForegrounds { get; set; } = null!;

	/// <inheritdoc cref="SudokuPane.DifficultyLevelBackgrounds"/>
	public ColorPalette DifficultyLevelBackgrounds { get; set; } = null!;

	/// <inheritdoc cref="SudokuPane.UserDefinedColorPalette"/>
	public ColorPalette UserDefinedColorPalette { get; set; } = null!;

	/// <inheritdoc cref="SudokuPane.AlmostLockedSetsColors"/>
	public ColorPalette AlmostLockedSetsColors { get; set; } = null!;

	/// <summary>
	/// Indicates the font data of given digits.
	/// </summary>
	public FontSerializationData GivenFontData { get; set; } = null!;

	/// <summary>
	/// Indicates the font data of modifiable digits.
	/// </summary>
	public FontSerializationData ModifiableFontData { get; set; } = null!;

	/// <summary>
	/// Indicates the font data of pencilmarked digits.
	/// </summary>
	public FontSerializationData PencilmarkFontData { get; set; } = null!;

	/// <summary>
	/// Indicates the font data of Baba-grouping characters.
	/// </summary>
	public FontSerializationData BabaGroupingFontData { get; set; } = null!;

	/// <summary>
	/// Indicates the font data of coordinate labels.
	/// </summary>
	public FontSerializationData CoordinateLabelFontData { get; set; } = null!;


	/// <inheritdoc/>
	public override void CoverProperties()
	{
		var pane = ((App)Application.Current).SudokuPane!;
		pane.DisplayCandidates = DisplayCandidates;
		pane.DisplayCursors = DisplayCursors;
		pane.UseDifferentColorToDisplayDeltaDigits = DistinctWithDeltaDigits;
		pane.HighlightCandidateCircleScale = (double)HighlightedPencilmarkBackgroundEllipseScale;
		pane.HighlightBackgroundOpacity = (double)HighlightedBackgroundOpacity;
		pane.ChainStrokeThickness = (double)ChainStrokeThickness;
		pane.CoordinateLabelDisplayKind = CoordinateLabelDisplayKind;
		pane.CoordinateLabelDisplayMode = CoordinateLabelDisplayMode;
		pane.DeltaCellColor = DeltaValueColor;
		pane.DeltaCandidateColor = DeltaPencilmarkColor;
		pane.BorderColor = SudokuPaneBorderColor;
		pane.CursorBackgroundColor = CursorBackgroundColor;
		pane.LinkColor = ChainColor;
		pane.NormalColor = NormalColor;
		pane.AssignmentColor = AssignmentColor;
		pane.OverlappedAssignmentColor = OverlappedAssignmentColor;
		pane.EliminationColor = EliminationColor;
		pane.CannibalismColor = CannibalismColor;
		pane.ExofinColor = ExofinColor;
		pane.EndofinColor = EndofinColor;
		pane.StrongLinkDashStyle = StrongLinkDashStyle;
		pane.WeakLinkDashStyle = WeakLinkDashStyle;
		pane.CycleLikeLinkDashStyle = CyclingCellLinkDashStyle;
		pane.OtherLinkDashStyle = OtherLinkDashStyle;
		pane.AuxiliaryColors = AuxiliaryColors;
		pane.DifficultyLevelForegrounds = DifficultyLevelForegrounds;
		pane.DifficultyLevelBackgrounds = DifficultyLevelBackgrounds;
		pane.UserDefinedColorPalette = UserDefinedColorPalette;
		pane.AlmostLockedSetsColors = AlmostLockedSetsColors;
		pane.ValueFont = new(GivenFontData.FontName);
		pane.ValueFontScale = (double)GivenFontData.FontScale;
		pane.GivenColor = GivenFontData.FontColor;
		pane.ModifiableColor = ModifiableFontData.FontColor;
		pane.PencilmarkFont = new(PencilmarkFontData.FontName);
		pane.PencilmarkFontScale = (double)PencilmarkFontData.FontScale;
		pane.PencilmarkColor = PencilmarkFontData.FontColor;
		pane.BabaGroupLabelFont = new(BabaGroupingFontData.FontName);
		pane.BabaGroupLabelFontScale = (double)BabaGroupingFontData.FontScale;
		pane.BabaGroupLabelColor = BabaGroupingFontData.FontColor;
		pane.CoordinateLabelFont = new(CoordinateLabelFontData.FontName);
		pane.CoordinateLabelFontScale = (double)CoordinateLabelFontData.FontScale;
		pane.CoordinateLabelColor = CoordinateLabelFontData.FontColor;
	}
}
