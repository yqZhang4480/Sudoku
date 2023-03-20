namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Firework</b> technique.
/// </summary>
public abstract class FireworkStep(Conclusion[] conclusions, View[]? views) : IntersectionStep(conclusions, views)
{
	/// <inheritdoc/>
	public override decimal BaseDifficulty => 5.9M;

	/// <inheritdoc/>
	public sealed override DifficultyLevel DifficultyLevel => DifficultyLevel.Fiendish;

	/// <inheritdoc/>
	public sealed override TechniqueGroup Group => TechniqueGroup.Firework;
}
