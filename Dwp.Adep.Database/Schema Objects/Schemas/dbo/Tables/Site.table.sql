CREATE TABLE [dbo].[Site] (
    [Code]             UNIQUEIDENTIFIER NOT NULL,
    [OrganisationCode] UNIQUEIDENTIFIER NOT NULL,
    [SiteName]         NVARCHAR (50)    NOT NULL,
    [PostCode]         NVARCHAR (50)    NOT NULL,
    [IsActive]         BIT              NOT NULL,
    [RowIdentifier]    TIMESTAMP        NOT NULL
);



