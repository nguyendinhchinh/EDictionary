using EDictionary.Core.Extensions;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using Iveonik.Stemmers;
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

		private Dictionary dictionary = new Dictionary();
		private History<string> history = new History<string>();
		private Word word; // current word

		public EDictionaryLib(IEDictionary view)
		{
			this.view = view;
		}

		public void InitWordList()
		{
			view.WordList = dictionary.GetDistinctWordList();
		}

		public string CorrectWord(string word)
		{
			IEnumerable<string> candidates = dictionary.Similar(word);
			return String.Join(Environment.NewLine, candidates);
		}

		/// <summary>
		/// Print all info about a word on view
		/// </summary>
		public void GetDefinition(string wordStr)
		{
			string wordID = wordStr.AppendWordNumber();
			word = dictionary.Search(wordStr);

			if (word != null)
				view.Definition = word.ToString();
		}

		private void UpdateHistory()
		{
			if (word != null && word.Keyword != history.Current)
				history.Add(word.Keyword);
		}

		/// <summary>
		/// Called on enter or doubleclick event on wordlist
		/// Run spellcheck for similar word when word not found
		/// </summary>
		public void GoToDefinition(string wordStr)
		{
			GetDefinition(wordStr);

			if (word == null)
				view.Definition = CorrectWord(view.Input);

			UpdateHistory();
		}

		/// <summary>
		/// Called when select highlight word in defintion window
		/// Stem a word when word not found instead of running spellcheck
		/// </summary>
		public void JumpToDefinition(string wordStr)
		{
			GetDefinition(wordStr);

			if (word == null)
			{
				var stemmer = new EnglishStemmer();

				GetDefinition(stemmer.Stem(wordStr));
			}

			UpdateHistory();
		}

		public void NextHistory()
		{
			GetDefinition(history.Next());

			if (!history.IsFirst)
				view.IsPrevHistoryEnabled = true;

			if (history.IsLast)
				view.IsNextHistoryEnabled = false;
		}

		public void PreviousHistory()
		{
			GetDefinition(history.Previous());

			if (!history.IsLast)
				view.IsNextHistoryEnabled = true;

			if (history.IsFirst)
				view.IsPrevHistoryEnabled = false;
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
