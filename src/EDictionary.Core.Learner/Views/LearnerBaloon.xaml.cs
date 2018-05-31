using EDictionary.Core.Learner.ViewModels;
using System.Windows.Controls;

namespace EDictionary.Core.Views
{
	/// <summary>
	/// Interaction logic for LearnerBaloon.xaml
	/// </summary>
	public partial class LearnerBaloon : UserControl
	{
		public LearnerBaloon()
		{
			InitializeComponent();

			var viewModel = new LearnerViewModel();

			DataContext = viewModel;
		}
	}
}
