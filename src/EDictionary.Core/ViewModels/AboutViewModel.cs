using EDictionary.Core.Utilities;
using EDictionary.Core.ViewModels.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace EDictionary.Core.ViewModels
{
	public class AboutViewModel : ViewModelBase, IAboutViewModel
	{
		private string licensePath;
		private string sourceCodeURL;
		private string bugReportURL;

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

		public AboutViewModel()
		{
			Authors = GetAuthors();

			License = "BSD 3-Clauses";

			licensePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LICENSE.rtf");
			sourceCodeURL = "https://github.com/NearHuscarl/E-Dictionary";
			bugReportURL = "https://github.com/NearHuscarl/E-Dictionary/issues";

			OpenSourceCodeCommand = new DelegateCommand(OpenSourceCode);
			OpenBugReportCommand = new DelegateCommand(OpenBugReport);
			OpenLicenseCommand = new DelegateCommand(OpenLicense);
		}

		private string GetAuthors()
		{
			StringBuilder builder = new StringBuilder();

			builder.AppendLine("Nguyễn Khánh Nguyên");
			builder.AppendLine("Lê Khắc Hậu Linh");
			builder.Append("Đỗ Thành Lộc");

			return builder.ToString();
		}

		public DelegateCommand OpenSourceCodeCommand { get; private set; }
		public DelegateCommand OpenBugReportCommand { get; private set; }
		public DelegateCommand OpenLicenseCommand { get; private set; }

		private void OpenSourceCode()
		{
			Process.Start(sourceCodeURL);
		}

		private void OpenBugReport()
		{
			Process.Start(bugReportURL);
		}

		private void OpenLicense()
		{
			Process.Start(licensePath);
		}
	}
}
