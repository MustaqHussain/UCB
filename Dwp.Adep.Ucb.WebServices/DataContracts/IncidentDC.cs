/*
* All links to organisation removed from the database (staffmemberbusinesscode, staffmemberbusinessareacode, staffmemberhomeofficecode & organisationcode).
* They will now be derived in the getincident service call so we need to keep them in the IncidentDC, hence why this has been removed outisde the DataContracts tt file.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace Dwp.Adep.Ucb.WebServices.DataContracts
{
    [DataContract]
    public partial class IncidentDC
    {

        [DataMember]
        public System.Guid Code
        {
            get;
            set;
        }

        [DataMember]
        public int IncidentID
        {
            get;
            set;
        }

        [DataMember]
        public string Type
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.Guid> ReferrerCode
        {
            get;
            set;
        }

        [DataMember]
        public string IncidentStatus
        {
            get;
            set;
        }

        [DataMember]
        public string StaffMemberTitle
        {
            get;
            set;
        }

        [DataMember]
        public string StaffMemberOtherTitle
        {
            get;
            set;
        }

        [DataMember]
        public string StaffMemberFirstName
        {
            get;
            set;
        }

        [DataMember]
        public string StaffMemberLastName
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.Guid> JobRoleCode
        {
            get;
            set;
        }

        [DataMember]
        public System.Guid StaffMemberBusinessCode
        {
            get;
            set;
        }

        [DataMember]
        public System.Guid StaffMemberBusinessAreaCode
        {
            get;
            set;
        }

        [DataMember]
        public System.Guid StaffMemberHomeOfficeCode
        {
            get;
            set;
        }

        [DataMember]
        public System.Guid StaffMemberHomeOfficeSiteCode
        {
            get;
            set;
        }

        [DataMember]
        public string IsStaffHadAppropriateTraining
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<int> StaffMemberYearsInCurrentPost
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<int> StaffMemberMonthsInCurrentRole
        {
            get;
            set;
        }

        [DataMember]
        public string ManagerFirstName
        {
            get;
            set;
        }

        [DataMember]
        public string ManagerTelephoneContactNumber
        {
            get;
            set;
        }

        [DataMember]
        public string ManagerLastName
        {
            get;
            set;
        }

        [DataMember]
        public System.DateTime IncidentDate
        {
            get;
            set;
        }

        [DataMember]
        public int FiscalYear
        {
            get;
            set;
        }

        [DataMember]
        public int FiscalQuarter
        {
            get;
            set;
        }

        [DataMember]
        public int FiscalMonth
        {
            get;
            set;
        }

        [DataMember]
        public string FiscalMonthAsText
        {
            get;
            set;
        }

        [DataMember]
        public string IncidentTime
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.Guid> CustomerCode
        {
            get;
            set;
        }

        [DataMember]
        public bool IsOthersPresent
        {
            get;
            set;
        }

        [DataMember]
        public string OthersPresent
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.Guid> EventLeadingToIncidentCode
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.Guid> IncidentLocationCode
        {
            get;
            set;
        }

        [DataMember]
        public System.Guid IncidentCategoryCode
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.Guid> IncidentTypeCode
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.Guid> IncidentDetailsCode
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.Guid> AbuseTypeCode
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.Guid> IncidentNarrativeCode
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsLineManageFastTrack
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsLineManagerRIDDOR
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsPoliceCalled
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> HasLineManagerReadReport
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.Guid> LineManagerNarrativeCode
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsNominatedFastTrack
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsNominatedRIDDOR
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsOralWarning
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.DateTime> OralWarningDate
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsWrittenWarning
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.DateTime> WrittenWarningDate
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsAssailantInterviewed
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.DateTime> AssailantInterviewedDate
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsSolicitorLetter
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.DateTime> SolicitorLetterDate
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsBanningOrder
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.DateTime> BanningOrderRequestedDate
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.Guid> FurtherInfoNarrativeCode
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsDeficienciesInProcess
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsImplementControlMeasures
        {
            get;
            set;
        }

        [DataMember]
        public string BannedFromOffices
        {
            get;
            set;
        }

        [DataMember]
        public string BannedFromOfficesEndDate
        {
            get;
            set;
        }

        [DataMember]
        public string NamedOfficer
        {
            get;
            set;
        }

        [DataMember]
        public string TelephoneContactNumber
        {
            get;
            set;
        }


        [DataMember]
        public Nullable<bool> IsITMarkersSet
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsPapersMarked
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsNotifiedParties
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.DateTime> ReviewDate
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsPreviousEvidenceReviewed
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsPreviousPartiesNotified
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsRepeatBehaviour
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<bool> IsControlMeasuresStillApply
        {
            get;
            set;
        }

        [DataMember]
        public System.Guid OrganisationCode
        {
            get;
            set;
        }

        [DataMember]
        public System.Guid SiteCode
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<int> NumberOfRecords
        {
            get;
            set;
        }

        [DataMember]
        public byte[] RowIdentifier
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.Guid> DeficienciesNarrativeCode
        {
            get;
            set;
        }

        [DataMember]
        public Nullable<System.Guid> ReviewActionNarrativeCode
        {
            get;
            set;
        }

        [DataMember]
        public string OtherIncidentLocation { get; set; }

        [DataMember]
        public Nullable<System.Guid> CurrentOwnerStaffCode
        {
            get;
            set;
        }
    }
}
