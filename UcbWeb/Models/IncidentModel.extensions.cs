using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using UcbWeb.DataAnnotation;
using Dwp.Adep.Ucb.ResourceLibrary;

namespace UcbWeb.Models
{
    public partial class IncidentModel : BaseModel
    {
        #region Fields for read-only version of  screen

        public string JobRoleDescription { get; set; }
        public string StaffMemberBusinessDescription { get; set; }
        public string StaffMemberBusinessAreaDescription { get; set; }
        public string StaffMemberHomeOfficeDescription { get; set; }
        public string EventLeadingToIncidentDescription { get; set; }
        public string IncidentLocationDescription { get; set; }
        public string IncidentCategoryDescription { get; set; }
        public string IncidentTypeDescription { get; set; }
        public string IncidentDetailsDescription { get; set; }
        public string AbuseTypeDescription { get; set; }
        public string ContingencyArrangementDescriptions { get; set; }
        public string ControlMeasureDescriptions { get; set; }
        public string SystemMarkedDescriptions { get; set; }
        public string InterestedPartyDescriptions { get; set; }

        public string ReferrerDescription { get; set; }

        #endregion

        #region extra properties for incident

        private IEnumerable<string> _contingencyArrangementCodes;
        public IEnumerable<string> ContingencyArrangementCodes
        {
            get { return _contingencyArrangementCodes; }
            set { _contingencyArrangementCodes = value; }
        }


        private IEnumerable<string> _controlMeasureCodes;
        public IEnumerable<string> ControlMeasureCodes
        {
            get { return _controlMeasureCodes; }
            set { _controlMeasureCodes = value; }
        }

        private IEnumerable<string> _systemMarkedCodes;
        public IEnumerable<string> SystemMarkedCodes
        {
            get { return _systemMarkedCodes; }
            set { _systemMarkedCodes = value; }
        }

        private IEnumerable<string> _interestedPartyCodes;
        public IEnumerable<string> InterestedPartyCodes
        {
            get { return _interestedPartyCodes; }
            set { _interestedPartyCodes = value; }
        }

        #endregion

        #region Extra fields for when Referral labels differ from incident labels. The fields point to the same private properties. They are just use to attach different metadata.

