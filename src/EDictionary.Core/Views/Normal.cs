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

		public string WordID
		{
			get
			{
				return lbIndex.GetItemText(lbIndex.SelectedItem);
			}
		}

		public List<string> WordList
		{
			set
			{
				lbIndex.DataSource = value;
			}
		}

		public string Definition
		{
			set
			{
				txtDefinition.Text = value;
			}
		}

		private void Normal_Load(object sender, EventArgs e)
		{
			eDictionaryLib.InitWordList();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			eDictionaryLib.GetDefinition(WordID);
		}
	}
}
