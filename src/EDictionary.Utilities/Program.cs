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

			string dataPath = Path.GetFullPath(
					Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\EDictionary\Data\raw\data\words"));
			string[] filePaths = Directory.GetFiles(dataPath, "*.json", SearchOption.TopDirectoryOnly);

			foreach (string filePath in filePaths)
			{
				string word = filePath.Split('\\').Last().Split('.').First();
				Console.WriteLine($"Insert {word} into table...");
				string jsonStr = File.ReadAllText(filePath);
				DataAccess.Insert(jsonStr);
			}
		}
	}
}
