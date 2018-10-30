CREATE TABLE [dbo].[Estimates] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [Estimator_Email]       NVARCHAR (MAX) NULL,
    [ClientId]              INT            NULL,
    [Estimator_DisplayName] NVARCHAR (MAX) NULL,
    [ProjectName]           NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Estimates] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Estimates_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Estimates_ClientId]
    ON [dbo].[Estimates]([ClientId] ASC);

