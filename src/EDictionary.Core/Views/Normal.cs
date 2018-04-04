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
		public Normal()
		{
			InitializeComponent();
		}

        private void grpIndex_Enter(object sender, EventArgs e)
        {

        }

        private void Normal_Load(object sender, EventArgs e)
        {

        }

        private string WordID;
        string wordID
        {
            get
            {
                return txtSearch.Text;
            }
        }
    }
}
