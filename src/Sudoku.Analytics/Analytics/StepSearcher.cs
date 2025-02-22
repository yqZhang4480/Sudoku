namespace Sudoku.Analytics;

/// <summary>
/// Represents a searcher that can creates <see cref="Step"/> instances for the specified technique.
/// </summary>
/// <param name="priority">
/// <para>
/// Indicates the priority value of the current step searcher.
/// This property is used for sorting multiple <see cref="StepSearcher"/> instances.
/// </para>
/// <para>
/// Please note that the set value cannot be same for different <see cref="StepSearcher"/> types;
/// otherwise, <see cref="InvalidOperationException"/> will be thrown while comparing with two <see cref="StepSearcher"/>s.
/// </para>
/// <para>
/// This property may be automatically generated by source generator. Therefore, you may not care about implementation of this property.
/// </para>
/// </param>
/// <param name="level">
/// <para>Indicates the level that the current step searcher belongs to.</para>
/// <para>
/// This property indicates how difficult the step searcher can be enabled.
/// </para>
/// </param>
/// <param name="runningArea">
/// <para>Indicates the running area which describes a function where the current step searcher can be invoked.</para>
/// <para>
/// By default, the step searcher will support
/// both <see cref="StepSearcherRunningArea.Searching"/> and <see cref="StepSearcherRunningArea.Gathering"/>.
/// </para>
/// </param>
/// <seealso cref="Step"/>
[Equals]
[GetHashCode]
[ToString(OtherModifiers = "sealed")]
public abstract partial class StepSearcher(
	[PrimaryConstructorParameter] int priority,
	[PrimaryConstructorParameter] int level,
	[PrimaryConstructorParameter] StepSearcherRunningArea runningArea = StepSearcherRunningArea.Searching | StepSearcherRunningArea.Gathering
) : IEquatable<StepSearcher>
{
	/// <summary>
	/// Determines whether the current step searcher contains split configuration,
	/// meaning it can be created as multiple instances in a same step searchers collection.
	/// </summary>
	public bool IsSplit => GetType().GetCustomAttribute<SplitStepSearcherAttribute>() is not null;

	/// <summary>
	/// Determines whether the current step searcher is a pure one, which means it doesn't use cached fields.
	/// </summary>
	public bool IsPure => StepSearcherMetadataInfo.IsPure;

	/// <summary>
	/// Determines whether we can adjust the ordering of the current step searcher as a customized configuration option before solving a puzzle.
	/// </summary>
	public bool IsFixed => StepSearcherMetadataInfo.IsFixed;

	/// <summary>
	/// Determines whether the current step searcher is not supported for sukaku solving mode.
	/// </summary>
	public bool IsNotSupportedForSukaku => StepSearcherMetadataInfo.ConditionalCases is var cases && cases.Flags(ConditionalCase.Standard);

	/// <summary>
	/// Determines whether the current step searcher is disabled
	/// by option <see cref="ConditionalCase.TimeComplexity"/> being configured.
	/// </summary>
	/// <seealso cref="ConditionalCase.TimeComplexity"/>
	public bool IsConfiguredSlow => StepSearcherMetadataInfo.ConditionalCases is var cases && cases.Flags(ConditionalCase.TimeComplexity);

	/// <summary>
	/// Determines whether the current step searcher is disabled
	/// by option <see cref="ConditionalCase.SpaceComplexity"/> being configured.
	/// </summary>
	/// <seealso cref="ConditionalCase.SpaceComplexity"/>
	public bool IsConfiguredHighAllocation => StepSearcherMetadataInfo.ConditionalCases is var cases && cases.Flags(ConditionalCase.SpaceComplexity);

	/// <summary>
	/// Indicates the split priority. This value cannot be greater than 16 due to design of <see cref="SplitStepSearcherAttribute"/>.
	/// </summary>
	/// <value>The value to be set. The value must be between 0 and 16 (i.e. <![CDATA[>= 0 and < 16]]>).</value>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <see langword="value"/> is below 0, greater than 16 or equal to 16.
	/// </exception>
	/// <seealso cref="SplitStepSearcherAttribute"/>
	[ImplicitField(RequiredReadOnlyModifier = false)]
	public int SplitPriority
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => _splitPriority;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[RequiresUnreferencedCode("This setter can only be invoked by reflection.")]
		internal init => _splitPriority = value is >= 0 and < 16 ? value : throw new ArgumentOutOfRangeException(nameof(value));
	}

	/// <summary>
	/// Returns the real name of this instance.
	/// </summary>
	[StringMember]
	public string Name => GetType().Name switch { var rawTypeName => GetString($"StepSearcherName_{rawTypeName}") ?? rawTypeName };

	/// <inheritdoc cref="StepSearcherAttribute.SupportedTechniques"/>
	public Technique[] SupportedTechniques => StepSearcherMetadataInfo.SupportedTechniques;

	/// <summary>
	/// Indicates the <see cref="DifficultyLevel"/>s whose corresponding step can be produced by the current step searcher instance.
	/// </summary>
	public DifficultyLevel[] DifficultyLevelRange => StepSearcherMetadataInfo.DifficultyLevels.GetAllFlags();

	/// <summary>
	/// Indicates the final priority value ID of the step searcher. This property is used as comparison.
	/// </summary>
	[HashCodeMember]
	private int PriorityId => Priority << 4 | SplitPriority;

	/// <summary>
	/// The internal information for the current step searcher instance.
	/// </summary>
	private StepSearcherAttribute StepSearcherMetadataInfo => GetType().GetCustomAttribute<StepSearcherAttribute>()!;


	/// <inheritdoc/>
	public bool Equals([NotNullWhen(true)] StepSearcher? other) => other is not null && PriorityId == other.PriorityId;

	/// <summary>
	/// Try to collect all available <see cref="Step"/>s using the current technique rule.
	/// </summary>
	/// <param name="context">
	/// <para>
	/// The analysis context. This argument offers you some elementary data configured or assigned, for the current loop of step searching.
	/// </para>
	/// <para>
	/// All available <see cref="Step"/> results will be stored in property <see cref="AnalysisContext.Accumulator"/>
	/// of this argument, if property <see cref="AnalysisContext.OnlyFindOne"/> returns <see langword="false"/>;
	/// otherwise, the property won't be used, and this method will return the first found step.
	/// </para>
	/// </param>
	/// <returns>
	/// Returns the first found step. The nullability of the return value is described as follow:
	/// <list type="bullet">
	/// <item>
	/// <see langword="null"/>:
	/// <list type="bullet">
	/// <item><c><paramref name="context"/>.OnlyFindOne == <see langword="false"/></c>.</item>
	/// <item><c><paramref name="context"/>.OnlyFindOne == <see langword="true"/></c>, but nothing found.</item>
	/// </list>
	/// </item>
	/// <item>
	/// Not <see langword="null"/>:
	/// <list type="bullet">
	/// <item>
	/// <c><paramref name="context"/>.OnlyFindOne == <see langword="true"/></c>,
	/// and found <b>at least one step</b>. In this case the return value is the first found step.
	/// </item>
	/// </list>
	/// </item>
	/// </list>
	/// </returns>
	/// <seealso cref="Step"/>
	/// <seealso cref="AnalysisContext"/>
	protected internal abstract Step? Collect(scoped ref AnalysisContext context);
}
