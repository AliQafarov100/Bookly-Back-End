using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookly_Back_End.Migrations
{
    public partial class updateCityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Deliveries_DeliveryId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_DeliveryId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "DeliveryId",
                table: "Cities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryId",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_DeliveryId",
                table: "Cities",
                column: "DeliveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Deliveries_DeliveryId",
                table: "Cities",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
