using QuanLyChiTieu.Models;

namespace QuanLyChiTieu.Services.Interfaces
{
    public interface IKhoanChiService
    {
        Task<List<KhoanChi>> LayDanhSachAsync();
        Task<KhoanChi> ThemAsync(KhoanChi khoanChi);
        Task XoaAsync(int id);
        Task<decimal> TinhTongChiThangAsync(int thang, int nam);
    }
}
