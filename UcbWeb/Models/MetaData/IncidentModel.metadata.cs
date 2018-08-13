using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using Dwp.Adep.Ucb.ResourceLibrary;
using UcbWeb.DataAnnotation;

namespace UcbWeb.Models
{
    [MetadataTypeAttribute(typeof(IncidentModel.IncidentModelMetadata))]
    public partial class IncidentModel
    {
        public partial class IncidentModelMetadata
        {
            #region Incident Fields
            [Key]
             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_INCIDENT_CODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_CODE", ResourceType = typeof(Resources))]
            public System.Guid Code { get; set; }

             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_INCIDENT_INCIDENTID", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_INCIDENTID", ResourceType = typeof(Resources))]
            public int IncidentID { get; set; }

             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [StringLength(50)]
            [Tooltip("TOOLTIP_INCIDENT_INCIDENTSTATUS", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_INCIDENTSTATUS", ResourceType = typeof(Resources))]
            public string IncidentStatus { get; set; }


            [Tooltip("TOOLTIP_REFERRAL_REFERRERCODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_REFERRAL_REFERRERCODE", ResourceType = typeof(Resources))]
            public Guid? ReferrerCode { get; set; }
      
             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [StringLength(50)]
            [Tooltip("TOOLTIP_INCIDENT_STAFFMEMBERTITLE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_STAFFMEMBERTITLE", ResourceType = typeof(Resources))]         
            public string StaffMemberTitle { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_INCIDENT_STAFFMEMBEROTHERTITLE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_STAFFMEMBEROTHERTITLE", ResourceType = typeof(Resources))]
            [RequiredIf("StaffMemberTitle", "Other")]
            public string StaffMemberOtherTitle { get; set; }

             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [StringLength(50)]
            [Tooltip("TOOLTIP_INCIDENT_STAFFMEMBERFIRSTNAME", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_STAFFMEMBERFIRSTNAME", ResourceType = typeof(Resources))]
            [RegularExpression("^[a-zA-Z0-9 .\'-]*$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
            public string StaffMemberFirstName { get; set; }

             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [StringLength(50)]
            [Tooltip("TOOLTIP_INCIDENT_STAFFMEMBERLASTNAME", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_STAFFMEMBERLASTNAME", ResourceType = typeof(Resources))]

            [RegularExpression("^[a-zA-Z0-9 .\'-]*$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
            public string StaffMemberLastName { get; set; }

             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_INCIDENT_JOBROLECODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_JOBROLECODE", ResourceType = typeof(Resources))]
            public System.Guid JobRoleCode { get; set; }

             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_INCIDENT_STAFFMEMBERBUSINESSCODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_STAFFMEMBERBUSINESSCODE", ResourceType = typeof(Resources))]
            public System.Guid StaffMemberBusinessCode { get; set; }

             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_INCIDENT_STAFFMEMBERBUSINESSAREACODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_STAFFMEMBERBUSINESSAREACODE", ResourceType = typeof(Resources))]
            public System.Guid StaffMemberBusinessAreaCode { get; set; }

             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_INCIDENT_STAFFMEMBERHOMEOFFICECODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_STAFFMEMBERHOMEOFFICECODE", ResourceType = typeof(Resources))]
            public System.Guid StaffMemberHomeOfficeCode { get; set; }

             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
             [Tooltip("TOOLTIP_INCIDENT_STAFFMEMBERHOMEOFFICESITECODE", ResourceType = typeof(Resources))]
             [Display(Name = "LABEL_INCIDENT_STAFFMEMBERHOMEOFFICESITECODE", ResourceType = typeof(Resources))]
             public System.Guid StaffMemberHomeOfficeSiteCode { get; set; }

             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_INCIDENT_ISSTAFFHADAPPROPRIATETRAINING", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISSTAFFHADAPPROPRIATETRAINING", ResourceType = typeof(Resources))]
            public string IsStaffHadAppropriateTraining { get; set; }

            [Range(0,50)]
            [Tooltip("TOOLTIP_INCIDENT_STAFFMEMBERYEARSINCURRENTPOST", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_STAFFMEMBERYEARSINCURRENTPOST", ResourceType = typeof(Resources))]
            public Nullable<int> StaffMemberYearsInCurrentPost { get; set; }

            [Range(0,11)]
            [Tooltip("TOOLTIP_INCIDENT_STAFFMEMBERMONTHSINCURRENTROLE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_STAFFMEMBERMONTHSINCURRENTROLE", ResourceType = typeof(Resources))]
            public Nullable<int> StaffMemberMonthsInCurrentRole { get; set; }

             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [StringLength(50)]
            [Tooltip("TOOLTIP_INCIDENT_MANAGERFIRSTNAME", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_MANAGERFIRSTNAME", ResourceType = typeof(Resources))]
            [RegularExpression("^[a-zA-Z0-9 .\'-]*$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
            public string ManagerFirstName { get; set; }

             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [StringLength(50)]
            [Tooltip("TOOLTIP_INCIDENT_MANAGERLASTNAME", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_MANAGERLASTNAME", ResourceType = typeof(Resources))]
            [RegularExpression("^[a-zA-Z0-9 .\'-]*$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
            public string ManagerLastName { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_INCIDENT_MANAGERTELEPHONECONTACTNUMBER", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_MANAGERTELEPHONECONTACTNUMBER", ResourceType = typeof(Resources))]
            [RegularExpression("^[0-9 ]*$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
            public string ManagerTelephoneContactNumber { get; set; }
             
            [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_INCIDENT_INCIDENTDATE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_INCIDENTDATE", ResourceType = typeof(Resources))]
            [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
            public Nullable<System.DateTime> IncidentDate { get; set; }

            [RegularExpression("^([01]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_INCIDENT_INCIDENTTIME", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_INCIDENTTIME", ResourceType = typeof(Resources))]
            [StringLength(5)]
            public string IncidentTime { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_CUSTOMERCODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_CUSTOMERCODE", ResourceType = typeof(Resources))]
            public Nullable<System.Guid> CustomerCode { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISOTHERSPRESENT", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISOTHERSPRESENT", ResourceType = typeof(Resources))]
            public Nullable<bool> IsOthersPresent { get; set; }

            [StringLength(250)]
            [Tooltip("TOOLTIP_INCIDENT_OTHERSPRESENT", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_OTHERSPRESENT", ResourceType = typeof(Resources))]
            public string OthersPresent { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_EVENTLEADINGTOINCIDENTCODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_EVENTLEADINGTOINCIDENTCODE", ResourceType = typeof(Resources))]
            public Nullable<System.Guid> EventLeadingToIncidentCode { get; set; }

             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_INCIDENT_INCIDENTLOCATIONCODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_INCIDENTLOCATIONCODE", ResourceType = typeof(Resources))]
            public System.Guid IncidentLocationCode { get; set; }

             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_INCIDENT_INCIDENTCATEGORYCODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_INCIDENTCATEGORYCODE", ResourceType = typeof(Resources))]
            public System.Guid IncidentCategoryCode { get; set; }

             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_INCIDENT_INCIDENTTYPECODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_INCIDENTTYPECODE", ResourceType = typeof(Resources))]
            public System.Guid IncidentTypeCode { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_INCIDENTDETAILSCODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_INCIDENTDETAILSCODE", ResourceType = typeof(Resources))]
            public Nullable<System.Guid> IncidentDetailsCode { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ABUSETYPECODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ABUSETYPECODE", ResourceType = typeof(Resources))]
            public Nullable<System.Guid> AbuseTypeCode { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISLINEMANAGEFASTTRACK", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISLINEMANAGEFASTTRACK", ResourceType = typeof(Resources))]
            public bool IsLineManageFastTrack { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISLINEMANAGERRIDDOR", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISLINEMANAGERRIDDOR", ResourceType = typeof(Resources))]
            public bool IsLineManagerRIDDOR { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISPOLICECALLED", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISPOLICECALLED", ResourceType = typeof(Resources))]
            public bool IsPoliceCalled { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_HASLINEMANAGERREADREPORT", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_HASLINEMANAGERREADREPORT", ResourceType = typeof(Resources))]
            public bool HasLineManagerReadReport { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_LINEMANAGERNARRATIVECODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_LINEMANAGERNARRATIVECODE", ResourceType = typeof(Resources))]
            public Nullable<System.Guid> LineManagerNarrativeCode { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISNOMINATEDFASTTRACK", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISNOMINATEDFASTTRACK", ResourceType = typeof(Resources))]
            public bool IsNominatedFastTrack { get; set; }

            //[Tooltip("TOOLTIP_INCIDENT_FASTTRACKATTACHMENTCODE", ResourceType = typeof(Resources))]
            //[Display(Name = "LABEL_INCIDENT_FASTTRACKATTACHMENTCODE", ResourceType = typeof(Resources))]
            //public Nullable<System.Guid> FastTrackAttachmentCode { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISNOMINATEDRIDDOR", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISNOMINATEDRIDDOR", ResourceType = typeof(Resources))]
            public bool IsNominatedRIDDOR { get; set; }

            //[Tooltip("TOOLTIP_INCIDENT_RIDDORATTACHEMENTCODE", ResourceType = typeof(Resources))]
            //[Display(Name = "LABEL_INCIDENT_RIDDORATTACHEMENTCODE", ResourceType = typeof(Resources))]
            //public Nullable<System.Guid> RIDDORAttachementCode { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISORALWARNING", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISORALWARNING", ResourceType = typeof(Resources))]
            public bool IsOralWarning { get; set; }

            [DataType(DataType.Date)]
            [Tooltip("TOOLTIP_INCIDENT_ORALWARNINGDATE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ORALWARNINGDATE", ResourceType = typeof(Resources))]
            public Nullable<System.DateTime> OralWarningDate { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISWRITTENWARNING", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISWRITTENWARNING", ResourceType = typeof(Resources))]
            public bool IsWrittenWarning { get; set; }

            [DataType(DataType.Date)]
            [Tooltip("TOOLTIP_INCIDENT_WRITTENWARNINGDATE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_WRITTENWARNINGDATE", ResourceType = typeof(Resources))]
            public Nullable<System.DateTime> WrittenWarningDate { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISASSAILANTINTERVIEWED", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISASSAILANTINTERVIEWED", ResourceType = typeof(Resources))]
            public bool IsAssailantInterviewed { get; set; }

            [DataType(DataType.Date)]
            [Tooltip("TOOLTIP_INCIDENT_ASSAILANTINTERVIEWEDDATE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ASSAILANTINTERVIEWEDDATE", ResourceType = typeof(Resources))]
            public Nullable<System.DateTime> AssailantInterviewedDate { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISSOLICITORLETTER", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISSOLICITORLETTER", ResourceType = typeof(Resources))]
            public bool IsSolicitorLetter { get; set; }

            [DataType(DataType.Date)]
            [Tooltip("TOOLTIP_INCIDENT_SOLICITORLETTERDATE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_SOLICITORLETTERDATE", ResourceType = typeof(Resources))]
            public Nullable<System.DateTime> SolicitorLetterDate { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISBANNINGORDER", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISBANNINGORDER", ResourceType = typeof(Resources))]
            public bool IsBanningOrder { get; set; }

            [DataType(DataType.Date)]
            [Tooltip("TOOLTIP_INCIDENT_BANNINGORDERREQUESTEDDATE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_BANNINGORDERREQUESTEDDATE", ResourceType = typeof(Resources))]
            public Nullable<System.DateTime> BanningOrderRequestedDate { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_CONTINGENCYARRANGEMENTCODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_CONTINGENCYARRANGEMENTCODE", ResourceType = typeof(Resources))]
            public List<System.String> ContingencyArrangementCodes { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_DEFICIENCIESNARRATIVECODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_DEFICIENCIESNARRATIVECODE", ResourceType = typeof(Resources))]
            public Nullable<System.Guid> DeficienciesNarrativeCode { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISDEFICIENCIESINPROCESS", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISDEFICIENCIESINPROCESS", ResourceType = typeof(Resources))]    
            public Nullable<bool> IsDeficienciesInProcess { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_FURTHERINFONARRATIVECODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_FURTHERINFONARRATIVECODE", ResourceType = typeof(Resources))]
            public Nullable<System.Guid> FurtherInfoNarrativeCode { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISIMPLEMENTCONTROLMEASURES", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISIMPLEMENTCONTROLMEASURES", ResourceType = typeof(Resources))]
            public bool IsImplementControlMeasures { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_CONTROLMEASURECODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_CONTROLMEASURECODE", ResourceType = typeof(Resources))]
            public List<System.String> ControlMeasureCodes { get; set; }

            [StringLength(150)]
            [Tooltip("TOOLTIP_INCIDENT_NAMEDOFFICER", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_NAMEDOFFICER", ResourceType = typeof(Resources))]
            [RegularExpression("^[a-zA-Z0-9 .\'-]*$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
            public string NamedOfficer { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_INCIDENT_TELEPHONECONTACTNUMBER", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_TELEPHONECONTACTNUMBER", ResourceType = typeof(Resources))]
            [RegularExpression("^[0-9 ]*$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
            public string TelephoneContactNumber { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISITMARKERSSET", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISITMARKERSSET", ResourceType = typeof(Resources))]
            public Nullable<bool> IsITMarkersSet { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ITSYSTEMSMARKED", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ITSYSTEMSMARKED", ResourceType = typeof(Resources))]
            public List<System.String> SystemMarkedCodes { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISPAPERSMARKED", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISPAPERSMARKED", ResourceType = typeof(Resources))]
            public Nullable<bool> IsPapersMarked { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISNOTIFIEDPARTIES", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISNOTIFIEDPARTIES", ResourceType = typeof(Resources))]
            public Nullable<bool> IsNotifiedParties { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_NOTIFIEDPARTIES", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_NOTIFIEDPARTIES", ResourceType = typeof(Resources))]
            public List<System.String> InterestedPartyCodes { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_REVIEWDATE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_REVIEWDATE", ResourceType = typeof(Resources))]
            public System.DateTime? ReviewDate { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISPREVIOUSEVIDENCEREVIEWED", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISPREVIOUSEVIDENCEREVIEWED", ResourceType = typeof(Resources))]
            public Nullable<bool> IsPreviousEvidenceReviewed { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISPREVIOUSPARTIESNOTIFIED", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISPREVIOUSPARTIESNOTIFIED", ResourceType = typeof(Resources))]
            public Nullable<bool> IsPreviousPartiesNotified { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISREPEATBEHAVIOUR", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISREPEATBEHAVIOUR", ResourceType = typeof(Resources))]
            public Nullable<bool> IsRepeatBehaviour { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_ISCONTROLMEASURESSTILLAPPLY", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_ISCONTROLMEASURESSTILLAPPLY", ResourceType = typeof(Resources))]
            public Nullable<bool> IsControlMeasuresStillApply { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_SITECODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_SITECODE", ResourceType = typeof(Resources))]
            public Nullable<System.Guid> SiteCode { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_NUMBEROFRECORDS", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_NUMBEROFRECORDS", ResourceType = typeof(Resources))]
            public int NumberOfRecords { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_CURRENTOWNERSTAFFCODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_CURRENTOWNERSTAFFCODE", ResourceType = typeof(Resources))]
            public Nullable<System.Guid> CurrentOwnerStaffCode { get; set; }

            [Tooltip("TOOLTIP_INCIDENT_OTHERINCIDENTLOCATION", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_OTHERINCIDENTLOCATION", ResourceType = typeof(Resources))]
            public string OtherIncidentLocation { get; set; }

            [StringLength(200)]
            [Tooltip("TOOLTIP_INCIDENT_BANNEDFROMOFFICES", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_BANNEDFROMOFFICES", ResourceType = typeof(Resources))]
            public string BannedFromOffices { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_INCIDENT_BANNEDFROMOFFICESENDDATE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_INCIDENT_BANNEDFROMOFFICESENDDATE", ResourceType = typeof(Resources))]
            public string BannedFromOfficesEndDate { get; set; }
            
            #endregion

        }
    }
}
