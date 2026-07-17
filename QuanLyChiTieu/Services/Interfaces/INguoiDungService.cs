using QuanLyChiTieu.Models;

namespace QuanLyChiTieu.Services.Interfaces
{
    public interface INguoiDungService
    {
        Task<List<NguoiDung>> LayDanhSachHoatDongAsync();
    }
}
