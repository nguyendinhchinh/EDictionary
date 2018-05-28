﻿namespace EDictionary.Core.ViewModels.AboutViewModel
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