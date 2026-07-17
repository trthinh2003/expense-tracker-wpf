using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace QuanLyChiTieu.Converters
{
    public class DuongDanAnhToBitmapConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            var duongDan = value as string;
            if (string.IsNullOrWhiteSpace(duongDan) || !File.Exists(duongDan)) return null;

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(duongDan);
            bitmap.CacheOption = BitmapCacheOption.OnLoad; // đọc hết vào RAM ngay, tránh khóa file
            bitmap.EndInit();
            return bitmap;
        }

        public object ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}