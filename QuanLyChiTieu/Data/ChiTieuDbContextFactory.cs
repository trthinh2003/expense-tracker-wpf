using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace QuanLyChiTieu.Data
{ 
    public class ChiTieuDbContextFactory : IDesignTimeDbContextFactory<ChiTieuDbContext> // chỉ dùng khi chạy lệnh `dotnet ef migrations add/update`, không dùng lúc app chạy thật
    {
        public ChiTieuDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("ChiTieuDb");

            var optionsBuilder = new DbContextOptionsBuilder<ChiTieuDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ChiTieuDbContext(optionsBuilder.Options);
        }
    }
}