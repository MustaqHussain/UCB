CREATE TABLE [dbo].[IntroductoryInformation] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [Description]   VARCHAR (MAX)    NOT NULL,
    [Locale]        NVARCHAR (20)    NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);



