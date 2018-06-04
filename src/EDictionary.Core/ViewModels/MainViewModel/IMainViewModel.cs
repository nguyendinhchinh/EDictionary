using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDictionary.Core.ViewModels.MainViewModel
{
	public interface IMainViewModel
	{
		double WindowMinimumHeight { get; set; }
		double WindowMinimumWidth { get; set; }

		bool IsTextBoxFocus { get; set; }

		List<string> WordList { get; }
		int WordListTopIndex { get; set; }

		string SearchedWord { get; set; }
		string HighlightedWord { get; set; }
		string SelectedWord { get; set; }
		string Definition { get; set; }

		List<string> OtherResults { get; }
		string HighlightedOtherResult { get; set; }
	}
}
