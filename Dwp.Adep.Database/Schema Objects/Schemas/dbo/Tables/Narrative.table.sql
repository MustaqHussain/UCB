CREATE TABLE [dbo].[Narrative] (
    [Code]                 UNIQUEIDENTIFIER NOT NULL,
    [NarrativeType]        NVARCHAR (25)    NOT NULL,
    [NarrativeDescription] NVARCHAR (1000)  NULL,
    [RowIdentifier]        TIMESTAMP        NOT NULL
);



