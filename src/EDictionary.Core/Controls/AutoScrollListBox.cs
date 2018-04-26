using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EDictionary.Core.Controls
{
	 public class AutoScrollListView : ListView
	 {
		  protected override void OnSelectionChanged(SelectionChangedEventArgs e)
		  {
				if (e.AddedItems != null)
				{
					 ScrollViewer scrollViewer = GetChildOfType<ScrollViewer>(this);

					 if (scrollViewer != null)
					 {
						  var selectedItem = e.AddedItems[0];

						  scrollViewer.ScrollToBottom();
						  this.ScrollIntoView(selectedItem);
					 }
				}

				base.OnSelectionChanged(e);
		  }

		  private static T GetChildOfType<T>(DependencyObject depObj) where T : DependencyObject
		  {
				if (depObj == null)
					 return null;

				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
				{
					 var child = VisualTreeHelper.GetChild(depObj, i);

					 var result = (child as T) ?? GetChildOfType<T>(child);
					 if (result != null)
						  return result;
				}
				return null;
		  }
	 }
}
