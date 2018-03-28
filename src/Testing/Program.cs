using System;
using System.IO;

namespace Testing
{
	class Program
	{
		static void Main(string[] args)
		{
			string executePath = System.AppDomain.CurrentDomain.BaseDirectory;
			string wordlistPath = Path.GetFullPath(
					Path.Combine(executePath, @"..\..\..\..\Util\vocabulary.txt"));
			
			Util.SpellCheck.GetVocabulary(wordlistPath);
			Util.SpellCheck.Main();
		}
	}
}
