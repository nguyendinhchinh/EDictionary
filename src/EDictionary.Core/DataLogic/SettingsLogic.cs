﻿using EDictionary.Core.Data;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using System.Threading.Tasks;

namespace EDictionary.Core.DataLogic
{
	public class SettingsLogic
   {
		private SettingsAccess settingsAccess;

		public SettingsLogic()
		{
			settingsAccess = new SettingsAccess();
		}

		public void SaveSettings(Settings settings)
		{
			settingsAccess.SaveSettings(settings);
		}

		public Settings LoadSettings()
		{
			var result = settingsAccess.LoadSettings();

			if (result.Status == Status.Success)
			{
				return result.Data;
			}

			return Settings.Default;
		}

		public async Task<Settings> LoadSettingsAsync()
		{
			return await Task.Run(() => this.LoadSettings());
		}
	}
}
