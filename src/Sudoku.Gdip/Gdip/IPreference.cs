﻿namespace Sudoku.Gdip;

/// <summary>
/// Defines the basic preferences.
/// </summary>
public interface IPreference : ICloneable<IPreference>
{
	/// <summary>
	/// Indicates whether the form shows candidates.
	/// </summary>
	bool ShowCandidates { get; set; }

	/// <summary>
	/// Indicates whether the grid painter will use new algorithm to render a house (lighter).
	/// </summary>
	bool ShowLightHouse { get; set; }

	/// <summary>
	/// Indicates the scale of values.
	/// </summary>
	decimal ValueScale { get; set; }

	/// <summary>
	/// Indicates the scale of candidates.
	/// </summary>
	decimal CandidateScale { get; set; }

	/// <summary>
	/// Indicates the grid line width of the sudoku grid to render.
	/// </summary>
	float GridLineWidth { get; set; }

	/// <summary>
	/// Indicates the block line width of the sudoku grid to render.
	/// </summary>
	float BlockLineWidth { get; set; }

	/// <summary>
	/// Indicates the footer text font size.
	/// </summary>
	float FooterTextFontSize { get; set; }

	/// <summary>
	/// Indicates the font of given digits to render.
	/// </summary>
	string? GivenFontName { get; set; }

	/// <summary>
	/// Indicates the font of modifiable digits to render.
	/// </summary>
	string? ModifiableFontName { get; set; }

	/// <summary>
	/// Indicates the font of candidate digits to render.
	/// </summary>
	string? CandidateFontName { get; set; }

	/// <summary>
	/// Indicates the font of unknown values to render.
	/// </summary>
	string? UnknownFontName { get; set; }

	/// <summary>
	/// Indicates the font of footer text.
	/// </summary>
	string FooterTextFontName { get; set; }

	/// <summary>
	/// Indicates the font style of the givens.
	/// </summary>
	FontStyle GivenFontStyle { get; set; }

	/// <summary>
	/// Indicates the font style of the modifiables.
	/// </summary>
	FontStyle ModifiableFontStyle { get; set; }

	/// <summary>
	/// Indicates the font style of the candidates.
	/// </summary>
	FontStyle CandidateFontStyle { get; set; }

	/// <summary>
	/// Indicates the font style of an unknown.
	/// </summary>
	FontStyle UnknownFontStyle { get; set; }

	/// <summary>
	/// Indicates the font style of footer text.
	/// </summary>
	FontStyle FooterTextFontStyle { get; set; }

	/// <summary>
	/// Indicates the given digits to render.
	/// </summary>
	Color GivenColor { get; set; }

	/// <summary>
	/// Indicates the modifiable digits to render.
	/// </summary>
	Color ModifiableColor { get; set; }

	/// <summary>
	/// Indicates the candidate digits to render.
	/// </summary>
	Color CandidateColor { get; set; }

	/// <summary>
	/// Indicates the color used for painting for focused cells.
	/// </summary>
	Color FocusedCellColor { get; set; }

	/// <summary>
	/// Indicates the elimination color.
	/// </summary>
	Color EliminationColor { get; set; }

	/// <summary>
	/// Indicates the cannibalism color.
	/// </summary>
	Color CannibalismColor { get; set; }

	/// <summary>
	/// Indicates the chain color.
	/// </summary>
	Color ChainColor { get; set; }

	/// <summary>
	/// Indicates the background color of the sudoku grid to render.
	/// </summary>
	Color BackgroundColor { get; set; }

	/// <summary>
	/// Indicates the grid line color of the sudoku grid to render.
	/// </summary>
	Color GridLineColor { get; set; }

	/// <summary>
	/// Indicates the block line color of the sudoku grid to render.
	/// </summary>
	Color BlockLineColor { get; set; }

	/// <summary>
	/// Indicates the color of the crosshatching outline.
	/// </summary>
	Color CrosshatchingOutlineColor { get; set; }

	/// <summary>
	/// Indicates the color of the crosshatching inner.
	/// </summary>
	Color CrosshatchingInnerColor { get; set; }

	/// <summary>
	/// Indicates the color of the unknown identifier color.
	/// </summary>
	Color UnknownIdentifierColor { get; set; }

	/// <summary>
	/// Indicates the color of the cross sign.
	/// </summary>
	Color CrossSignColor { get; set; }

	/// <summary>
	/// Indicates footer text color.
	/// </summary>
	Color FooterTextColor { get; set; }

