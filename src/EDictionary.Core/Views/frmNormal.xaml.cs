using EDictionary.Core.ViewModels;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace EDictionary.Core.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class frmNormal 
	{
		public frmNormal()
		{
			InitializeComponent();
			DataContext = new MainViewModel();
		}

		private void Grid_Loaded(object sender, RoutedEventArgs e)
		{
			//var range = new TextRange(rtxDefinition.Document.ContentStart, rtxDefinition.Document.ContentEnd);
			//range.Load("hæt æksɪdənt", DataFormats.Rtf);
		}
	}
}
