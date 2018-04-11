using EDictionary.Core.Extensions;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.Presenters
{
	public class EDictionaryLib
	{
		private IEDictionary eDictionaryView;
		public bool IsActiveTextbox { get; set; } = true;

		public EDictionaryLib(IEDictionary view)
		{
			DataAccess.Create();
			eDictionaryView = view;
		}

		public void InitWordList()
		{
			List<string> words = DataAccess.GetWordList();
			
			words = words.Select(x => x.StripWordNumber()).Distinct().ToList();
			words.Sort();

			eDictionaryView.WordList = words;

			SpellCheck.GetVocabulary(eDictionaryView.WordList);
		}

		/// <summary>
		/// Print all info about a word on view
		/// </summary>
		public void GetDefinition(string wordStr)
		{
			string wordID = wordStr.AppendWordNumber();
			Word word = DataAccess.LookUp(wordID);

			if (word == null)
			{
				// some word have multiple form like "truck_1" (noun) and "truck_2" (verb)
				word = DataAccess.LookUp(wordID);

				if (word == null)
				{
					IEnumerable<string> candidates = SpellCheck.Candidates(eDictionaryView.Input);

					eDictionaryView.Definition = String.Join(Environment.NewLine, candidates);
				}
				else
				{
					eDictionaryView.Definition = word.ToString();
				}
			}
			else
			{
				eDictionaryView.Definition = word.ToString();
			}
		}

		public void UpdateWordlistCurrentIndex()
		{
			eDictionaryView.SelectedIndex = Search.Prefix(eDictionaryView.Input, eDictionaryView.WordList);

			if (eDictionaryView.TopIndex != eDictionaryView.SelectedIndex)
			{
				eDictionaryView.TopIndex = eDictionaryView.SelectedIndex;
			}
		}

		public void SelectItem(int index)
		{
			eDictionaryView.SelectedIndex = index;
		}
	}
}
