using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Input;

namespace EDictionary.Core.ViewModels.SettingsViewModel
{
	public class SettingsViewModel : ViewModelBase, ISettingsViewModel
	{
		private bool canEditCustomWordList;
		private string option;
		private List<string> customWordList;

		public bool CanEditCustomWordList
		{
			get { return canEditCustomWordList; }
			set { SetPropertyAndNotify(ref canEditCustomWordList, value); }
		}
		public int MinInterval { get; set; }
		public int SecInterval { get; set; }

		public string Option
		{
			get { return option; }

			set
			{
				if (value == "Custom")
					CanEditCustomWordList = true;
				else
					CanEditCustomWordList = false;

				SetPropertyAndNotify(ref option, value);
			}
		}

		public List<string> CustomWordList
		{
			get { return customWordList; }
			set { SetProperty(ref customWordList, value); }
		}

		#region Actions

		public Action CloseAction { get; set; }

		#endregion

		public SettingsViewModel()
		{
			SaveCommand = new DelegateCommand(Save);
			CloseCommand = new DelegateCommand(Close);

			Load();
		}

		#region Commands

		public DelegateCommand SaveCommand { get; private set; }
		public DelegateCommand CloseCommand { get; private set; }

		#endregion

		private void Load()
		{
			LoadSettings();
		}

		private void LoadSettings()
		{
			Settings settings = new Settings();

			Result<Settings> result = settings.LoadSettings();

			if (result.Status == Status.Success)
			{
				this.MinInterval = result.Data.MinInterval;
				this.SecInterval = result.Data.SecInterval;
				this.Option = result.Data.Option;
				this.CustomWordList = result.Data.CustomWordList;
			}
		}

		private void Save()
		{
			try
			{
				LogWriter.Instance.WriteLine("Saving Settings begins");

				Settings settings = new Settings()
				{
					SecInterval = this.SecInterval,
					MinInterval = this.MinInterval,
					CustomWordList = this.CustomWordList,
					Option = this.Option,
				};

				settings.SaveSettings(settings);

				LogWriter.Instance.WriteLine("Saving Settings ends");

			}
			catch (Exception ex)
			{
				var errorMsg = new StringBuilder();

				errorMsg.AppendLine("Error occured in saving settings - SettingsViewModel.Save()");
				errorMsg.AppendLine(ex.Message);

				if (ex.InnerException != null)
				{
					errorMsg.AppendLine(ex.InnerException.Message);
				}

				LogWriter.Instance.WriteLine(errorMsg.ToString());
			}

			this.CloseAction.Invoke();
		}

		private void Close()
		{
			CloseAction.Invoke();
		}
	}
}
