CREATE TABLE [dbo].[IncidentType] (
    [Code]                 UNIQUEIDENTIFIER NOT NULL,
    [IncidentCategoryCode] UNIQUEIDENTIFIER NOT NULL,
    [Description]          NVARCHAR (100)   NOT NULL,
    [HasDetails]           BIT              NOT NULL,
    [HasAbuseType]         BIT              NOT NULL,
    [IsActive]             BIT              NOT NULL,
    [RowIdentifier]        TIMESTAMP        NOT NULL
);



