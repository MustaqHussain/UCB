CREATE TABLE [dbo].[CustomerControlMeasure] (
    [Code]               UNIQUEIDENTIFIER NOT NULL,
    [CustomerCode]       UNIQUEIDENTIFIER NOT NULL,
    [ControlMeasureCode] UNIQUEIDENTIFIER NOT NULL,
    [RowIdentifier]      TIMESTAMP        NOT NULL
);



