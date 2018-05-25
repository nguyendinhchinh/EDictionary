using EDictionary.Core.ViewModels.MainViewModel;
using System;
using System.Windows;

namespace EDictionary.Core.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
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
