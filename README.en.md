<p align="center">
  <a href="README.md">Tiếng Việt</a> |
  <a href="README.en.md"><strong>English</strong></a>
</p>

# Expense Manager for Shared Housing

A WPF desktop app for managing shared roommate expenses, with receipt photo attachments and automatic cost splitting.

## Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Setup & Run](#setup--run)
- [Developer Notes](#developer-notes)
- [Roadmap](#roadmap)
- [License](#license)

## Features

- Add expenses (item, amount, payer, attached receipt photo)
- View expense list and monthly totals
- Default even split among members, with support for custom split ratios per expense
- Store receipt images locally, no cloud upload required

## Tech Stack

- .NET 8 / WPF
- Entity Framework Core (Code First + Migrations) - SQL Server / LocalDB
- MVVM Pattern (custom ViewModelBase, RelayCommand / AsyncRelayCommand)
- Dependency Injection via Microsoft.Extensions.DependencyInjection

## Project Structure

```
QuanLyChiTieu/
├── Models/       # Entity classes (NguoiDung, KhoanChi, DanhMuc, ChiTietChiaTien)
├── Data/         # DbContext, Migrations
├── Services/     # Business logic (KhoanChiService, NguoiDungService, ChiaTienService, HinhAnhService)
├── ViewModels/   # MVVM ViewModels
├── Views/        # XAML Views
├── Converters/   # IValueConverter for data binding
├── Commands/     # RelayCommand, AsyncRelayCommand
└── Resources/    # Shared styles and colors
```

## Setup & Run

Requirements:
- Visual Studio 2022 or later (with .NET Desktop Development workload)
- SQL Server LocalDB (bundled with Visual Studio) or SQL Server Express

Steps:

1. Clone the repo:
```bash
git clone <repo-url>
cd QuanLyChiTieu
```

2. Check the connection string in appsettings.json, defaults to LocalDB:
```json
{
  "ConnectionStrings": {
    "ChiTieuDb": "Server=(localdb)\\MSSQLLocalDB;Database=QuanLyChiTieuDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

3. Run migrations to create the database:
```bash
dotnet ef database update
```

4. Build & run:
```bash
dotnet run
```
Or open in Visual Studio and press F5.

## Developer Notes

- Receipt images are stored at %AppData%\QuanLyChiTieu\Images; the DB only stores the filename (HinhAnhPath column), not binary data.
- The database seeds 3 default users (Người 1, Người 2, Người 3) and 4 base categories on first migration.
- Split logic: if an expense has no rows in ChiTietChiaTien, it is treated as split evenly among all active users.

## Roadmap

- Custom split-ratio screen per expense
- Zoomable receipt image viewer
- Spending stats by category / month
- "Who owes whom" summary

## License

MIT