﻿using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using System;
using System.IO;
using System.Xml.Serialization;

namespace EDictionary.Core.Data
{
	public class SettingsAccess
	{
		public Result<Settings> LoadSettings()
		{
			try
			{
				Settings settings = new Settings();

				using (FileStream stream = new FileStream(Settings.Path, FileMode.Open))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(Settings));
					settings = serializer.Deserialize(stream) as Settings;
				}

				return new Result<Settings>(settings, "Loading successful", "", Status.Success);
			}
			catch (Exception ex)
			{
				return new Result<Settings>(Settings.Default, "Error occured on loading settings", "", Status.Error, ex);
			}
		}

		public Result SaveSettings(Settings settings)
		{
			try
			{
				if (!Directory.Exists(Settings.Directory))
					Directory.CreateDirectory(Settings.Directory);

				using (FileStream stream = new FileStream(Settings.Path, FileMode.Create))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(Settings));
					serializer.Serialize(stream, settings);
				}

				return new Result(Status.Success);
			}
			catch (Exception ex)
			{
				return new Result("Error occured on saving settings", Status.Error, ex);
			}
		}
	}
}