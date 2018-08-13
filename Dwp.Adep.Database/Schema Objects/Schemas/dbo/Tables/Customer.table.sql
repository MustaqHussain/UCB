CREATE TABLE [dbo].[Customer] (
    [Code]                       UNIQUEIDENTIFIER NOT NULL,
    [Title]                      NVARCHAR (50)    NULL,
    [OtherTitle]                 NVARCHAR (50)    NULL,
    [FirstName]                  NVARCHAR (50)    NULL,
    [OtherNames]                 NVARCHAR (50)    NULL,
    [LastName]                   NVARCHAR (50)    NULL,
    [NINO]                       CHAR (9)         NULL,
    [IsCustomerReported]         BIT              NOT NULL,
    [OtherPersonTitle]           NVARCHAR (50)    NULL,
    [OtherPersonOtherTitle]      NVARCHAR (50)    NULL,
    [OtherPersonFirstName]       NVARCHAR (50)    NULL,
    [OtherPersonOtherNames]      NVARCHAR (50)    NULL,
    [OtherPersonLastName]        NVARCHAR (50)    NULL,
    [OtherPersonNINO]            CHAR (9)         NULL,
    [RelationshipToCustomerCode] UNIQUEIDENTIFIER NULL,
    [HouseNumberOrName]          NVARCHAR (50)    NULL,
    [Street]                     NVARCHAR (50)    NULL,
    [Town]                       NVARCHAR (50)    NULL,
    [County]                     NVARCHAR (50)    NULL,
    [PostCode]                   NVARCHAR (50)    NULL,
    [RowIdentifier]              TIMESTAMP        NOT NULL
);