	/// <summary>
	/// Indicates the color 1.
	/// </summary>
	Color Color1 { get; set; }

	/// <summary>
	/// Indicates the color 2.
	/// </summary>
	Color Color2 { get; set; }

	/// <summary>
	/// Indicates the color 3.
	/// </summary>
	Color Color3 { get; set; }

	/// <summary>
	/// Indicates the color 4.
	/// </summary>
	Color Color4 { get; set; }

	/// <summary>
	/// Indicates the color 5.
	/// </summary>
	Color Color5 { get; set; }

	/// <summary>
	/// Indicates the color 6.
	/// </summary>
	Color Color6 { get; set; }

	/// <summary>
	/// Indicates the color 7.
	/// </summary>
	Color Color7 { get; set; }

	/// <summary>
	/// Indicates the color 8.
	/// </summary>
	Color Color8 { get; set; }

	/// <summary>
	/// Indicates the color 9.
	/// </summary>
	Color Color9 { get; set; }

	/// <summary>
	/// Indicates the color 10.
	/// </summary>
	Color Color10 { get; set; }

	/// <summary>
	/// Indicates the color 11.
	/// </summary>
	Color Color11 { get; set; }

	/// <summary>
	/// Indicates the color 12.
	/// </summary>
	Color Color12 { get; set; }

	/// <summary>
	/// Indicates the color 13.
	/// </summary>
	Color Color13 { get; set; }

	/// <summary>
	/// Indicates the color 14.
	/// </summary>
	Color Color14 { get; set; }

	/// <summary>
	/// Indicates the color 15.
	/// </summary>
	Color Color15 { get; set; }


	/// <summary>
	/// Indicates the default instance.
	/// </summary>
	static IPreference Default => new Preference();


	/// <summary>
	/// Copies and covers the current instance via the newer instance.
	/// </summary>
	/// <param name="newPreferences">The newer instance to copy.</param>
	void CoverBy(IPreference newPreferences);

	/// <summary>
	/// Try to get the result color value.
	/// </summary>
	/// <param name="colorIdentifier">The color identifier.</param>
	/// <param name="result">The result color got.</param>
	/// <returns>The <see cref="bool"/> result.</returns>
	/// <exception cref="InvalidOperationException">Throws when the ID is invalid.</exception>
	protected internal sealed bool TryGetColor(Identifier colorIdentifier, out Color result)
	{
		if (colorIdentifier is { Mode: IdentifierColorMode.Id, Id: var id })
		{
			return (result = (Color?)typeof(IPreference).GetProperty($"Color{id}")?.GetValue(this) ?? Color.Transparent) != Color.Transparent;
		}
		else
		{
			result = Color.Transparent;
			return false;
		}
	}
}

