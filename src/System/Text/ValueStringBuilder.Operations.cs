﻿using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
#if DEBUG
using System.Diagnostics;
#endif

namespace System.Text
{
	partial struct ValueStringBuilder
	{
		/// <summary>
		/// Inserts a new character into the collection at the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The character you want to insert into the collection.</param>
		/// <param name="count">The number.</param>
		public void Insert(int index, char value, int count)
		{
			if (Length > _chars.Length - count)
			{
				Grow(count);
			}

			int remaining = Length - index;
			_chars.Slice(index, remaining).CopyTo(_chars[(index + count)..]);
			_chars.Slice(index, count).Fill(value);
			Length += count;
		}

		/// <summary>
		/// Inserts a new string into the collection at the specified index.
		/// </summary>
		/// <param name="index">The index you want to insert.</param>
		/// <param name="s">The string you want to insert.</param>
		public void Insert(int index, string s)
		{
			int count = s.Length;
			if (Length > _chars.Length - count)
			{
				Grow(count);
			}

			int remaining = Length - index;
			_chars.Slice(index, remaining).CopyTo(_chars[(index + count)..]);
			s.AsSpan().CopyTo(_chars[index..]);
			Length += count;
		}

		/// <summary>
		/// Remove a serial of characters from the specified index, with the specified length.
		/// </summary>
		/// <param name="startIndex">The start index.</param>
		/// <param name="length">The length you want to remove.</param>
		/// <remarks>
		/// This method will be costly (move a lot of elements), so you shouldn't call this method usually.
		/// </remarks>
		public unsafe void Remove(int startIndex, int length)
		{
			fixed (char* pThis = _chars)
			{
				int i = startIndex;
				for (char* p = pThis + startIndex; i < Length; i++, p++)
				{
					// Assign the value.
					// Please note that 'p[a]' is equi to '*(p + a)'.
					*p = p[length];
				}
			}

			Length -= length;
		}

		/// <summary>
		/// Removes the specified number of characters from the end of the collection.
		/// </summary>
		/// <param name="length">The number of characters you want to remove.</param>
		public void RemoveFromEnd(int length) => Length -= length;

		/// <summary>
		/// To ensure the capacity in order to append characters into this collection.
		/// </summary>
		/// <param name="capacity">The capacity value to ensure.</param>
		public void EnsureCapacity(int capacity)
		{
#if DEBUG
			// This is not expected to be called this with negative capacity
			Debug.Assert(capacity >= 0);
#endif

			// If the caller has a bug and calls this with negative capacity,
			// make sure to call Grow to throw an exception.
			if (capacity > Capacity)
			{
				Grow(capacity - Length);
			}
		}

		/// <summary>
		/// To clear the builder.
		/// </summary>
		public void Clear() => Dispose(false);

		/// <summary>
		/// Append a character at the tail of the collection.
		/// </summary>
		/// <param name="c">The character.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(char c)
		{
			int pos = Length;
			if ((uint)pos < (uint)_chars.Length)
			{
				_chars[pos] = c;
				Length = pos + 1;
			}
			else
			{
				Grow(1);
				Append(c);
			}
		}

		/// <summary>
		/// Append a serial of same characters into the collection.
		/// </summary>
		/// <param name="c">The character.</param>
		/// <param name="count">The number of the character you want to append.</param>
		public void Append(char c, int count)
		{
			if (Length > _chars.Length - count)
			{
				Grow(count);
			}

			var dst = _chars.Slice(Length, count);
			for (int i = 0; i < dst.Length; i++)
			{
				dst[i] = c;
			}

			Length += count;
		}

		/// <summary>
		/// Append a value.
		/// </summary>
		/// <typeparam name="TUnmanaged">The type of the value.</typeparam>
		/// <param name="value">The value.</param>
		public void Append<TUnmanaged>(TUnmanaged value) where TUnmanaged : unmanaged =>
			Append(value switch
			{
				sbyte s => s.ToString(),
				byte b => b.ToString(),
				short s => s.ToString(),
				ushort u => u.ToString(),
				nint n => n.ToString(),
				nuint n => n.ToString(),
				int i => i.ToString(),
				uint u => u.ToString(),
				long l => l.ToString(),
				ulong u => u.ToString(),
				char c => c.ToString(),
				bool b => b.ToString(),
				float f => f.ToString(),
				double d => d.ToString(),
				decimal d => d.ToString(),
				_ => value.ToString()
			});

		/// <summary>
		/// Append a string into the collection.
		/// </summary>
		/// <param name="s">The string.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(string? s)
		{
			switch (s)
			{
				case null:
				{
					return;
				}
				case { Length: 1 } when Length is var pos && (uint)pos < (uint)_chars.Length:
				{
					fixed (char* c = s)
					{
						_chars[pos] = *c;
					}

					Length = pos + 1;
					break;
				}
				default:
				{
					AppendSlow(s);

					break;
				}
			}
		}

