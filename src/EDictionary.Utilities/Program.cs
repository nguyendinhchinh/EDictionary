using EDictionary.Core.Models;
using EDictionary.Core.Presenters;
using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Utilities
{
	class Program
	{
		static void Main(string[] args)
		{
			DataAccess.Create();

			//string dataPath = Path.GetFullPath(
			//        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"D:\E-Dictionary\src\EDictionary\Data\raw\data\words"));
			//string[] filePaths = Directory.GetFiles(dataPath, "*.json", SearchOption.TopDirectoryOnly);

			//foreach (string filePath in filePaths)
			//{
			//    string word = filePath.Split('\\').Last().Split('.').First();
			//    string jsonStr = File.ReadAllText(filePath);

			//    Console.WriteLine($"Insert {word} into table...");
			//    DataAccess.Insert(jsonStr);
			//}

			string dataPath = Path.GetFullPath(
					Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\EDictionary\Data\words.sqlite"));
			Word word;
			string strWord;
			//DataAccess.GetWordList();

			while (true)
			{
				strWord = Console.ReadLine();
				word = DataAccess.LookUp(strWord);
				Console.WriteLine($"Word: {word.word}");
				Console.WriteLine($"Keyword: {word.Keyword}");
				Console.WriteLine($"Wordform: {word.Wordform}");
			}
		}
	}
}
