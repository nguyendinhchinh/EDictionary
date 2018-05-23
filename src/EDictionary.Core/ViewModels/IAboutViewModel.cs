namespace EDictionary.Core.ViewModels
{
	public interface IAboutViewModel
	{
		string Version { get; }
		string Authors { get; set; }
		string License { get; set; }
		string SourceCodeURL { get; set; }
	}
}
