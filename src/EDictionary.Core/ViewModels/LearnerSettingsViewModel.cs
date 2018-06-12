using EDictionary.Core.Models;
using EDictionary.Core.ViewModels.Interfaces;
using System.Collections.Generic;

namespace EDictionary.Core.ViewModels
{
	public class LearnerSettingsViewModel : ViewModelBase, ILearnerSettingsViewModel
	{
		private bool canEditCustomOptions;

		private int minInterval;
		private int secInterval;

		private int timeout;

		private VocabularySource option;
		private List<string> customWordList = new List<string>();

		private bool useHistoryWordlist;
		private bool useCustomWordlist;

		public bool CanEditCustomOptions
		{
			get { return canEditCustomOptions; }
			set { SetPropertyAndNotify(ref canEditCustomOptions, value); }
		}

		public int MinInterval
		{
			get { return minInterval; }
			set { SetPropertyAndNotify(ref minInterval, value); }
		}

		public int SecInterval
		{
			get { return secInterval; }
			set { SetPropertyAndNotify(ref secInterval, value); }
		}

		public int Timeout
		{
			get { return timeout; }
			set { SetPropertyAndNotify(ref timeout, value); }
		}

		public VocabularySource Option
		{
			get { return option; }

			set
			{
				if (value == VocabularySource.Custom)
					CanEditCustomOptions = true;
				else
					CanEditCustomOptions = false;

				SetPropertyAndNotify(ref option, value);
			}
		}

		public List<string> CustomWordList
		{
			get { return customWordList; }
			set { SetPropertyAndNotify(ref customWordList, value); }
		}

		public bool UseHistoryWordlist
		{
			get { return useHistoryWordlist; }
			set { SetPropertyAndNotify(ref useHistoryWordlist, value); }
		}

		public bool UseCustomWordlist
		{
			get { return useCustomWordlist; }
			set { SetPropertyAndNotify(ref useCustomWordlist, value); }
		}
	}
}
