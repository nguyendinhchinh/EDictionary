using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EDictionary.Core.Models
{
	public enum VocabularySource
	{
		Full,
		Custom,
	}

	[Serializable]
	public class Settings
	{
		[XmlIgnore]
		public static readonly string Directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");

		[XmlIgnore]
		public static readonly string Path = System.IO.Path.Combine(Directory, "settings.xml");

		[XmlIgnore]
		public static Settings Default = new Settings()
		{
			RunAtStartup = true,
			IsLearnerEnabled = true,
			IsDynamicEnabled = true,

			MinInterval = 20,
			SecInterval = 0,
			VocabularySource = VocabularySource.Full,
			CustomWordList = new List<string>(),
			UseHistoryWordlist = true,
			UseCustomWordlist = false,
			Timeout = 12,

			ModifierShortcut = "LControl",
			KeyShortcut = "Z",
		};

		public bool RunAtStartup { get; set; }
		public bool IsLearnerEnabled { get; set; }
		public bool IsDynamicEnabled { get; set; }

		public int MinInterval { get; set; }
		public int SecInterval { get; set; }

		public VocabularySource VocabularySource { get; set; }

		public List<string> CustomWordList { get; set; }

		public bool UseHistoryWordlist { get; set; }
		public bool UseCustomWordlist { get; set; }

		public int Timeout { get; set; }

		public string KeyShortcut { get; set; } = "";
		public string ModifierShortcut { get; set; } = "";

		public Settings()
		{
			CustomWordList = new List<string>();
		}
	}
}