/// <summary>
/// Indicates a preference instance.
/// </summary>
file sealed class Preference : IPreference
{
	/// <summary>
	/// Initializes a <see cref="Preference"/> instance.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal Preference()
	{
	}


	/// <inheritdoc/>
	public bool ShowCandidates { get; set; } = true;

	/// <inheritdoc/>
	public bool ShowLightHouse { get; set; } = true;

	/// <inheritdoc/>
	public decimal ValueScale { get; set; } = .9M;

	/// <inheritdoc/>
	public decimal CandidateScale { get; set; } = .3M;

	/// <inheritdoc/>
	public float GridLineWidth { get; set; } = 1.5F;

	/// <inheritdoc/>
	public float BlockLineWidth { get; set; } = 3F;

	/// <inheritdoc/>
	public float FooterTextFontSize { get; set; } = 24;

	/// <inheritdoc/>
	public string? GivenFontName { get; set; } = "MiSans";

	/// <inheritdoc/>
	public string? ModifiableFontName { get; set; } = "MiSans";

	/// <inheritdoc/>
	public string? CandidateFontName { get; set; } = "MiSans";

	/// <inheritdoc/>
	public string? UnknownFontName { get; set; } = "Times New Roman";

	/// <inheritdoc/>
	public string FooterTextFontName { get; set; } = "MiSans";

	/// <inheritdoc/>
	public FontStyle GivenFontStyle { get; set; } = FontStyle.Regular;

	/// <inheritdoc/>
	public FontStyle ModifiableFontStyle { get; set; } = FontStyle.Regular;

	/// <inheritdoc/>
	public FontStyle CandidateFontStyle { get; set; } = FontStyle.Regular;

	/// <inheritdoc/>
	public FontStyle UnknownFontStyle { get; set; } = FontStyle.Italic | FontStyle.Bold;

	/// <inheritdoc/>
	public FontStyle FooterTextFontStyle { get; set; } = FontStyle.Bold;

	/// <inheritdoc/>
	public Color GivenColor { get; set; } = Color.Black;

	/// <inheritdoc/>
	public Color ModifiableColor { get; set; } = Color.Blue;

	/// <inheritdoc/>
	public Color CandidateColor { get; set; } = Color.DimGray;

	/// <inheritdoc/>
	public Color FocusedCellColor { get; set; } = Color.FromArgb(32, Color.Yellow);

	/// <inheritdoc/>
	public Color EliminationColor { get; set; } = Color.FromArgb(255, 118, 132);

	/// <inheritdoc/>
	public Color CannibalismColor { get; set; } = Color.FromArgb(235, 0, 0);

	/// <inheritdoc/>
	public Color ChainColor { get; set; } = Color.Red;

	/// <inheritdoc/>
	public Color BackgroundColor { get; set; } = Color.White;

	/// <inheritdoc/>
	public Color GridLineColor { get; set; } = Color.Black;

	/// <inheritdoc/>
	public Color BlockLineColor { get; set; } = Color.Black;

	/// <inheritdoc/>
	public Color CrosshatchingOutlineColor { get; set; } = Color.FromArgb(192, Color.Black);

	/// <inheritdoc/>
	public Color CrosshatchingInnerColor { get; set; } = Color.Transparent;

	/// <inheritdoc/>
	public Color UnknownIdentifierColor { get; set; } = Color.FromArgb(192, Color.Red);

	/// <inheritdoc/>
	public Color CrossSignColor { get; set; } = Color.FromArgb(192, Color.Black);

	/// <inheritdoc/>
	public Color FooterTextColor { get; set; } = Color.Black;

	/// <inheritdoc/>
	public Color Color1 { get; set; } = Color.FromArgb(63, 218, 101);

	/// <inheritdoc/>
	public Color Color2 { get; set; } = Color.FromArgb(255, 192, 89);

	/// <inheritdoc/>
	public Color Color3 { get; set; } = Color.FromArgb(127, 187, 255);

	/// <inheritdoc/>
	public Color Color4 { get; set; } = Color.FromArgb(216, 178, 255);

	/// <inheritdoc/>
	public Color Color5 { get; set; } = Color.FromArgb(197, 232, 140);

	/// <inheritdoc/>
	public Color Color6 { get; set; } = Color.FromArgb(255, 203, 203);

	/// <inheritdoc/>
	public Color Color7 { get; set; } = Color.FromArgb(178, 223, 223);

	/// <inheritdoc/>
	public Color Color8 { get; set; } = Color.FromArgb(252, 220, 165);

	/// <inheritdoc/>
	public Color Color9 { get; set; } = Color.FromArgb(255, 255, 150);

	/// <inheritdoc/>
	public Color Color10 { get; set; } = Color.FromArgb(247, 222, 143);

	/// <inheritdoc/>
	public Color Color11 { get; set; } = Color.FromArgb(220, 212, 252);

	/// <inheritdoc/>
	public Color Color12 { get; set; } = Color.FromArgb(255, 118, 132);

	/// <inheritdoc/>
	public Color Color13 { get; set; } = Color.FromArgb(206, 251, 237);

	/// <inheritdoc/>
	public Color Color14 { get; set; } = Color.FromArgb(215, 255, 215);

	/// <inheritdoc/>
	public Color Color15 { get; set; } = Color.FromArgb(192, 192, 192);


	/// <inheritdoc/>
	public IPreference Clone()
	{
		var instance = IPreference.Default;
		foreach (var propertyInfo in typeof(Preference).GetProperties(BindingFlags.Instance | BindingFlags.Public))
		{
			if (propertyInfo is { CanRead: true, CanWrite: true })
			{
				var originalValue = propertyInfo.GetValue(this);
				propertyInfo.SetValue(instance, originalValue);
			}
		}

		return instance;
	}

	/// <inheritdoc/>
	public void CoverBy(IPreference newPreferences)
	{
		foreach (var propertyInfo in typeof(Preference).GetProperties(BindingFlags.Instance | BindingFlags.Public))
		{
			if (propertyInfo is { CanRead: true, CanWrite: true })
			{
				var originalValue = propertyInfo.GetValue(newPreferences);
				propertyInfo.SetValue(this, originalValue);
			}
		}
	}
}
