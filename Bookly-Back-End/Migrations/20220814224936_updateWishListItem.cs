using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookly_Back_End.Migrations
{
    public partial class updateWishListItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishListItems_AspNetUsers_AppUserId1",
                table: "WishListItems");

            migrationBuilder.DropIndex(
                name: "IX_WishListItems_AppUserId1",
                table: "WishListItems");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "WishListItems");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "WishListItems",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_AppUserId",
                table: "WishListItems",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishListItems_AspNetUsers_AppUserId",
                table: "WishListItems",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishListItems_AspNetUsers_AppUserId",
                table: "WishListItems");

            migrationBuilder.DropIndex(
                name: "IX_WishListItems_AppUserId",
                table: "WishListItems");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "WishListItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "WishListItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_AppUserId1",
                table: "WishListItems",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WishListItems_AspNetUsers_AppUserId1",
                table: "WishListItems",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
