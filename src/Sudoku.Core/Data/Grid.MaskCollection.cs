﻿namespace Sudoku.Data;

partial struct Grid
{
	/// <summary>
	/// Defines a collection that iterates the mask list of the sudoku grid.
	/// </summary>
	public readonly unsafe ref partial struct MaskCollection
	{
		/// <summary>
		/// Indicates the enumerator created.
		/// </summary>
		private readonly Enumerator _enumerator;


		/// <summary>
		/// Initializes a <see cref="MaskCollection"/> via the mask pointer.
		/// </summary>
		/// <param name="mask">The pointer to the mask list.</param>
		public MaskCollection(short* mask) => _enumerator = new(mask);


		/// <summary>
		/// Gets the enumerator of the iteration.
		/// </summary>
		/// <returns>The enumerator of the iteration.</returns>
		public Enumerator GetEnumerator() => _enumerator;
	}
}
