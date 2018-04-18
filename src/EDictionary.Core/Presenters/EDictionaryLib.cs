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

		private Dictionary dictionary = new Dictionary();
		private History<string> history = new History<string>();

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
		/// Return Word object from data layer lookup
		/// </summary>
		public string GetDefinition(string wordStr)
		{
			return dictionary.Search(wordStr)?.ToString();
		}

		private void UpdateHistory()
		{
			if (dictionary.currentWord != null && dictionary.currentWord.Keyword != history.Current)
				history.Add(dictionary.currentWord.Keyword);
		}

		/// <summary>
		/// Called on enter or doubleclick event on wordlist
		/// Run spellcheck for similar word when word not found
		/// </summary>
		public void GoToDefinition(string wordStr)
		{
			string definition = GetDefinition(wordStr)
				?? GetDefinition(Stemmer.Stem(wordStr))
				?? CorrectWord(view.Input);

			view.Definition = definition;
			UpdateHistory();
		}

		/// <summary>
		/// Called when select highlight word in defintion window
		/// Stem a word when word not found instead of running spellcheck
		/// </summary>
		public void JumpToDefinition(string wordStr)
		{
			string definition = GetDefinition(wordStr)
				?? GetDefinition(Stemmer.Stem(wordStr));

			if (definition != null)
				view.Definition = definition;

			UpdateHistory();
		}

		public void NextHistory()
		{
			view.Definition = GetDefinition(history.Next());

			if (!history.IsFirst)
				view.IsPrevHistoryEnabled = true;

			if (history.IsLast)
				view.IsNextHistoryEnabled = false;
		}

		public void PreviousHistory()
		{
			view.Definition = GetDefinition(history.Previous());

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

		public void PlayAmericaAudio()
		{
			dictionary.PlayAudio(Dialect.NamE);
		}

		public void PlayBritianAudio()
		{
			dictionary.PlayAudio(Dialect.BrE);
		}
	}
}
