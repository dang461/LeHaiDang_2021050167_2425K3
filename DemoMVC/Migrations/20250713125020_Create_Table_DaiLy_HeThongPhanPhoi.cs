using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoMVC.Migrations
{
    /// <inheritdoc />
    public partial class Create_Table_DaiLy_HeThongPhanPhoi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeThongPhanPhoi",
                columns: table => new
                {
                    MaHTPP = table.Column<string>(type: "TEXT", nullable: false),
                    TenHTPP = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeThongPhanPhoi", x => x.MaHTPP);
                });

            migrationBuilder.CreateTable(
                name: "DaiLys",
                columns: table => new
                {
                    MaDaiLy = table.Column<string>(type: "TEXT", nullable: false),
                    TenDaiLy = table.Column<string>(type: "TEXT", nullable: false),
                    DiaChi = table.Column<string>(type: "TEXT", nullable: false),
                    NguoiDaiDien = table.Column<string>(type: "TEXT", nullable: false),
                    DienThoai = table.Column<string>(type: "TEXT", nullable: false),
                    MaHTPP = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaiLys", x => x.MaDaiLy);
                    table.ForeignKey(
                        name: "FK_DaiLys_HeThongPhanPhoi_MaHTPP",
                        column: x => x.MaHTPP,
                        principalTable: "HeThongPhanPhoi",
                        principalColumn: "MaHTPP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaiLys_MaHTPP",
                table: "DaiLys",
                column: "MaHTPP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DaiLys");

            migrationBuilder.DropTable(
                name: "HeThongPhanPhoi");
        }
    }
}
