CREATE TABLE [dbo].[Organisation] (
    [Code]                 UNIQUEIDENTIFIER NOT NULL,
    [ID]                   INT              NOT NULL,
    [Name]                 NVARCHAR (1000)  NOT NULL,
    [OrganisationTypeCode] UNIQUEIDENTIFIER NOT NULL,
    [HEO]                  NVARCHAR (35)    NULL,
    [DateDeleted]          DATETIME         NULL,
    [IsActive]             BIT              NULL,
    [RowIdentifier]        TIMESTAMP        NOT NULL
);



