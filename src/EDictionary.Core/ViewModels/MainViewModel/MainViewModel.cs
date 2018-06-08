using EDictionary.Core.DataLogic;
using EDictionary.Core.Models;
using EDictionary.Core.Models.WordComponents;
using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace EDictionary.Core.ViewModels.MainViewModel
{
	public class MainViewModel : ViewModelBase, IMainViewModel
	{
		#region Fields

		private WordLogic wordLogic;
		private HistoryLogic historyLogic;
		private History<string> history;

		private bool isTextBoxFocus;
		private bool headerVisibility;
		private bool resetScrollViewer;

		private int wordListTopIndex;
		private Word currentWord;
		private string searchedWord = "";
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
			set { SetPropertyAndNotify(ref isTextBoxFocus, value); }
		}

		/// <summary>
		/// true -> Visibility.Visible
		/// false -> Visibility.Collapsed
		/// </summary>
		public bool HeaderVisibility
		{
			get { return headerVisibility; }
			set { SetPropertyAndNotify(ref headerVisibility, value); }
		}

		public bool ResetScrollViewer
		{
			get { return resetScrollViewer; }
			set { SetPropertyAndNotify(ref resetScrollViewer, value); }
		}

		public List<string> WordList { get; set; }

		public int WordListTopIndex
		{
			get { return wordListTopIndex; }
			set { SetPropertyAndNotify(ref wordListTopIndex, value); }
		}

		public Word CurrentWord
		{
			get { return currentWord; }
			set { SetPropertyAndNotify(ref currentWord, value); }
		}

		public string SearchedWord
		{
			get { return searchedWord; }

			set
			{
				if (SetPropertyAndNotify(ref searchedWord, value))
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
					SearchedWord = HighlightedWord;
					IsTextBoxFocus = true;
				}
			}
		}

		public string SelectedWord
		{
			get { return selectedWord.ToLower(); }
			set { SetProperty(ref selectedWord, value); }
		}

		public string Definition
		{
			get { return definition; }

			set
			{
				if (SetPropertyAndNotify(ref definition, value))
				{
					ResetScrollViewer = true;
					SearchFromSelectionCommand.RaiseCanExecuteChanged();
					PlayBrEAudioCommand.RaiseCanExecuteChanged();
					PlayNAmEAudioCommand.RaiseCanExecuteChanged();
				}
			}
		}

		public List<string> OtherResults
		{
			get { return otherResultNameToID.Keys.ToList(); }
		}

		public string HighlightedOtherResult
		{
			get { return highlightedOtherResult; }
			set { SetProperty(ref highlightedOtherResult, value); }
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

			otherResultNameToID = new Dictionary<string, string>();

			LoadCommands();
			LoadWordlistAndHistory();

			SearchIcon = "SearchIcon";

			watch.Print("init viewmodel");
		}

		private void LoadCommands()
		{
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

			CloseCommand = new DelegateCommand(OnCloseWindow);
		}

		private void LoadWordlistAndHistory()
		{
			wordLogic = new WordLogic();
			WordList = wordLogic.WordList;

			historyLogic = new HistoryLogic();
			history = historyLogic.LoadHistory<string>();

			Word word;
			if (history.Count > 0)
				word = wordLogic.Search(history.Current);
			else
				word = wordLogic.Search(WordList.FirstOrDefault());

			if (word != null)
				ShowDefinition(word);

			NotifyHistoryChange();
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
		public DelegateCommand CloseCommand { get; private set; }

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
			await Task.Run(() => WordListTopIndex = Search.Prefix(SearchedWord, WordList));
		}

		private void ShowDefinition(Word word)
		{
			HeaderVisibility = true;
			CurrentWord = word;
			Definition = word.ToDisplayedString();
		}

		private string CorrectWord(string word)
		{
			HeaderVisibility = false;
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
			Console.WriteLine(">>> " + SearchedWord);
			Watcher watch = new Watcher();

			Word word = wordLogic.Search(SearchedWord);

			watch.Print("Search");

			if (word == null)
			{
				var stemmedWord = Stemmer.Stem(SearchedWord);

				if (SearchedWord != stemmedWord)
					word = wordLogic.Search(stemmedWord);
			}
			watch.Print("Stem");

			if (word != null)
			{
				ShowDefinition(word);

				watch.Print("Update Definition");
			}
			else
				Definition = CorrectWord(SearchedWord);

			UpdateHistory(word);

			watch.Print("Update History - Finish");
		}

		public bool CanSearchFromInput()
		{
			if (string.IsNullOrEmpty(SearchedWord))
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

				if (SearchedWord != stemmedWord)
					word = wordLogic.Search(stemmedWord);
			}

			if (word != null)
				ShowDefinition(word);

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
				ShowDefinition(word);

			UpdateHistory(word);
		}

		#endregion

		#region History

		private async void UpdateHistory(Word word)
		{
			await Task.Run(() =>
			{
				if (word == null)
					return;

				if (word.Name != history.Current)
					history.Add(word.Name);

				UpdateOtherResultList();

				Application.Current.Dispatcher.Invoke(() => NotifyHistoryChange());
			});
		}

		private void NotifyHistoryChange()
		{
			PreviousHistoryCommand.RaiseCanExecuteChanged();
			NextHistoryCommand.RaiseCanExecuteChanged();
		}

		public void NextHistory()
		{
			Word word = wordLogic.Search(history.Next());
			ShowDefinition(word);

			NotifyHistoryChange();
		}

		public void PreviousHistory()
		{
			Word word = wordLogic.Search(history.Previous());
			ShowDefinition(word);

			NotifyHistoryChange();
		}

		public bool CanGoToNextHistory()
		{
			if (history == null || history.Count == 0)
				return false;

			return !history.IsLast;
		}

		public bool CanGoToPreviousHistory()
		{
			if (history == null || history.Count == 0)
				return false;

			return !history.IsFirst;
		}

		#endregion

		#region PlayAudio

		public void PlayBrEAudio()
		{
			CurrentWord.PlayAudio(Dialect.BrE);
		}

		public void PlayNAmEAudio()
		{
			CurrentWord.PlayAudio(Dialect.NAmE);
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
			if (CurrentWord.Similars == null)
				return;

			otherResultNameToID.Clear();

			foreach (var similarWord in CurrentWord.Similars)
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
				ShowDefinition(word);

			UpdateHistory(word);
		}

		#endregion

		private void OnCloseWindow()
		{
			historyLogic.SaveHistory(history);
		}
	}
}
