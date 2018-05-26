using EDictionary.Core.Data;
using EDictionary.Core.Extensions;
using EDictionary.Core.Models.WordComponents;
using EDictionary.Core.Utilities;
using EDictionary.Vendors.RTF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EDictionary.Core.Models
{
	public class EDict
	{
		private readonly string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "audio");
		private DataAccess dataAccess = new DataAccess();

		public Dictionary<string, List<string>> NameToIDs { get; set; }
		public List<string> WordList { get; set; }

		public EDict()
		{
			NameToIDs = new Dictionary<string, List<string>>();
			List<string> wordIDs = GetWordIDList();
			List<string> wordNames = GetWordNameList();

			int currentIndex;

			for (int i = 0; i <= wordNames.Count - 1; i++)
			{
				NameToIDs.Add(wordNames[i], new List<string>() { wordIDs[i] });
				currentIndex = i;

				while (wordNames[i] == wordNames.NextItem(i))
				{
					NameToIDs[wordNames[currentIndex]].Add(wordIDs[i + 1]);
					i++;
				}
			}

			WordList = NameToIDs.Keys.ToList();
			WordList.Sort();
		}

		private List<string> GetWordIDList()
		{
			Result<List<string>> result = dataAccess.SelectID();

			if (result.Status != Status.Success)
			{
				LogWriter.Instance.WriteLine($"Error occured at GetWordIDList in class Dictionary: {result.Message}");
				return null;
			}

			return result.Data;
		}

		private List<string> GetWordNameList()
		{
			Result<List<string>> result = dataAccess.SelectName();

			if (result.Status != Status.Success)
			{
				LogWriter.Instance.WriteLine($"Error occured at GetWordNameList in class Dictionary: {result.Message}");
				return null;
			}

			return result.Data;
		}

		public Word Search(string word)
		{
			var index = SearchIndex(word);

			if (index == -1)
				return null;

			word = WordList[index];

			Result<Word> result = dataAccess.SelectDefinitionFrom(NameToIDs[word][0]);

			if (result.Status != Status.Success)
			{
				LogWriter.Instance.WriteLine($"Error occured at Search in class Dictionary: {result.Message}");
				return null;
			}

			// TODO: Set default Prefix when scraping
			for (int i = 0; i < result.Data.Pronunciations.Count(); i++)
			{
				var pron = result.Data.Pronunciations[i];

				if (pron.Prefix == null)
				{
					if (pron.Filename.Contains("__gb"))
						pron.Prefix = "BrE";
					else
						pron.Prefix = "NAmE";
				}
			}

			return result.Data;
		}

		private int SearchIndex(string word)
		{
			return WordList.FindIndex(x => x.Equalize().Equals(word.Equalize(), StringComparison.OrdinalIgnoreCase));
		}

		public Word SearchID(string wordID)
		{
			Result<Word> result = dataAccess.SelectDefinitionFrom(wordID);

			if (result.Status != Status.Success)
			{
				LogWriter.Instance.WriteLine($"Error occured at Search in class Dictionary: {result.Message}");
				return null;
			}

			return result.Data;
		}

		public string GetFilename(Word word, Dialect dialect)
		{
			return word.Pronunciations
				.Where(x => x.Prefix == dialect.ToString())
				.Select(x => x.Filename)
				.First();
		}

		public void PlayAudio(Word word, Dialect dialect)
		{
			string filename = null;
			string audioFile;

			filename = GetFilename(word, dialect);
			audioFile = Path.Combine(audioPath, filename);

			Audio.Play(audioFile);
		}

		public string GetSuggestions(string wrongWord, IEnumerable<string> candidates)
		{
			RTFBuilder builder = new RTFBuilder();

			builder.AppendLine();
			builder.FontSize(30).Append("No match for ");
			builder.ForeColor(ColorPicker.Color("LightRed"));
			builder.FontSize(30).Append($"\"{wrongWord}\"");
			builder.FontSize(30).AppendLine(" in the dictionary");
			builder.AppendLine();

			if (candidates.Count() == 1)
				return builder.ToString();

			builder.FontSize(20).AppendLine("Did you mean:");
			foreach (var candidate in candidates)
			{
				builder.ForeColor(ColorPicker.Color("LightCyan"));
				builder.AppendLine(" • " + candidate);
			}

			return builder.ToString();
		}
	}
}
