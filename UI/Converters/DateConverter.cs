using System.Globalization;

namespace MauiApp5.UI.Converters;

public class DateConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is DateOnly)
		{
			var date = (DateOnly) value;
			return date.ToDateTime(TimeOnly.MinValue);
		}

		return DateTime.MinValue;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is DateTime)
		{
			var date = (DateTime) value;
			return DateOnly.FromDateTime(date);
		}

		return DateOnly.MinValue;
	}
}
