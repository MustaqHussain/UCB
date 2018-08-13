


CREATE View [dbo].[vwIncident]
AS
/*----------------------------------------------

IncidentVw View

Description:
Provides row level authorisation for Incident data

Assocated items:
IncidentInsertTrigger

Implementation considerations:
To implement this view, rename the Incident table to "IncidentTbl" and rename this view to "Incident"
Applications should transparently use this view instead of the table.

Security considerations:
Access to the underlying Incident table should be restricted.
----------------------------------------------*/

-- Get all incidents within Level 2 as read-only
SELECT
  Code, IncidentID, IncidentStatus, StaffMemberTitle, StaffMemberOtherTitle, StaffMemberFirstName, StaffMemberLastName, JobRoleCode,
  StaffMemberBusinessCode, StaffMemberBusinessAreaCode, StaffMemberHomeOfficeCode, StaffMemberHomeOfficeSiteCode, IsStaffHadAppropriateTraining,
  StaffMemberYearsInCurrentPost, StaffMemberMonthsInCurrentRole, ManagerFirstName, ManagerLastName, IncidentDate, FiscalYear, FiscalQuarter,
  FiscalMonth, FiscalMonthAsText, IncidentTime, CustomerCode, IsOthersPresent, OthersPresent, EventLeadingToIncidentCode, IncidentLocationCode,
  IncidentCategoryCode, IncidentTypeCode, IncidentDetailsCode, AbuseTypeCode, IncidentNarrativeCode, IsLineManageFastTrack, IsLineManagerRIDDOR,
  IsPoliceCalled, HasLineManagerReadReport, LineManagerNarrativeCode, IsNominatedFastTrack, IsNominatedRIDDOR, IsOralWarning, OralWarningDate,
  IsWrittenWarning, WrittenWarningDate, IsAssailantInterviewed, AssailantInterviewedDate, IsSolicitorLetter, SolicitorLetterDate, IsBanningOrder,
  BanningOrderRequestedDate, FurtherInfoNarrativeCode, IsDeficienciesInProcess, IsImplementControlMeasures, NamedOfficer, TelephoneContactNumber,
  IsITMarkersSet, IsPapersMarked, IsNotifiedParties, ReviewDate, IsPreviousEvidenceReviewed, IsPreviousPartiesNotified, IsRepeatBehaviour,
  IsControlMeasuresStillApply, OrganisationCode, SiteCode, NumberOfRecords, RowIdentifier, DeficienciesNarrativeCode
FROM Incident i WHERE StaffMemberBusinessAreaCode IN
  (SELECT oh1.OrganisationCode
	  FROM StaffOrganisation so
	  INNER JOIN OrganisationHierarchy oh ON oh.OrganisationCode = so.OrganisationCode
	  INNER JOIN Organisation o ON oh.AncestorOrganisationCode = o.Code
	  INNER JOIN OrganisationType ot ON o.OrganisationTypeCode = ot.Code
	  INNER JOIN OrganisationTypeGroup otg ON ot.OrganisationTypeGroupCode = otg.Code
	  INNER JOIN OrganisationHierarchy oh1 ON oh1.AncestorOrganisationCode = o.Code
	  WHERE otg.Name = 'UCB'
	  AND ot.LevelNumber = 2
	  AND (CAST(so.StaffCode AS VARCHAR(36)) = SUBSTRING(CAST
		  ((SELECT     context_info
		     FROM         Master.dbo.sysprocesses
		     WHERE     (spid = @@SPID)) AS VARCHAR(128)), 0, 37)))
		AND EXISTS (SELECT 1 FROM StaffAttributes sa
		  INNER JOIN ApplicationAttribute aa ON sa.ApplicationAttributeCode = aa.Code
		  WHERE aa.AttributeName = 'UCB-EDIT' OR  aa.AttributeName = 'UCB-READ-ONLY'
		  AND (CAST(StaffCode AS VARCHAR(36)) = SUBSTRING(CAST
			  ((SELECT     context_info
			     FROM         Master.dbo.sysprocesses
			     WHERE     (spid = @@SPID)) AS VARCHAR(128)), 0, 37)))
	     
