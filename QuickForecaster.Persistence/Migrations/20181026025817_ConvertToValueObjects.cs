using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickForecaster.Persistence.Migrations
{
    public partial class ConvertToValueObjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Estimator",
                table: "Estimates",
                newName: "Estimator_Email");

            migrationBuilder.RenameColumn(
                name: "AccountManager",
                table: "Clients",
                newName: "AccountManager_Email");

            migrationBuilder.AddColumn<string>(
                name: "Estimator_DisplayName",
                table: "Estimates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountManager_DisplayName",
                table: "Clients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estimator_DisplayName",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "AccountManager_DisplayName",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "Estimator_Email",
                table: "Estimates",
                newName: "Estimator");

            migrationBuilder.RenameColumn(
                name: "AccountManager_Email",
                table: "Clients",
                newName: "AccountManager");
        }
    }
}
