﻿namespace Sudoku.Runtime.Serialization.Converter;

/// <summary>
/// Defines a JSON converter that is used for the serialization and deserialization
/// on type <see cref="AlmostLockedSetNode"/>.
/// </summary>
/// <remarks>
/// JSON Pattern:
/// <code>
/// {
///   "fullCells": [
///     "r1c1",
///     "r1c2",
///     "r1c3"
///   ],
///   "cells": [
///     "r1c1",
///     "r1c2",
///     "r1c3"
///   ],
///   "digit": 2
/// }
/// </code>
/// </remarks>
/// <seealso cref="AlmostLockedSetNode"/>
public sealed class AlmostLockedSetNodeJsonConverter : JsonConverter<AlmostLockedSetNode>
{
	/// <inheritdoc/>
	public override AlmostLockedSetNode? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
		{
			throw new JsonException();
		}

		if (!reader.Read() || reader.TokenType != JsonTokenType.PropertyName
			|| reader.GetString() != nameof(AlmostLockedSetNode.FullCells))
		{
			throw new JsonException();
		}

		var fullCells = RxCyNotation.ParseCells(reader.GetString()!);
		if (!reader.Read() || reader.TokenType != JsonTokenType.String)
		{
			throw new JsonException();
		}

		if (!reader.Read() || reader.TokenType != JsonTokenType.PropertyName
			|| reader.GetString() != nameof(AlmostLockedSetNode.Cells))
		{
			throw new JsonException();
		}

		var cells = RxCyNotation.ParseCells(reader.GetString()!);
		if (!reader.Read() || reader.TokenType != JsonTokenType.Number)
		{
			throw new JsonException();
		}

		if (!reader.Read() || reader.TokenType != JsonTokenType.PropertyName
			|| reader.GetString() != nameof(AlmostLockedSetNode.Digit))
		{
			throw new JsonException();
		}

		byte digit = reader.GetByte();
		if (!reader.Read() || reader.TokenType != JsonTokenType.EndObject)
		{
			throw new JsonException();
		}

		return new((byte)(digit - 1), cells, fullCells - cells);
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, AlmostLockedSetNode value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(nameof(AlmostLockedSetNode.FullCells));
		writer.WriteNestedObject(value.FullCells, options);
		writer.WritePropertyName(nameof(AlmostLockedSetNode.Cells));
		writer.WriteNestedObject(value.Cells, options);
		writer.WriteNumber(nameof(AlmostLockedSetNode.Digit), value.Digit + 1);
		writer.WriteEndObject();
	}
}
