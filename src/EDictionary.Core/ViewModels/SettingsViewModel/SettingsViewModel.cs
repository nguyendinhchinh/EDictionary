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

		private bool canEditCustomWordList;
		private Option option;
		private List<string> customWordList = new List<string>();

		public bool CanEditCustomWordList
		{
			get { return canEditCustomWordList; }
			set { SetPropertyAndNotify(ref canEditCustomWordList, value); }
		}
		public int MinInterval { get; set; }
		public int SecInterval { get; set; }

		public Option Option
		{
			get { return option; }

			set
			{
				if (value == Option.Custom)
					CanEditCustomWordList = true;
				else
					CanEditCustomWordList = false;

				SetPropertyAndNotify(ref option, value);
			}
		}

		public List<string> CustomWordList
		{
			get { return customWordList; }
			set { SetPropertyAndNotify(ref customWordList, value); }
		}

		#region Actions

		public Action CloseAction { get; set; }

		#endregion

		public SettingsViewModel()
		{
			SaveCommand = new DelegateCommand(SaveAndReload);
			CloseCommand = new DelegateCommand(Close);

			settingsLogic = new SettingsLogic();

			LoadSettingsAsync();
		}

		#region Commands

		public DelegateCommand SaveCommand { get; private set; }
		public DelegateCommand CloseCommand { get; private set; }

		#endregion

		private async void LoadSettingsAsync()
		{
			Settings settings = await settingsLogic.LoadSettingsAsync();

			this.MinInterval = settings.MinInterval;
			this.SecInterval = settings.SecInterval;
			this.Option = settings.Option;
			this.CustomWordList = settings.CustomWordList;
		}

		private void SaveAndReload()
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

				settingsLogic.SaveSettings(settings);

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
