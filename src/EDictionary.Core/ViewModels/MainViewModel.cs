using EDictionary.Core.Commands;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace EDictionary.Core.ViewModels
{
	public class MainViewModel : IMainViewModel, INotifyPropertyChanged
	{
		#region Fields

		private Dictionary dictionary { get; set; }
		private History<Word> history;
		private int wordListTopIndex;
		private string currentWord;
		private string highlightedWord;
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

		public int WordListTopIndex
		{
			get
			{
				return wordListTopIndex;
			}
			set
			{
				if (value != wordListTopIndex)
				{
					wordListTopIndex = value;
					NotifyPropertyChanged("WordListTopIndex");
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

		public string HighlightedWord
		{
			get
			{
				return highlightedWord;
			}
			set
			{
				if (value != highlightedWord)
				{
					highlightedWord = value;
					SearchFromHighlight();
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

		public MainViewModel()
		{
			dictionary = new Dictionary();
			history = new History<Word>();

			SpellCheck.GetVocabulary(Wordlist);

			SearchFromInputCommand = new SearchFromInputCommand(this);
			SearchFromSelectionCommand = new SearchFromSelectionCommand(this);
			SearchFromHighlightCommand = new SearchFromHighlightCommand(this);
			UpdateWordlistIndexCommand = new UpdateWordlistIndexCommand(this);

			PlayNAmEAudioCommand = new PlayNAmEAudioCommand(this);
			PlayBrEAudioCommand = new PlayBrEAudioCommand(this);

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

		public ICommand SearchFromInputCommand
		{
			get;
			private set;
		}

		public ICommand SearchFromSelectionCommand
		{
			get;
			private set;
		}

		public ICommand SearchFromHighlightCommand
		{
			get;
			private set;
		}

		public ICommand UpdateWordlistIndexCommand
		{
			get;
			private set;
		}

		public ICommand PlayNAmEAudioCommand
		{
			get;
			private set;
		}

		public ICommand PlayBrEAudioCommand
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
			WordListTopIndex = Search.Prefix(CurrentWord, Wordlist);
		}

		public string CorrectWord(string word)
		{
			IEnumerable<string> candidates = SpellCheck.Candidates(word);
			return String.Join(Environment.NewLine, candidates);
		}

		#endregion

		#region Definition Utils

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

		#region SearchFromInput

		/// <summary>
		/// Called on enter or doubleclick event on wordlist
		/// Run spellcheck for similar word when word not found
		/// </summary>
		public void SearchFromInput()
		{
			Word word = dictionary.Search(CurrentWord);

			Definition = GetDefinition(CurrentWord)
				?? GetDefinition(Stemmer.Stem(CurrentWord))
				?? CorrectWord(CurrentWord);

			UpdateHistory(word);
		}

		public bool CanSearchFromInput()
		{
			if (string.IsNullOrEmpty(CurrentWord))
				return false;

			return true;
		}

		#endregion

		#region SearchFromSelection

		/// <summary>
		/// Called when select highlight word in definition window
		/// Stem a word when word not found instead of running spellcheck
		/// </summary>
		public void SearchFromSelection()
		{
			Word word = dictionary.Search(SelectedWord);

			string definition = word?.ToString()
				?? GetDefinition(Stemmer.Stem(SelectedWord));

			if (definition != null)
				Definition = definition;

			UpdateHistory(word);
		}

		public bool CanSearchFromSelection()
		{
			if (string.IsNullOrEmpty(Definition))
				return false;

			return true;
		}

		#endregion

		#region SearchFromHighlight

		/// <summary>
		/// Called when double click highlighted word in listview
		/// Therefore, there is no need to run stemmer or spellcheck
		/// </summary>
		public void SearchFromHighlight()
		{
			Word word = dictionary.Search(HighlightedWord);

			string definition = word?.ToString();

			if (definition != null)
				Definition = definition;

			UpdateHistory(word);
		}

		public bool CanSearchFromHighlight()
		{
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

		#region PlayAudio

		public void PlayAudio(Dialect dialect)
		{
			dictionary.PlayAudio(history.Current, dialect);
		}

		public bool CanPlayAudio()
		{
			if (string.IsNullOrEmpty(Definition))
				return false;

			return true;
		}

		#endregion
	}
}
