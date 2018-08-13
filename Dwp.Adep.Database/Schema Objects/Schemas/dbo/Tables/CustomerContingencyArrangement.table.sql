CREATE TABLE [dbo].[CustomerContingencyArrangement] (
    [Code]                       UNIQUEIDENTIFIER NOT NULL,
    [CustomerCode]               UNIQUEIDENTIFIER NOT NULL,
    [ContingencyArrangementCode] UNIQUEIDENTIFIER NOT NULL,
    [RowIdentifier]              TIMESTAMP        NOT NULL
);



