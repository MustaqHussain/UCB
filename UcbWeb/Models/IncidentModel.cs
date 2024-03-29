﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace UcbWeb.Models
{
    public partial class IncidentModel : BaseModel
    {

        public virtual System.Guid Code
        {
            get { return _code; }
            set { _code = value; }
        }
        private System.Guid _code;

        public virtual int IncidentID
        {
            get { return _incidentID; }
            set { _incidentID = value; }
        }
        private int _incidentID;

        public virtual string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private string _type;

        public virtual Nullable<System.Guid> ReferrerCode
        {
            get { return _referrerCode; }
            set { _referrerCode = value; }
        }
        private Nullable<System.Guid> _referrerCode;

        public virtual string IncidentStatus
        {
            get { return _incidentStatus; }
            set { _incidentStatus = value; }
        }
        private string _incidentStatus;

        public virtual string StaffMemberTitle
        {
            get { return _staffMemberTitle; }
            set { _staffMemberTitle = value; }
        }
        private string _staffMemberTitle;

        public virtual string StaffMemberOtherTitle
        {
            get { return _staffMemberOtherTitle; }
            set { _staffMemberOtherTitle = value; }
        }
        private string _staffMemberOtherTitle;

        public virtual string StaffMemberFirstName
        {
            get { return _staffMemberFirstName; }
            set { _staffMemberFirstName = value; }
        }
        private string _staffMemberFirstName;

        public virtual string StaffMemberLastName
        {
            get { return _staffMemberLastName; }
            set { _staffMemberLastName = value; }
        }
        private string _staffMemberLastName;

        public virtual Nullable<System.Guid> JobRoleCode
        {
            get { return _jobRoleCode; }
            set { _jobRoleCode = value; }
        }
        private Nullable<System.Guid> _jobRoleCode;

        public virtual System.Guid StaffMemberBusinessCode
        {
            get { return _staffMemberBusinessCode; }
            set { _staffMemberBusinessCode = value; }
        }
        private System.Guid _staffMemberBusinessCode;

        public virtual System.Guid StaffMemberBusinessAreaCode
        {
            get { return _staffMemberBusinessAreaCode; }
            set { _staffMemberBusinessAreaCode = value; }
        }
        private System.Guid _staffMemberBusinessAreaCode;

        public virtual System.Guid StaffMemberHomeOfficeCode
        {
            get { return _staffMemberHomeOfficeCode; }
            set { _staffMemberHomeOfficeCode = value; }
        }
        private System.Guid _staffMemberHomeOfficeCode;

        public virtual System.Guid StaffMemberHomeOfficeSiteCode
        {
            get { return _staffMemberHomeOfficeSiteCode; }
            set { _staffMemberHomeOfficeSiteCode = value; }
        }
        private System.Guid _staffMemberHomeOfficeSiteCode;

        public virtual string IsStaffHadAppropriateTraining
        {
            get { return _isStaffHadAppropriateTraining; }
            set { _isStaffHadAppropriateTraining = value; }
        }
        private string _isStaffHadAppropriateTraining;

        public virtual Nullable<int> StaffMemberYearsInCurrentPost
        {
            get { return _staffMemberYearsInCurrentPost; }
            set { _staffMemberYearsInCurrentPost = value; }
        }
        private Nullable<int> _staffMemberYearsInCurrentPost;

        public virtual Nullable<int> StaffMemberMonthsInCurrentRole
        {
            get { return _staffMemberMonthsInCurrentRole; }
            set { _staffMemberMonthsInCurrentRole = value; }
        }
        private Nullable<int> _staffMemberMonthsInCurrentRole;

        public virtual string ManagerFirstName
        {
            get { return _managerFirstName; }
            set { _managerFirstName = value; }
        }
        private string _managerFirstName;

        public virtual string ManagerLastName
        {
            get { return _managerLastName; }
            set { _managerLastName = value; }
        }
        private string _managerLastName;

        public virtual string ManagerTelephoneContactNumber
        {
            get { return _managerTelephoneContactNumber; }
            set { _managerTelephoneContactNumber = value; }
        }
        private string _managerTelephoneContactNumber;

        public virtual System.DateTime IncidentDate
        {
            get { return _incidentDate; }
            set { _incidentDate = value; }
        }
        private System.DateTime _incidentDate;

        public virtual int FiscalYear
        {
            get { return _fiscalYear; }
            set { _fiscalYear = value; }
        }
        private int _fiscalYear;

        public virtual int FiscalQuarter
        {
            get { return _fiscalQuarter; }
            set { _fiscalQuarter = value; }
        }
        private int _fiscalQuarter;

        public virtual int FiscalMonth
        {
            get { return _fiscalMonth; }
            set { _fiscalMonth = value; }
        }
        private int _fiscalMonth;

        public virtual string FiscalMonthAsText
        {
            get { return _fiscalMonthAsText; }
            set { _fiscalMonthAsText = value; }
        }
        private string _fiscalMonthAsText;

        public virtual string IncidentTime
        {
            get { return _incidentTime; }
            set { _incidentTime = value; }
        }
        private string _incidentTime;

        public virtual Nullable<System.Guid> CustomerCode
        {
            get { return _customerCode; }
            set { _customerCode = value; }
        }
        private Nullable<System.Guid> _customerCode;

        public virtual bool IsOthersPresent
        {
            get { return _isOthersPresent; }
            set { _isOthersPresent = value; }
        }
        private bool _isOthersPresent;

        public virtual string OthersPresent
        {
            get { return _othersPresent; }
            set { _othersPresent = value; }
        }
        private string _othersPresent;

        public virtual Nullable<System.Guid> EventLeadingToIncidentCode
        {
            get { return _eventLeadingToIncidentCode; }
            set { _eventLeadingToIncidentCode = value; }
        }
        private Nullable<System.Guid> _eventLeadingToIncidentCode;

        public virtual Nullable<System.Guid> IncidentLocationCode
        {
            get { return _incidentLocationCode; }
            set { _incidentLocationCode = value; }
        }
        private Nullable<System.Guid> _incidentLocationCode;

        public virtual System.Guid IncidentCategoryCode
        {
            get { return _incidentCategoryCode; }
            set { _incidentCategoryCode = value; }
        }
        private System.Guid _incidentCategoryCode;

        public virtual Nullable<System.Guid> IncidentTypeCode
        {
            get { return _incidentTypeCode; }
            set { _incidentTypeCode = value; }
        }
        private Nullable<System.Guid> _incidentTypeCode;

        public virtual Nullable<System.Guid> IncidentDetailsCode
        {
            get { return _incidentDetailsCode; }
            set { _incidentDetailsCode = value; }
        }
        private Nullable<System.Guid> _incidentDetailsCode;

        public virtual Nullable<System.Guid> AbuseTypeCode
        {
            get { return _abuseTypeCode; }
            set { _abuseTypeCode = value; }
        }
        private Nullable<System.Guid> _abuseTypeCode;

        public virtual Nullable<System.Guid> IncidentNarrativeCode
        {
            get { return _incidentNarrativeCode; }
            set { _incidentNarrativeCode = value; }
        }
        private Nullable<System.Guid> _incidentNarrativeCode;

        public virtual Nullable<bool> IsLineManageFastTrack
        {
            get { return _isLineManageFastTrack; }
            set { _isLineManageFastTrack = value; }
        }
        private Nullable<bool> _isLineManageFastTrack;

        public virtual Nullable<bool> IsLineManagerRIDDOR
        {
            get { return _isLineManagerRIDDOR; }
            set { _isLineManagerRIDDOR = value; }
        }
        private Nullable<bool> _isLineManagerRIDDOR;

        public virtual Nullable<bool> IsPoliceCalled
        {
            get { return _isPoliceCalled; }
            set { _isPoliceCalled = value; }
        }
        private Nullable<bool> _isPoliceCalled;

        public virtual Nullable<bool> HasLineManagerReadReport
        {
            get { return _hasLineManagerReadReport; }
            set { _hasLineManagerReadReport = value; }
        }
        private Nullable<bool> _hasLineManagerReadReport;

        public virtual Nullable<System.Guid> LineManagerNarrativeCode
        {
            get { return _lineManagerNarrativeCode; }
            set { _lineManagerNarrativeCode = value; }
        }
        private Nullable<System.Guid> _lineManagerNarrativeCode;

        public virtual Nullable<bool> IsNominatedFastTrack
        {
            get { return _isNominatedFastTrack; }
            set { _isNominatedFastTrack = value; }
        }
        private Nullable<bool> _isNominatedFastTrack;

        public virtual Nullable<bool> IsNominatedRIDDOR
        {
            get { return _isNominatedRIDDOR; }
            set { _isNominatedRIDDOR = value; }
        }
        private Nullable<bool> _isNominatedRIDDOR;

        public virtual Nullable<bool> IsNominatedReferralEvidence
        {
            get { return _isNominatedReferralEvidence; }
            set { _isNominatedReferralEvidence = value; }
        }
        private Nullable<bool> _isNominatedReferralEvidence;

        public virtual Nullable<bool> IsOralWarning
        {
            get { return _isOralWarning; }
            set { _isOralWarning = value; }
        }
        private Nullable<bool> _isOralWarning;

        public virtual Nullable<System.DateTime> OralWarningDate
        {
            get { return _oralWarningDate; }
            set { _oralWarningDate = value; }
        }
        private Nullable<System.DateTime> _oralWarningDate;

        public virtual Nullable<bool> IsWrittenWarning
        {
            get { return _isWrittenWarning; }
            set { _isWrittenWarning = value; }
        }
        private Nullable<bool> _isWrittenWarning;

        public virtual Nullable<System.DateTime> WrittenWarningDate
        {
            get { return _writtenWarningDate; }
            set { _writtenWarningDate = value; }
        }
        private Nullable<System.DateTime> _writtenWarningDate;

        public virtual Nullable<bool> IsAssailantInterviewed
        {
            get { return _isAssailantInterviewed; }
            set { _isAssailantInterviewed = value; }
        }
        private Nullable<bool> _isAssailantInterviewed;

        public virtual Nullable<System.DateTime> AssailantInterviewedDate
        {
            get { return _assailantInterviewedDate; }
            set { _assailantInterviewedDate = value; }
        }
        private Nullable<System.DateTime> _assailantInterviewedDate;

        public virtual Nullable<bool> IsSolicitorLetter
        {
            get { return _isSolicitorLetter; }
            set { _isSolicitorLetter = value; }
        }
        private Nullable<bool> _isSolicitorLetter;

        public virtual Nullable<System.DateTime> SolicitorLetterDate
        {
            get { return _solicitorLetterDate; }
            set { _solicitorLetterDate = value; }
        }
        private Nullable<System.DateTime> _solicitorLetterDate;

        public virtual Nullable<bool> IsBanningOrder
        {
            get { return _isBanningOrder; }
            set { _isBanningOrder = value; }
        }
        private Nullable<bool> _isBanningOrder;

        public virtual Nullable<System.DateTime> BanningOrderRequestedDate
        {
            get { return _banningOrderRequestedDate; }
            set { _banningOrderRequestedDate = value; }
        }
        private Nullable<System.DateTime> _banningOrderRequestedDate;

        public virtual Nullable<System.Guid> FurtherInfoNarrativeCode
        {
            get { return _furtherInfoNarrativeCode; }
            set { _furtherInfoNarrativeCode = value; }
        }
        private Nullable<System.Guid> _furtherInfoNarrativeCode;

        public virtual Nullable<bool> IsDeficienciesInProcess
        {
            get { return _isDeficienciesInProcess; }
            set { _isDeficienciesInProcess = value; }
        }
        private Nullable<bool> _isDeficienciesInProcess;

        public virtual Nullable<bool> IsImplementControlMeasures
        {
            get { return _isImplementControlMeasures; }
            set { _isImplementControlMeasures = value; }
        }
        private Nullable<bool> _isImplementControlMeasures;

        public virtual string BannedFromOffices
        {
            get { return _bannedFromOffices; }
            set { _bannedFromOffices = value; }
        }
        private string _bannedFromOffices;

        public virtual string BannedFromOfficesEndDate
        {
            get { return _bannedFromOfficesEndDate; }
            set { _bannedFromOfficesEndDate = value; }
        }
        private string _bannedFromOfficesEndDate;

        public virtual string NamedOfficer
        {
            get { return _namedOfficer; }
            set { _namedOfficer = value; }
        }
        private string _namedOfficer;

        public virtual string TelephoneContactNumber
        {
            get { return _telephoneContactNumber; }
            set { _telephoneContactNumber = value; }
        }
        private string _telephoneContactNumber;

        public virtual Nullable<bool> IsITMarkersSet
        {
            get { return _isITMarkersSet; }
            set { _isITMarkersSet = value; }
        }
        private Nullable<bool> _isITMarkersSet;

        public virtual Nullable<bool> IsPapersMarked
        {
            get { return _isPapersMarked; }
            set { _isPapersMarked = value; }
        }
        private Nullable<bool> _isPapersMarked;

        public virtual Nullable<bool> IsNotifiedParties
        {
            get { return _isNotifiedParties; }
            set { _isNotifiedParties = value; }
        }
        private Nullable<bool> _isNotifiedParties;

        public virtual Nullable<System.DateTime> ReviewDate
        {
            get { return _reviewDate; }
            set { _reviewDate = value; }
        }
        private Nullable<System.DateTime> _reviewDate;

        public Nullable<System.Guid> CurrentOwnerStaffCode
        {
            get { return _currentOwnerStaffCode; }
            set { _currentOwnerStaffCode = value; }
        }
        private Nullable<System.Guid> _currentOwnerStaffCode;

        public virtual Nullable<bool> IsPreviousEvidenceReviewed
        {
            get { return _isPreviousEvidenceReviewed; }
            set { _isPreviousEvidenceReviewed = value; }
        }
        private Nullable<bool> _isPreviousEvidenceReviewed;

        public virtual Nullable<bool> IsPreviousPartiesNotified
        {
            get { return _isPreviousPartiesNotified; }
            set { _isPreviousPartiesNotified = value; }
        }
        private Nullable<bool> _isPreviousPartiesNotified;

        public virtual Nullable<bool> IsRepeatBehaviour
        {
            get { return _isRepeatBehaviour; }
            set { _isRepeatBehaviour = value; }
        }
        private Nullable<bool> _isRepeatBehaviour;

        public virtual Nullable<bool> IsControlMeasuresStillApply
        {
            get { return _isControlMeasuresStillApply; }
            set { _isControlMeasuresStillApply = value; }
        }
        private Nullable<bool> _isControlMeasuresStillApply;

        public virtual System.Guid OrganisationCode
        {
            get { return _organisationCode; }
            set { _organisationCode = value; }
        }
        private System.Guid _organisationCode;

        public virtual System.Guid SiteCode
        {
            get { return _siteCode; }
            set { _siteCode = value; }
        }
        private System.Guid _siteCode;

        public virtual Nullable<int> NumberOfRecords
        {
            get { return _numberOfRecords; }
            set { _numberOfRecords = value; }
        }
        private Nullable<int> _numberOfRecords;

        public virtual byte[] RowIdentifier
        {
            get { return _rowIdentifier; }
            set { _rowIdentifier = value; }
        }
        private byte[] _rowIdentifier;

        public virtual Nullable<System.Guid> DeficienciesNarrativeCode
        {
            get { return _deficienciesNarrativeCode; }
            set { _deficienciesNarrativeCode = value; }
        }
        private Nullable<System.Guid> _deficienciesNarrativeCode;

        public virtual Nullable<System.Guid> ReviewActionNarrativeCode
        {
            get { return _reviewActionNarrativeCode; }
            set { _reviewActionNarrativeCode = value; }
        }
        private Nullable<System.Guid> _reviewActionNarrativeCode;
                
        public virtual string OtherIncidentLocation
        {
            get { return _otherIncidentLocation; }
            set { _otherIncidentLocation = value; }
        }
        private string _otherIncidentLocation;
    }
}
