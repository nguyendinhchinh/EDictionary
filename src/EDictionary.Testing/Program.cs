using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using System;
using System.IO;

namespace EDictionary.Testing
{
	class Program
	{
		public static string ExecutePath { get; set; }
		public static string SrcPath { get; set; }

		static Program()
		{
			ExecutePath = System.AppDomain.CurrentDomain.BaseDirectory;
			SrcPath = Path.GetFullPath(Path.Combine(ExecutePath, @"..\..\..\"));

			string wordlistPath = Path.Combine(SrcPath, @"EDictionary\Data\vocabulary.txt");
			SpellCheck.GetVocabulary(wordlistPath);
		}

		static void TestSpellCheck()
		{
			SpellCheck.ReadFromStdIn();
		}

		static void TestJsonHelper()
		{
			string strWord;
			Word word;

			while (!string.IsNullOrEmpty(strWord = Console.ReadLine().Trim()))
			{
				try
				{
					string jsonPath = Path.Combine(SrcPath, String.Format(@"EDictionary\Data\raw\data\words\{0}.json", strWord));
					string strJson = File.ReadAllText(jsonPath);

					word = JsonHelper.Deserialize(strJson);
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
			/* TestJsonHelper(); */
		}
	}
}
