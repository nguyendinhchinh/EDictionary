using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EDictionary.Core.Utilities;
using EDictionary.Core.Models;

namespace EDictionary.Core.Views
{
	public partial class Testing : Form
	{
		static private string srcPath;

		public Testing()
		{
			InitializeComponent();

			string executePath = System.AppDomain.CurrentDomain.BaseDirectory;
			srcPath = Path.GetFullPath(Path.Combine(executePath, @"..\..\..\"));
			string wordlistPath = Path.Combine(srcPath, @"EDictionary\Data\vocabulary.txt");

			SpellCheck.GetVocabulary(wordlistPath);
		}

		private void btnSpellCheck_Click(object sender, EventArgs e)
		{
			string word = txtSpellCheck.Text;
			IEnumerable<string> results = SpellCheck.Candidates(word);
			lbxSpellCheck.DataSource = results.ToList();
		}

		private void btnReadJson_Click(object sender, EventArgs e)
		{
			try
			{
				string path = Path.Combine(srcPath, String.Format(@"EDictionary\Data\words\{0}.json", txtSpellCheck.Text));
				Word word = Json.Read(path);
				/* lbxSpellCheck.DataSource = word; */
				MessageBox.Show(word.Keyword);
			}
			catch (System.IO.FileNotFoundException)
			{
				MessageBox.Show("Word not found");
			}
		}
	}
}
