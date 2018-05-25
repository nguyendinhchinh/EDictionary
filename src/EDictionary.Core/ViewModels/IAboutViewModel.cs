namespace EDictionary.Core.ViewModels
{
	public interface IAboutViewModel
	{
		string Version { get; }
		string Authors { get; }
		string License { get; }
		string SourceCodeURL { get; }
		string BugReportURL { get; }
	}
}
