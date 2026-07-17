namespace QuanLyChiTieu.Models
{
    public class KhoanChi
    {
        public int Id { get; set; }
        public string TenMatHang { get; set; } = null!;
        public decimal SoTien { get; set; }
        public DateTime NgayMua { get; set; }

        public int NguoiTraId { get; set; }
        public NguoiDung NguoiTra { get; set; } = null!;

        public int? DanhMucId { get; set; }
        public DanhMuc? DanhMuc { get; set; }

        public string? HinhAnhPath { get; set; }
        public string? GhiChu { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;

        public ICollection<ChiTietChiaTien> ChiTietChia { get; set; } = new List<ChiTietChiaTien>();
    }
}
