using EDictionary.Core.DataLogic;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EDictionary.Core.ViewModels.SettingsViewModel
{
	public class SettingsViewModel : ViewModelBase, ISettingsViewModel
	{
		private SettingsLogic settingsLogic;

		private bool isClose = false;
		private bool canEditCustomOptions;

		private int minInterval;
		private int secInterval;

		private Option option;
		private List<string> customWordList = new List<string>();

		private bool useHistoryWordlist;
		private bool useCustomWordlist;

		private int timeout;

		public bool IsClose
		{
			get { return isClose; }
			set { SetPropertyAndNotify(ref isClose, value); }
		}

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

		public Option Option
		{
			get { return option; }

			set
			{
				if (value == Option.Custom)
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

		public SettingsViewModel()
		{
			SaveCommand = new DelegateCommand(SaveSettings);
			CloseCommand = new DelegateCommand(Close);

			settingsLogic = new SettingsLogic();

			LoadSettings();
		}

		#region Commands

		public DelegateCommand SaveCommand { get; private set; }
		public DelegateCommand CloseCommand { get; private set; }

		#endregion

		private async void LoadSettings()
		{
			Settings settings = await settingsLogic.LoadSettingsAsync();

			this.MinInterval = settings.MinInterval;
			this.SecInterval = settings.SecInterval;
			this.Option = settings.Option;
			this.CustomWordList = settings.CustomWordList;
			this.UseHistoryWordlist = settings.UseHistoryWordlist;
			this.UseCustomWordlist = settings.UseCustomWordlist;
			this.Timeout = settings.Timeout;
		}

		private void SaveSettings()
		{
			try
			{
				LogWriter.Instance.WriteLine("Saving Settings begins");

				Settings settings = new Settings()
				{
					SecInterval = this.SecInterval,
					MinInterval = this.MinInterval,
					Option = this.Option,
					CustomWordList = this.CustomWordList,
					UseHistoryWordlist = this.UseHistoryWordlist,
					UseCustomWordlist = this.UseCustomWordlist,
					Timeout = this.Timeout,
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

			Close();
		}

		private void Close()
		{
			IsClose = true;
		}
	}
}
