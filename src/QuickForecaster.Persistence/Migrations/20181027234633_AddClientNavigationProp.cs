using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickForecaster.Persistence.Migrations
{
    public partial class AddClientNavigationProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId1",
                table: "Estimates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estimates_ClientId1",
                table: "Estimates",
                column: "ClientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Estimates_Clients_ClientId1",
                table: "Estimates",
                column: "ClientId1",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estimates_Clients_ClientId1",
                table: "Estimates");

            migrationBuilder.DropIndex(
                name: "IX_Estimates_ClientId1",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "Estimates");
        }
    }
}
