using System.Collections.Generic;

namespace EDictionary.Core.ViewModels.Interfaces
{
	public interface IDynamicSettingsViewModel
	{
		List<string> ModifierKeys { get; }
	}
}