UNION

-- Get all incidents within Level 3 as update
SELECT 
  Code, IncidentID, IncidentStatus, StaffMemberTitle, StaffMemberOtherTitle, StaffMemberFirstName, StaffMemberLastName, JobRoleCode,
  StaffMemberBusinessCode, StaffMemberBusinessAreaCode, StaffMemberHomeOfficeCode, StaffMemberHomeOfficeSiteCode, IsStaffHadAppropriateTraining,
  StaffMemberYearsInCurrentPost, StaffMemberMonthsInCurrentRole, ManagerFirstName, ManagerLastName, IncidentDate, FiscalYear, FiscalQuarter,
  FiscalMonth, FiscalMonthAsText, IncidentTime, CustomerCode, IsOthersPresent, OthersPresent, EventLeadingToIncidentCode, IncidentLocationCode,
  IncidentCategoryCode, IncidentTypeCode, IncidentDetailsCode, AbuseTypeCode, IncidentNarrativeCode, IsLineManageFastTrack, IsLineManagerRIDDOR,
  IsPoliceCalled, HasLineManagerReadReport, LineManagerNarrativeCode, IsNominatedFastTrack, IsNominatedRIDDOR, IsOralWarning, OralWarningDate,
  IsWrittenWarning, WrittenWarningDate, IsAssailantInterviewed, AssailantInterviewedDate, IsSolicitorLetter, SolicitorLetterDate, IsBanningOrder,
  BanningOrderRequestedDate, FurtherInfoNarrativeCode, IsDeficienciesInProcess, IsImplementControlMeasures, NamedOfficer, TelephoneContactNumber,
  IsITMarkersSet, IsPapersMarked, IsNotifiedParties, ReviewDate, IsPreviousEvidenceReviewed, IsPreviousPartiesNotified, IsRepeatBehaviour,
  IsControlMeasuresStillApply, OrganisationCode, SiteCode, NumberOfRecords, RowIdentifier, DeficienciesNarrativeCode
FROM Incident i WHERE StaffMemberBusinessAreaCode IN
  (SELECT oh1.OrganisationCode
	  FROM StaffOrganisation so
	  INNER JOIN OrganisationHierarchy oh ON oh.OrganisationCode = so.OrganisationCode
	  INNER JOIN Organisation o ON oh.AncestorOrganisationCode = o.Code
	  INNER JOIN OrganisationType ot ON o.OrganisationTypeCode = ot.Code
	  INNER JOIN OrganisationTypeGroup otg ON ot.OrganisationTypeGroupCode = otg.Code
	  INNER JOIN OrganisationHierarchy oh1 ON oh1.AncestorOrganisationCode = o.Code
	  WHERE otg.Name = 'UCB'
	  AND ot.LevelNumber = 3
	  AND (CAST(so.StaffCode AS VARCHAR(36)) = SUBSTRING(CAST
		  ((SELECT     context_info
		     FROM         Master.dbo.sysprocesses
		     WHERE     (spid = @@SPID)) AS VARCHAR(128)), 0, 37)))
		AND EXISTS (SELECT 1 FROM StaffAttributes sa
		  INNER JOIN ApplicationAttribute aa ON sa.ApplicationAttributeCode = aa.Code
		  WHERE aa.AttributeName = 'UCB-BUSINESS-AREA-MANAGER'
		  AND (CAST(StaffCode AS VARCHAR(36)) = SUBSTRING(CAST
			  ((SELECT     context_info
			     FROM         Master.dbo.sysprocesses
			     WHERE     (spid = @@SPID)) AS VARCHAR(128)), 0, 37)))

UNION

