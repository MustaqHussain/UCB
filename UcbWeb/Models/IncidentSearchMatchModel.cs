//------------------------------------------------------------------------------
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
    public partial class IncidentSearchMatchModel
    {
    
        public virtual System.Guid Code
        {
    	    get;
            set;
        }
    
        public virtual int IncidentID
        {
    	    get;
            set;
        }
    
        public virtual string Type
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.Guid> ReferrerCode
        {
    	    get;
            set;
        }
    
        public virtual string Referrer
        {
    	    get;
            set;
        }
    
        public virtual string IncidentStatus
        {
    	    get;
            set;
        }
    
        public virtual string StaffMemberTitle
        {
    	    get;
            set;
        }
    
        public virtual string StaffMemberOtherTitle
        {
    	    get;
            set;
        }
    
        public virtual string StaffMemberFirstName
        {
    	    get;
            set;
        }
    
        public virtual string StaffMemberLastName
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.Guid> JobRoleCode
        {
    	    get;
            set;
        }
    
        public virtual string JobRole
        {
    	    get;
            set;
        }
    
        public virtual System.Guid StaffMemberHomeOfficeSiteCode
        {
    	    get;
            set;
        }
    
        public virtual string StaffMemberHomeOfficeSite
        {
    	    get;
            set;
        }
    
        public virtual string IsStaffHadAppropriateTraining
        {
    	    get;
            set;
        }
    
        public virtual Nullable<int> StaffMemberYearsInCurrentPost
        {
    	    get;
            set;
        }
    
        public virtual Nullable<int> StaffMemberMonthsInCurrentRole
        {
    	    get;
            set;
        }
    
        public virtual string ManagerFirstName
        {
    	    get;
            set;
        }
    
        public virtual string ManagerLastName
        {
    	    get;
            set;
        }
    
        public virtual System.DateTime IncidentDate
        {
    	    get;
            set;
        }
    
        public virtual int FiscalYear
        {
    	    get;
            set;
        }
    
        public virtual int FiscalQuarter
        {
    	    get;
            set;
        }
    
        public virtual int FiscalMonth
        {
    	    get;
            set;
        }
    
        public virtual string FiscalMonthAsText
        {
    	    get;
            set;
        }
    
        public virtual string IncidentTime
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.Guid> CustomerCode
        {
    	    get;
            set;
        }
    
        public virtual string Customer
        {
    	    get;
            set;
        }
    
        public virtual bool IsOthersPresent
        {
    	    get;
            set;
        }
    
        public virtual string OthersPresent
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.Guid> EventLeadingToIncidentCode
        {
    	    get;
            set;
        }
    
        public virtual string EventLeadingToIncident
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.Guid> IncidentLocationCode
        {
    	    get;
            set;
        }
    
        public virtual string IncidentLocation
        {
    	    get;
            set;
        }
    
        public virtual System.Guid IncidentCategoryCode
        {
    	    get;
            set;
        }
    
        public virtual string IncidentCategory
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.Guid> IncidentTypeCode
        {
    	    get;
            set;
        }
    
        public virtual string IncidentType
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.Guid> IncidentDetailsCode
        {
    	    get;
            set;
        }
    
        public virtual string IncidentDetails
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.Guid> AbuseTypeCode
        {
    	    get;
            set;
        }
    
        public virtual string AbuseType
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.Guid> IncidentNarrativeCode
        {
    	    get;
            set;
        }
    
        public virtual string IncidentNarrative
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsLineManageFastTrack
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsLineManagerRIDDOR
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsPoliceCalled
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> HasLineManagerReadReport
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.Guid> LineManagerNarrativeCode
        {
    	    get;
            set;
        }
    
        public virtual string LineManagerNarrative
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsNominatedFastTrack
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsNominatedRIDDOR
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsOralWarning
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.DateTime> OralWarningDate
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsWrittenWarning
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.DateTime> WrittenWarningDate
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsAssailantInterviewed
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.DateTime> AssailantInterviewedDate
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsSolicitorLetter
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.DateTime> SolicitorLetterDate
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsBanningOrder
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.DateTime> BanningOrderRequestedDate
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.Guid> FurtherInfoNarrativeCode
        {
    	    get;
            set;
        }
    
        public virtual string FurtherInfoNarrative
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsDeficienciesInProcess
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsImplementControlMeasures
        {
    	    get;
            set;
        }
    
        public virtual string NamedOfficer
        {
    	    get;
            set;
        }
    
        public virtual string TelephoneContactNumber
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsITMarkersSet
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsPapersMarked
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsNotifiedParties
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.DateTime> ReviewDate
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsPreviousEvidenceReviewed
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsPreviousPartiesNotified
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsRepeatBehaviour
        {
    	    get;
            set;
        }
    
        public virtual Nullable<bool> IsControlMeasuresStillApply
        {
    	    get;
            set;
        }
    
        public virtual System.Guid SiteCode
        {
    	    get;
            set;
        }
    
        public virtual string Site
        {
    	    get;
            set;
        }
    
        public virtual Nullable<int> NumberOfRecords
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.Guid> DeficienciesNarrativeCode
        {
    	    get;
            set;
        }
    
        public virtual string DeficienciesNarrative
        {
    	    get;
            set;
        }
    
        public virtual string OtherIncidentLocation
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.Guid> CurrentOwnerStaffCode
        {
    	    get;
            set;
        }
    
        public virtual string CurrentOwnerStaff
        {
    	    get;
            set;
        }
    
        public virtual string ManagerTelephoneContactNumber
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.Guid> ReviewActionNarrativeCode
        {
    	    get;
            set;
        }
    
        public virtual string ReviewActionNarrative
        {
    	    get;
            set;
        }
    
        public virtual string BannedFromOffices
        {
    	    get;
            set;
        }
    
        public virtual string BannedFromOfficesEndDate
        {
    	    get;
            set;
        }
    }
}
