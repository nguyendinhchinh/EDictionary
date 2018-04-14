using EDictionary.Core.Data;
using EDictionary.Core.Extensions;
using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.Models
{
	public class Dictionary
	{
		private DataAccess dataAccess = new DataAccess();
		public Word Word { get; set; }

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
			return dataAccess.LookUp(word)
				?? dataAccess.LookUp(word.AppendWordNumber(1))
				?? dataAccess.LookUp(word.AppendWordNumber(2))
				?? dataAccess.LookUp(word.AppendWordNumber(3));
		}

		public IEnumerable<string> Similar(string word)
		{
			return SpellCheck.Candidates(word);
		}
	}
}
