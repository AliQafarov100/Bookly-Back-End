using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookly_Back_End.Migrations
{
    public partial class createAuthorSocialMediaAndUpdateAwardTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialMedias_Authors_AuthorId",
                table: "SocialMedias");

            migrationBuilder.DropIndex(
                name: "IX_SocialMedias_AuthorId",
                table: "SocialMedias");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "SocialMedias");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Books",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Books",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Awards",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuthorSocialMedias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(nullable: false),
                    SocialMediaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorSocialMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorSocialMedias_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorSocialMedias_SocialMedias_SocialMediaId",
                        column: x => x.SocialMediaId,
                        principalTable: "SocialMedias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorSocialMedias_AuthorId",
                table: "AuthorSocialMedias",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorSocialMedias_SocialMediaId",
                table: "AuthorSocialMedias",
                column: "SocialMediaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorSocialMedias");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Awards");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "SocialMedias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedias_AuthorId",
                table: "SocialMedias",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMedias_Authors_AuthorId",
                table: "SocialMedias",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
