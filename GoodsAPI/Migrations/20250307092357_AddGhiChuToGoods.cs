using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddGhiChuToGoods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "Goods",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "Goods");
        }
    }
}
