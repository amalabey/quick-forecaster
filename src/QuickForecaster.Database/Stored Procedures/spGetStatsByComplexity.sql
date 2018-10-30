CREATE PROCEDURE [dbo].[spGetStatsByComplexity]
	                        @EstimateId int
                        AS
                        BEGIN
	                        SELECT BI.Confidence Confidence, 
		                        COUNT(BI.Id) NumberOfItems
	                        FROM [dbo].[BacklogItems] as BI
	                        WHERE BI.EstimateId = @EstimateId
	                        GROUP BY BI.Confidence
                        END
