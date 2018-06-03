using EDictionary.Core.Data;
using EDictionary.Core.Extensions;
using EDictionary.Core.Models;
using EDictionary.Core.Models.WordComponents;
using EDictionary.Core.Utilities;
using EDictionary.Theme.Utilities;
using EDictionary.Vendors.RTF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EDictionary.Core.DataLogic
{
	/// <summary>
	/// Logic to manipulate Word model: Search for definition, play audio...
	/// </summary>
	public class WordLogic
   {
		private readonly string audioPath;
		private AudioManager audioPlayer;
		private DataAccess dataAccess;
		private SpellCheck spellCheck;

		public Dictionary<string, List<string>> NameToIDs { get; set; }
		public List<string> WordList
		{
			get
			{
				var wordList = NameToIDs.Keys.ToList();
				wordList.Sort();

				return wordList;
			}
		}

		public WordLogic()
		{
			audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "audio");
			dataAccess = new DataAccess();
			audioPlayer = new AudioManager();

			InitWordList();
			spellCheck = new SpellCheck(WordList);
		}

		private void InitWordList()
		{
			NameToIDs = new Dictionary<string, List<string>>();
			List<string> wordIDs = GetWordIDList();
			List<string> wordNames = GetWordNameList();

			int currentIndex;

			for (int i = 0; i <= wordNames.Count - 1; i++)
			{
				string currentKey = wordNames[i].Trim().ToLower();
				NameToIDs.Add(currentKey, new List<string>() { wordIDs[i] });
				currentIndex = i;

				while (wordNames[currentIndex] == wordNames.NextItem(i))
				{
					NameToIDs[currentKey].Add(wordIDs[i + 1]);
					i++;
				}
			}
		}

		public List<string> GetWordIDList()
		{
			Result<List<string>> result = dataAccess.SelectID();

			if (result.Status != Status.Success)
			{
				LogWriter.Instance.WriteLine($"Error occured at GetWordIDList in class Dictionary: {result.Message}");
				return null;
			}

			return result.Data;
		}

		public List<string> GetWordNameList()
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
			if (word == null)
				return null;

			word = word.Trim().ToLower();

			if (!NameToIDs.ContainsKey(word))
				return null;

			Result<Word> result = dataAccess.SelectDefinitionFrom(NameToIDs[word].FirstOrDefault());

			if (result.Status != Status.Success)
			{
				LogWriter.Instance.WriteLine($"Error occured at Search in class Dictionary: {result.Message}");
				return null;
			}

			return result.Data;
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

		private string GetFilename(Word word, Dialect dialect)
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

			audioPlayer.Play(audioFile);
		}

		public string GetSuggestions(string wrongWord)
		{
			IEnumerable<string> candidates = spellCheck.Candidates(wrongWord);

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
