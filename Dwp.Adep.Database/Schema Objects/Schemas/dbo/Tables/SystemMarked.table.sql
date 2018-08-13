﻿CREATE TABLE [dbo].[SystemMarked] (
    [Code]          UNIQUEIDENTIFIER NOT NULL,
    [Description]   NVARCHAR (100)   NOT NULL,
    [IsActive]      BIT              NOT NULL,
    [RowIdentifier] TIMESTAMP        NOT NULL
);



