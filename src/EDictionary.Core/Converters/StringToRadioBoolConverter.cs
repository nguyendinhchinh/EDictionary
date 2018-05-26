using System;
using System.Globalization;
using System.Windows.Data;

namespace EDictionary.Core.Converters
{
	public class StringToRadioBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string option = (string)value;

			if (option == parameter.ToString())
				return true;
			
			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return parameter;
		}
	}
}
