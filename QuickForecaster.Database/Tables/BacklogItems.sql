CREATE TABLE [dbo].[BacklogItems] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [Task]                NVARCHAR (MAX)  NULL,
    [Confidence]          NVARCHAR (MAX)  NOT NULL,
    [PessimisticEstimate] DECIMAL (18, 2) NOT NULL,
    [OptimisticEstimate]  DECIMAL (18, 2) NOT NULL,
    [EstimateId]          INT             NULL,
    CONSTRAINT [PK_BacklogItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BacklogItems_Estimates_EstimateId] FOREIGN KEY ([EstimateId]) REFERENCES [dbo].[Estimates] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_BacklogItems_EstimateId]
    ON [dbo].[BacklogItems]([EstimateId] ASC);

