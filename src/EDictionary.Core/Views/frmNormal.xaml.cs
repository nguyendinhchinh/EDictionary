using EDictionary.Core.Extensions;
using EDictionary.Core.Presenters;
using EDictionary.Core.ViewModels;
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

namespace EDictionary.Core.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class frmNormal : Window, IEDictionary
	{
		private EDictionaryLib eDictionaryLib;

		public frmNormal()
		{
			InitializeComponent();
			DataContext = new EDictionaryViewModel();
		}

		#region get set

		public string Input
		{
			get
			{
				return txtSearch.Text.ToLower();
			}
			set
			{
				txtSearch.Text = value;
			}
		}
		public List<string> WordList
		{
			set
			{
				lbxIndex.ItemsSource = value;
			}
			get
			{
				return (List<string>)lbxIndex.ItemsSource;
			}
		}

		public int SelectedIndex
		{
			get
			{
				return lbxIndex.SelectedIndex;
			}
			set
			{
				lbxIndex.SelectedIndex = value.Clamp(0, WordList.Count - 1);
			}
		}

		//public int TopIndex
		//{
		//	get
		//	{
		//		return lbxIndex.TopIndex;
		//	}
		//	set
		//	{
		//		lbxIndex.TopIndex = value;
		//	}
		//}

		public string Definition
		{
			set
			{
				rtxDefinition.Document.Blocks.Clear();
				rtxDefinition.Document.Blocks.Add(new Paragraph(new Run(value)));
			}
		}

		public string SelectedWord
		{
			get
			{
				return rtxDefinition.Selection.Text.Trim().ToLower();
			}
		}

		public bool IsNextHistoryEnabled
		{
			get
			{
				return btnNextHistory.IsEnabled;
			}
			set
			{
				btnNextHistory.IsEnabled = value;
			}
		}

		public bool IsPrevHistoryEnabled
		{
			get
			{
				return btnPrevHistory.IsEnabled;
			}
			set
			{
				btnPrevHistory.IsEnabled = value;
			}
		}

		#endregion
	}
}
