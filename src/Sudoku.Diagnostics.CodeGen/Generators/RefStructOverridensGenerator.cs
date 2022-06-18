﻿namespace Sudoku.Diagnostics.CodeGen.Generators;

/// <summary>
/// Indicates the generator that generates the default overridden methods in a <see langword="ref struct"/>.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class RefStructOverridensGenerator : ISourceGenerator
{
	/// <inheritdoc/>
	public void Execute(GeneratorExecutionContext context)
	{
		if (
			context is not
			{
				SyntaxContextReceiver: Receiver { Collection: var symbolsFound },
				Compilation: var compilation
			}
		)
		{
			return;
		}

		var onTopLevel = OnTopLevel;
		var onNestedLevel = OnNested;
		(
			from symbol in symbolsFound
			group symbol by symbol.ContainingType is null into tupleGroupedByIsNested
			from typeTuple in tupleGroupedByIsNested
			select (Action: tupleGroupedByIsNested.Key ? onTopLevel : onNestedLevel, Type: typeTuple)
		).ForEach(e => e.Action(context, e.Type, compilation));
	}

	/// <inheritdoc/>
	public void Initialize(GeneratorInitializationContext context)
		=> context.RegisterForSyntaxNotifications(() => new Receiver(context.CancellationToken));

	/// <summary>
	/// Generates for top-leveled <see langword="ref struct"/> types.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="type">The type.</param>
	/// <param name="compilation">The compilation instance.</param>
	private void OnTopLevel(GeneratorExecutionContext context, INamedTypeSymbol type, Compilation compilation)
	{
		var (_, _, namespaceName, genericParameterList, _, _, readOnlyKeyword, _, _, _) =
			SymbolOutputInfo.FromSymbol(type);

		var methods = type.GetMembers().OfType<IMethodSymbol>().ToArray();
		string equalsMethod = Array.Exists(methods, ExistsEquals)
			? $"""// Can't generate '{nameof(Equals)}' because the method is impl'ed by user."""
			: $"""
			/// <inheritdoc cref="object.Equals(object?)"/>
				/// <exception cref="global::System.NotSupportedException">Always throws.</exception>
				[global::System.CodeDom.Compiler.GeneratedCode("{GetType().FullName}", "{VersionValue}")]
				[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
			#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER || (NETCOREAPP3_0 || NETCOREAPP3_1)
				[global::System.Diagnostics.CodeAnalysis.DoesNotReturn]
			#endif
				[global::System.Obsolete("You can't use or call this method.", true)]
				[global::System.Runtime.CompilerServices.CompilerGenerated]
				public override {readOnlyKeyword}bool Equals(object? obj) => throw new global::System.NotSupportedException();
			""";

		string getHashCodeMethod = Array.Exists(methods, ExistsGetHashCode)
			? $"""// Can't generate '{nameof(GetHashCode)}' because the method is impl'ed by user."""
			: $"""
			/// <inheritdoc cref="object.GetHashCode"/>
				/// <exception cref="global::System.NotSupportedException">Always throws.</exception>
				[global::System.CodeDom.Compiler.GeneratedCode("{GetType().FullName}", "{VersionValue}")]
				[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
			#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER || (NETCOREAPP3_0 || NETCOREAPP3_1)
				[global::System.Diagnostics.CodeAnalysis.DoesNotReturn]
			#endif
				[global::System.Obsolete("You can't use or call this method.", true)]
				[global::System.Runtime.CompilerServices.CompilerGenerated]
				public override {readOnlyKeyword}int GetHashCode() => throw new global::System.NotSupportedException();
			""";

		string toStringMethod = Array.Exists(methods, ExistsToString)
			? $"""// Can't generate '{nameof(ToString)}' because the method is impl'ed by user."""
			: $"""
			/// <inheritdoc cref="object.ToString"/>
				/// <exception cref="global::System.NotSupportedException">Always throws.</exception>
				[global::System.CodeDom.Compiler.GeneratedCode("{GetType().FullName}", "{VersionValue}")]
				[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
			#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER || (NETCOREAPP3_0 || NETCOREAPP3_1)
				[global::System.Diagnostics.CodeAnalysis.DoesNotReturn]
			#endif
				[global::System.Obsolete("You can't use or call this method.", true, DiagnosticId = "BAN")]
				[global::System.Runtime.CompilerServices.CompilerGenerated]
				public override {readOnlyKeyword}string? ToString() => throw new global::System.NotSupportedException();
			""";

		context.AddSource(
			type.ToFileName(),
			Shortcuts.RefStructDefaultOverrides,
			$$"""
			#pragma warning disable CS0809
			#nullable enable
			
			namespace {{namespaceName}};
			
			partial struct {{type.Name}}{{genericParameterList}}
			{
				{{equalsMethod}}
			
				{{getHashCodeMethod}}
			
				{{toStringMethod}}
			}
			"""
		);
	}

	/// <summary>
	/// Generates for nested-leveled <see langword="ref struct"/> types.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="type">The type.</param>
	/// <param name="compilation">The compilation instance.</param>
	private void OnNested(GeneratorExecutionContext context, INamedTypeSymbol type, Compilation compilation)
	{
		var (_, _, namespaceName, genericParameterList, _, _, readOnlyKeyword, _, _, _) =
			SymbolOutputInfo.FromSymbol(type);

		// If nested type, the 'genericParametersList' may contain the dot '.' such as
		//
		//     <TKey, TValue>.KeyCollection
		//
		// We should remove the characters before the dot.
		if (!string.IsNullOrEmpty(genericParameterList)
			&& (genericParameterList.IndexOf("where") is var tempIndex and not -1 ? ..tempIndex : ..) is var range
			&& genericParameterList[range].LastIndexOf('.') is var dot and not -1)
		{
			if (dot + 1 >= genericParameterList.Length)
			{
				return;
			}

			genericParameterList = genericParameterList[(dot + 1)..];
			if (genericParameterList.IndexOf('<') == -1)
			{
				genericParameterList = string.Empty;
			}
		}

		// Get outer types.
		var outerTypes = new Stack<(INamedTypeSymbol Type, int Indenting)>();
		int outerTypesCount = 0;
		for (var o = type.ContainingType; o is not null; o = o.ContainingType)
		{
			outerTypesCount++;
		}

		string mi = new('\t', outerTypesCount + 1);
		string ti = new('\t', outerTypesCount);
		for (var outer = type.ContainingType; outer is not null; outer = outer.ContainingType)
		{
			outerTypes.Push((outer, outerTypesCount--));
		}

		StringBuilder outerTypeDeclarationsStart = new(), outerTypeDeclarationsEnd = new();
		var indentingStack = new Stack<string>();
		foreach (var (outerType, currentIndenting) in outerTypes)
		{
			var (_, outerFullTypeName, _, _, _, outerTypeKind, _, _, _, _) = SymbolOutputInfo.FromSymbol(outerType);

			string outerGenericParametersList;
			int lastDot = outerFullTypeName.LastIndexOf('.');
			if (lastDot == -1)
			{
				int lt = outerFullTypeName.IndexOf('<'), gt = outerFullTypeName.IndexOf('>');
				if (lt == -1)
				{
					outerGenericParametersList = string.Empty;
				}
				else if (gt < lt)
				{
					continue;
				}
				else
				{
					outerGenericParametersList = outerFullTypeName[lt..gt];
				}
			}
			else
			{
				int start = lastDot + 1;
				if (start >= outerFullTypeName.Length)
				{
					continue;
				}

				string temp = outerFullTypeName[start..];
				int lt = temp.IndexOf('<'), gt = temp.IndexOf('>');
				if (lt == -1)
				{
					outerGenericParametersList = string.Empty;
				}
				else if (gt < lt)
				{
					continue;
				}
				else
				{
					outerGenericParametersList = temp[lt..(gt + 1)];
				}
			}

			string indenting = new('\t', currentIndenting - 1);

			outerTypeDeclarationsStart
				.AppendLine($"{indenting}partial {outerTypeKind}{outerType.Name}{outerGenericParametersList}")
				.AppendLine($"{indenting}{{");

			indentingStack.Push(indenting);
		}

		foreach (string indenting in indentingStack)
		{
			outerTypeDeclarationsEnd.AppendLine($"{indenting}}}");
		}

		// Remove the last new line.
		outerTypeDeclarationsStart.Remove(outerTypeDeclarationsStart.Length - 2, 2);
		outerTypeDeclarationsEnd.Remove(outerTypeDeclarationsEnd.Length - 2, 2);

		var methods = type.GetMembers().OfType<IMethodSymbol>().ToArray();
		string equalsMethod = Array.Exists(methods, ExistsEquals)
			? $"""{mi}// Can't generate '{nameof(Equals)}' because the method is impl'ed by user."""
			: $"""
			{mi}/// <inheritdoc cref="object.Equals(object?)"/>
			{mi}/// <exception cref="global::System.NotSupportedException">Always throws.</exception>
			{mi}[global::System.CodeDom.Compiler.GeneratedCode("{GetType().FullName}", "{VersionValue}")]
			{mi}[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
			{mi}[global::System.Diagnostics.CodeAnalysis.DoesNotReturn]
			{mi}[global::System.Obsolete("You can't use or call this method.", true)]
			{mi}[global::System.Runtime.CompilerServices.CompilerGenerated]
			{mi}public override {readOnlyKeyword}bool Equals(object? obj) => throw new global::System.NotSupportedException();
			""";

		string getHashCodeMethod = Array.Exists(methods, ExistsGetHashCode)
			? $"""{mi}// Can't generate '{nameof(GetHashCode)}' because the method is impl'ed by user."""
			: $"""
			{mi}/// <inheritdoc cref="object.GetHashCode"/>
			{mi}/// <exception cref="global::System.NotSupportedException">Always throws.</exception>
			{mi}[global::System.CodeDom.Compiler.GeneratedCode("{GetType().FullName}", "{VersionValue}")]
			{mi}[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
			{mi}[global::System.Diagnostics.CodeAnalysis.DoesNotReturn]
			{mi}[global::System.Obsolete("You can't use or call this method.", true)]
			{mi}[global::System.Runtime.CompilerServices.CompilerGenerated]
			{mi}public override {readOnlyKeyword}int GetHashCode() => throw new global::System.NotSupportedException();
			""";

		string toStringMethod = Array.Exists(methods, ExistsToString)
			? $"""{mi}// Can't generate '{nameof(ToString)}' because the method is impl'ed by user."""
			: $"""
			{mi}/// <inheritdoc cref="object.ToString"/>
			{mi}/// <exception cref="global::System.NotSupportedException">Always throws.</exception>
			{mi}[global::System.CodeDom.Compiler.GeneratedCode("{GetType().FullName}", "{VersionValue}")]
			{mi}[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
			{mi}[global::System.Diagnostics.CodeAnalysis.DoesNotReturn]
			{mi}[global::System.Obsolete("You can't use or call this method.", true)]
			{mi}[global::System.Runtime.CompilerServices.CompilerGenerated]
			{mi}public override {readOnlyKeyword}string? ToString() => throw new global::System.NotSupportedException();
			""";

		context.AddSource(
			type.ToFileName(),
			Shortcuts.RefStructDefaultOverrides,
			$$"""
			#pragma warning disable CS0809
			#nullable enable
			
			namespace {{namespaceName}};
			
			{{outerTypeDeclarationsStart}}
			{{ti}}partial struct {{type.Name}}{{genericParameterList}}
			{{ti}}{
			{{equalsMethod}}
			
			{{getHashCodeMethod}}
			
			{{toStringMethod}}
			{{ti}}}
			{{outerTypeDeclarationsEnd}}
			"""
		);
	}


	/// <summary>
	/// Indicates whether the specified method symbol overrides <see cref="object.Equals(object)"/>.
	/// </summary>
	/// <param name="symbol">The symbol to be determined.</param>
	/// <returns>A <see cref="bool"/> result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool ExistsEquals(IMethodSymbol symbol)
		=> symbol is
		{
			IsStatic: false,
			Name: nameof(Equals),
			Parameters: [{ Type.SpecialType: SpecialType.System_Object }],
			ReturnType.SpecialType: SpecialType.System_Boolean
		};

	/// <summary>
	/// Indicates whether the specified method symbol overrides <see cref="object.GetHashCode"/>.
	/// </summary>
	/// <param name="symbol">The symbol to be determined.</param>
	/// <returns>A <see cref="bool"/> result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool ExistsGetHashCode(IMethodSymbol symbol)
		=> symbol is
		{
			IsStatic: false,
			Name: nameof(GetHashCode),
			Parameters: [],
			ReturnType.SpecialType: SpecialType.System_Int32
		};

	/// <summary>
	/// Indicates whether the specified method symbol overrides <see cref="object.ToString"/>.
	/// </summary>
	/// <param name="symbol">The symbol to be determined.</param>
	/// <returns>A <see cref="bool"/> result.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool ExistsToString(IMethodSymbol symbol)
		=> symbol is
		{
			IsStatic: false,
			Name: nameof(ToString),
			Parameters: [],
			ReturnType.SpecialType: SpecialType.System_String
		};
}