		/// <summary>
		/// Append a string that is represented as a <see cref="char"/>*.
		/// </summary>
		/// <param name="value">The string.</param>
		/// <param name="length">The length of the string.</param>
		public unsafe void Append(char* value, int length)
		{
			int pos = Length;
			if (pos > _chars.Length - length)
			{
				Grow(length);
			}

			var dst = _chars.Slice(Length, length);
			for (int i = 0; i < dst.Length; i++)
			{
				dst[i] = *value++;
			}
			Length += length;
		}

		/// <summary>
		/// Append a serial of characters.
		/// </summary>
		/// <param name="value">(<see langword="in"/> parameter) The characters.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(in ReadOnlySpan<char> value)
		{
			int pos = Length;
			if (pos > _chars.Length - value.Length)
			{
				Grow(value.Length);
			}

			value.CopyTo(_chars[Length..]);
			Length += value.Length;
		}

		/// <summary>
		/// Append a new line string.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine() => Append(Environment.NewLine);

		/// <summary>
		/// Append a new line string through the specified unknown typed instance.
		/// </summary>
		/// <param name="instance">The instance.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(object instance) => AppendLine(instance.ToString());

		/// <summary>
		/// Append a character, and then apped a new line string.
		/// </summary>
		/// <param name="c">The character.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(char c)
		{
			Append(c);
			AppendLine();
		}

		/// <summary>
		/// Append a series of same characters, whose length is specified
		/// as the argument <paramref name="count"/>.
		/// </summary>
		/// <param name="c">The character.</param>
		/// <param name="count">The number of characters.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(char c, int count)
		{
			Append(c, count);
			AppendLine();
		}

		/// <summary>
		/// Append a string that represented as a pointer.
		/// </summary>
		/// <param name="s">The string.</param>
		/// <param name="length">The length.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void AppendLine(char* s, int length)
		{
			Append(s, length);
			AppendLine();
		}

		/// <summary>
		/// Append a string, and then append a new line.
		/// </summary>
		/// <param name="s">The string.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(string? s)
		{
			Append(s);
			AppendLine();
		}

		/// <summary>
		/// Append a series of elements into the current collection.
		/// </summary>
		/// <typeparam name="T">The type of each element.</typeparam>
		/// <param name="list">The list.</param>
		public void AppendLineRange<T>(IEnumerable<T> list)
		{
			foreach (var element in list)
			{
				AppendLine(element?.ToString());
			}
		}

		/// <summary>
		/// Append a serial of strings from a serial of elements.
		/// </summary>
		/// <typeparam name="TUnmanaged">The type of each element.</typeparam>
		/// <param name="list">The list of elements.</param>
		/// <param name="separator">The separator when an element is finished to append.</param>
		public unsafe void AppendRange<TUnmanaged>(IEnumerable<TUnmanaged> list, string? separator = null)
			where TUnmanaged : unmanaged
		{
			foreach (var element in list)
			{
				Append(element);
				if (separator is not null)
				{
					Append(separator);
				}
			}

			if (separator is not null)
			{
				RemoveFromEnd(separator.Length);
			}
		}

		/// <summary>
		/// Append a serial of strings converted from a serial of elements.
		/// </summary>
		/// <typeparam name="TUnmanaged">The type of each element.</typeparam>
		/// <param name="list">The list of elements.</param>
		/// <param name="converter">The converter.</param>
		/// <param name="separator">The separator when an element is finished to append.</param>
		public unsafe void AppendRange<TUnmanaged>(
			IEnumerable<TUnmanaged> list, delegate* managed<TUnmanaged, string?> converter,
			string? separator = null) where TUnmanaged : unmanaged
		{
			foreach (var element in list)
			{
				Append(converter(element));
				if (separator is not null)
				{
					Append(separator);
				}
			}

			if (separator is not null)
			{
				RemoveFromEnd(separator.Length);
			}
		}

		/// <summary>
		/// Append a serial of strings from a serial of elements specified as a pointer.
		/// </summary>
		/// <typeparam name="TUnmanaged">The type of each element.</typeparam>
		/// <param name="list">The list of elements.</param>
		/// <param name="length">The length of the list.</param>
		/// <param name="separator">The separator when an element is finished to append.</param>
		public unsafe void AppendRange<TUnmanaged>(TUnmanaged* list, int length, string? separator = null)
			where TUnmanaged : unmanaged
		{
			int index = 0;
			for (var p = list; index < length; index++, p++)
			{
				Append(list[index]);
				if (separator is not null)
				{
					Append(separator);
				}
			}

			if (separator is not null)
			{
				RemoveFromEnd(separator.Length);
			}
		}

