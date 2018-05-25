using EDictionary.Core.Utilities;
using System;
using System.Reflection;
using System.Text;

namespace EDictionary.Core.ViewModels.AboutViewModel
{
	public class AboutViewModel : ViewModelBase, IAboutViewModel
	{
		public string Version
		{
			get
			{
				Version version = Assembly.GetExecutingAssembly().GetName().Version;
				return $"{version.Major}.{version.Minor}.{version.Build}";
			}
		}
		public string Authors { get; private set; }
		public string License { get; private set; }
		public string SourceCodeURL { get; private set; }
		public string BugReportURL { get; private set; }

		public AboutViewModel()
		{
			Authors = GetAuthors();

			License = "BSD 3-Clauses";

			SourceCodeURL = "https://github.com/NearHuscarl/E-Dictionary";
			BugReportURL = "https://github.com/NearHuscarl/E-Dictionary/issues";

			OpenSourceCodeCommand = new DelegateCommand(OpenSourceCode);
			OpenBugReportCommand = new DelegateCommand(OpenBugReport);
		}

		private string GetAuthors()
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendLine("Nguyễn Khánh Nguyên");
			builder.AppendLine("Lê Khắc Hậu Linh");
			builder.AppendLine("Đỗ Thành Lộc");

			return builder.ToString();
		}

		public DelegateCommand OpenSourceCodeCommand { get; private set; }
		public DelegateCommand OpenBugReportCommand { get; private set; }

		private void OpenSourceCode()
		{
			System.Diagnostics.Process.Start(SourceCodeURL);
		}

		private void OpenBugReport()
		{
			System.Diagnostics.Process.Start(BugReportURL);
		}
	}
}
