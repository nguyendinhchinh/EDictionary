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
		private Word word; // current word

		public EDictionaryLib(IEDictionary view)
		{
			this.view = view;
		}

		public void InitWordList()
		{
			List<string> words = dictionary.GetWordList();
			
			words = words.Select(x => x.StripWordNumber()).Distinct().ToList();
			words.Sort();

			view.WordList = words;
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

			if (word == null)
			{
				view.Definition = CorrectWord(view.Input);
			}
			else
			{
				view.Definition = word.ToString();
			}
		}

		public void GoToDefinition(string wordStr)
		{
			GetDefinition(wordStr);

			if (word == null)
				return;

			if (word.Keyword != history.Current)
			    history.Add(word.Keyword);
		}

		public void NextHistory()
		{
			GetDefinition(history.Next());
		}

		public void PreviousHistory()
		{
			GetDefinition(history.Previous());
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
