using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace EDictionary.Core.Converters
{
	public class ListToStringConverter : IValueConverter
   {
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return String.Join(Environment.NewLine, ((List<string>)value).ToArray());
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string str = (string)value;

			return str.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
		}
	}
}
