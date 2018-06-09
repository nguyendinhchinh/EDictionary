using EDictionary.Core.Utilities;

namespace EDictionary.Core.ViewModels.DefinitionViewModel
{
	public interface IDefinitionVM
	{
		bool HeaderVisibility { get; set; }
		bool ResetScrollViewer { get; set; }

		DelegateCommand PlayBrEAudioCommand { get; }
		DelegateCommand PlayNAmEAudioCommand { get; }
	}
}
