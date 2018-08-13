

CREATE VIEW [dbo].[vwLevel4Organisation]
AS
SELECT     o.Code, o.Name AS Level4OrganisationName, ot.Code AS Level4TypeCode, oh.AncestorOrganisationCode AS Level3OrganisationCode 
FROM         dbo.Organisation AS o INNER JOIN
                      dbo.OrganisationHierarchy AS oh ON o.Code = oh.OrganisationCode INNER JOIN
                      dbo.OrganisationType AS ot ON o.OrganisationTypeCode = ot.Code INNER JOIN
                      dbo.OrganisationTypeGroup AS otg ON ot.OrganisationTypeGroupCode = otg.Code
WHERE     (otg.Name = 'UCB') AND (ot.LevelNumber = 4) AND (oh.ImmediateParent = 1)




