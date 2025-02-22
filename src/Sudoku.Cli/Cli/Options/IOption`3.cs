namespace Sudoku.Cli.Options;

/// <summary>
/// <inheritdoc cref="IOption{TSelf, T}" path="/summary"/>
/// </summary>
/// <typeparam name="TSelf">
/// <inheritdoc cref="IOption{TSelf, T}" path="/typeparam[@name='TSelf']"/>
/// </typeparam>
/// <typeparam name="T">
/// <inheritdoc cref="IOption{TSelf, T}" path="/typeparam[@name='T']"/>
/// </typeparam>
/// <typeparam name="TConverter">The type of the converter.</typeparam>
public interface IOption<TSelf, out T, TConverter> : IOption<TSelf, T>
	where TSelf : class, IOption<TSelf, T, TConverter>, new()
	where TConverter : class, IArgumentConverter<TConverter, T>
{
	/// <summary>
	/// <inheritdoc cref="Option{T}.Option(string, ParseArgument{T}, bool, string?)" path="/param[@name='isDefault']"/>
	/// </summary>
	/// <remarks>This property is <see langword="false"/> by default.</remarks>
	static virtual bool IsDefault { get; } = false;


	/// <inheritdoc cref="IOption{TSelf, T}.CreateOption"/>
	[SuppressMessage("Style", "IDE0002:Simplify Member Access", Justification = "<Pending>")]
	static new Option<T> CreateOption()
	{
		var result = new Option<T>(TSelf.Aliases, TConverter.ConvertValue, TSelf.IsDefault, TSelf.Description) { IsRequired = TSelf.IsRequired };
		result.SetDefaultValue(TSelf.DefaultValue);

		return result;
	}
}
