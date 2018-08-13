CREATE TABLE [dbo].[IncidentUpdateEvent] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [Type]          NVARCHAR (20)    NULL,
    [IncidentCode]  UNIQUEIDENTIFIER NOT NULL,
    [DateTime]      DATETIME         NOT NULL,
    [UpdateBy]      NVARCHAR (50)    NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);

