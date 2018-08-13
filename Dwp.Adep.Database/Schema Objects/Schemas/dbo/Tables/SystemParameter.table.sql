CREATE TABLE [dbo].[SystemParameter] (
    [Code]           UNIQUEIDENTIFIER NOT NULL,
    [Name]           NVARCHAR (50)    NOT NULL,
    [Description]    NVARCHAR (100)   NOT NULL,
    [ParameterValue] NVARCHAR (MAX)   NOT NULL,
    [IsSecure]       BIT              NOT NULL,
    [RowIdentifier]  TIMESTAMP        NOT NULL
);



