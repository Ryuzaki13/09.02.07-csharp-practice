using System;
using System.Globalization;
using System.Windows.Data;

namespace StudentManager
{
	public class BoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType == typeof(bool))
			{
				if ((bool)value)
				{
					return "*";
				}
			}
			return "";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
