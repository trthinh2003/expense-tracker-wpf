<p align="center">
  <a href="README.md"><strong>Tiếng Việt</strong></a> |
  <a href="README.en.md">English</a>
</p>

# Quản Lý Chi Tiêu Trọ

Ứng dụng desktop WPF quản lý chi tiêu chung cho phòng trọ nhiều người ở, hỗ trợ lưu khoản chi kèm ảnh hóa đơn và chia tiền tự động.

## Mục lục

- [Tính năng](#tính-năng)
- [Công nghệ sử dụng](#công-nghệ-sử-dụng)
- [Cấu trúc project](#cấu-trúc-project)
- [Cài đặt & chạy thử](#cài-đặt--chạy-thử)
- [Ghi chú phát triển](#ghi-chú-phát-triển)
- [Roadmap](#roadmap)
- [License](#license)

## Tính năng

- Thêm khoản chi tiêu (mặt hàng, số tiền, người trả, ảnh hóa đơn đính kèm)
- Xem danh sách chi tiêu, tổng chi trong tháng
- Chia tiền mặc định đều cho các thành viên, hỗ trợ tùy chỉnh tỷ lệ chia riêng cho từng khoản chi
- Lưu ảnh hóa đơn cục bộ, không cần upload cloud

## Công nghệ sử dụng

- .NET 8 / WPF
- Entity Framework Core (Code First + Migrations) - SQL Server / LocalDB
- MVVM Pattern (ViewModelBase tự viết, RelayCommand / AsyncRelayCommand)
- Dependency Injection qua Microsoft.Extensions.DependencyInjection

## Cấu trúc project

```
QuanLyChiTieu/
├── Models/       # Entity classes (NguoiDung, KhoanChi, DanhMuc, ChiTietChiaTien)
├── Data/         # DbContext, Migrations
├── Services/     # Business logic (KhoanChiService, NguoiDungService, ChiaTienService, HinhAnhService)
├── ViewModels/   # MVVM ViewModels
├── Views/        # XAML Views
├── Converters/   # IValueConverter cho Binding
├── Commands/     # RelayCommand, AsyncRelayCommand
└── Resources/    # Style, màu sắc dùng chung
```

## Cài đặt & chạy thử

Yêu cầu:
- Visual Studio 2022 trở lên (có workload .NET Desktop Development)
- SQL Server LocalDB (đi kèm Visual Studio) hoặc SQL Server Express

Các bước:

1. Clone repo:
```bash
git clone <repo-url>
cd QuanLyChiTieu
```

2. Kiểm tra connection string trong appsettings.json, mặc định dùng LocalDB:
```json
{
  "ConnectionStrings": {
    "ChiTieuDb": "Server=(localdb)\\MSSQLLocalDB;Database=QuanLyChiTieuDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

3. Chạy migration để tạo database:
```bash
dotnet ef database update
```

4. Build & chạy:
```bash
dotnet run
```
Hoặc mở bằng Visual Studio, nhấn F5.

## Ghi chú phát triển

- Ảnh hóa đơn được lưu tại %AppData%\QuanLyChiTieu\Images, chỉ lưu tên file trong DB (cột HinhAnhPath), không lưu binary.
- Database mặc định seed sẵn 3 người dùng (Người 1, Người 2, Người 3) và 4 danh mục cơ bản khi migrate lần đầu.
- Logic chia tiền: nếu khoản chi không có dòng nào trong bảng ChiTietChiaTien, mặc định hiểu là chia đều cho tất cả người dùng đang hoạt động.

## Roadmap

- Màn hình tùy chỉnh tỷ lệ chia tiền cho từng khoản chi
- Xem ảnh hóa đơn phóng to
- Thống kê chi tiêu theo danh mục / theo tháng
- Tổng hợp "ai nợ ai bao nhiêu"

## License

MIT