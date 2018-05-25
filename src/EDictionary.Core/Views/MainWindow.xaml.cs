using EDictionary.Core.ViewModels.MainViewModel;
using System;

namespace EDictionary.Core.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow 
	{
		private MainViewModel viewModel;

		public MainWindow()
		{
			InitializeComponent();

			viewModel = new MainViewModel();

			DataContext = viewModel;

			viewModel.ShowSettingsWindowAction = new Action(this.ShowSettingsWindow);
			viewModel.ShowAboutWindowAction = new Action(this.ShowAboutWindow);
		}

		private void ShowSettingsWindow()
		{
			var settings = new SettingsWindow();
			settings.ShowDialog();
		}

		private void ShowAboutWindow()
		{
			var about = new AboutWindow();
			about.ShowDialog();
		}
	}
}
