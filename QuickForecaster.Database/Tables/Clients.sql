CREATE TABLE [dbo].[Clients] (
    [Id]                         INT            IDENTITY (1, 1) NOT NULL,
    [Name]                       NVARCHAR (MAX) NULL,
    [AccountManager_Email]       NVARCHAR (MAX) NULL,
    [AccountManager_DisplayName] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED ([Id] ASC)
);

