using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace ExRatesTweets.W8.Common
{
    public class CurrencyCodeToFlagPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string code = (string)value;
            return Flag.Flags[code];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
