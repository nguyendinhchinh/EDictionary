using EDictionary.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EDictionary.Testing
{
	class Program
	{
		private static Dictionary dictionary;

		static Program()
		{
			dictionary = new Dictionary();
		}

		static void TestSpellCheck()
		{
			string name;
			IEnumerable<string> candidates;

			while (!string.IsNullOrEmpty(name = Console.ReadLine().Trim()))
			{
				try
				{
					candidates = dictionary.Similar(name);
					foreach (var candidate in candidates)
						Console.WriteLine(candidate);
				}
				catch (System.IO.FileNotFoundException)
				{
					Console.Error.WriteLine("Word not found");
				}
			}
		}

		static void TestLookUp()
		{
			string name;
			Word word;

			while (!string.IsNullOrEmpty(name = Console.ReadLine().Trim()))
			{
				try
				{
					word = dictionary.Search(name);
					Console.WriteLine(word.ToString());
				}
				catch (System.IO.FileNotFoundException)
				{
					Console.Error.WriteLine("Word not found");
				}
			}
		}

		static void Main(string[] args)
		{
			TestSpellCheck();
			// TestLookUp();
		}
	}
}
