using EDictionary.Core.ViewModels.AboutViewModel;
using System.Windows;

namespace EDictionary.Core.Views
{
	/// <summary>
	/// Interaction logic for AboutWindow.xaml
	/// </summary>
	public partial class AboutWindow : Window
	{
		private AboutViewModel viewModel;

		public AboutWindow()
		{
			InitializeComponent();

			viewModel = new AboutViewModel();

			DataContext = viewModel;
		}
	}
}
