using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookly_Back_End.Migrations
{
    public partial class addOrderProductAndUpdateOrderAndBookTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Orders_OrderId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_OrderId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "BasketItems");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_AppUserId",
                table: "OrderProducts",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_BookId",
                table: "OrderProducts",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Orders",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "BasketItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_OrderId",
                table: "BasketItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Orders_OrderId",
                table: "BasketItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
