//
// Copyright (c) 2016 Patryk Strach
//
// This file is part of OldFileRemover.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;

namespace OldFileRemover
{
	/// <summary>
	/// Class used for converting size in bytes to a string with the biggest unit postfix.
	/// </summary>
	internal static class UserFriendlySizeFormatter
	{
		private static readonly string[] SizeUnitsLookUpTable = { "B", "KiB", "MiB", "GiB" };

		/// <summary>
		/// Formats the specified size.
		/// </summary>
		/// <param name="size">
		/// The size to format.
		/// </param>
		/// <returns>
		/// String representing specified value with biggest unit postfix.
		/// For example:
		/// For 825, the function will return "825 B", for 1100, it will return "1.07 KB",
		/// for 3 gigabytes "3 GB" and so on.
		/// </returns>
		public static string Format(long size)
		{
			int   unitIndex = 0;
			float newSize   = size;

			while(newSize > 1000.0f && unitIndex < SizeUnitsLookUpTable.Length - 1)
			{
				newSize /= 1024.0f;
				unitIndex++;
			}

			return String.Format("{0:G3} {1}", newSize, SizeUnitsLookUpTable[unitIndex]);
		}
	}
}

