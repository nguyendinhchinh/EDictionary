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
	public class TopIndexListView : ListView
	{
		// https://www.wpftutorial.net/DependencyProperties.html

		public static readonly DependencyProperty TopIndexProperty = DependencyProperty.Register(
				"TopIndex",
				typeof(int),
				typeof(TopIndexListView),
				new FrameworkPropertyMetadata(-1,
					OnTopIndexChangedProperty,
					OnCoerceTopIndexProperty)
				);

		public int TopIndex
		{
			get
			{
				return (int)GetValue(TopIndexProperty);
			}
			set
			{
				SetValue(TopIndexProperty, value);
			}
		}

		private static void OnTopIndexChangedProperty(DependencyObject source, DependencyPropertyChangedEventArgs e)
		{
			TopIndexListView listView = source as TopIndexListView;

			if (listView.Items.Count == 0)
			{
				return;
			}

			ScrollViewer scrollViewer = GetChildOfType<ScrollViewer>(listView);

			if (scrollViewer != null)
			{
				scrollViewer.ScrollToBottom();
				listView.ScrollIntoView(listView.Items[listView.TopIndex]);
			}
		}

		private static object OnCoerceTopIndexProperty(DependencyObject sender, object data)
		{
			TopIndexListView listView = sender as TopIndexListView;
			int topIndex = (int)data;

			if (topIndex > listView.Items.Count - 1)
				return listView.Items.Count - 1;

			if (topIndex < 0)
				return 0;

			return topIndex;
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
