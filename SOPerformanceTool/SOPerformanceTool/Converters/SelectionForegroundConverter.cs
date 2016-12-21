using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SOPerformanceTool.Converters
{
    /// <summary>
    /// This class represents the SelectionForegroundConverter
    /// </summary>
    class SelectionForegroundConverter : IValueConverter
    {
        /// <summary>
        /// Convert visibility to Foreground color
        /// </summary>
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && (Visibility)value == Visibility.Visible)
                return 1;
            return 0.68;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
