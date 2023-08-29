using System.Globalization;

namespace MAUIBrowser.Converters
{
    public class IsVisibleButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not int count)
                return false;
            if (count <= 0)
                return false;
            else return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
