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
		string WordID { get; }
		string Definition { set; }
		List<string> WordList { set; }
		// List<string> Candidates { set; }
	}
}
