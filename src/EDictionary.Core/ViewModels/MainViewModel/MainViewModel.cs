using EDictionary.Core.DataLogic;
using EDictionary.Core.Models;
using EDictionary.Core.Models.WordComponents;
using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace EDictionary.Core.ViewModels.MainViewModel
{
	public class MainViewModel : ViewModelBase, IMainViewModel
	{
		#region Fields

		private WordLogic wordLogic;
		private History<Word> history;
		private bool isTextBoxFocus;
		private int wordListTopIndex;
		private string currentWord = "";
		private string highlightedWord;
		private string selectedWord;
		private string definition;
		private Dictionary<string, string> otherResultNameToID;
		private string highlightedOtherResult;
		private object searchIcon;

		#endregion

		#region Properties

		public double WindowMinimumHeight { get; set; } = 350;

		public double WindowMinimumWidth { get; set; } = 450;

		public bool IsTextBoxFocus
		{
			get { return isTextBoxFocus; }

			set
			{
				SetPropertyAndNotify(ref isTextBoxFocus, value);
			}
		}

		public List<string> WordList { get; set; }

		public int WordListTopIndex
		{
			get
			{
				return wordListTopIndex;
			}
			set
			{
				SetPropertyAndNotify(ref wordListTopIndex, value);
			}
		}

		public string CurrentWord
		{
			get { return currentWord; }

			set
			{
				if (SetPropertyAndNotify(ref currentWord, value))
				{
					SearchFromInputCommand.RaiseCanExecuteChanged();
				}
			}
		}

		public string HighlightedWord
		{
			get { return highlightedWord; }

			set
			{
				if (SetProperty(ref highlightedWord, value))
				{
					CurrentWord = HighlightedWord;
					IsTextBoxFocus = true;
				}
			}
		}

		public string SelectedWord
		{
			get { return selectedWord.ToLower(); }

			set
			{
				SetProperty(ref selectedWord, value);
			}
		}

		public string Definition
		{
			get { return definition; }

			set
			{
				if (SetPropertyAndNotify(ref definition, value))
				{
					SearchFromSelectionCommand.RaiseCanExecuteChanged();
					PlayBrEAudioCommand.RaiseCanExecuteChanged();
					PlayNAmEAudioCommand.RaiseCanExecuteChanged();
				}
			}
		}

		public List<string> OtherResults
		{
			get
			{
				return otherResultNameToID.Keys.ToList();
			}
		}

		public string HighlightedOtherResult
		{
			get
			{
				return highlightedOtherResult;
			}
			set
			{
				SetProperty(ref highlightedOtherResult, value);
			}
		}

		public object SearchIcon
		{
			get { return searchIcon; }
			set
			{
				App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Send,
					new Action(() => SetPropertyAndNotify(ref searchIcon, value)
				));
				
			}
		}

		#endregion

		#region Actions

		public Action ShowSettingsWindowAction { get; set; }
		public Action ShowAboutWindowAction { get; set; }

		#endregion

		#region Constructor

		public MainViewModel()
		{
			var watch = new Watcher();

			Task.Run(() =>
			{
				wordLogic = new WordLogic();
				WordList = wordLogic.WordList;
				NotifyPropertyChanged("WordList");
			});

			history = new History<Word>();
			otherResultNameToID = new Dictionary<string, string>();

			SearchFromInputCommand = new DelegateCommand(SearchFromInput, CanSearchFromInput);
			SearchFromSelectionCommand = new DelegateCommand(SearchFromSelection, CanSearchFromSelection);
			SearchFromHighlightCommand = new DelegateCommand(SearchFromHighlight);
			UpdateWordlistTopIndexCommand = new DelegateCommand(UpdateWordlistTopIndex);

			PlayNAmEAudioCommand = new DelegateCommand(PlayNAmEAudio, CanPlayAudio);
			PlayBrEAudioCommand = new DelegateCommand(PlayBrEAudio, CanPlayAudio);

			NextHistoryCommand = new DelegateCommand(NextHistory, CanGoToNextHistory);
			PreviousHistoryCommand = new DelegateCommand(PreviousHistory, CanGoToPreviousHistory);

			SearchHighlightedOtherResultCommand = new DelegateCommand(SearchHighlightedOtherResult, CanSearchHighlightedOtherResult);

			OpenSettingCommand = new DelegateCommand(OpenSettings);
			OpenAboutCommand = new DelegateCommand(OpenAbout);

			SearchIcon = "SearchIcon";

			watch.Print("init viewmodel");
		}

		#endregion

		#region Commands

		public DelegateCommand SearchFromInputCommand { get; private set; }
		public DelegateCommand SearchFromSelectionCommand { get; private set; }
		public DelegateCommand SearchFromHighlightCommand { get; private set; }
		public DelegateCommand UpdateWordlistTopIndexCommand { get; private set; }
		public DelegateCommand PlayNAmEAudioCommand { get; private set; }
		public DelegateCommand PlayBrEAudioCommand { get; private set; }
		public DelegateCommand NextHistoryCommand { get; private set; }
		public DelegateCommand SearchHighlightedOtherResultCommand { get; private set; }
		public DelegateCommand PreviousHistoryCommand { get; private set; }
		public DelegateCommand OpenSettingCommand { get; private set; }
		public DelegateCommand OpenAboutCommand { get; private set; }

		#endregion

		#region Open Windows

		private void OpenSettings()
		{
			ShowSettingsWindowAction.Invoke();
		}

		private void OpenAbout()
		{
			ShowAboutWindowAction.Invoke();
		}

		#endregion

		#region Wordlist

		public async void UpdateWordlistTopIndex()
		{
			await Task.Run(() => WordListTopIndex = Search.Prefix(CurrentWord, WordList));
		}

		public string CorrectWord(string word)
		{
			return wordLogic.GetSuggestions(word);
		}

		#endregion

		#region SearchFromInput

		/// <summary>
		/// Called on enter or doubleclick event on wordlist
		/// Run spellcheck for similar word when word not found
		/// </summary>
		public void SearchFromInput()
		{
			//SearchIcon = "SpinnerIcon";
			//NotifyPropertyChanged("SearchIcon");
			Console.WriteLine();
			Console.WriteLine(">>> " + CurrentWord);
			Watcher watch = new Watcher();

			Word word = wordLogic.Search(CurrentWord);

			watch.Print("Search");

			if (word == null)
			{
				var stemmedWord = Stemmer.Stem(CurrentWord);

				if (CurrentWord != stemmedWord)
					word = wordLogic.Search(stemmedWord);
			}
			watch.Print("Stem");

			if (word != null)
			{
				string str = word.ToRTFString();

				watch.Print("To RFT");

				Definition = str;

				watch.Print("Update Definition");
			}
			else
				Definition = CorrectWord(CurrentWord);

			UpdateHistory(word);

			watch.Print("Update History");
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
			Word word = wordLogic.Search(SelectedWord);

			if (word == null)
			{
				var stemmedWord = Stemmer.Stem(SelectedWord);

				if (CurrentWord != stemmedWord)
					word = wordLogic.Search(stemmedWord);
			}

			if (word != null)
				Definition = word.ToRTFString();

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
			Word word = wordLogic.Search(HighlightedWord);

			if (word != null)
				Definition = word.ToRTFString();

			UpdateHistory(word);
		}

		#endregion

		#region History

		private void UpdateHistory(Word word)
		{
			if (word == null)
				return;

			if (word != history.Current)
				history.Add(word);

			UpdateOtherResultList();

			NextHistoryCommand.RaiseCanExecuteChanged();
			PreviousHistoryCommand.RaiseCanExecuteChanged();
		}

		public void NextHistory()
		{
			Word word = null;

			history.Next(ref word);
			Definition = word.ToRTFString();

			PreviousHistoryCommand.RaiseCanExecuteChanged();
			NextHistoryCommand.RaiseCanExecuteChanged();
		}

		public void PreviousHistory()
		{
			Word word = null;

			history.Previous(ref word);
			Definition = word.ToRTFString();

			PreviousHistoryCommand.RaiseCanExecuteChanged();
			NextHistoryCommand.RaiseCanExecuteChanged();
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

		public void PlayBrEAudio()
		{
			wordLogic.PlayAudio(history.Current, Dialect.BrE);
		}

		public void PlayNAmEAudio()
		{
			wordLogic.PlayAudio(history.Current, Dialect.NAmE);
		}

		public bool CanPlayAudio()
		{
			if (string.IsNullOrEmpty(Definition))
				return false;

			return true;
		}

		#endregion

		#region OtherResult

		public void UpdateOtherResultList()
		{
			Word word = history.Current;

			if (word.Similars == null)
				return;

			otherResultNameToID.Clear();

			foreach (var similarWord in word.Similars)
			{
				otherResultNameToID.Add(similarWord.Replace('_', ' '), similarWord);
			}
			NotifyPropertyChanged("OtherResults");
		}

		public bool CanSearchHighlightedOtherResult()
		{
			if (HighlightedOtherResult == null)
				return false;

			return true;
		}

		public void SearchHighlightedOtherResult()
		{
			Word word = wordLogic.SearchID(otherResultNameToID[HighlightedOtherResult]);

			if (word != null)
				Definition = word.ToRTFString();

			UpdateHistory(word);
		}

		#endregion
	}
}
