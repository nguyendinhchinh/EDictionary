using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EDictionary.Core.Extensions
{
	public static class StringExtensions
	{
		/// <summary>
		/// "time_1" -> "time"
		/// </summary>
		public static string StripWordNumber(this string str)
		{
			return Regex.Replace(str, @"_[0-9]$", string.Empty);
		}

		/// <summary>
		/// "time" -> "time_1" (by default)
		/// </summary>
		public static string AppendWordNumber(this string str, int wordIDNumber = 1)
		{
			return str + $"_{wordIDNumber}";
		}
	}
}
