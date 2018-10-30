using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickForecaster.Persistence.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BacklogItems_Estimates_EstimateId1",
                table: "BacklogItems");

            migrationBuilder.DropIndex(
                name: "IX_BacklogItems_EstimateId1",
                table: "BacklogItems");

            migrationBuilder.DropColumn(
                name: "EstimateId1",
                table: "BacklogItems");

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Name", "AccountManager_DisplayName", "AccountManager_Email" },
                values: new object[,]
                {
                    { 1, "Contoso", "John Contoso", "jdoe@contoso.com" },
                    { 2, "Fabrikam", "John Fabrikam", "jdoe@fabrikam.com" },
                    { 3, "Wingtip", "John Wingtip", "jdoe@wingtip.com" },
                    { 4, "MyDrive", "John Mydrive", "jdoe@mydrive.com" }
                });

            migrationBuilder.InsertData(
                table: "Estimates",
                columns: new[] { "Id", "ClientId", "ProjectName", "Estimator_DisplayName", "Estimator_Email" },
                values: new object[] { 1, 1, "Contoso Website", "Jane Contoso", "jane@Contoso.com" });

            migrationBuilder.InsertData(
                table: "Estimates",
                columns: new[] { "Id", "ClientId", "ProjectName", "Estimator_DisplayName", "Estimator_Email" },
                values: new object[] { 2, 2, "Fabrikam Mobile App", "Jane Fabrikam", "jane@fabrikam.com" });

            migrationBuilder.InsertData(
                table: "Estimates",
                columns: new[] { "Id", "ClientId", "ProjectName", "Estimator_DisplayName", "Estimator_Email" },
                values: new object[] { 3, 2, "Fabrikam DevOps", "Tom Fabrikam", "tom@fabrikam.com" });

            migrationBuilder.InsertData(
                table: "BacklogItems",
                columns: new[] { "Id", "Confidence", "EstimateId", "OptimisticEstimate", "PessimisticEstimate", "Task" },
                values: new object[,]
                {
                    { 1, "High", 1, 4.0m, 6.0m, "Create database schema" },
                    { 2, "Medium", 1, 10.0m, 16.0m, "Create CI/CD pipeline" },
                    { 3, "Low", 1, 8.0m, 10.0m, "Create service layer" },
                    { 4, "High", 1, 5.0m, 10.0m, "Setup dev environment" },
                    { 5, "High", 1, 15.0m, 30.0m, "Acceptance testing" },
                    { 6, "High", 2, 4.0m, 6.0m, "Create home page" },
                    { 7, "Medium", 2, 10.0m, 16.0m, "Create contact-us page" },
                    { 8, "Low", 2, 8.0m, 10.0m, "Browser testing" },
                    { 9, "High", 2, 5.0m, 10.0m, "Client demo" },
                    { 10, "High", 3, 15.0m, 30.0m, "Document deployment guide" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BacklogItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BacklogItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BacklogItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BacklogItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BacklogItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BacklogItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BacklogItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "BacklogItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "BacklogItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "BacklogItems",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Estimates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Estimates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Estimates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "EstimateId1",
                table: "BacklogItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BacklogItems_EstimateId1",
                table: "BacklogItems",
                column: "EstimateId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BacklogItems_Estimates_EstimateId1",
                table: "BacklogItems",
                column: "EstimateId1",
                principalTable: "Estimates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
