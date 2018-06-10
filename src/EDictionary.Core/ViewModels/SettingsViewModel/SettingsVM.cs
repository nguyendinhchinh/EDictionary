using EDictionary.Core.DataLogic;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EDictionary.Core.ViewModels.SettingsViewModel
{
	public class SettingsVM : ViewModelBase, ISettingsVM
	{
		private SettingsLogic settingsLogic;

		private GeneralSettingsVM generalSettingsVM;
		private LearnerSettingsVM learnerSettingsVM;
		private DynamicSettingsVM dynamicSettingsVM;

		private bool isClose = false;

		public bool IsClose
		{
			get { return isClose; }
			set { SetPropertyAndNotify(ref isClose, value); }
		}

		public GeneralSettingsVM GeneralSettingsVM
		{
			get { return generalSettingsVM; }
			set { SetPropertyAndNotify(ref generalSettingsVM, value); }
		}

		public LearnerSettingsVM LearnerSettingsVM
		{
			get { return learnerSettingsVM; }
			set { SetPropertyAndNotify(ref learnerSettingsVM, value); }

		}

		public DynamicSettingsVM DynamicSettingsVM
		{
			get { return dynamicSettingsVM; }
			set { SetPropertyAndNotify(ref dynamicSettingsVM, value); }
		}

		#region Constructor

		public SettingsVM()
		{
			InitChildViewModels();

			SaveCommand = new DelegateCommand(SaveSettings);
			ApplyCommand = new DelegateCommand(ApplySettings);
		}

		private async void InitChildViewModels()
		{
			settingsLogic = new SettingsLogic();

			Settings settings = await settingsLogic.LoadSettingsAsync();

			GeneralSettingsVM = new GeneralSettingsVM()
			{
				RunAtStartup = settings.RunAtStartup,
				IsLearnerEnabled = settings.IsLearnerEnabled,
				IsDynamicEnabled = settings.IsDynamicEnabled,
			};

			LearnerSettingsVM = new LearnerSettingsVM()
			{
				MinInterval = settings.MinInterval,
				SecInterval = settings.SecInterval,
				Timeout = settings.Timeout,
				Option = settings.Option,
				CustomWordList = settings.CustomWordList,
				UseHistoryWordlist = settings.UseHistoryWordlist,
				UseCustomWordlist = settings.UseCustomWordlist,
			};

			DynamicSettingsVM = new DynamicSettingsVM();
		}

		#endregion

		#region Commands

		public DelegateCommand SaveCommand { get; private set; }
		public DelegateCommand ApplyCommand { get; private set; }

		#endregion

		private void ApplySettings()
		{
			try
			{
				LogWriter.Instance.WriteLine("Saving Settings begins");

				Settings settings = new Settings()
				{
					RunAtStartup = GeneralSettingsVM.RunAtStartup,
					IsLearnerEnabled = GeneralSettingsVM.IsLearnerEnabled,
					IsDynamicEnabled = GeneralSettingsVM.IsDynamicEnabled,

					SecInterval = LearnerSettingsVM.SecInterval,
					MinInterval = LearnerSettingsVM.MinInterval,
					Option = LearnerSettingsVM.Option,
					CustomWordList = LearnerSettingsVM.CustomWordList,
					UseHistoryWordlist = LearnerSettingsVM.UseHistoryWordlist,
					UseCustomWordlist = LearnerSettingsVM.UseCustomWordlist,
					Timeout = LearnerSettingsVM.Timeout,
				};

				settingsLogic.SaveSettings(settings);

				LogWriter.Instance.WriteLine("Saving Settings ends");

			}
			catch (Exception ex)
			{
				var errorMsg = new StringBuilder();

				errorMsg.AppendLine("Error occured in saving settings - SettingsViewModel.SaveSettings()");
				errorMsg.AppendLine(ex.Message);

				if (ex.InnerException != null)
				{
					errorMsg.AppendLine(ex.InnerException.Message);
				}

				LogWriter.Instance.WriteLine(errorMsg.ToString());
			}
		}

		private void SaveSettings()
		{
			ApplySettings();
			Close();
		}

		private void Close()
		{
			IsClose = true;
		}
	}
}
