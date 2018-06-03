using EDictionary.Core.DataLogic;
using EDictionary.Core.Extensions;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using EDictionary.Core.ViewModels;
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
		Resume,
	}

	public class LearnerViewModel : ViewModelBase, ILearnerViewModel
	{
		#region Fields

		private SettingsLogic settingsLogic;
		private WordLogic wordLogic;

		private Status nextStatus;
		private string definition;
		private TimeSpan interval;
		private List<string> wordList;

		#endregion

		#region Properties

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

		private void DispatchIfNecessary(Action action)
		{
			if (Application.Current.Dispatcher.CheckAccess())
			{
				action.Invoke();
			}
			else
			{
				Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
				{
					action.Invoke();
				}));
			}
		}

		public void OpenMainDictionary() => ShowMainDictionaryAction.Invoke();
		public void OpenSettings() => ShowSettingsWindowAction.Invoke();
		public void OpenAbout() => ShowAboutWindowAction.Invoke();
		public void OpenLearnerBalloon() => DispatchIfNecessary(ShowLearnerBalloonAction);
		public void CloseApp() => Application.Current.Shutdown();

		#endregion

		#region Constructor

		public LearnerViewModel()
		{
			wordList = new List<string>();

			settingsLogic = new SettingsLogic();
			wordLogic = new WordLogic();

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

			interval = TimeSpan.FromMinutes(settings.MinInterval);
			interval = TimeSpan.FromSeconds(settings.SecInterval);

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

			Definition = wordLogic.Search(randWord).ToRTFString(mini: true);
		}

		public void Run()
		{
			ReloadSettings();
			interval = TimeSpan.FromSeconds(5);

			if (wordList.Count == 0)
				return;

			while (true)
			{
				Thread.Sleep(interval);

				SetRandomWord();
				OpenLearnerBalloon();
			}
		}

		private void ToggleActive()
		{
			if (NextStatus == Status.Stop)
			{
				NextStatus = Status.Resume;
			}
			else
			{
				NextStatus = Status.Stop;
			}
		}
	}
}
