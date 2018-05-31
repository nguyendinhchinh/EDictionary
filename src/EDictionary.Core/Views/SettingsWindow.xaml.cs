using EDictionary.Controls;
using EDictionary.Core.ViewModels.SettingsViewModel;
using System;

namespace EDictionary.Core.Views
{
	/// <summary>
	/// Interaction logic for SettingWindow.xaml
	/// </summary>
	public partial class SettingsWindow : ExtendedWindow
	{
		private SettingsViewModel viewModel;

		public SettingsWindow()
		{
			InitializeComponent();

			viewModel = new SettingsViewModel();

			DataContext = viewModel;

			viewModel.CloseAction = new Action(this.Close);
		}
	}
}
