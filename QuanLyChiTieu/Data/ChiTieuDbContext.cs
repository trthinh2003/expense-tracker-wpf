using Microsoft.EntityFrameworkCore;
using QuanLyChiTieu.Models;
using System.IO;

namespace QuanLyChiTieu.Data
{
    public class ChiTieuDbContext : DbContext
    {
        public ChiTieuDbContext(DbContextOptions<ChiTieuDbContext> options) : base(options) { }

        public DbSet<NguoiDung> NguoiDung { get; set; } = null!;
        public DbSet<DanhMuc> DanhMuc { get; set; } = null!;
        public DbSet<KhoanChi> KhoanChi { get; set; } = null!;
        public DbSet<ChiTietChiaTien> ChiTietChiaTien { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietChiaTien>()
                .HasIndex(ct => new { ct.KhoanChiId, ct.NguoiDungId })
                .IsUnique();

            modelBuilder.Entity<KhoanChi>()
                .HasOne(k => k.NguoiTra)
                .WithMany(n => n.KhoanChiDaTra)
                .HasForeignKey(k => k.NguoiTraId)
                .OnDelete(DeleteBehavior.Restrict); // tránh xóa nhầm người khi còn khoản chi tham chiếu

            modelBuilder.Entity<KhoanChi>()
                .Property(k => k.SoTien)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ChiTietChiaTien>()
                .Property(ct => ct.TyLe)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<NguoiDung>().HasData(
                new NguoiDung { Id = 1, Ten = "Người 1", NgayThamGia = new DateTime(2026, 1, 1), DangHoatDong = true },
                new NguoiDung { Id = 2, Ten = "Người 2", NgayThamGia = new DateTime(2026, 1, 1), DangHoatDong = true },
                new NguoiDung { Id = 3, Ten = "Người 3", NgayThamGia = new DateTime(2026, 1, 1), DangHoatDong = true }
            );

            modelBuilder.Entity<DanhMuc>().HasData(
                new DanhMuc { Id = 1, TenDanhMuc = "Ăn uống" },
                new DanhMuc { Id = 2, TenDanhMuc = "Điện nước" },
                new DanhMuc { Id = 3, TenDanhMuc = "Đồ dùng chung" },
                new DanhMuc { Id = 4, TenDanhMuc = "Khác" }
            );
        }
    }
}
