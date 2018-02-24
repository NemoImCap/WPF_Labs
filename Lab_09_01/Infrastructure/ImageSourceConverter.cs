using System;
using System.IO;
using System.Globalization;
using System.Windows.Data;

namespace Lab_09_01.Infrastructure
{
    public class ImageSourceConverter : IValueConverter
    {
        string imageDirectory = Directory.GetCurrentDirectory();
        string ImageDirectory
        {
            get
            {
                return Path.Combine(imageDirectory, "images");
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if ((string)value != "")
                {
                    return Path.Combine(ImageDirectory, (string)value);
                }
            }

            return Path.Combine(ImageDirectory, "placeholder.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
