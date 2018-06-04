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
		private Word currentWord;

		private bool isTextBoxFocus;
		private int wordListTopIndex;
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

			LoadWordlistAndHistory();

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

		private async void LoadWordlistAndHistory()
		{
			await Task.Run(() =>
			{
				wordLogic = new WordLogic();
				WordList = wordLogic.WordList;
				NotifyPropertyChanged("WordList");

				historyLogic = new HistoryLogic();
				history = historyLogic.LoadHistory<string>();

				if (history.Count > 0)
					currentWord = wordLogic.Search(history.Current);

				Application.Current.Dispatcher.Invoke(() =>
				{
					Definition = currentWord.ToRTFString();

					NextHistoryCommand.RaiseCanExecuteChanged();
					PreviousHistoryCommand.RaiseCanExecuteChanged();
				});
			});
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
			await Task.Run(() => WordListTopIndex = Search.Prefix(SearchedWord, WordList));
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
			Console.WriteLine(">>> " + SearchedWord);
			Watcher watch = new Watcher();

			currentWord = wordLogic.Search(SearchedWord);

			watch.Print("Search");

			if (currentWord == null)
			{
				var stemmedWord = Stemmer.Stem(SearchedWord);

				if (SearchedWord != stemmedWord)
					currentWord = wordLogic.Search(stemmedWord);
			}
			watch.Print("Stem");

			if (currentWord != null)
			{
				string str = currentWord.ToRTFString();

				watch.Print("To RFT");

				Definition = str;

				watch.Print("Update Definition");
			}
			else
				Definition = CorrectWord(SearchedWord);

			UpdateHistory(currentWord);

			watch.Print("Update History");
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
			currentWord = wordLogic.Search(SelectedWord);

			if (currentWord == null)
			{
				var stemmedWord = Stemmer.Stem(SelectedWord);

				if (SearchedWord != stemmedWord)
					currentWord = wordLogic.Search(stemmedWord);
			}

			if (currentWord != null)
				Definition = currentWord.ToRTFString();

			UpdateHistory(currentWord);
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
			currentWord = wordLogic.Search(HighlightedWord);

			if (currentWord != null)
				Definition = currentWord.ToRTFString();

			UpdateHistory(currentWord);
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

				historyLogic.SaveHistory(history);
				UpdateOtherResultList();

				Application.Current.Dispatcher.Invoke(() =>
				{
					NextHistoryCommand.RaiseCanExecuteChanged();
					PreviousHistoryCommand.RaiseCanExecuteChanged();
				});
			});
		}

		public void NextHistory()
		{
			history.Next(out string word);
			currentWord = wordLogic.Search(word);
			Definition = currentWord.ToRTFString();

			PreviousHistoryCommand.RaiseCanExecuteChanged();
			NextHistoryCommand.RaiseCanExecuteChanged();

			historyLogic.SaveHistory(history);
		}

		public void PreviousHistory()
		{
			history.Previous(out string word);
			currentWord = wordLogic.Search(word);
			Definition = currentWord.ToRTFString();

			PreviousHistoryCommand.RaiseCanExecuteChanged();
			NextHistoryCommand.RaiseCanExecuteChanged();

			historyLogic.SaveHistory(history);
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
			wordLogic.PlayAudio(currentWord, Dialect.BrE);
		}

		public void PlayNAmEAudio()
		{
			wordLogic.PlayAudio(currentWord, Dialect.NAmE);
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
			if (currentWord.Similars == null)
				return;

			otherResultNameToID.Clear();

			foreach (var similarWord in currentWord.Similars)
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
