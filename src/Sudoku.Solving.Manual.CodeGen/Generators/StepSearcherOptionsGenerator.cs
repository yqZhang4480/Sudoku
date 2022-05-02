﻿namespace Sudoku.Diagnostics.CodeGen.Generators;

/// <summary>
/// Defines a source generator that generates the source code for the searcher options
/// on a step searcher.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class StepSearcherOptionsGenerator : ISourceGenerator
{
	/// <inheritdoc/>
	public void Execute(GeneratorExecutionContext context)
	{
		// Checks whether the reference project is valid.
		if (
			context is not
			{
				Compilation.Assembly: { Name: "Sudoku.Solving.Manual" } assemblySymbol,
				SyntaxContextReceiver: Receiver
				{
					EnabledAreasFields: var enabledAreaFields,
					DisabledReasonFields: var disabledReasonFields
				}
			}
		)
		{
			return;
		}

		// Checks whether the assembly has marked any attributes.
		if (assemblySymbol.GetAttributes() is not { IsDefaultOrEmpty: false } attributesData)
		{
			return;
		}

		// Gather the valid attributes data.
		var foundAttributesData = new List<Tuple>();
		const string comma = ", ";
		const string attributeTypeName = "Sudoku.Solving.Manual.SearcherConfigurationAttribute<>";
		foreach (var attributeData in attributesData)
		{
			// Check validity.
			if (
#pragma warning disable IDE0055
				attributeData is not
				{
					AttributeClass:
					{
						IsGenericType: true,
						TypeArguments: [
							INamedTypeSymbol
							{
								IsRecord: false,
								ContainingNamespace: var containingNamespace,
								Name: var stepSearcherName
							} stepSearcherTypeSymbol
						]
					} attributeClassSymbol,
					ConstructorArguments: [
						{ Type.SpecialType: SpecialType.System_Int32, Value: int priority },
						{ Type.TypeKind: TypeKind.Enum, Value: byte dl }
					],
					NamedArguments: var namedArguments
				}
#pragma warning restore IDE0055
			)
			{
				continue;
			}

			// Checks whether the type is valid.
			var unboundAttributeTypeSymbol = attributeClassSymbol.ConstructUnboundGenericType();
			if (unboundAttributeTypeSymbol.ToDisplayString(TypeFormats.FullName) != attributeTypeName)
			{
				continue;
			}

			// Adds the necessary info into the collection.
			foundAttributesData.Add(
				new(stepSearcherTypeSymbol, containingNamespace, priority, dl, stepSearcherName, namedArguments));
		}

		// Checks whether the collection has duplicated priority values.
		for (int i = 0; i < foundAttributesData.Count - 1; i++)
		{
			int a = foundAttributesData[i].Priority;
			for (int j = i + 1; j < foundAttributesData.Count; j++)
			{
				int b = foundAttributesData[j].Priority;
				if (a == b)
				{
					throw new InvalidOperationException(
						"Cannot operate because two found step searchers has a same priority value.");
				}
			}
		}

		// Iterate on each valid attribute data, and checks the inner value to be used
		// by the source generator to output.
		foreach (var (type, @namespace, priority, level, name, namedArguments) in foundAttributesData)
		{
			// Checks whether the attribute has configured any extra options.
			byte? enabledAreas = null;
			short? disabledReason = null;
			if (namedArguments is not [])
			{
				foreach (var kvp in namedArguments)
				{
					switch (kvp)
					{
						case ("EnabledAreas", { Value: byte ea }):
						{
							enabledAreas = ea;
							break;
						}
						case ("DisabledReason", { Value: short dr }):
						{
							disabledReason = dr;
							break;
						}
					}
				}
			}

			// Gather the extra options on step searcher.
			StringBuilder? sb = null;
			if (enabledAreas is not null || disabledReason is not null)
			{
				sb = new StringBuilder().Append(comma);
				if (enabledAreas is { } ea)
				{
					string targetStr = f(ea, "EnabledAreas");
					sb.Append($"EnabledAreas: {targetStr}{comma}");
				}
				if (disabledReason is { } dr)
				{
					string targetStr = f(dr, "DisabledReason");
					sb.Append($"DisabledReason: {targetStr}{comma}");
				}

				sb.Remove(sb.Length - comma.Length, comma.Length);
			}

			// Output the generated code.
			context.AddSource(
				type.ToFileName(),
				"sso",
				$$"""
				namespace {{@namespace.ToDisplayString(TypeFormats.FullName)}};
				
				partial class {{name}}
				{
					/// <inheritdoc/>
					[global::{{typeof(GeneratedCodeAttribute).FullName}}("{{typeof(StepSearcherOptionsGenerator).FullName}}", "{{VersionValue}}")]
					[global::{{typeof(CompilerGeneratedAttribute).FullName}}]
					public global::Sudoku.Solving.Manual.SearchingOptions Options { get; set; } =
						new({{priority}}, global::Sudoku.Solving.Manual.DisplayingLevel.{{(char)(level + 'A' - 1)}}{{sb}});
				}
				"""
			);
		}


		unsafe string f<T>(T field, string typeName) where T : unmanaged
		{
			long l = sizeof(T) switch
			{
				1 or 2 or 4 => Unsafe.As<T, int>(ref field),
				8 => Unsafe.As<T, long>(ref field),
				_ => default
			};

			// Special case: If the value is zero, just get the default field in the enumeration field
			// or just get the expression '(T)0' as the emitted code.
			if (l == 0)
			{
				return $"(global::Sudoku.Solving.Manual.{typeName})0";
			}

			var targetList = new List<string>();
			for (var (temp, i) = (l, 0); temp != 0; temp >>= 1, i++)
			{
				if ((temp & 1) == 0)
				{
					continue;
				}

				switch (typeName)
				{
					case "EnabledAreas" when enabledAreaFields[(byte)(1 << i)] is var fieldValue:
					{
						targetList.Add($"global::Sudoku.Solving.Manual.EnabledAreas.{fieldValue}");

						break;
					}
					case "DisabledReason" when disabledReasonFields[(short)(1 << i)] is var fieldValue:
					{
						targetList.Add($"global::Sudoku.Solving.Manual.DisabledReason.{fieldValue}");

						break;
					}
				}
			}

			return string.Join(" | ", targetList);
		}
	}

	/// <inheritdoc/>
	public void Initialize(GeneratorInitializationContext context)
		=> context.RegisterForSyntaxNotifications(() => new Receiver(context.CancellationToken));
}
