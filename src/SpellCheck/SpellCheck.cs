using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Util
{
	static public class SpellCheck
	{
		public static HashSet<string> vocabulary;
		private const string alphabet = "abcdefghijklmnopqrstuvwxyz";
		private static Func<string, bool> IsValidWord = word => vocabulary.Contains(word) ? true : false;

		static SpellCheck() // static constructor
		{
			vocabulary = ReadWordList();
		}

		private static HashSet<string> ReadWordList()
		{
			string WordListFile = System.IO.Path.GetFullPath(@".\words_alpha.txt");

			if (!File.Exists(WordListFile))
			{
				throw new Exception("Cannot find vocabulary text file");
			}

			return new HashSet<string>(
					from line in File.ReadLines(WordListFile) select line);
		}

		public static IEnumerable<string> Edits1(string word)
		{
			var splits = from i in Enumerable.Range(0, word.Length)
				select new {a = word.Substring(0, i), b = word.Substring(i)};

			var deletes = from s in splits
				where s.b != "" // we know it can't be null
				select s.a + s.b.Substring(1);

			var transposes = from s in splits
				where s.b.Length > 1
				select s.a + s.b[1] + s.b[0] + s.b.Substring(2);

			var replaces = from s in splits
				from c in alphabet
				where s.b != ""
				select s.a + c + s.b.Substring(1);

			var inserts = from s in splits
				from c in alphabet
				select s.a + c + s.b;

			return deletes
				.Union(transposes) // union translates into a set
				.Union(replaces)
				.Union(inserts);
		}

		private static IEnumerable<string> Edits2(string word)
		{
			return (
					from e1 in Edits1(word)
					from e2 in Edits1(e1)
					select e2
					).Distinct();
		}

		private static IEnumerable<string> Known(IEnumerable<string> words)
		{
			return words.Where(word => IsValidWord(word));
		}

		private static IEnumerable<string> Candidates(string word)
		{
			var candidates = new[] {
					Known(new[] {word}),
					Known(Edits1(word)),
					Known(Edits2(word)),
					new[] {word},
					}.First(knowns => knowns.Any());

			return candidates;
		}

		private static void ReadFromStdIn()
		{
			string word;

			while (!string.IsNullOrEmpty(word = Console.ReadLine().Trim()))
			{
				foreach (var candidate in Candidates(word))
				{
					Console.Write(candidate + " ");
				}
				Console.WriteLine();
			}
		}

		// for testing only
		public static void Main()
		{
			ReadFromStdIn();
		}
	}
}
