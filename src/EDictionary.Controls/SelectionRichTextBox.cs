using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace EDictionary.Controls
{
	public class SelectionRichTextBox : RichTextBox
	{
		public SelectionRichTextBox()
		{
			SetResourceReference(StyleProperty, typeof(RichTextBox));
		}
	}
}
