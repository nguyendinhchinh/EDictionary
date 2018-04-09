using EDictionary.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EDictionary.Core.Presenters;
using EDictionary.Core.Utilities;

namespace EDictionary.Core.Views
{
	public partial class Normal : Form, IEDictionary
	{
		private EDictionaryLib eDictionaryLib;

		public Normal()
		{
			InitializeComponent();
			eDictionaryLib = new EDictionaryLib(this);
		}

#region get set
		public string WordID
		{
			get
			{
				return lbxIndex.GetItemText(lbxIndex.SelectedItem);
			}
		}

		public string Input
		{
			get
			{
				return txtSearch.Text;
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
				lbxIndex.DataSource = value;
			}
			get
			{
				return (List<string>)lbxIndex.DataSource;
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
				lbxIndex.SelectedIndex = value;
			}
		}

		public int TopIndex
		{
			get
			{
				return lbxIndex.TopIndex;
			}
			set
			{
				lbxIndex.TopIndex = value;
			}
		}

		public string Definition
		{
			set
			{
				txtDefinition.Text = value;
			}
		}
#endregion

		private void Normal_Load(object sender, EventArgs e)
		{
			eDictionaryLib.InitWordList();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			eDictionaryLib.GetDefinition(WordID);
		}

		private void txtSearch_KeyUp(object sender, KeyEventArgs e)
		{
			eDictionaryLib.UpdateWordlistCurrentIndex();
		}
	}
}
