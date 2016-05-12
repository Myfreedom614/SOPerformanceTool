using System;
using Windows.UI.Xaml.Data;

namespace SOPerformanceTool.Converters
{
    public class DoubleToPercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                double num = (double)value;
                return String.Format("{0:P2}", num);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
