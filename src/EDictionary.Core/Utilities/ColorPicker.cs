using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EDictionary.Core.Utilities
{
	/// <summary>
	/// Read color from Resource Dictionary based on resource key
	/// </summary>
	public static class ColorPicker
	{
		private static Uri colorUri = new Uri("pack://application:,,,/EDictionary.Theme;component/Styles/Colors.xaml", UriKind.RelativeOrAbsolute);
		private static ResourceDictionary colorDict = new ResourceDictionary()
		{
			Source = colorUri,
		};

		public static Color Color(string key)
		{
			System.Windows.Media.SolidColorBrush brush = (System.Windows.Media.SolidColorBrush)colorDict[key];

			return System.Drawing.Color.FromArgb(brush.Color.A, brush.Color.R, brush.Color.G, brush.Color.B);
		}
	}
}
