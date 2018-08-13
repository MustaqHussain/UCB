
CREATE VIEW [dbo].[vwStaffRoles]
AS
SELECT s.Code, s.FirstName, s.LastName
	,CASE WHEN EXISTS(SELECT 1 FROM SiteStaff ss WHERE ss.StaffCode = s.Code AND ss.responsibility='Nominated Manager') THEN 1 ELSE 0 END AS IsNominatedManager
	,CASE WHEN EXISTS(SELECT 1 FROM SiteStaff ss WHERE ss.StaffCode = s.Code AND ss.responsibility='Deputy Nominated Manager') THEN 1 ELSE 0 END AS IsDeputyNominatedManager
	,CASE WHEN EXISTS(SELECT 1 FROM StaffAttributes sa
		INNER JOIN ApplicationAttribute aa on aa.Code = sa.ApplicationAttributeCode
		WHERE sa.StaffCode = s.Code AND aa.AttributeName='IsBusinessAreaManager' AND sa.LookupValue='Yes' AND sa.IsActive=1) THEN 1 ELSE 0 END AS IsBusinessAreaManager
	,CASE WHEN EXISTS(SELECT 1 FROM StaffAttributes sa
		INNER JOIN ApplicationAttribute aa on aa.Code = sa.ApplicationAttributeCode
		WHERE sa.StaffCode = s.Code AND aa.AttributeName='IsAdmin' AND sa.LookupValue='Yes' AND sa.IsActive=1) THEN 1 ELSE 0 END AS IsAdmin
	,CASE WHEN EXISTS(SELECT 1 FROM StaffAttributes sa
		INNER JOIN ApplicationAttribute aa on aa.Code = sa.ApplicationAttributeCode
		WHERE sa.StaffCode = s.Code AND aa.AttributeName='IsTradeUnionUser' AND sa.LookupValue='Yes' AND sa.IsActive=1) THEN 1 ELSE 0 END AS IsTradeUnionUser
FROM Staff s
