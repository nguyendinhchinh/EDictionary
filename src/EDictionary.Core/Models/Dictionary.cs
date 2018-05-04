using EDictionary.Core.Data;
using EDictionary.Core.Extensions;
using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EDictionary.Core.Models
{
	public enum Dialect
	{
		NAmE,
		BrE,
	}

	public class Dictionary
	{
		private readonly string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "audio");
		private DataAccess dataAccess = new DataAccess();
		public List<string> Wordlist { get; set; }

		public Dictionary()
		{
			Wordlist = GetWordList();
		}

		public List<string> GetWordList()
		{
			Result<List<string>> result = dataAccess.GetWordList();

			if (result.Status != Status.Success)
			{
				LogWriter.Instance.WriteLine($"Error occured at GetWordList in class Dictionary: {result.Message}");
				return null;
			}

			List<string> words = result.Data
				.Select(x => x.StripWordNumber())
				.Distinct()
				.ToList();

			words.Sort();

			return words;
		}

		public Word Search(string word)
		{
			Result<Word> result = dataAccess.LookUp(word);

			if (result.Status != Status.Success)
			{
				LogWriter.Instance.WriteLine($"Error occured at Search in class Dictionary: {result.Message}");
				return null;
			}

			if (result.Data == null)
			{
				result = dataAccess.LookUpSimilar(word);

				if (result.Status != Status.Success)
				{
					LogWriter.Instance.WriteLine($"Error occured at Search in class Dictionary: {result.Message}");
					return null;
				}
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
	}
}