         [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
        [Tooltip("TOOLTIP_REFERRAL_REFERRALID", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_REFERRALID", ResourceType = typeof(Resources))]
        public int ReferralID 
        {
            get { return _incidentID; }
            set { _incidentID = value; }
        }

         [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
        [StringLength(50)]
        [Tooltip("TOOLTIP_REFERRAL_REFERRALSTATUS", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_REFERRALSTATUS", ResourceType = typeof(Resources))]
        public string ReferralStatus 
        {
            get { return _incidentStatus; }
            set { _incidentStatus = value; }
        }

        [Tooltip("TOOLTIP_REFERRAL_REFERRALDATE", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_REFERRALDATE", ResourceType = typeof(Resources))]
         [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
         [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
         public DateTime ReferralDate
        {
            get { return _incidentDate; }
            set { _incidentDate = value; }
        }


        [Tooltip("TOOLTIP_REFERRAL_ISIMPLEMENTCONTROLMEASURES", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_ISIMPLEMENTCONTROLMEASURES", ResourceType = typeof(Resources))]
         [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
        public bool? ReferralIsImplementControlMeasures
        {
            get { return _isImplementControlMeasures; }
            set { _isImplementControlMeasures = value; }
        }

        [Tooltip("TOOLTIP_REFERRAL_CONTROLMEASURECODE", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_CONTROLMEASURECODE", ResourceType = typeof(Resources))]
        [RequiredIf("ReferralIsImplementControlMeasures", "True")]
        public IEnumerable<String> ReferralControlMeasureCodes 
        {
            get { return _controlMeasureCodes; }
            set { _controlMeasureCodes = value; }

        }

        [Tooltip("TOOLTIP_REFERRAL_ISITMARKERSSET", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_ISITMARKERSSET", ResourceType = typeof(Resources))]
        public bool? ReferralIsITMarkersSet
        {
            get { return _isITMarkersSet; }
            set { _isITMarkersSet = value; }
        }

        [Tooltip("TOOLTIP_REFERRAL_ITSYSTEMSMARKED", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_ITSYSTEMSMARKED", ResourceType = typeof(Resources))]
        public IEnumerable<String> ReferralSystemMarkedCodes
        {
            get { return _systemMarkedCodes; }
            set { _systemMarkedCodes = value; }
        }

        [Tooltip("TOOLTIP_REFERRAL_NOTIFIEDPARTIES", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_NOTIFIEDPARTIES", ResourceType = typeof(Resources))]
        public IEnumerable<String> ReferralInterestedPartyCodes
        {
            get { return _interestedPartyCodes; }
            set { _interestedPartyCodes = value; }
        }

        [Tooltip("TOOLTIP_REFERRAL_REVIEWDATE", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_REVIEWDATE", ResourceType = typeof(Resources))]
        public System.DateTime? ReferralReviewDate
        {
            get { return _reviewDate; }
            set { _reviewDate = value; }
        }


        [Tooltip("TOOLTIP_REFERRAL_ISPREVIOUSEVIDENCEREVIEWED", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_ISPREVIOUSEVIDENCEREVIEWED", ResourceType = typeof(Resources))]
        public Nullable<bool> ReferralIsPreviousEvidenceReviewed 
        {
            get { return IsPreviousEvidenceReviewed; }
            set { _isPreviousEvidenceReviewed = value; }
        }

        [Tooltip("TOOLTIP_REFERRAL_ISPREVIOUSPARTIESNOTIFIED", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_ISPREVIOUSPARTIESNOTIFIED", ResourceType = typeof(Resources))]
        public Nullable<bool> ReferralIsPreviousPartiesNotified
        {
            get { return _isPreviousPartiesNotified; }
            set { _isPreviousPartiesNotified = value; }
        }

        [Tooltip("TOOLTIP_REFERRAL_ISREPEATBEHAVIOUR", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_ISREPEATBEHAVIOUR", ResourceType = typeof(Resources))]
        public Nullable<bool> ReferralIsRepeatBehaviour 
        {
            get { return _isRepeatBehaviour; }
            set { _isRepeatBehaviour = value; }
        }

        [Tooltip("TOOLTIP_REFERRAL_ISCONTROLMEASURESSTILLAPPLY", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_ISCONTROLMEASURESSTILLAPPLY", ResourceType = typeof(Resources))]
        public Nullable<bool> ReferralIsControlMeasuresStillApply
        {
            get { return _isControlMeasuresStillApply; }
            set { _isControlMeasuresStillApply = value; }
        }

        [StringLength(100)]
        [Tooltip("TOOLTIP_REFERRAL_NAMEDOFFICER", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_NAMEDOFFICER", ResourceType = typeof(Resources))]
        [RegularExpression("^[a-zA-Z0-9 .\'-]*$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
        public string ReferralNamedOfficer 
        {
            get { return _namedOfficer; }
            set { _namedOfficer = value; }
        }

        [StringLength(50)]
        [Tooltip("TOOLTIP_REFERRAL_TELEPHONECONTACTNUMBER", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_TELEPHONECONTACTNUMBER", ResourceType = typeof(Resources))]
        [RegularExpression("^[0-9 ]*$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
        public string ReferralTelephoneContactNumber 
        {
            get { return _telephoneContactNumber; }
            set { _telephoneContactNumber = value; }
        }

        [StringLength(200)]
        [Tooltip("TOOLTIP_REFERRAL_BANNEDFROMOFFICES", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_BANNEDFROMOFFICES", ResourceType = typeof(Resources))]
        public string ReferralBannedFromOffices
        {
            get { return _bannedFromOffices; }
            set { _bannedFromOffices = value; }
        }

        [StringLength(50)]
        [Tooltip("TOOLTIP_REFERRAL_BANNEDFROMOFFICESENDDATE", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_BANNEDFROMOFFICESENDDATE", ResourceType = typeof(Resources))]
        public string ReferralBannedFromOfficesEndDate
        {
            get { return _bannedFromOfficesEndDate; }
            set { _bannedFromOfficesEndDate = value; }
        }

        #endregion

    }
}