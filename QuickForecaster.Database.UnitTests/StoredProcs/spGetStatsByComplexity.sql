--------Setup Test class
EXEC tSQLt.NewTestClass 'spGetStatsByComplexityTest';
GO

--------Setup Test Method (SP)
CREATE PROCEDURE spGetStatsByComplexityTest.[test when getting stats with existing backlog items results correct aggregated data]
AS
BEGIN
    IF OBJECT_ID('actual') IS NOT NULL DROP TABLE actual;
    IF OBJECT_ID('expected') IS NOT NULL DROP TABLE expected;
	

------Fake Table
    EXEC tSQLt.FakeTable 'BacklogItems';

	INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (1, N'Create database schema', N'High', CAST(6.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), 1);
	INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (2, N'Create CI/CD pipeline', N'Medium', CAST(16.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), 1);
	INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (3, N'Create service layer', N'Low', CAST(10.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), 1);
	INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (4, N'Setup dev environment', N'High', CAST(10.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 1);
	INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (5, N'Acceptance testing', N'High', CAST(30.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), 1);
	INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (6, N'Create home page', N'High', CAST(6.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), 2);
	INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (7, N'Create contact-us page', N'Medium', CAST(16.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), 2);
	INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (8, N'Browser testing', N'Low', CAST(10.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), 2);
	INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (9, N'Client demo', N'High', CAST(10.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 2);
	INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (10, N'Document deployment guide', N'High', CAST(30.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), 3)

------Execution
	CREATE TABLE actual (
	    Confidence VARCHAR(MAX),
	    NumberOfItems INT
    );

    INSERT INTO actual
    EXEC dbo.spGetStatsByComplexity 1;

------Assertion
    CREATE TABLE expected (
	    Confidence VARCHAR(MAX),
	    NumberOfItems INT
    );

	INSERT INTO expected (Confidence, NumberOfItems) SELECT 'High', 3;
	INSERT INTO expected (Confidence, NumberOfItems) SELECT 'Medium', 1;
	INSERT INTO expected (Confidence, NumberOfItems) SELECT 'Low', 1;

	EXEC tSQLt.AssertEqualsTable 'expected', 'actual';
END;
GO