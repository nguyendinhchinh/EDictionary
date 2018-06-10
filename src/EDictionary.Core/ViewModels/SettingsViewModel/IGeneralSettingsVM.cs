namespace EDictionary.Core.ViewModels.SettingsViewModel
{
	public interface IGeneralSettingsVM
	{
		bool RunAtStartup { get; set; }
		bool IsLearnerEnabled { get; set; }
		bool IsDynamicEnabled { get; set; }
	}
}
