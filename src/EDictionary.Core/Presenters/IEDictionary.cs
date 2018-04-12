using EDictionary.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.Presenters
{
	public interface IEDictionary
	{
		string Input { get; set; }
		string Definition { set; }
        string SelectedWord { get; }

		int TopIndex { get; set; }
		int SelectedIndex { get; set; }
   
		List<string> WordList { get; set; }
	}
}
