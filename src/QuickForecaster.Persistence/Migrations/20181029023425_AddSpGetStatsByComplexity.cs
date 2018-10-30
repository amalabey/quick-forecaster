using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickForecaster.Persistence.Migrations
{
    public partial class AddSpGetStatsByComplexity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[spGetStatsByComplexity]
	                        @EstimateId int
                        AS
                        BEGIN
	                        SELECT BI.Confidence Confidence, 
		                        COUNT(BI.Id) NumberOfItems
	                        FROM [dbo].[BacklogItems] as BI
	                        WHERE BI.EstimateId = @EstimateId
	                        GROUP BY BI.Confidence
                        END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {            
            var sp = @"DROP PROCEDURE[dbo].[spGetStatsByComplexity]";
            migrationBuilder.Sql(sp);
        }
    }
}
