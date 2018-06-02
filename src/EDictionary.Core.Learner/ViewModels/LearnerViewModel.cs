using EDictionary.Core.DataLogic;
using EDictionary.Core.Extensions;
using EDictionary.Core.Models;
using EDictionary.Core.ViewModels;
using System;
using System.Collections.Generic;

namespace EDictionary.Core.Learner.ViewModels
{
	public class LearnerViewModel : ViewModelBase, ILearnerViewModel
	{
		#region Fields

		private SettingsLogic settingsLogic;
		private WordLogic wordLogic;

		private string definition;
		private TimeSpan interval;
		private List<string> wordList;

		#endregion

		#region Properties

		public string Definition
		{
			get { return definition; }
			set { SetPropertyAndNotify(ref definition, value); }
		}

		#endregion

		#region Constructor

		public LearnerViewModel()
		{
			wordList = new List<string>();

			settingsLogic = new SettingsLogic();
			wordLogic = new WordLogic();

			ReloadSettings();

			if (wordList.Count != 0)
				ShowRandomWord();
		}

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

		private void ShowRandomWord()
		{
			var randWord = wordList.PickRandom();

			Definition = wordLogic.Search(randWord).ToRTFString(mini: true);
		}
	}
}
