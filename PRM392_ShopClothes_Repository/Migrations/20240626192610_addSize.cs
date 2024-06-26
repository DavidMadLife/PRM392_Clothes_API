using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRM392_ShopClothes_Repository.Migrations
{
    public partial class addSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "CartItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "CartItem");
        }
    }
}
