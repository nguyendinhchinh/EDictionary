using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EDictionary.Core.Models
{
	public enum Option
	{
		Full,
		Custom,
	}

	[Serializable]
	public class Settings
	{
		[XmlIgnore]
		public static readonly string Directory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings");

		[XmlIgnore]
		public static readonly string Path = System.IO.Path.Combine(Directory, "settings.xml");

		[XmlIgnore]
		public static Settings Default = new Settings()
		{
			MinInterval = 20,
			SecInterval = 0,
			Option = Option.Full,
			CustomWordList = new List<string>(),
		};

		public int MinInterval;

		public int SecInterval;

		public Option Option;

		public List<string> CustomWordList { get; set; }

		public Settings()
		{
			CustomWordList = new List<string>();
		}
	}
}
