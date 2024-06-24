using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRM392_ShopClothes_Repository.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
