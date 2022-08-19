using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookly_Back_End.Migrations
{
    public partial class updateContactForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "ContactForms",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "ContactForms",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "ContactForms");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "ContactForms",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);
        }
    }
}
