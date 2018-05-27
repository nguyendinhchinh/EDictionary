using EDictionary.Core.Data;
using EDictionary.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace EDictionary.Core.Models
{
	[Serializable]
	public class Settings
	{
		private SettingsAccess settingsAccess;

		[XmlIgnore]
		public static readonly string Directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings");

		[XmlIgnore]
		public static readonly string Path = System.IO.Path.Combine(Directory, "settings.xml");

		[XmlIgnore]
		public static Settings Default = new Settings()
		{
			MinInterval = 20,
			SecInterval = 0,
			Option = "All",
			CustomWordList = null,
		};

		public int MinInterval;

		public int SecInterval;

		public string Option;

		public List<string> CustomWordList { get; set; }

		public Settings()
		{
			settingsAccess = new SettingsAccess();
		}

		public void SaveSettings(Settings settings)
		{
			settingsAccess.SaveSettings(settings);
		}

		public Result<Settings> LoadSettings()
		{
			return settingsAccess.LoadSettings();
		}
	}
}
