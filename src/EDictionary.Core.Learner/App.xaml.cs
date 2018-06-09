using EDictionary.Core.Learner.ViewModels;
using EDictionary.Core.Learner.Views;
using EDictionary.Core.Views;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace EDictionary.Core.Learner
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private TaskbarIcon learnerNotifyIcon;
		private LearnerVM viewModel;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			//create the notifyicon (it's a resource declared in LearnerNotifyIcon.xaml
			learnerNotifyIcon = (TaskbarIcon)FindResource("LearnerNotifyIcon");

			viewModel = new LearnerVM()
			{
				ShowMainDictionaryAction = new Action(ShowMainDictionary),
				ShowSettingsWindowAction = new Action(ShowSettingsWindow),
				ShowAboutWindowAction = new Action(ShowAboutWindow),
				ShowLearnerBalloonAction = new Action(ShowLearnerBalloon),
				HideLearnerBalloonAction = new Action(HideLearnerBalloon),
			};

			learnerNotifyIcon.DataContext = viewModel;

			Task.Run(() => viewModel.Run());
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

		private void ShowLearnerBalloon()
		{
			LearnerBaloon balloon = new LearnerBaloon();

			learnerNotifyIcon.ShowCustomBalloon(balloon, PopupAnimation.None, null);
		}

		private void HideLearnerBalloon()
		{
			learnerNotifyIcon.CloseBalloon();
		}

		protected override void OnExit(ExitEventArgs e)
		{
			learnerNotifyIcon.Dispose(); // the icon would clean up automatically, but this is cleaner
			base.OnExit(e);
		}
	}
}
