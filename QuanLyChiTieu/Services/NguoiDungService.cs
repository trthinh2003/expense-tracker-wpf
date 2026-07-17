using Microsoft.EntityFrameworkCore;
using QuanLyChiTieu.Data;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services.Interfaces;

namespace QuanLyChiTieu.Services
{
    public class NguoiDungService : INguoiDungService
    {
        private readonly ChiTieuDbContext _db;
        public NguoiDungService(ChiTieuDbContext db) => _db = db;

        public async Task<List<NguoiDung>> LayDanhSachHoatDongAsync()
        {
            return await _db.NguoiDung
                .Where(n => n.DangHoatDong)
                .OrderBy(n => n.Id)
                .ToListAsync();
        }
    }
}
