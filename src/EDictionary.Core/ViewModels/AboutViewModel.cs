using EDictionary.Core.Utilities;
using System;
using System.Reflection;
using System.Text;

namespace EDictionary.Core.ViewModels
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
		public string Authors { get; set; }
		public string License { get; set; }
		public string SourceCodeURL { get; set; }

		public AboutViewModel()
		{
			Authors = GetAuthors();

			License = "BSD 3-Clauses";

			SourceCodeURL = "https://github.com/NearHuscarl/E-Dictionary";

			OpenSourceCodeCommand = new DelegateCommand(OpenSourceCode);
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

		private void OpenSourceCode()
		{
			System.Diagnostics.Process.Start(SourceCodeURL);
		}
	}
}
