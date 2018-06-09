using EDictionary.Controls;
using EDictionary.Core.ViewModels.AboutViewModel;

namespace EDictionary.Core.Views
{
	/// <summary>
	/// Interaction logic for AboutWindow.xaml
	/// </summary>
	public partial class AboutWindow : ExtendedWindow
	{
		private AboutVM viewModel;

		public AboutWindow()
		{
			InitializeComponent();

			viewModel = new AboutVM();

			DataContext = viewModel;
		}
	}
}
