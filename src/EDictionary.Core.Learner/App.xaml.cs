using EDictionary.Core.Learner.ViewModels;
using EDictionary.Core.Learner.Views;
using EDictionary.Core.Views;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace EDictionary.Core.Learner
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : System.Windows.Application
	{
		private TaskbarIcon taskbarIcon;
		private LearnerViewModel learnerVM;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			learnerVM = new LearnerViewModel()
			{
				ShowMainDictionaryAction = new Action(ShowMainDictionary),
				ShowSettingsWindowAction = new Action(ShowSettingsWindow),
				ShowAboutWindowAction = new Action(ShowAboutWindow),
				ShowLearnerBalloonAction = new Action(ShowLearnerBalloon),
				HideLearnerBalloonAction = new Action(HideLearnerBalloon),
				ShowDefinitionPopupAction = new Action(ShowDefinitionPopup),
			};

			taskbarIcon = (TaskbarIcon)FindResource("EDTaskbarIcon"); // See TaskbarIconView.xaml
			taskbarIcon.DataContext = learnerVM;

			learnerVM.RunAsync();
		}

		private void ShowMainDictionary()
		{
			var mainDictionary = new MainWindow();

			mainDictionary.ShowDialog();
		}

		private void ShowSettingsWindow()
		{
			var settingsWindow = new SettingsWindow();

			settingsWindow.ShowDialog();
		}

		private void ShowAboutWindow()
		{	
			var aboutWindow = new AboutWindow();

			aboutWindow.ShowDialog();
		}

		private void ShowDefinitionPopup()
		{
			MessageBox.Show("Hello world");
			//DefinitionPopup popup = new DefinitionPopup();

			//popup.ShowDialog();
		}

		private void ShowLearnerBalloon()
		{
			LearnerBalloon balloon = new LearnerBalloon();

			taskbarIcon.ShowCustomBalloon(balloon, PopupAnimation.None, timeout: null);
		}

		private void HideLearnerBalloon()
		{
			taskbarIcon.CloseBalloon();
		}

		protected override void OnExit(ExitEventArgs e)
		{
			taskbarIcon.Dispose(); // the icon would clean up automatically, but this is cleaner

			base.OnExit(e);
		}
	}
}
