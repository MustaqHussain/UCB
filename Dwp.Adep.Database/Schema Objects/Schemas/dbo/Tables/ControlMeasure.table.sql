CREATE TABLE [dbo].[ControlMeasure] (
    [Code]                      UNIQUEIDENTIFIER NOT NULL,
    [ControlMeasureDescription] NVARCHAR (50)    NOT NULL,
    [IsActive]                  BIT              NOT NULL,
    [RowIdentifier]             TIMESTAMP        NOT NULL
);

