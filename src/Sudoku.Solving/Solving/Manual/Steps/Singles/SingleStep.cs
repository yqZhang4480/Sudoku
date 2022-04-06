﻿namespace Sudoku.Solving.Manual.Steps;

/// <summary>
/// Provides with a step that is a <b>Single</b> technique.
/// </summary>
/// <param name="Conclusions"><inheritdoc/></param>
/// <param name="Views"><inheritdoc/></param>
/// <param name="Cell">Indicates the cell used.</param>
/// <param name="Digit">Indicates the digit used.</param>
public abstract record class SingleStep(
	ImmutableArray<Conclusion> Conclusions, ImmutableArray<View> Views, int Cell, int Digit) :
	Step(Conclusions, Views),
	IElementaryStep
{
	/// <inheritdoc/>
	public sealed override bool ShowDifficulty => base.ShowDifficulty;

	/// <inheritdoc/>
	public sealed override string Name => base.Name;

	/// <inheritdoc/>
	public sealed override Stableness Stableness => base.Stableness;

	/// <inheritdoc/>
	public sealed override DifficultyLevel DifficultyLevel => DifficultyLevel.Easy;

	/// <inheritdoc/>
	public sealed override TechniqueTags TechniqueTags => TechniqueTags.Singles;

	/// <inheritdoc/>
	public sealed override TechniqueGroup TechniqueGroup => TechniqueGroup.Single;
}
