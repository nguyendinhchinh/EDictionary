using EDictionary.Core.Learner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
		}
		
		//private void Button_Click(object sender, RoutedEventArgs e)
		//{
		//	LearnerBaloon balloon = new LearnerBaloon();

		//	EDictionaryLearner.ShowCustomBalloon(balloon, System.Windows.Controls.Primitives.PopupAnimation.Fade, 5000);
		//}
	}
}
