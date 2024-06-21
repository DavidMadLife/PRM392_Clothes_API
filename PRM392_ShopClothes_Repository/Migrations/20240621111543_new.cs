using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRM392_ShopClothes_Repository.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Member_MemberId",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Cart",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_MemberId",
                table: "Cart",
                newName: "IX_Cart_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Order_OrderId",
                table: "Cart",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Order_OrderId",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Cart",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_OrderId",
                table: "Cart",
                newName: "IX_Cart_MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Member_MemberId",
                table: "Cart",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
