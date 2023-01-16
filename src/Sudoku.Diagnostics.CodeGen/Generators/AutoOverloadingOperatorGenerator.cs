﻿namespace Sudoku.Diagnostics.CodeGen.Generators;

/// <summary>
/// Defines a source generator that generates source code as operator overloading statements.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class AutoOverloadingOperatorGenerator : IIncrementalGenerator
{
	/// <inheritdoc/>
	public void Initialize(IncrementalGeneratorInitializationContext context)
		=> context.RegisterSourceOutput(
			context.SyntaxProvider
				.ForAttributeWithMetadataName(
					"System.Diagnostics.CodeGen.GeneratedOverloadingOperatorAttribute",
					static (node, _)
						=> node is TypeDeclarationSyntax { Modifiers: var m and not [] } and (StructDeclarationSyntax or ClassDeclarationSyntax)
						&& m.Any(SyntaxKind.PartialKeyword),
					static (gasc, ct) =>
					{
						if (gasc is not
							{
								Attributes: [{ ConstructorArguments: [{ Value: int rawValue and not 0 }] }],
								TargetSymbol: INamedTypeSymbol typeSymbol,
								SemanticModel.Compilation: var compilation
							})
						{
							return (Data?)null;
						}

						return new((Operator)rawValue, typeSymbol, compilation);
					}
				)
				.Where(static data => data is not null)
				.Collect(),
			(spc, data) =>
			{
				var codeSnippet = new List<string>();
				foreach (var (operatorKinds, typeSymbol, compilation) in data.CastToNotNull())
				{
					var largeStructAttribute = compilation.GetTypeByMetadataName("System.Diagnostics.CodeAnalysis.IsLargeStructAttribute")!;
					var @namespace = typeSymbol.ContainingNamespace.ToDisplayString(ExtendedSymbolDisplayFormat.FullyQualifiedFormatWithConstraints);
					var typeName = typeSymbol.Name;
					var typeKind = typeSymbol.GetTypeKindModifier();

					var operatorsCodeSnippet = new List<string>();
					foreach (var element in operatorKinds)
					{
						switch (element)
						{
							case Operator.Equality: operatorsCodeSnippet.Add(op_Equality(typeSymbol)); break;
							case Operator.Inequality: operatorsCodeSnippet.Add(op_Inequality(typeSymbol)); break;
						}
					}

					codeSnippet.Add(
						$$"""
						namespace {{@namespace["global::".Length..]}}
						{
							partial {{typeKind}} {{typeName}}
							{
							{{string.Join("\r\n\r\n\t", operatorsCodeSnippet)}}
							}
						}
						"""
					);


					string op_Equality(INamedTypeSymbol typeSymbol)
					{
						var isLargeStruct = (
							from attributeData in typeSymbol.GetAttributes()
							where SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass, largeStructAttribute)
							select attributeData
						).Any();
						var fullName = isLargeStruct
							? $"scoped in {f(typeSymbol)}"
							: typeSymbol.IsRefLikeType
								? $"scoped {f(typeSymbol)}"
								: typeSymbol.TypeKind == TypeKind.Class ? $"{f(typeSymbol)}?" : f(typeSymbol);
						var crefText = isLargeStruct
							? $@" cref=""IEqualityOperators{{TSelf, TOther, TResult}}.{nameof(op_Equality)}(TSelf, TOther)"""
							: string.Empty;
						var executingCode = typeSymbol.TypeKind == TypeKind.Class
							? "(left, right) switch { (null, null) => true, (not null, not null) => left.Equals(right), _ => false }"
							: "left.Equals(right)";

						return
							$"""
								/// <inheritdoc{crefText}/>
									[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
									[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{GetType().FullName}", "{VersionValue}")]
									[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
									public static bool operator ==({fullName} left, {fullName} right)
										=> {executingCode};
							""";
					}

					string op_Inequality(INamedTypeSymbol typeSymbol)
					{
						var isLargeStruct = (
							from attributeData in typeSymbol.GetAttributes()
							where SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass, largeStructAttribute)
							select attributeData
						).Any();
						var fullName = isLargeStruct
							? $"scoped in {f(typeSymbol)}"
							: typeSymbol.IsRefLikeType
								? $"scoped {f(typeSymbol)}"
								: typeSymbol.TypeKind == TypeKind.Class ? $"{f(typeSymbol)}?" : f(typeSymbol);
						var crefText = isLargeStruct || typeSymbol.IsRefLikeType
							? $@" cref=""IEqualityOperators{{TSelf, TOther, TResult}}.{nameof(op_Inequality)}(TSelf, TOther)"""
							: string.Empty;

						return
							$"""
								/// <inheritdoc{crefText}/>
									[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
									[global::System.CodeDom.Compiler.GeneratedCodeAttribute("{GetType().FullName}", "{VersionValue}")]
									[global::System.Runtime.CompilerServices.MethodImplAttribute(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
									public static bool operator !=({fullName} left, {fullName} right)
										=> !(left == right);
							""";
					}
				}

				spc.AddSource(
					$"OperatorsOverloading.g.{Shortcuts.GeneratedOverloadingOperator}.cs",
					$$"""
					// <auto-generated />

					#nullable enable

					{{string.Join("\r\n\r\n", codeSnippet)}}
					"""
				);


				static string f(INamedTypeSymbol typeSymbol)
					=> typeSymbol.ToDisplayString(ExtendedSymbolDisplayFormat.FullyQualifiedFormatWithConstraints);
			}
		);
}

/// <summary>
/// Defines a kind of an operator.
/// </summary>
[Flags]
file enum Operator
{
	/// <summary>
	/// Indicates <c><see langword="operator"/> ==</c>.
	/// </summary>
	Equality = 1,

	/// <summary>
	/// Indicates <c><see langword="operator"/> !=</c>.
	/// </summary>
	Inequality = 2
}

/// <summary>
/// Defines a gathered data tuple.
/// </summary>
/// <param name="OperatorKinds">The operator kinds.</param>
/// <param name="TypeSymbol">The type symbol.</param>
/// <param name="Compilation">Indicates the compilation.</param>
file readonly record struct Data(Operator OperatorKinds, INamedTypeSymbol TypeSymbol, Compilation Compilation);

/// <include file='../../global-doc-comments.xml' path='g/csharp11/feature[@name="file-local"]/target[@name="class" and @when="extension"]'/>
file static class Extensions
{
	/// <summary>
	/// Get an enumerator instance that is used by <see langword="foreach"/> loop.
	/// </summary>
	/// <typeparam name="T">The type of an enumeration.</typeparam>
	/// <param name="this">The current field to be iterated.</param>
	/// <returns>An <see cref="IEnumerator{T}"/> instance.</returns>
	public static IEnumerator<T> GetEnumerator<T>(this T @this) where T : unmanaged, Enum
	{
		foreach (var field in Enum.GetValues(typeof(T)).Cast<T>())
		{
			if (@this.HasFlag(field))
			{
				yield return field;
			}
		}
	}
}