-- Get all incidents for Nominated Manager and Deputy Nominated Manager as update
SELECT   
  i.Code, i.IncidentID, i.IncidentStatus, i.StaffMemberTitle, i.StaffMemberOtherTitle, i.StaffMemberFirstName, i.StaffMemberLastName, i.JobRoleCode,
  i.StaffMemberBusinessCode, i.StaffMemberBusinessAreaCode, i.StaffMemberHomeOfficeCode, i.StaffMemberHomeOfficeSiteCode, i.IsStaffHadAppropriateTraining,
  i.StaffMemberYearsInCurrentPost, i.StaffMemberMonthsInCurrentRole, i.ManagerFirstName, i.ManagerLastName, i.IncidentDate, i.FiscalYear, i.FiscalQuarter,
  i.FiscalMonth, i.FiscalMonthAsText, i.IncidentTime, i.CustomerCode, i.IsOthersPresent, i.OthersPresent, i.EventLeadingToIncidentCode, i.IncidentLocationCode,
  i.IncidentCategoryCode, i.IncidentTypeCode, i.IncidentDetailsCode, i.AbuseTypeCode, i.IncidentNarrativeCode, i.IsLineManageFastTrack, i.IsLineManagerRIDDOR,
  i.IsPoliceCalled, i.HasLineManagerReadReport, i.LineManagerNarrativeCode, i.IsNominatedFastTrack, i.IsNominatedRIDDOR, i.IsOralWarning, i.OralWarningDate,
  i.IsWrittenWarning, i.WrittenWarningDate, i.IsAssailantInterviewed, i.AssailantInterviewedDate, i.IsSolicitorLetter, i.SolicitorLetterDate, i.IsBanningOrder,
  i.BanningOrderRequestedDate, i.FurtherInfoNarrativeCode, i.IsDeficienciesInProcess, i.IsImplementControlMeasures, i.NamedOfficer, i.TelephoneContactNumber,
  i.IsITMarkersSet, i.IsPapersMarked, i.IsNotifiedParties, i.ReviewDate, i.IsPreviousEvidenceReviewed, i.IsPreviousPartiesNotified, i.IsRepeatBehaviour,
  i.IsControlMeasuresStillApply, i.OrganisationCode, i.SiteCode, i.NumberOfRecords, i.RowIdentifier, i.DeficienciesNarrativeCode
FROM Incident i
  INNER JOIN SiteStaff ss ON i.SiteCode = ss.SiteCode
  INNER JOIN Site s ON ss.SiteCode = s.Code
  WHERE 
  CAST(ss.StaffCode AS VARCHAR(36)) = SUBSTRING(CAST
    ((SELECT     context_info
       FROM         Master.dbo.sysprocesses
       WHERE     (spid = @@SPID)) AS VARCHAR(128)), 0, 37)

UNION

-- Get all incidents as read-only for Trade Union
SELECT
  i.Code, i.IncidentID, i.IncidentStatus, i.StaffMemberTitle, i.StaffMemberOtherTitle, i.StaffMemberFirstName, i.StaffMemberLastName, i.JobRoleCode,
  i.StaffMemberBusinessCode, i.StaffMemberBusinessAreaCode, i.StaffMemberHomeOfficeCode, i.StaffMemberHomeOfficeSiteCode, i.IsStaffHadAppropriateTraining,
  i.StaffMemberYearsInCurrentPost, i.StaffMemberMonthsInCurrentRole, i.ManagerFirstName, i.ManagerLastName, i.IncidentDate, i.FiscalYear, i.FiscalQuarter,
  i.FiscalMonth, i.FiscalMonthAsText, i.IncidentTime, i.CustomerCode, i.IsOthersPresent, i.OthersPresent, i.EventLeadingToIncidentCode, i.IncidentLocationCode,
  i.IncidentCategoryCode, i.IncidentTypeCode, i.IncidentDetailsCode, i.AbuseTypeCode, i.IncidentNarrativeCode, i.IsLineManageFastTrack, i.IsLineManagerRIDDOR,
  i.IsPoliceCalled, i.HasLineManagerReadReport, i.LineManagerNarrativeCode, i.IsNominatedFastTrack, i.IsNominatedRIDDOR, i.IsOralWarning, i.OralWarningDate,
  i.IsWrittenWarning, i.WrittenWarningDate, i.IsAssailantInterviewed, i.AssailantInterviewedDate, i.IsSolicitorLetter, i.SolicitorLetterDate, i.IsBanningOrder,
  i.BanningOrderRequestedDate, i.FurtherInfoNarrativeCode, i.IsDeficienciesInProcess, i.IsImplementControlMeasures, i.NamedOfficer, i.TelephoneContactNumber,
  i.IsITMarkersSet, i.IsPapersMarked, i.IsNotifiedParties, i.ReviewDate, i.IsPreviousEvidenceReviewed, i.IsPreviousPartiesNotified, i.IsRepeatBehaviour,
  i.IsControlMeasuresStillApply, i.OrganisationCode, i.SiteCode, i.NumberOfRecords, i.RowIdentifier, i.DeficienciesNarrativeCode
