using EDictionary.Core.ViewModels;
using System.Windows;

namespace EDictionary.Core.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class frmNormal : Window
	{
		public frmNormal()
		{
			InitializeComponent();
			DataContext = new EDictionaryViewModel();
		}
	}
}
