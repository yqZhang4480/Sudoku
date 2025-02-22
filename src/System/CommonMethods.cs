namespace System;

/// <summary>
/// Represents a set of methods to be used as method groups.
/// </summary>
public static class CommonMethods
{
	/// <summary>
	/// Merges two integers by bits. This method will be used by LINQ method
	/// <see cref="Enumerable.Aggregate{TSource}(IEnumerable{TSource}, Func{TSource, TSource, TSource})"/>.
	/// </summary>
	/// <param name="interim">The interim value.</param>
	/// <param name="next">The next value to be merged by its bits.</param>
	/// <returns>The final value merged.</returns>
	/// <seealso cref="Enumerable.Aggregate{TSource}(IEnumerable{TSource}, Func{TSource, TSource, TSource})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int BitMerger(int interim, int next) => interim | 1 << next;

	/// <inheritdoc cref="BitMerger(int, int)"/>
	/// <typeparam name="T">The type of the value. The value must be an integer type, and supports for shifting operators.</typeparam>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T BitMerger<T>(T interim, T next) where T : IBinaryInteger<T>, IShiftOperators<T, T, T>
		=> interim | T.MultiplicativeIdentity << next;

	/// <summary>
	/// Merges two flags of type <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The type of the enumeration.</typeparam>
	/// <param name="left">The first instance to be merged.</param>
	/// <param name="right">The second instance to be merged.</param>
	/// <returns>A merged result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe T EnumFlagMerger<T>(T left, T right) where T : unmanaged, Enum
		=> sizeof(T) switch
		{
			1 or 2 or 4 when (As<T, int>(ref left) | As<T, int>(ref right)) is var f => As<int, T>(ref f),
			8 when (As<T, long>(ref left) | As<T, long>(ref right)) is var f => As<long, T>(ref f),
			_ => throw new NotSupportedException(ErrorInfo_UnderlyingTypeNotSupported<T>())
		};

	/// <summary>
	/// Returns the argument <paramref name="value"/>.
	/// </summary>
	/// <typeparam name="T">The type of the argument.</typeparam>
	/// <param name="value">The value.</param>
	/// <returns>The value itself.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T ReturnSelf<T>(T value) => value;


	private static string ErrorInfo_UnderlyingTypeNotSupported<T>()
		=> $"""
		The target enumeration type '{typeof(T).Name}' has a wrong underlying numeric type. 
		All possible underlying types for an enumeration type must be of size 1, 2, 4 or 8 bits.
		""".RemoveLineEndings();
}
