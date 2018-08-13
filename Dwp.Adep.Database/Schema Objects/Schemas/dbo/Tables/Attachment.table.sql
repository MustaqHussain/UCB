CREATE TABLE [dbo].[Attachment] (
    [Code]           UNIQUEIDENTIFIER NOT NULL,
    [IncidentCode]   UNIQUEIDENTIFIER NOT NULL,
    [AttachmentType] NVARCHAR (50)    NOT NULL,
    [Name]           NVARCHAR (500)   NOT NULL,
    [LoadedDate]     DATETIME         NOT NULL,
    [LoadedBy]       VARCHAR (50)     NOT NULL,
    [RowIdentifier]  TIMESTAMP        NOT NULL
);

