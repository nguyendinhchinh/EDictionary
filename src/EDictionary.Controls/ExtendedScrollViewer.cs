using System.Windows;
using System.Windows.Controls;

namespace EDictionary.Controls
{
	public class ExtendedScrollViewer : ScrollViewer
	{
		public ExtendedScrollViewer()
		{
			SetResourceReference(StyleProperty, typeof(ScrollViewer));
			RequestBringIntoView += (object sender, RequestBringIntoViewEventArgs e) => e.Handled = true;
		}
	}
}
