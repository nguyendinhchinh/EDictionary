﻿namespace EDictionary.Core.ViewModels.SettingsViewModel
{
	public class GeneralSettingsVM : ViewModelBase, IGeneralSettingsVM
	{
		private bool runAtStartup;
		private bool isLearnerEnabled;
		private bool isDynamicEnabled;

		public bool RunAtStartup
		{
			get { return runAtStartup; }
			set { SetPropertyAndNotify(ref runAtStartup, value); }
		}

		public bool IsLearnerEnabled
		{
			get { return isLearnerEnabled; }
			set { SetPropertyAndNotify(ref isLearnerEnabled, value); }
		}

		public bool IsDynamicEnabled
		{
			get { return isDynamicEnabled; }
			set { SetPropertyAndNotify(ref isDynamicEnabled, value); }
		}
	}
}
