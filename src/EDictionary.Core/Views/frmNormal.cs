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
using EDictionary.Core.Extensions;

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
				lbxIndex.SelectedIndex = value.Clamp(0, WordList.Count - 1);
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
				return rtxDefinition.SelectedText.Trim().ToLower();
			}
		}

		public bool IsNextHistoryEnabled
		{
			get
			{
				return btnNextHistory.Enabled;
			}
			set
			{
				btnNextHistory.Enabled = value;
			}
		}

		public bool IsPrevHistoryEnabled
		{
			get
			{
				return btnPrevHistory.Enabled;
			}
			set
			{
				btnPrevHistory.Enabled = value;
			}
		}

		#endregion

		private void Normal_Load(object sender, EventArgs e)
		{
			eDictionaryLib.InitWordList();
		}

		private void txtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (eDictionaryLib.IsActiveTextbox)
					eDictionaryLib.GoToDefinition(Input);
				else
					eDictionaryLib.GoToDefinition(WordList[SelectedIndex]);

				e.SuppressKeyPress = true; // suppress beeping sound when enter
			}

			if (e.KeyCode == Keys.Up)
			{
				eDictionaryLib.SelectItem(--SelectedIndex);
			}
			else if (e.KeyCode == Keys.Down)
			{
				eDictionaryLib.SelectItem(++SelectedIndex);
			}
		}

		private void txtSearch_KeyUp(object sender, KeyEventArgs e)
		{ 
			if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
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

		private void btnSearch_Click(object sender, EventArgs e)
		{
			eDictionaryLib.GoToDefinition(Input);
		}

		private void lbxIndex_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			eDictionaryLib.GoToDefinition(WordList[SelectedIndex]);
		}

		private void rtxDefinition_DoubleClick(object sender, EventArgs e)
		{
			eDictionaryLib.JumpToDefinition(SelectedWord);
		}

		private void btnNextHistory_Click(object sender, EventArgs e)
		{
			eDictionaryLib.NextHistory();
		}

		private void btnPrevHistory_Click(object sender, EventArgs e)
		{
			eDictionaryLib.PreviousHistory();
		}

		private void btnAmerica_Click(object sender, EventArgs e)
		{
			eDictionaryLib.PlayAmericaAudio();
		}

		private void btnBritian_Click(object sender, EventArgs e)
		{
			eDictionaryLib.PlayBritianAudio();
		}
	}
}
