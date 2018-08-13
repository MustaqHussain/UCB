CREATE TABLE [dbo].[SiteStaff] (
    [Code]           UNIQUEIDENTIFIER NOT NULL,
    [SiteCode]       UNIQUEIDENTIFIER NOT NULL,
    [StaffCode]      UNIQUEIDENTIFIER NOT NULL,
    [Responsibility] NCHAR (50)       NOT NULL,
    [RowIdentifier]  TIMESTAMP        NOT NULL
);



