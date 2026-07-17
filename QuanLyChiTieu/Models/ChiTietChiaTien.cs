namespace QuanLyChiTieu.Models
{
    public class ChiTietChiaTien
    {
        public int Id { get; set; }
        public int KhoanChiId { get; set; }
        public KhoanChi KhoanChi { get; set; } = null!;

        public int NguoiDungId { get; set; }
        public NguoiDung NguoiDung { get; set; } = null!;

        public decimal TyLe { get; set; }
    }
}
