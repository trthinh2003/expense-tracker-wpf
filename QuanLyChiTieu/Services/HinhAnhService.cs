using QuanLyChiTieu.Services.Interfaces;
using System.IO;

namespace QuanLyChiTieu.Services
{
    public class HinhAnhService : IHinhAnhService
    {
        private readonly string _thuMucLuu;

        public HinhAnhService()
        {
            _thuMucLuu = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "QuanLyChiTieu", "Images");
            Directory.CreateDirectory(_thuMucLuu);
        }

        public string LuuAnh(string duongDanFileGoc)
        {
            var tenFileMoi = $"{Guid.NewGuid()}{Path.GetExtension(duongDanFileGoc)}";
            var duongDanDich = Path.Combine(_thuMucLuu, tenFileMoi);

            File.Copy(duongDanFileGoc, duongDanDich, overwrite: false);

            return tenFileMoi;
        }

        public string LayDuongDanDayDu(string? duongDanTuongDoi)
        {
            if (string.IsNullOrWhiteSpace(duongDanTuongDoi)) return string.Empty;
            return Path.Combine(_thuMucLuu, duongDanTuongDoi);
        }
    }
}