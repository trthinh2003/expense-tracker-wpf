using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuanLyChiTieu.Data;
using QuanLyChiTieu.Services;
using QuanLyChiTieu.Services.Interfaces;
using QuanLyChiTieu.ViewModels;
using QuanLyChiTieu.Views;
using System.Windows;

namespace QuanLyChiTieu
{
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Đọc appsettings.json từ thư mục chạy app
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = config.GetConnectionString("ChiTieuDb");

            var services = new ServiceCollection();

            services.AddDbContext<ChiTieuDbContext>(options =>
                options.UseSqlServer(connectionString),
                ServiceLifetime.Transient);

            services.AddTransient<IKhoanChiService, KhoanChiService>();
            services.AddTransient<INguoiDungService, NguoiDungService>();
            services.AddTransient<IChiaTienService, ChiaTienService>();
            services.AddSingleton<IHinhAnhService, HinhAnhService>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainWindow>();

            ServiceProvider = services.BuildServiceProvider();

            using (var scope = ServiceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ChiTieuDbContext>();
                db.Database.Migrate();
            }

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}