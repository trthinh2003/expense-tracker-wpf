namespace QuanLyChiTieu.Models
{
    public class NguoiDung
    {
        public int Id { get; set; }
        public string Ten { get; set; } = null!;
        public DateTime NgayThamGia { get; set; } = DateTime.Now;
        public bool DangHoatDong { get; set; } = true;

        public ICollection<KhoanChi> KhoanChiDaTra { get; set; } = new List<KhoanChi>();
        public ICollection<ChiTietChiaTien> PhanChiaCuaToi { get; set; } = new List<ChiTietChiaTien>();
    }
}
