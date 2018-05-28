using EDictionary.Core.Models;
using System.Collections.Generic;

namespace EDictionary.Core.ViewModels.SettingsViewModel
{
	public interface ISettingsViewModel
	{
		bool CanEditCustomWordList { get; set; }
		int MinInterval { get; set; }
		int SecInterval { get; set; }
		Option Option { get; set; }
		List<string> CustomWordList { get; set; }
	}
}
