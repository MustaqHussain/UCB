CREATE TABLE [dbo].[StandardReport] (
    [Code]              UNIQUEIDENTIFIER NOT NULL,
    [ReportName]        NVARCHAR (250)   NOT NULL,
    [ReportDescription] NVARCHAR (500)   NOT NULL,
    [Category]          NVARCHAR (50)    NOT NULL,
    [SortOrder]         INT              NOT NULL,
    [ReportUrl]         NVARCHAR (250)   NOT NULL,
    [RowIdentifier]     TIMESTAMP        NOT NULL
);



