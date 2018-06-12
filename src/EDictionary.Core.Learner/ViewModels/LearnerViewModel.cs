using EDictionary.Core.DataLogic;
using EDictionary.Core.Extensions;
using EDictionary.Core.Learner.Extensions;
using EDictionary.Core.Learner.Utilities;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using EDictionary.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace EDictionary.Core.Learner.ViewModels
{
	public enum Status
	{
		Stop,
		Pause,
		Run,
	}

	public class LearnerViewModel : ViewModelBase, ILearnerViewModel
	{
		#region Fields

		private readonly Random random;

		private SettingsLogic settingsLogic;
		private HistoryLogic historyLogic;
		private WordLogic wordLogic;

		private DefinitionViewModel definitionVM;

		private Status prevStatus;
		private Status currentStatus;
		private Status nextStatus;

		private string statusIcon;

		private TimeSpan spawnInterval;
		private TimeSpan activeInterval;

		private int spawnCounter;
		private int activeCounter;

		private DispatcherTimer spawnTimer;
		private DispatcherTimer activeTimer;

		private VocabularySource option;

		private bool useCustomWordlist;
		private bool useHistoryWordlist;

		private List<string> wordList;
		private List<string> historyWordlist;


		private GlobalKeyboardHook keyboardHook;

		private Modifiers modifierShortcut;
		private Keys keyShortcut;

		#endregion

		#region Properties

		public DefinitionViewModel DefinitionVM
		{
			get { return definitionVM; }
			protected set { SetPropertyAndNotify(ref definitionVM, value); }
		}

		public Status CurrentStatus
		{
			get { return currentStatus; }

			private set
			{
				prevStatus = CurrentStatus;

				if (value == Status.Run)
					NextStatus = Status.Stop;
				if (value == Status.Stop)
					NextStatus = Status.Run;

				SetProperty(ref currentStatus, value);
			}
		}

		public Status NextStatus
		{
			get { return nextStatus; }

			private set
			{
				if (value == Status.Run)
					StatusIcon = "ToggleOnIcon";
				else if (value == Status.Stop)
					StatusIcon = "ToggleOffIcon";

				SetPropertyAndNotify(ref nextStatus, value);
			}
		}

		public string StatusIcon
		{
			get { return statusIcon; }
			set { SetPropertyAndNotify(ref statusIcon, value); }
		}

		#endregion

		#region Actions

		public Action ShowMainDictionaryAction { get; set; }
		public Action ShowSettingsWindowAction { get; set; }
		public Action ShowAboutWindowAction { get; set; }
		public Action ShowLearnerBalloonAction { get; set; }
		public Action HideLearnerBalloonAction { get; set; }
		public Action ShowDefinitionPopupAction { get; set; }

		public void OpenMainDictionary() => ShowMainDictionaryAction.Invoke();
		public void OpenSettings()
		{
			ShowSettingsWindowAction.Invoke();
			ReloadSettings();
		}
		public void OpenAbout() => ShowAboutWindowAction.Invoke();
		public void CloseApp() => System.Windows.Application.Current.Shutdown();
		public void OpenLearnerBalloon() => DispatchIfNecessary(ShowLearnerBalloonAction);
		public void CloseLearnerBalloon() => DispatchIfNecessary(HideLearnerBalloonAction);
		public void OpenDefinitionPopup() => DispatchIfNecessary(ShowDefinitionPopupAction);

		#endregion

		#region Constructor

		public LearnerViewModel()
		{
			random = new Random();

			settingsLogic = new SettingsLogic();
			historyLogic = new HistoryLogic();
			wordLogic = new WordLogic();

			InitLearner();
			InitDynamic();

			LoadCommands();
		}

		private void InitLearner()
		{
			wordList = new List<string>();
			historyWordlist = new List<string>();

			DefinitionVM = new DefinitionViewModel();

			spawnTimer = new DispatcherTimer();
			spawnTimer.Tick += OnSpawnTimerTick;
			spawnTimer.Interval = new TimeSpan(hours: 0, minutes: 0, seconds: 1);

			activeTimer = new DispatcherTimer();
			activeTimer.Tick += OnActiveTimerTick;
			activeTimer.Interval = new TimeSpan(hours: 0, minutes: 0, seconds: 1);
		}

		private void InitDynamic()
		{
			keyboardHook = new GlobalKeyboardHook();

			keyboardHook.KeyPressedCallback += (keyInfo) =>
			{
				if (keyInfo.KeysHold.Contains((Keys)modifierShortcut) && keyInfo.KeyData == keyShortcut)
					OpenDefinitionPopup();
			};

			keyboardHook.Hook();
		}

		private void LoadCommands()
		{
			OpenMainDictionaryCommand = new DelegateCommand(OpenMainDictionary);
			ToggleActiveCommand = new DelegateCommand(ToggleActive);
			OpenSettingsCommand = new DelegateCommand(OpenSettings);
			OpenAboutCommand = new DelegateCommand(OpenAbout);
			ExitAppCommand = new DelegateCommand(CloseApp);

			OpenLearnerBalloonCommand = new DelegateCommand(OpenLearnerBalloon);
			CloseLearnerBalloonCommand = new DelegateCommand(CloseLearnerBalloon);
			OpenDefinitionPopupCommand = new DelegateCommand(OpenDefinitionPopup);

			PauseCommand = new DelegateCommand(() => CurrentStatus = Status.Pause);
			ContinueCommand = new DelegateCommand(() => CurrentStatus = prevStatus);
		}

		#endregion

		#region Commands

		public DelegateCommand OpenMainDictionaryCommand { get; private set; }
		public DelegateCommand ToggleActiveCommand { get; private set; }
		public DelegateCommand OpenSettingsCommand { get; private set; }
		public DelegateCommand OpenAboutCommand { get; private set; }
		public DelegateCommand ExitAppCommand { get; private set; }
		public DelegateCommand OpenLearnerBalloonCommand { get; private set; }
		public DelegateCommand CloseLearnerBalloonCommand { get; private set; }
		public DelegateCommand OpenDefinitionPopupCommand { get; private set; }
		public DelegateCommand PauseCommand { get; private set; }
		public DelegateCommand ContinueCommand { get; private set; }

		#endregion

		private void ReloadSettings()
		{
			Settings settings = settingsLogic.LoadSettings();

			CurrentStatus = settings.IsLearnerEnabled ? Status.Run : Status.Stop;

			spawnInterval = TimeSpan.FromMinutes(settings.MinInterval);
			spawnInterval = TimeSpan.FromSeconds(settings.SecInterval);
			activeInterval = TimeSpan.FromSeconds(settings.Timeout);

			spawnCounter = (int)spawnInterval.TotalSeconds;
			activeCounter = (int)activeInterval.TotalSeconds;

			useCustomWordlist = settings.UseCustomWordlist;
			useHistoryWordlist = settings.UseHistoryWordlist;

			option = settings.VocabularySource;

			if (option == VocabularySource.Full)
			{
				wordList = wordLogic.WordList;
			}
			else if (option == VocabularySource.Custom)
			{
				wordList = settings.CustomWordList.Distinct().ToList();

				if (useHistoryWordlist)
				{
					historyWordlist = historyLogic.GetCollection<string>().Distinct().ToList();
				}
			}

			modifierShortcut = settings.ModifierShortcut.ToModifier();
			keyShortcut = settings.KeyShortcut.ToKey();

			AddKeyToHook(modifierShortcut, keyShortcut);
		}

		private void AddKeyToHook(Modifiers modifier, Keys key)
		{
			keyboardHook.hookedKeys.Clear();
			keyboardHook.hookedKeys.Add((Keys)modifier);
			keyboardHook.hookedKeys.Add(key);

			//foreach (Keys keyy in Enum.GetValues(typeof(Keys)))
			//{
			//	keyboardHook.hookedKeys.Add(keyy);
			//}
		}

		private string GetRandomWordFromWordLists(params List<string>[] wordListArray)
		{
			var wordLists = new List<List<string>>(wordListArray);

			// Remove any empty wordlists
			foreach (var list in wordLists.ToList())
			{
				if (!list.Any())
					wordLists.Remove(list);
			}

			if (!wordLists.Any())
				return null;

			List<string> wordList = new List<List<string>>(wordLists).PickRandom();

			return wordList.PickRandom();
		}

		/// <summary>
		/// Return true on success, false on failure (Word list is empty)
		/// </summary>
		/// <returns></returns>
		private bool SetRandomWord()
		{
			string randWord = null;

			if (option == VocabularySource.Full)
			{
				randWord = GetRandomWordFromWordLists(wordList);
			}
			else if (option == VocabularySource.Custom)
			{
				if (useCustomWordlist && !useHistoryWordlist)
					randWord = GetRandomWordFromWordLists(wordList);

				else if (useHistoryWordlist && !useCustomWordlist)
					randWord = GetRandomWordFromWordLists(historyWordlist);

				else // (useCustomWordlist && useHistoryWordlist)
					randWord = GetRandomWordFromWordLists(wordList, historyWordlist);
			}

			if (randWord == null)
				return false;

			Word word = wordLogic.Search(randWord);

			DefinitionVM.Word = word;
			DefinitionVM.Definition = word.ToDisplayedString();

			return true;
		}

		private void OnSpawnTimerTick(object sender, EventArgs e)
		{
			if (CurrentStatus == Status.Stop)
				return;

			spawnCounter--;

			if (spawnCounter <= 0)
			{
				if (SetRandomWord())
					OpenLearnerBalloon();

				spawnCounter = (int)spawnInterval.TotalSeconds;

				spawnTimer.Stop();
				activeTimer.Start();
			}
		}

		private void OnActiveTimerTick(object sender, EventArgs e)
		{
			if (CurrentStatus == Status.Pause)
				return;

			activeCounter--;

			if (activeCounter <= 0)
			{
				CloseLearnerBalloon();

				activeCounter = (int)activeInterval.TotalSeconds;

				activeTimer.Stop();
				spawnTimer.Start();
			}
		}

		private void ToggleActive()
		{
			if (NextStatus == Status.Run)
			{
				CurrentStatus = Status.Run;
			}
			else
			{
				CurrentStatus = Status.Stop;
			}
		}

		public async void RunAsync()
		{
			await Task.Run(() =>
			{
				CurrentStatus = Status.Run;

				ReloadSettings();

				spawnTimer.Start();
			});
		}
	}
}
