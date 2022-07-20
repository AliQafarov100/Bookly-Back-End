using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookly_Back_End.Migrations
{
    public partial class updateLanguageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLanguages_Languages_LanguageId",
                table: "BookLanguages");

            migrationBuilder.DropColumn(
                name: "LaguageId",
                table: "BookLanguages");

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "BookLanguages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookLanguages_Languages_LanguageId",
                table: "BookLanguages",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLanguages_Languages_LanguageId",
                table: "BookLanguages");

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "BookLanguages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "LaguageId",
                table: "BookLanguages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_BookLanguages_Languages_LanguageId",
                table: "BookLanguages",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
