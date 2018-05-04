using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.ViewModels
{
	public interface IMainViewModel
	{
		double WindowMinimumHeight { get; set; }
		double WindowMinimumWidth { get; set; }

		List<string> Wordlist { get; }
		int WordListTopIndex { get; set; }

		string CurrentWord { get; set; }
		string HighlightedWord { get; set; }
		string SelectedWord { get; set; }
		string Definition { get; set; }
	}
}
