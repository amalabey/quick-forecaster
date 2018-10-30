SET IDENTITY_INSERT [dbo].[Clients] ON 
GO
INSERT [dbo].[Clients] ([Id], [Name], [AccountManager_Email], [AccountManager_DisplayName]) VALUES (1, N'Contoso', N'jdoe@contoso.com', N'John Contoso')
GO
INSERT [dbo].[Clients] ([Id], [Name], [AccountManager_Email], [AccountManager_DisplayName]) VALUES (2, N'Fabrikam', N'jdoe@fabrikam.com', N'John Fabrikam')
GO
INSERT [dbo].[Clients] ([Id], [Name], [AccountManager_Email], [AccountManager_DisplayName]) VALUES (3, N'Wingtip', N'jdoe@wingtip.com', N'John Wingtip')
GO
INSERT [dbo].[Clients] ([Id], [Name], [AccountManager_Email], [AccountManager_DisplayName]) VALUES (4, N'MyDrive', N'jdoe@mydrive.com', N'John Mydrive')
GO
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[Estimates] ON 
GO
INSERT [dbo].[Estimates] ([Id], [Estimator_Email], [ClientId], [Estimator_DisplayName], [ProjectName]) VALUES (1, N'jane@Contoso.com', 1, N'Jane Contoso', N'Contoso Website')
GO
INSERT [dbo].[Estimates] ([Id], [Estimator_Email], [ClientId], [Estimator_DisplayName], [ProjectName]) VALUES (2, N'jane@fabrikam.com', 2, N'Jane Fabrikam', N'Fabrikam Mobile App')
GO
INSERT [dbo].[Estimates] ([Id], [Estimator_Email], [ClientId], [Estimator_DisplayName], [ProjectName]) VALUES (3, N'tom@fabrikam.com', 2, N'Tom Fabrikam', N'Fabrikam DevOps')
GO
SET IDENTITY_INSERT [dbo].[Estimates] OFF
GO
SET IDENTITY_INSERT [dbo].[BacklogItems] ON 
GO
INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (1, N'Create database schema', N'High', CAST(6.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (2, N'Create CI/CD pipeline', N'Medium', CAST(16.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (3, N'Create service layer', N'Low', CAST(10.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (4, N'Setup dev environment', N'High', CAST(10.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (5, N'Acceptance testing', N'High', CAST(30.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), 1)
GO
INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (6, N'Create home page', N'High', CAST(6.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), 2)
GO
INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (7, N'Create contact-us page', N'Medium', CAST(16.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), 2)
GO
INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (8, N'Browser testing', N'Low', CAST(10.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), 2)
GO
INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (9, N'Client demo', N'High', CAST(10.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 2)
GO
INSERT [dbo].[BacklogItems] ([Id], [Task], [Confidence], [PessimisticEstimate], [OptimisticEstimate], [EstimateId]) VALUES (10, N'Document deployment guide', N'High', CAST(30.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), 3)
GO
SET IDENTITY_INSERT [dbo].[BacklogItems] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20181026025057_InitialCreate', N'2.1.4-rtm-31024')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20181026025817_ConvertToValueObjects', N'2.1.4-rtm-31024')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20181027223736_AddProjectNameColumn', N'2.1.4-rtm-31024')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20181027234633_AddClientNavigationProp', N'2.1.4-rtm-31024')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20181028001252_AddEstimateNavigationProp', N'2.1.4-rtm-31024')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20181028072317_SeedData', N'2.1.4-rtm-31024')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20181029023425_AddSpGetStatsByComplexity', N'2.1.4-rtm-31024')
GO
