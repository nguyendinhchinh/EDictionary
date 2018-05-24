using EDictionary.Core.ViewModels;
using System.Windows;

namespace EDictionary.Core.Views
{
	/// <summary>
	/// Interaction logic for frmPopUp.xaml
	/// </summary>
	public partial class PopupWindow : Window
	{
		public PopupWindow()
		{
			InitializeComponent();

			var viewModel = new PopupViewModel();

			DataContext = viewModel;
		}
	}
}
