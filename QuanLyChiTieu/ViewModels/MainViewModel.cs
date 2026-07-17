using System.Collections.ObjectModel;
using QuanLyChiTieu.Commands;
using QuanLyChiTieu.Models;
using QuanLyChiTieu.Services.Interfaces;
using QuanLyChiTieu.ViewModels.Base;

namespace QuanLyChiTieu.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IKhoanChiService _khoanChiService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IHinhAnhService _hinhAnhService;


        public MainViewModel(
            IKhoanChiService khoanChiService,
            INguoiDungService nguoiDungService,
            IHinhAnhService hinhAnhService
        )
        {
            _khoanChiService = khoanChiService;
            _nguoiDungService = nguoiDungService;
            _hinhAnhService = hinhAnhService;

            DanhSachKhoanChi = new ObservableCollection<KhoanChi>();
            DanhSachNguoiDung = new ObservableCollection<NguoiDung>();

            ThemCommand = new AsyncRelayCommand(_ => ThemKhoanChiAsync(), _ => KiemTraHopLe());

            _ = TaiDuLieuAsync();

            ChonAnhCommand = new RelayCommand(_ => ChonAnh());
        }

        public ObservableCollection<KhoanChi> DanhSachKhoanChi { get; }
        public ObservableCollection<NguoiDung> DanhSachNguoiDung { get; }

        private string _tenMatHang = string.Empty;
        public string TenMatHang
        {
            get => _tenMatHang;
            set => SetProperty(ref _tenMatHang, value);
        }

        private string _soTienNhap = string.Empty;
        public string SoTienNhap
        {
            get => _soTienNhap;
            set => SetProperty(ref _soTienNhap, value);
        }

        private NguoiDung? _nguoiTraDuocChon;
        public NguoiDung? NguoiTraDuocChon
        {
            get => _nguoiTraDuocChon;
            set => SetProperty(ref _nguoiTraDuocChon, value);
        }

        private decimal _tongChiThang;
        public decimal TongChiThang
        {
            get => _tongChiThang;
            private set => SetProperty(ref _tongChiThang, value);
        }

        private string? _duongDanAnhTamThoi; // full path ảnh gốc vừa chọn, chưa copy vào AppData
        public string? DuongDanAnhTamThoi
        {
            get => _duongDanAnhTamThoi;
            set => SetProperty(ref _duongDanAnhTamThoi, value);
        }

        private string _tieuDeThongTin = string.Empty;
        public string TieuDeThongTin
        {
            get => _tieuDeThongTin;
            set => SetProperty(ref _tieuDeThongTin, value);
        }

        public RelayCommand ChonAnhCommand { get; }

        public AsyncRelayCommand ThemCommand { get; }

        private bool KiemTraHopLe()
        {
            return !string.IsNullOrWhiteSpace(TenMatHang)
                && decimal.TryParse(SoTienNhap, out var soTien) && soTien > 0
                && NguoiTraDuocChon != null;
        }

        private void ChonAnh()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Hình ảnh (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
                Title = "Chọn ảnh hóa đơn"
            };

            if (dialog.ShowDialog() == true)
            {
                DuongDanAnhTamThoi = dialog.FileName; // chỉ lưu tạm để preview, chưa copy vội
            }
        }

        private async Task TaiDuLieuAsync()
        {
            var nguoiDung = await _nguoiDungService.LayDanhSachHoatDongAsync();
            DanhSachNguoiDung.Clear();
            foreach (var n in nguoiDung) DanhSachNguoiDung.Add(n);

            NguoiTraDuocChon ??= DanhSachNguoiDung.FirstOrDefault();

            TieuDeThongTin = $"{DanhSachNguoiDung.Count} người · Tháng {DateTime.Now.Month}/{DateTime.Now.Year}";

            await TaiDanhSachChiTieuAsync();
        }

        private async Task TaiDanhSachChiTieuAsync()
        {
            var danhSach = await _khoanChiService.LayDanhSachAsync();
            DanhSachKhoanChi.Clear();
            foreach (var kc in danhSach) DanhSachKhoanChi.Add(kc);

            TongChiThang = await _khoanChiService.TinhTongChiThangAsync(DateTime.Now.Month, DateTime.Now.Year);
        }

        private async Task ThemKhoanChiAsync()
        {
            if (!KiemTraHopLe() || NguoiTraDuocChon == null) return;

            string? tenFileAnh = null;
            if (!string.IsNullOrWhiteSpace(DuongDanAnhTamThoi))
                tenFileAnh = _hinhAnhService.LuuAnh(DuongDanAnhTamThoi);

            var khoanChi = new KhoanChi
            {
                TenMatHang = TenMatHang.Trim(),
                SoTien = decimal.Parse(SoTienNhap),
                NgayMua = DateTime.Now,
                NguoiTraId = NguoiTraDuocChon.Id,
                HinhAnhPath = tenFileAnh
            };

            await _khoanChiService.ThemAsync(khoanChi);

            TenMatHang = string.Empty;
            SoTienNhap = string.Empty;
            DuongDanAnhTamThoi = null;

            await TaiDanhSachChiTieuAsync();
        }
    }
}