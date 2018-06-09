using EDictionary.Core.DataLogic;
using EDictionary.Core.Extensions;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using EDictionary.Core.ViewModels;
using EDictionary.Core.ViewModels.DefinitionViewModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace EDictionary.Core.Learner.ViewModels
{
	public enum Status
	{
		Stop,
		Pause,
		Run,
	}

	public class LearnerVM : ViewModelBase, ILearnerVM
	{
		#region Fields

		private SettingsLogic settingsLogic;
		private WordLogic wordLogic;

		private DefinitionVM definitionVM;

		private Status status;
		private Status nextStatus;
		private string definition;

		private TimeSpan spawnInterval;
		private TimeSpan activeInterval;

		private int spawnCounter;
		private int activeCounter;

		private DispatcherTimer spawnTimer;
		private DispatcherTimer activeTimer;

		private List<string> wordList;

		#endregion

		#region Properties

		public DefinitionVM DefinitionVM
		{
			get { return definitionVM; }
			protected set { SetPropertyAndNotify(ref definitionVM, value); }
		}

		public Status NextStatus
		{
			get { return nextStatus; }
			set { SetPropertyAndNotify(ref nextStatus, value); }
		}

		public string Definition
		{
			get { return definition; }
			set { SetPropertyAndNotify(ref definition, value); }
		}

		#endregion

		#region Actions

		public Action ShowMainDictionaryAction { get; set; }
		public Action ShowSettingsWindowAction { get; set; }
		public Action ShowAboutWindowAction { get; set; }
		public Action ShowLearnerBalloonAction { get; set; }
		public Action HideLearnerBalloonAction { get; set; }

		public void OpenMainDictionary() => ShowMainDictionaryAction.Invoke();
		public void OpenSettings()
		{
			ShowSettingsWindowAction.Invoke();
			ReloadSettings();
		}
		public void OpenAbout() => ShowAboutWindowAction.Invoke();
		public void OpenLearnerBalloon() => DispatchIfNecessary(ShowLearnerBalloonAction);
		public void CloseLearnerBalloon() => DispatchIfNecessary(HideLearnerBalloonAction);
		public void CloseApp() => Application.Current.Shutdown();

		#endregion

		#region Constructor

		public LearnerVM()
		{
			wordList = new List<string>();

			DefinitionVM = new DefinitionVM();

			settingsLogic = new SettingsLogic();
			wordLogic = new WordLogic();

			spawnTimer = new DispatcherTimer();
			spawnTimer.Tick += OnSpawnTimerTick;
			spawnTimer.Interval = new TimeSpan(0, 0, 1);

			activeTimer = new DispatcherTimer();
			activeTimer.Tick += OnActiveTimerTick;
			activeTimer.Interval = new TimeSpan(0, 0, 1);

			OpenMainDictionaryCommand = new DelegateCommand(OpenMainDictionary);
			ToggleActiveCommand = new DelegateCommand(ToggleActive);
			OpenSettingsCommand = new DelegateCommand(OpenSettings);
			OpenAboutCommand = new DelegateCommand(OpenAbout);
			ExitAppCommand = new DelegateCommand(CloseApp);
			OpenLearnerBalloonCommand = new DelegateCommand(OpenLearnerBalloon);
		}

		#endregion

		#region Commands

		public DelegateCommand OpenMainDictionaryCommand { get; set; }
		public DelegateCommand ToggleActiveCommand { get; set; }
		public DelegateCommand OpenSettingsCommand { get; set; }
		public DelegateCommand OpenAboutCommand { get; set; }
		public DelegateCommand ExitAppCommand { get; set; }
		public DelegateCommand OpenLearnerBalloonCommand { get; set; }

		#endregion

		private void ReloadSettings()
		{
			Settings settings = settingsLogic.LoadSettings();

			spawnInterval = TimeSpan.FromMinutes(settings.MinInterval);
			spawnInterval = TimeSpan.FromSeconds(settings.SecInterval);
			activeInterval = TimeSpan.FromSeconds(settings.Timeout);

			spawnCounter = (int)spawnInterval.TotalSeconds;
			activeCounter = (int)activeInterval.TotalSeconds;

			if (settings.Option == Option.Full)
			{
				wordList = wordLogic.WordList;
			}
			else if (settings.Option == Option.Custom)
			{
				wordList = settings.CustomWordList;
			}
		}

		private void SetRandomWord()
		{
			var randWord = wordList.PickRandom();

			Word word = wordLogic.Search(randWord);

			DefinitionVM.Word = word;
			DefinitionVM.Definition = word.ToDisplayedString();
		}

		private void OnSpawnTimerTick(object sender, EventArgs e)
		{
			if (status == Status.Stop)
				return;

			spawnCounter--;

			if (spawnCounter <= 0)
			{
				SetRandomWord();
				OpenLearnerBalloon();

				spawnCounter = (int)spawnInterval.TotalSeconds;

				spawnTimer.Stop();
				activeTimer.Start();
			}
		}

		private void OnActiveTimerTick(object sender, EventArgs e)
		{
			if (status == Status.Stop)
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

		public void Run()
		{
			status = Status.Run;

			ReloadSettings();

			if (wordList.Count == 0)
				return;

			spawnTimer.Start();
		}

		private void ToggleActive()
		{
			if (status == Status.Stop)
			{
				status = Status.Run;
				NextStatus = Status.Stop;
			}
			else
			{
				status = Status.Stop;
				NextStatus = Status.Run;
			}
		}
	}
}