FROM Incident i
WHERE EXISTS
	(SELECT 1 FROM StaffAttributes sa
		INNER JOIN ApplicationAttribute aa ON sa.ApplicationAttributeCode = aa.Code
		WHERE aa.AttributeName = 'UCB-TRADE_UNION'
		AND (CAST(StaffCode AS VARCHAR(36)) = SUBSTRING(CAST
			((SELECT     context_info
			   FROM         Master.dbo.sysprocesses
			   WHERE     (spid = @@SPID)) AS VARCHAR(128)), 0, 37)))
			   
UNION

-- Get all incidents as update for Admin
SELECT
  i.Code, i.IncidentID, i.IncidentStatus, i.StaffMemberTitle, i.StaffMemberOtherTitle, i.StaffMemberFirstName, i.StaffMemberLastName, i.JobRoleCode,
  i.StaffMemberBusinessCode, i.StaffMemberBusinessAreaCode, i.StaffMemberHomeOfficeCode, i.StaffMemberHomeOfficeSiteCode, i.IsStaffHadAppropriateTraining,
  i.StaffMemberYearsInCurrentPost, i.StaffMemberMonthsInCurrentRole, i.ManagerFirstName, i.ManagerLastName, i.IncidentDate, i.FiscalYear, i.FiscalQuarter,
  i.FiscalMonth, i.FiscalMonthAsText, i.IncidentTime, i.CustomerCode, i.IsOthersPresent, i.OthersPresent, i.EventLeadingToIncidentCode, i.IncidentLocationCode,
  i.IncidentCategoryCode, i.IncidentTypeCode, i.IncidentDetailsCode, i.AbuseTypeCode, i.IncidentNarrativeCode, i.IsLineManageFastTrack, i.IsLineManagerRIDDOR,
  i.IsPoliceCalled, i.HasLineManagerReadReport, i.LineManagerNarrativeCode, i.IsNominatedFastTrack, i.IsNominatedRIDDOR, i.IsOralWarning, i.OralWarningDate,
  i.IsWrittenWarning, i.WrittenWarningDate, i.IsAssailantInterviewed, i.AssailantInterviewedDate, i.IsSolicitorLetter, i.SolicitorLetterDate, i.IsBanningOrder,
  i.BanningOrderRequestedDate, i.FurtherInfoNarrativeCode, i.IsDeficienciesInProcess, i.IsImplementControlMeasures, i.NamedOfficer, i.TelephoneContactNumber,
  i.IsITMarkersSet, i.IsPapersMarked, i.IsNotifiedParties, i.ReviewDate, i.IsPreviousEvidenceReviewed, i.IsPreviousPartiesNotified, i.IsRepeatBehaviour,
  i.IsControlMeasuresStillApply, i.OrganisationCode, i.SiteCode, i.NumberOfRecords, i.RowIdentifier, i.DeficienciesNarrativeCode
FROM Incident i
WHERE EXISTS 
	(SELECT 1 FROM StaffAttributes sa
		INNER JOIN ApplicationAttribute aa ON sa.ApplicationAttributeCode = aa.Code
		WHERE aa.AttributeName = 'UCB-ADMIN'
		AND (CAST(StaffCode AS VARCHAR(36)) = SUBSTRING(CAST
			((SELECT     context_info
			   FROM         Master.dbo.sysprocesses
			   WHERE     (spid = @@SPID)) AS VARCHAR(128)), 0, 37)))

GO


