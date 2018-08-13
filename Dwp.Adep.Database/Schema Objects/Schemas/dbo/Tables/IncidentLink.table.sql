CREATE TABLE [dbo].[IncidentLink] (
    [Code]               UNIQUEIDENTIFIER NOT NULL,
    [IncidentCode]       UNIQUEIDENTIFIER NULL,
    [LinkedIncidentCode] UNIQUEIDENTIFIER NULL,
    [CustomerName]       VARCHAR (50)     NULL,
    [IncidentId]         INT              NULL,
    [RowIdentifier]      TIMESTAMP        NOT NULL
);



