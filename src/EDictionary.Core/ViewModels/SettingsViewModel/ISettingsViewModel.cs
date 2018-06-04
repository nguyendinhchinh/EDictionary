using EDictionary.Core.Models;
using System.Collections.Generic;

namespace EDictionary.Core.ViewModels.SettingsViewModel
{
	public interface ISettingsViewModel
	{
		bool IsClose { set; }
		bool CanEditCustomOptions { get; set; }

		int MinInterval { get; set; }
		int SecInterval { get; set; }

		Option Option { get; set; }
		List<string> CustomWordList { get; set; }

		bool UseHistoryWordlist { get; set; }
		bool UseCustomWordlist { get; set; }
	}
}
