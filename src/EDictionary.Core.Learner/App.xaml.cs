using EDictionary.Core.Learner.ViewModels;
using EDictionary.Core.Learner.Views;
using EDictionary.Core.Views;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace EDictionary.Core.Learner
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		/// <summary>
		/// This is not a window class so we cannot use .OpenDialog() to make sure
		/// only one instance of window is opened at a time, therefore we'll have
		/// to keep track of the window manually
		/// </summary>
		private AboutWindow aboutWindow;
		private SettingsWindow settingsWindow;

		private TaskbarIcon taskbarIcon;
		private LearnerViewModel learnerVM;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			learnerVM = new LearnerViewModel()
			{
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

		private void ShowWindowInNewThread<T>() where T : Window, new()
		{
			Thread thread = new Thread(() =>
			{
				var window = new T();

				window.ShowDialog();
				window.Closed += (sender, e) => window.Dispatcher.BeginInvokeShutdown(System.Windows.Threading.DispatcherPriority.Background);
				System.Windows.Threading.Dispatcher.Run();
			});

			thread.IsBackground = true;
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
		}

		private void ShowSettingsWindow()
		{
			if (settingsWindow != null)
			{
				settingsWindow.Focus();
				return;
			}

			settingsWindow = new SettingsWindow();
			settingsWindow.Closed += (s, e) => settingsWindow = null;
			settingsWindow.ShowDialog();
		}

		private void ShowAboutWindow()
		{
			if (aboutWindow != null)
			{
				aboutWindow.Focus();
				return;
			}

			aboutWindow = new AboutWindow();
			aboutWindow.Closed += (s, e) => aboutWindow = null;
			aboutWindow.ShowDialog();
		}

		private void ShowDefinitionPopup()
		{
			DefinitionPopup popup = new DefinitionPopup();

			popup.ShowDialog();
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
