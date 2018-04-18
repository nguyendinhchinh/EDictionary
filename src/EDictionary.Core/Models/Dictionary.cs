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
		NamE,
		BrE,
	}

	public class Dictionary
	{
		private readonly string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "audio");
		private DataAccess dataAccess = new DataAccess();
		public Word currentWord { get; set; }

		public Dictionary()
		{
			SpellCheck.GetVocabulary(GetDistinctWordList());
		}

		public List<string> GetWordList()
		{
			return dataAccess.GetWordList();
		}

		public List<string> GetDistinctWordList()
		{
			List<string> words = GetWordList();

			words = words.Select(x => x.StripWordNumber()).Distinct().ToList();
			words.Sort();

			return words;
		}

		public Word Search(string word)
		{
			currentWord = dataAccess.LookUp(word)
				?? dataAccess.LookUp(word.AppendWordNumber(1))
				?? dataAccess.LookUp(word.AppendWordNumber(2))
				?? dataAccess.LookUp(word.AppendWordNumber(3));

			return currentWord;
		}

		public IEnumerable<string> Similar(string word)
		{
			return SpellCheck.Candidates(word);
		}

		private string ExtractFilename(string url)
		{
			return url.Split('/').Last();
		}

		public void PlayAudio(Dialect dialect)
		{
			string filename = null;
			string audioFile;

			if (dialect == Dialect.NamE)
			{
				filename = ExtractFilename(currentWord.Pronunciations.America.Url);
			}
			else if (dialect == Dialect.BrE)
			{
				filename = ExtractFilename(currentWord.Pronunciations.Britain.Url);
			}

			audioFile = Path.Combine(audioPath, filename);

			Audio.Play(audioFile);
		}
	}
}
