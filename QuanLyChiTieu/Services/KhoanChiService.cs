using Microsoft.EntityFrameworkCore;
using QuanLyChiTieu.Data;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services.Interfaces;

namespace QuanLyChiTieu.Services
{
    public class KhoanChiService : IKhoanChiService
    {
        private readonly ChiTieuDbContext _db;
        public KhoanChiService(ChiTieuDbContext db) => _db = db;

        public async Task<List<KhoanChi>> LayDanhSachAsync()
        {
            return await _db.KhoanChi
                .Include(k => k.NguoiTra)
                .Include(k => k.DanhMuc)
                .OrderByDescending(k => k.NgayMua)
                .ToListAsync();
        }

        public async Task<KhoanChi> ThemAsync(KhoanChi khoanChi)
        {
            _db.KhoanChi.Add(khoanChi);
            await _db.SaveChangesAsync();
            return khoanChi;
        }

        public async Task XoaAsync(int id)
        {
            var khoanChi = await _db.KhoanChi.FindAsync(id);
            if (khoanChi != null)
            {
                _db.KhoanChi.Remove(khoanChi);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<decimal> TinhTongChiThangAsync(int thang, int nam)
        {
            return await _db.KhoanChi
                .Where(k => k.NgayMua.Month == thang && k.NgayMua.Year == nam)
                .SumAsync(k => k.SoTien);
        }
    }
}