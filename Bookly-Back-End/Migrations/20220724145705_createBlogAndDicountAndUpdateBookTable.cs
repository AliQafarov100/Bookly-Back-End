using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookly_Back_End.Migrations
{
    public partial class createBlogAndDicountAndUpdateBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldPrice",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Photo = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    SubTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountPercent = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_DiscountId",
                table: "Books",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Discounts_DiscountId",
                table: "Books",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Discounts_DiscountId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Books_DiscountId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Books");

            migrationBuilder.AddColumn<decimal>(
                name: "OldPrice",
                table: "Books",
                type: "decimal(6,2)",
                nullable: true);
        }
    }
}
