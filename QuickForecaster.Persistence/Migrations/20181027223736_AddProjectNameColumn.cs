using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickForecaster.Persistence.Migrations
{
    public partial class AddProjectNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "Estimates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "Estimates");
        }
    }
}
