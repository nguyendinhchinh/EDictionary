using EDictionary.Core.Learner.ViewModels;
using EDictionary.Core.Learner.Views;
using System;
using System.Windows;

namespace EDictionary.Core.Learner
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private LearnerViewModel viewModel;

		public MainWindow()
		{
			InitializeComponent();

			viewModel = new LearnerViewModel
			{
				ShowLearnerBalloonAction = new Action(ShowLearnerBalloon)
			};

			DataContext = viewModel;
		}

		private void ShowLearnerBalloon()
		{
			LearnerBaloon balloon = new LearnerBaloon();

			EDictionaryLearner.ShowCustomBalloon(balloon, System.Windows.Controls.Primitives.PopupAnimation.Fade, 5000);
		}
	}
}
