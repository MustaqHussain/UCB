CREATE TABLE [dbo].[IncidentInterestedParty] (
    [Code]                UNIQUEIDENTIFIER NOT NULL,
    [IncidentCode]        UNIQUEIDENTIFIER NOT NULL,
    [InterestedPartyCode] UNIQUEIDENTIFIER NOT NULL,
    [RowIdentifier]       TIMESTAMP        NOT NULL
);



