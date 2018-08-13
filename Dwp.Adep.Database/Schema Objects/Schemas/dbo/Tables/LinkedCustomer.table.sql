CREATE TABLE [dbo].[LinkedCustomer] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [Customer1Code] UNIQUEIDENTIFIER NOT NULL,
    [Customer2Code] UNIQUEIDENTIFIER NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);



