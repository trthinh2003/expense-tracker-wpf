using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyChiTieu.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMuc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayThamGia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DangHoatDong = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KhoanChi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMatHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayMua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTraId = table.Column<int>(type: "int", nullable: false),
                    DanhMucId = table.Column<int>(type: "int", nullable: true),
                    HinhAnhPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhoanChi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KhoanChi_DanhMuc_DanhMucId",
                        column: x => x.DanhMucId,
                        principalTable: "DanhMuc",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KhoanChi_NguoiDung_NguoiTraId",
                        column: x => x.NguoiTraId,
                        principalTable: "NguoiDung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietChiaTien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhoanChiId = table.Column<int>(type: "int", nullable: false),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    TyLe = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietChiaTien", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietChiaTien_KhoanChi_KhoanChiId",
                        column: x => x.KhoanChiId,
                        principalTable: "KhoanChi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietChiaTien_NguoiDung_NguoiDungId",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DanhMuc",
                columns: new[] { "Id", "TenDanhMuc" },
                values: new object[,]
                {
                    { 1, "Ăn uống" },
                    { 2, "Điện nước" },
                    { 3, "Đồ dùng chung" },
                    { 4, "Khác" }
                });

            migrationBuilder.InsertData(
                table: "NguoiDung",
                columns: new[] { "Id", "DangHoatDong", "NgayThamGia", "Ten" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Người 1" },
                    { 2, true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Người 2" },
                    { 3, true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Người 3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietChiaTien_KhoanChiId_NguoiDungId",
                table: "ChiTietChiaTien",
                columns: new[] { "KhoanChiId", "NguoiDungId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietChiaTien_NguoiDungId",
                table: "ChiTietChiaTien",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_KhoanChi_DanhMucId",
                table: "KhoanChi",
                column: "DanhMucId");

            migrationBuilder.CreateIndex(
                name: "IX_KhoanChi_NguoiTraId",
                table: "KhoanChi",
                column: "NguoiTraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietChiaTien");

            migrationBuilder.DropTable(
                name: "KhoanChi");

            migrationBuilder.DropTable(
                name: "DanhMuc");

            migrationBuilder.DropTable(
                name: "NguoiDung");
        }
    }
}
