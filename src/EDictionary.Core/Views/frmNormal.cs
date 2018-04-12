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
	public partial class frmNormal : Form, IEDictionary
	{
		private EDictionaryLib eDictionaryLib;

		public frmNormal()
		{
			InitializeComponent();
			eDictionaryLib = new EDictionaryLib(this);
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
				rtxDefinition.Text = value;
			}
		}

		public string SelectedWord
		{
			get
			{
				return rtxDefinition.SelectedText.Trim();
			}
		}

		#endregion

		private void Normal_Load(object sender, EventArgs e)
		{
			eDictionaryLib.InitWordList();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			eDictionaryLib.GetDefinition(Input);
		}

		private void txtSearch_KeyUp(object sender, KeyEventArgs e)
		{ 
			if (e.KeyCode == Keys.Enter)
			{
				if (eDictionaryLib.IsActiveTextbox)
					eDictionaryLib.GoToDefinition(Input);
				else
					eDictionaryLib.GoToDefinition(WordList[SelectedIndex]);
			}

			else if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
			{
				eDictionaryLib.UpdateWordlistCurrentIndex();
			}

			if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
			{
				eDictionaryLib.IsActiveTextbox = false;
			}
			else
			{
				eDictionaryLib.IsActiveTextbox = true;
			}
		}

		private void lbxIndex_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void txtDefinition_TextChanged(object sender, EventArgs e)
		{

		}

		private void lbxIndex_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			eDictionaryLib.GoToDefinition(WordList[SelectedIndex]);
		}

		private void txtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up)
			{
				SelectedIndex--;
				eDictionaryLib.SelectItem(SelectedIndex);
			}
			else if (e.KeyCode == Keys.Down)
			{
				SelectedIndex++;
				eDictionaryLib.SelectItem(SelectedIndex);
			}
		}

		private void rtxDefinition_DoubleClick(object sender, EventArgs e)
		{
			eDictionaryLib.GoToDefinition(SelectedWord);
		}

		private void btnNextHistory_Click(object sender, EventArgs e)
		{
			eDictionaryLib.NextHistory();
		}

		private void btnPrevHistory_Click(object sender, EventArgs e)
		{
			eDictionaryLib.PreviousHistory();
		}
	}
}
