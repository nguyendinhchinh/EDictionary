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
		private IEDictionary view;
		public bool IsActiveTextbox { get; set; } = true;

		public EDictionaryLib(IEDictionary view)
		{
			DataAccess.Create();
			this.view = view;
		}

		public void InitWordList()
		{
			List<string> words = DataAccess.GetWordList();
			
			words = words.Select(x => x.StripWordNumber()).Distinct().ToList();
			words.Sort();

			view.WordList = words;

			SpellCheck.GetVocabulary(view.WordList);
		}

		public string CorrectWord(string word)
		{
			IEnumerable<string> candidates = SpellCheck.Candidates(word);
			return String.Join(Environment.NewLine, candidates);
		}

		/// <summary>
		/// Print all info about a word on view
		/// </summary>
		public void GetDefinition(string wordStr, bool isLink=false)
		{
			string wordID = wordStr.AppendWordNumber();
			Word word = DataAccess.LookUp(wordStr) ?? DataAccess.LookUp(wordID);

			if (word == null)
			{
				view.Definition = CorrectWord(view.Input);
			}
			else
			{
				view.Definition = word.ToString();

                if (isLink)
                    History.AddLink(word.Keyword);
                else
                    History.Add(word.Keyword);
            }
        }

        public void GoTo(string wordStr)
        {
            GetDefinition(wordStr, true);
        }

		public void UpdateWordlistCurrentIndex()
		{
			view.SelectedIndex = Search.Prefix(view.Input, view.WordList);

			if (view.TopIndex != view.SelectedIndex)
			{
				view.TopIndex = view.SelectedIndex;
			}
		}

		public void SelectItem(int index)
		{
			view.SelectedIndex = index;
		}
	}
}
