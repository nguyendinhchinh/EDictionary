using EDictionary.Core.Commands;
using EDictionary.Core.Extensions;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace EDictionary.Core.ViewModels
{
	public class EDictionaryViewModel : INotifyPropertyChanged
	{
		#region Fields

		private Dictionary dictionary { get; set; }
		private History<Word> history;
		private int topIndex;
		private string currentWord;
		private string selectedWord;
		private string definition;

		#endregion

		#region Properties

		public double WindowMinimumHeight { get; set; } = 350;

		public double WindowMinimumWidth { get; set; } = 450;

		public List<string> Wordlist
		{
			get
			{
				return dictionary.Wordlist;
			}
		}

		public int TopIndex
		{
			get
			{
				return topIndex;
			}
			set
			{
				if (value != topIndex)
				{
					topIndex = value.Clamp(0, Wordlist.Count - 1);
					NotifyPropertyChanged("TopIndex");
				}
			}
		}

		public string CurrentWord
		{
			get
			{
				return currentWord.Trim().ToLower();
			}
			set
			{
				if (value != currentWord)
				{
					currentWord = value;
					UpdateWordlistTopIndex();
				}
			}
		}

		public string SelectedWord
		{
			get
			{
				return selectedWord.ToLower();
			}
			set
			{
				if (value != definition)
				{
					selectedWord = value;
				}
			}
		}

		public string Definition
		{
			get
			{
				return definition;
			}

			set
			{
				if (value != definition)
				{
					definition = value;
					NotifyPropertyChanged("Definition");
				}
			}
		}

		#endregion

		#region Constructor

		public EDictionaryViewModel()
		{
			dictionary = new Dictionary();
			history = new History<Word>();

			SpellCheck.GetVocabulary(Wordlist);

			GoToDefinitionCommand = new GoToDefinitionCommand(this);
			JumpToDefinitionCommand = new JumpToDefinitionCommand(this);
			UpdateWordlistIndexCommand = new UpdateWordlistIndexCommand(this);
			NextHistoryCommand = new NextHistoryCommand(this);
			PreviousHistoryCommand = new PreviousHistoryCommand(this);
		}

		#endregion

		#region INotifyPropertyChanged Implementation

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;

			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region Commands

		public ICommand GoToDefinitionCommand
		{
			get;
			private set;
		}

		public ICommand JumpToDefinitionCommand
		{
			get;
			private set;
		}

		public ICommand UpdateWordlistIndexCommand
		{
			get;
			private set;
		}

		public ICommand NextHistoryCommand
		{
			get;
			private set;
		}

		public ICommand PreviousHistoryCommand
		{
			get;
			private set;
		}

		#endregion

		#region Wordlist

		public void UpdateWordlistTopIndex()
		{
			TopIndex = Search.Prefix(CurrentWord, Wordlist);
		}

		public string CorrectWord(string word)
		{
			IEnumerable<string> candidates = SpellCheck.Candidates(word);
			return String.Join(Environment.NewLine, candidates);
		}

		#endregion

		#region definition utils

		/// <summary>
		/// Return Word object from data layer lookup
		/// </summary>
		private string GetDefinition(string wordStr)
		{
			return dictionary.Search(wordStr)?.ToString();
		}

		private void UpdateHistory(Word word)
		{
			if (word != null && word != history.Current)
				history.Add(word);
		}

		#endregion

		#region GoToDefinition

		/// <summary>
		/// Called on enter or doubleclick event on wordlist
		/// Run spellcheck for similar word when word not found
		/// </summary>
		public void GoToDefinition()
		{
			Word word = dictionary.Search(CurrentWord);

			Definition = word?.ToString()
				?? GetDefinition(Stemmer.Stem(CurrentWord))
				?? CorrectWord(CurrentWord);

			UpdateHistory(word);
		}

		public bool CanGoToDefinition()
		{
			if (string.IsNullOrEmpty(CurrentWord))
				return false;

			return true;
		}

		#endregion

		#region JumpToDefinition

		/// <summary>
		/// Called when select highlight word in definition window
		/// Stem a word when word not found instead of running spellcheck
		/// </summary>
		public void JumpToDefinition()
		{
			Word word = dictionary.Search(SelectedWord);

			string definition = word?.ToString()
				?? GetDefinition(Stemmer.Stem(SelectedWord));

			if (definition != null)
				Definition = definition;

			UpdateHistory(word);
		}

		public bool CanJumpToDefinition()
		{
			if (string.IsNullOrEmpty(CurrentWord))
				return false;

			return true;
		}

	   #endregion

		#region History

		public void NextHistory()
		{
			Word word = null;

			history.Next(ref word);
			Definition = word.ToString();
		}

		public void PreviousHistory()
		{
			Word word = null;

			history.Previous(ref word);
			Definition = word.ToString();
		}

		public bool CanGoToNextHistory()
		{
			if (history.Count == 0)
				return false;

			return !history.IsLast;
		}

		public bool CanGoToPreviousHistory()
		{
			if (history.Count == 0)
				return false;

			return !history.IsFirst;
		}

		#endregion
	}
}