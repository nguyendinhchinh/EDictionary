using EDictionary.Core.Data;
using EDictionary.Core.Extensions;
using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

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
			Wordlist = GetDistinctWordList();
		}

		public List<string> GetWordList()
		{
			return dataAccess.GetWordList();
		}

		public List<string> GetDistinctWordList()
		{
			List<string> words = GetWordList();

			words = words
				.Select(x => x.StripWordNumber())
				.Distinct()
				.ToList();

			words.Sort();

			return words;
		}

		public Word Search(string word)
		{
			return dataAccess.LookUp(word)
				?? dataAccess.LookUp(word.AppendWordNumber(1))
				?? dataAccess.LookUp(word.AppendWordNumber(2))
				?? dataAccess.LookUp(word.AppendWordNumber(3));
		}

		public string GetFilename(Word word, Dialect dialect)
		{
			var result = word.Pronunciations
				.Where(x => x.Prefix == dialect.ToString())
				.Select(x => x.Filename)
				.ToList();

			return result[0];
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
