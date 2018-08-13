CREATE TABLE [dbo].[AttachmentData] (
    [Code]           UNIQUEIDENTIFIER NOT NULL,
    [AttachmentCode] UNIQUEIDENTIFIER NOT NULL,
    [DocumentBitmap] IMAGE            NOT NULL,
    [RowIdentifier]  TIMESTAMP        NOT NULL
);