		/// <summary>
		/// Append a serial of strings converted from a serial of elements specified as a pointer.
		/// </summary>
		/// <typeparam name="TUnmanaged">The type of each element.</typeparam>
		/// <param name="list">The list of elements.</param>
		/// <param name="length">The length of the list.</param>
		/// <param name="converter">The converter.</param>
		/// <param name="separator">The separator when an element is finished to append.</param>
		public unsafe void AppendRange<TUnmanaged>(
			TUnmanaged* list, int length, delegate* managed<TUnmanaged, string?> converter,
			string? separator = null) where TUnmanaged : unmanaged
		{
			int index = 0;
			for (var p = list; index < length; index++, p++)
			{
				Append(converter(list[index]));
				if (separator is not null)
				{
					Append(separator);
				}
			}

			if (separator is not null)
			{
				RemoveFromEnd(separator.Length);
			}
		}

		/// <summary>
		/// Append a string representation of a specified instance, and then append a new line.
		/// </summary>
		/// <typeparam name="TUnmanaged">The type of the instance.</typeparam>
		/// <param name="value">The value.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine<TUnmanaged>(TUnmanaged value) where TUnmanaged : unmanaged
		{
			Append(value);
			AppendLine();
		}

		/// <summary>
		/// Append a span.
		/// </summary>
		/// <param name="length">The length of the characters.</param>
		/// <returns>The span.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<char> AppendSpan(int length)
		{
			int originalPos = Length;
			if (originalPos > _chars.Length - length)
			{
				Grow(length);
			}

			Length = originalPos + length;
			return _chars.Slice(originalPos, length);
		}

		/// <summary>
		/// Reverse the string builder instance. For example, if the list holds a string <c>"Hello"</c>,
		/// after called this method, the string will be <c>"olleH"</c>.
		/// </summary>
		public unsafe void Reverse()
		{
			fixed (char* p = _chars)
			{
				for (int i = 0; i < Length >> 1; i++)
				{
					char c = p[i];
					p[i] = p[Length - 1 - i];
					p[Length - 1 - i] = c;
				}
			}
		}

		/// <summary>
		/// To dispose the collection, all fields and properties will be cleared. In other words,
		/// this method is nearly equivalent to the code <c>this = default;</c>.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose() => Dispose(true);

		/// <summary>
		/// To dispose the collection. Although this method is <see langword="public"/>,
		/// you may not call this method, because this method will be called automatically when
		/// the method <see cref="ToString"/> is called.
		/// </summary>
		/// <param name="clearAll">Indicates whether we should return the buffer.</param>
		/// <seealso cref="ToString"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Dispose(bool clearAll)
		{
			char[]? toReturn = _chunk;

			if (clearAll)
			{
				// For safety, to avoid using pooled array if this instance is erroneously appended to again.
				this = default;
			}
			else
			{
				// Store the previous data, but clear the length value to 0.
				Length = 0;
				_chars.Clear();
			}

			// Returns the buffer memory.
			if (toReturn is not null)
			{
				ArrayPool<char>.Shared.Return(toReturn);
			}
		}

		/// <summary>
		/// Resize the internal buffer either by doubling current buffer size or
		/// by adding <paramref name="additionalCapacityBeyondPos"/> to
		/// <see cref="Length"/> whichever is greater.
		/// </summary>
		/// <param name="additionalCapacityBeyondPos">Number of chars requested beyond current position.</param>
		/// <seealso cref="Length"/>
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Grow(int additionalCapacityBeyondPos)
		{
#if DEBUG
			Debug.Assert(additionalCapacityBeyondPos > 0);
			Debug.Assert(
				Length > _chars.Length - additionalCapacityBeyondPos,
				"Grow called incorrectly, no resize is needed."
			);
#endif

			// Make sure to let Rent throw an exception
			// if the caller has a bug and the desired capacity is negative.
			char[] poolArray = ArrayPool<char>.Shared.Rent(
				(int)Math.Max((uint)(Length + additionalCapacityBeyondPos), (uint)_chars.Length * 2)
			);

			// If lack of space to store extra characters, just creates a new one,
			// and copy them into the new collection.
			_chars[..Length].CopyTo(poolArray);

			char[]? toReturn = _chunk;
			_chars = _chunk = poolArray;
			if (toReturn is not null)
			{
				ArrayPool<char>.Shared.Return(toReturn);
			}
		}

		/// <summary>
		/// Append a string.
		/// </summary>
		/// <param name="s">The string.</param>
		private void AppendSlow(string s)
		{
			int pos = Length;
			if (pos > _chars.Length - s.Length)
			{
				Grow(s.Length);
			}

			s.AsSpan().CopyTo(_chars[pos..]);
			Length += s.Length;
		}
	}
}
