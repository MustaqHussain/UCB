
using UcbWeb.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dwp.Adep.Ucb.ResourceLibrary;
using System;
using System.Web.Mvc;
using UcbWeb.Helpers;
using System.Linq;
using UcbWeb.DataAnnotation;

namespace UcbWeb.ViewModels
{
    public partial class IncidentVM : IValidatableObject
    {

        public CustomerModel CustomerItem { get; set; }

        // gary
        public StaffModel CurrentOwnerItem { get; set; }

        // gary
        [Tooltip("TOOLTIP_INCIDENT_CURRENTOWNERSTAFFCODE", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_INCIDENT_CURRENTOWNERSTAFFCODE", ResourceType = typeof(Resources))]
        public bool HandleCaseFlag { get; set; }

        public IncidentModel IncidentItem { get; set; }

        public List<ReferrerModel> ReferrerList { get; set; }

        public List<JobRoleModel> JobRoleList { get; set; }
        public List<OrganisationModel> StaffMemberBusinessList { get; set; }
        public List<OrganisationModel> StaffMemberBusinessAreaList { get; set; }
        public List<SiteModel> StaffMemberHomeOfficeList { get; set; }

        public List<EventLeadingToIncidentModel> EventLeadingToIncidentList { get; set; }
        public List<IncidentLocationModel> IncidentLocationList { get; set; }
        public List<IncidentCategoryModel> IncidentCategoryList { get; set; }
        public List<IncidentTypeModel> IncidentTypeList { get; set; }
        public List<IncidentDetailModel> IncidentDetailsList { get; set; }
        public List<AbuseTypeModel> AbuseTypeList { get; set; }
        public List<RelationshipToCustomerModel> RelationshipToCustomerList { get; set; }


        public List<SelectListItem> TitleList { get; set; }



        //gary
        //public List<StaffModel> RelationshipToCurrentOwnerList { get; set; }

        public NarrativeModel IncidentNarrativeItem { get; set; }
        public NarrativeModel LineManagerNarrativeItem { get; set; }
        private NarrativeModel _furtherInfoNarrativeItem {get;set;}

        public NarrativeModel FurtherInfoNarrativeItem 
        {
            get { return _furtherInfoNarrativeItem; }
            set { _furtherInfoNarrativeItem = value; }
        }

        [Tooltip("TOOLTIP_REFERRAL_NOTES", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_NOTES", ResourceType = typeof(Resources))]
        public NarrativeModel ReferralFurtherInfoNarrativeItem
        {
            get { return _furtherInfoNarrativeItem; }
            set { _furtherInfoNarrativeItem = value; }
        }


        public NarrativeModel DeficienciesNarrativeItem { get; set; }

        public NarrativeModel ReviewActionNarrativeItem { get; set; }
        
        public List<AttachmentModel> FastTrackAttachmentList { get; set; }
        public List<AttachmentModel> RIDDORAttachmentList { get; set; }
        public List<AttachmentModel> RepeatBehaviourAttachmentList { get; set; }
        public List<AttachmentModel> GeneralEvidenceAttachmentList { get; set; }
        public List<AttachmentModel> FurtherInfoAttachmentList { get; set; }
        public List<AttachmentModel> ReferralEvidenceAttachmentList { get; set; }

        public List<SelectListItem> ContingencyArrangementList { get; set; }
        public List<SelectListItem> ControlMeasureList { get; set; }

        public List<SelectListItem> SystemMarkedList { get; set; }
        public List<SelectListItem> InterestedPartyList { get; set; }
                 
        public string Message { get; set; }

        public bool PreviewIncident { get; set; }
        public string LinkingParameter { get; set; }

        public string AdminEmailAddress { get; set; }
        public string DeputyAdminEmailAddress { get; set; }

        public string ButtonPressed { get; set; }

        [Tooltip("TOOLTIP_INCIDENT_LINEMANAGEREMAIL", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_INCIDENT_LINEMANAGEREMAIL", ResourceType = typeof(Resources))]
        public string LineManagerEmailAddress { get; set; }

        public List<string> LineManagerEmailAddressList { get; set; }

        public string StatusChangeTo { get; set; }

        public List<string> AvailableStatus { get; set; }

        public List<IncidentUpdateEventModel> IncidentUpdateEvents { get; set; }

        public string IsDeleteConfirmed { get; set; }
        public string IsRemoveAttachmentConfirmed { get; set; }
        public string IsExitConfirmed { get; set; }
        public string IsSubmitConfirmed { get; set; }
        public string IsSaveConfirmed { get; set; }
        public string IsSaveAndCloseConfirmed { get; set; }
        public string IsPublishConfirmed { get; set; }
        public string IsArchiveConfirmed { get; set; }
        public bool IsViewDirty { get; set; }

        public bool? ShowIncidentDetail { get; set; }
        public bool? ShowAbuseType { get; set; }

        public string Copy {get; set;}

        private string _nominatedManager;

        [Tooltip("TOOLTIP_INCIDENT_NOMINATEDMANAGER", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_INCIDENT_NOMINATEDMANAGER", ResourceType = typeof(Resources))]
        public string NominatedManager 
        {
            get { return _nominatedManager; }
            set { _nominatedManager = value; }
        }

        [Tooltip("TOOLTIP_REFERRAL_NOMINATEDMANAGER", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_REFERRAL_NOMINATEDMANAGER", ResourceType = typeof(Resources))]
        public string ReferralNominatedManager
        {
            get { return _nominatedManager; }
            set { _nominatedManager = value; }
        }

        [Tooltip("TOOLTIP_INCIDENT_DEPUTYNOMINATEDMANAGERS", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_INCIDENT_DEPUTYNOMINATEDMANAGERS", ResourceType = typeof(Resources))]
        public string DeputyNominatedManagers { get; set; }

        public string NominatedManagerEmailAddress { get; set; }

        public bool IsReadOnly { get; set; }

        public List<IncidentLinkModel> LinkedIncidents { get; set; }

        public IncidentAccessContext AccessContext { get; set; }

        public IncidentContext IncidentContext { get; set; }

        public string IncidentAdvisoryNote { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (IncidentItem.Type == IncidentType.Incident)
            {
                //Check Incident Category and Type consistent
                CacheManager cacheManagerList = new CacheManager();
                Guid codeForCategory = cacheManagerList.IncidentListCache.IncidentTypeList.Find(x => x.Code == IncidentItem.IncidentTypeCode).IncidentCategoryCode;
                if (!IncidentItem.IncidentCategoryCode.Equals(codeForCategory))
                {
                    errors.Add(new ValidationResult(Resources.VAL_INCIDENT_CATEGORY_TYPE_INCONSISTENT, new List<string> { "IncidentItem.IncidentCategoryCode" }));
                }

                //Incident Narrative is Mandatory
                if (IncidentNarrativeItem == null || string.IsNullOrEmpty(IncidentNarrativeItem.NarrativeDescription))
                {
                    errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_INCIDENTNARRATIVECODE), new List<string> { "IncidentNarrativeItem.NarrativeDescription" }));
                }
                //Do not allow dates in the future
                if (IncidentItem.IncidentDate > DateTime.Now.Date)
                {
                    errors.Add(new ValidationResult(Resources.VAL_INCIDENTDATE_FUTURE, new List<string> { "IncidentItem.IncidentDate" }));
                }
                //Do not allow dates prior to 01/01/2000
                DateTime earliestDate = new DateTime(2000, 1, 1);
                if (IncidentItem.IncidentDate < earliestDate)
                {
                    errors.Add(new ValidationResult(Resources.VAL_INCIDENTDATE_TOOEARLY, new List<string> { "IncidentItem.IncidentDate" }));
                }
                
                //************************************************************************************************************************************************************
                // The following validation is only done when when the incident is in a status of 'New', 'Submitted', 'Live','Archived' (i.e. the Line Manager section or Nominated Manager section)
                //***********************************************************************************************************************************************************
                if (IncidentItem.IncidentStatus == IncidentStatus.New || IncidentItem.IncidentStatus == IncidentStatus.Submitted
                    || IncidentItem.IncidentStatus == IncidentStatus.Live || IncidentItem.IncidentStatus == IncidentStatus.Archived)
                {
                    //IsLineManagerFastTrack is mandatory
                    if (IncidentItem.IsLineManageFastTrack == null)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_ISLINEMANAGEFASTTRACK), new List<string> { "IncidentItem.IsLineManageFastTrack" }));
                    }

                    //IsLineManagerRiddor is mandatory
                    if (IncidentItem.IsLineManagerRIDDOR == null)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_ISLINEMANAGERRIDDOR), new List<string> { "IncidentItem.IsLineManagerRIDDOR" }));
                    }

                    //IsPoliceCalled is mandatory
                    if (IncidentItem.IsPoliceCalled == null)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_ISPOLICECALLED), new List<string> { "IncidentItem.IsPoliceCalled" }));
                    }

                    //HasLineManagerReadReport is mandatory
                    if (IncidentItem.HasLineManagerReadReport == null && IncidentContext == IncidentContext.FromLink)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_HASLINEMANAGERREADREPORT), new List<string> { "IncidentItem.HasLineManagerReadReport" }));
                    }

                    //HasLineManagerReadReport must be set to 'Yes'
                    if (IncidentItem.HasLineManagerReadReport == false && IncidentContext==IncidentContext.FromLink)
                    {
                        errors.Add(new ValidationResult(Resources.VAL_CONFIRMREADREPORT, new List<string> { "IncidentItem.HasLineManagerReadReport" }));
                    }
              
                }
                if (StatusChangeTo != null && StatusChangeTo != "")
                {
                    if (StatusChangeTo != IncidentItem.IncidentStatus)
                    {
                        if (IncidentItem.IsImplementControlMeasures.HasValue && IncidentItem.IsImplementControlMeasures.Value)
                        {
                            errors.Add(new ValidationResult(Resources.VAL_CHANGESTATUSWITHCONTROLMEASURES, new List<string> { "StatusChangeTo" }));
                        }
                    }
                }

                //If 'Banned from Office', 'To Be Seen By Named Officer' has been picked as a control measure
                if (IncidentItem.ControlMeasureCodes != null)
                {
                    CacheManager cacheManager = new CacheManager();
                    string codeForNamedOfficerControlMeasure = Resources.VAL_CONTROLMEASURE_TOBESEENBYNAMEDOFFICER_GUID;
                    string codeForBannedFromOfficeControlMeasure = Resources.VAL_CONTROLMEASURE_BANNEDFROMOFFICE_GUID;

                    if (IncidentItem.IsImplementControlMeasures == true && IncidentItem.ControlMeasureCodes.Contains(codeForBannedFromOfficeControlMeasure))
                    {
                        if (String.IsNullOrEmpty(IncidentItem.BannedFromOffices))
                        {
                            errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_BANNEDFROMOFFICES), new List<string> { "IncidentItem.BannedFromOffices" }));
                        }
                        if (IncidentItem.BannedFromOfficesEndDate==null)
                        {
                            errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_BANNEDFROMOFFICESENDDATE), new List<string> { "IncidentItem.BannedFromOfficesEndDate" }));
                        }
                    }

                    if (IncidentItem.IsImplementControlMeasures == true && IncidentItem.ControlMeasureCodes.Contains(codeForNamedOfficerControlMeasure))
                    {
                        if (String.IsNullOrEmpty(IncidentItem.NamedOfficer))
                        {
                            errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_NAMEDOFFICER), new List<string> { "IncidentItem.NamedOfficer" }));
                        }
                        if (String.IsNullOrEmpty(IncidentItem.TelephoneContactNumber))
                        {
                            errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_TELEPHONECONTACTNUMBER), new List<string> { "IncidentItem.TelephoneContactNumber" }));
                        }
                    }
                }

                //Publish/Archive button pressed OR Save button pressed when incident is already Published or Archived
                if (ButtonPressed == "Archive" || ButtonPressed == "Publish" || (ButtonPressed == "Save" && (IncidentItem.IncidentStatus == IncidentStatus.Archived || IncidentItem.IncidentStatus == IncidentStatus.Live)))
                {
                    //IsNominatedFastTrack must be filled in
                    if (IncidentItem.IsNominatedFastTrack == null)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_ISNOMINATEDFASTTRACK), new List<string> { "IncidentItem.IsNominatedFastTrack" }));
                    }

                    //IsNominatedRIDDOR must be filled in
                    if (IncidentItem.IsNominatedRIDDOR == null)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_ISNOMINATEDRIDDOR), new List<string> { "IncidentItem.IsNominatedRIDDOR" }));
                    }

                    //IsOralWarning must be filled in
                    if (IncidentItem.IsOralWarning == null)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_ISORALWARNING), new List<string> { "IncidentItem.IsOralWarning" }));
                    }

                    //OralWarningDate must be populated if IsOralWarning has been set to 'Yes'
                    /*if (IncidentItem.IsOralWarning == true && !IncidentItem.OralWarningDate.HasValue)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_ORALWARNINGDATE), new List<string> { "IncidentItem.OralWarningDate" }));
                    }*/

                    //IsWrittenWarning must be filled in
                    if (IncidentItem.IsWrittenWarning == null)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_ISWRITTENWARNING), new List<string> { "IncidentItem.IsWrittenWarning" }));
                    }
                    
                    //WrittenWarnignDate must be populated if IsWrittenWarning has been set to 'Yes'
                    /*if (IncidentItem.IsWrittenWarning == true && !IncidentItem.WrittenWarningDate.HasValue)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_WRITTENWARNINGDATE), new List<string> { "IncidentItem.WrittenWarningDate" }));
                    }*/

                    //IsAssailantInterviewed must be filled in
                    if (IncidentItem.IsAssailantInterviewed == null)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_ISASSAILANTINTERVIEWED), new List<string> { "IncidentItem.IsAssailantInterviewed" }));
                    }

                    //DateInterviewed must be populated if IsAssailantInterviewed has been set to 'Yes'
                    /*if (IncidentItem.IsAssailantInterviewed == true && !IncidentItem.AssailantInterviewedDate.HasValue)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_ASSAILANTINTERVIEWEDDATE), new List<string> { "IncidentItem.AssailantInterviewedDate" }));
                    }*/

                    //IsSolicitorLetter must be filled in
                    if (IncidentItem.IsSolicitorLetter == null)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_ISSOLICITORLETTER), new List<string> { "IncidentItem.IsSolicitorLetter" }));
                    }

                    //Solicitor date must be populated if IsSolictorLetter has been set to 'Yes'
                    /*if (IncidentItem.IsSolicitorLetter == true && !IncidentItem.SolicitorLetterDate.HasValue)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_SOLICITORLETTERDATE), new List<string> { "IncidentItem.SolicitorLetterDate" }));
                    }*/

                    //IsBanningOrder must be filled in
                    if (IncidentItem.IsBanningOrder == null)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_ISBANNINGORDER), new List<string> { "IncidentItem.IsBanningOrder" }));
                    }

                    //Banning order date must be populated if IsBanningOrder has been set to 'Yes'
                    /*if (IncidentItem.IsBanningOrder == true && !IncidentItem.BanningOrderRequestedDate.HasValue)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_BANNINGORDERREQUESTEDDATE), new List<string> { "IncidentItem.BanningOrderRequestedDate" }));
                    }*/

                    //ContingencyArrangements must be filled in
                    if (IncidentItem.ContingencyArrangementCodes == null)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_CONTINGENCYARRANGEMENTCODE), new List<string> { "IncidentItem.ContingencyArrangementCodes" }));
                    }

                    //IsImplementControlMeasures must be filled in
                    if (IncidentItem.IsImplementControlMeasures == null)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_ISIMPLEMENTCONTROLMEASURES), new List<string> { "IncidentItem.IsImplementControlMeasures" }));
                    }

                    //ControlMeasures must be filled in if 'IsImplementControlMeasures' has been set to 'Yes'
                    if (IncidentItem.IsImplementControlMeasures == true && IncidentItem.ControlMeasureCodes == null)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_CONTROLMEASURECODE), new List<string> { "IncidentItem.ControlMeasureCodes" }));
                    }

                    //IT Systems marked must be picked if 'IsITMarkersSet' has been set to 'Yes'
                    if (IncidentItem.IsITMarkersSet == true && IncidentItem.SystemMarkedCodes == null)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_ITSYSTEMSMARKED), new List<string> { "IncidentItem.SystemMarkedCodes" }));
                    }

                    //Interested parties must be picked if 'IsNotifiedParties' has been set to 'Yes'
                    if (IncidentItem.IsNotifiedParties == true && IncidentItem.InterestedPartyCodes == null)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_NOTIFIEDPARTIES), new List<string> { "IncidentItem.InterestedPartyCodes" }));
                    }

                    //Control measures s must be picked if 'IsImplementControlMeasures' has been set to 'Yes'
                    if (IncidentItem.IsImplementControlMeasures == true && IncidentItem.ControlMeasureCodes == null)
                    {
                        errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_CONTROLMEASURECODE), new List<string> { "IncidentItem.ControlMeasureCodes" }));
                    }

                    //If IsControMeasuresStillApply has been entered, then it must match IsImplementControlMeasures
                    if (IncidentItem.IsControlMeasuresStillApply != null)
                    {
                        if (IncidentItem.IsImplementControlMeasures != IncidentItem.IsControlMeasuresStillApply)
                        {
                            errors.Add(new ValidationResult(Resources.VAL_INCIDENT_CONTROLMEASUREINCONSISTENCY, new List<string> { "IncidentItem.IsImplementControlMeasures", "IncidentItem.IsControlMeasuresStillApply" }));
                        }
                    }

                }
            }

            if (IncidentItem.Type == IncidentType.Referral)
            {
                //Do not allow dates in the future
                if (IncidentItem.ReferralDate > DateTime.Now.Date)
                {
                    errors.Add(new ValidationResult(Resources.VAL_INCIDENTDATE_FUTURE, new List<string> { "IncidentItem.ReferralDate" }));
                }
                //Do not allow dates prior to 01/01/2000
                DateTime earliestDate = new DateTime(2000, 1, 1);
                if (IncidentItem.IncidentDate < earliestDate)
                {
                    errors.Add(new ValidationResult(Resources.VAL_INCIDENTDATE_TOOEARLY, new List<string> { "IncidentItem.ReferralDate" }));
                }

                if (StatusChangeTo != null && StatusChangeTo != "")
                {
                    if (StatusChangeTo != IncidentItem.IncidentStatus)
                    {
                        if (IncidentItem.ReferralIsImplementControlMeasures.HasValue && IncidentItem.ReferralIsImplementControlMeasures.Value)
                        {
                            errors.Add(new ValidationResult(Resources.VAL_CHANGESTATUSWITHCONTROLMEASURES, new List<string> { "StatusChangeTo" }));
                        }
                    }
                }

                //IT Systems marked must be picked if 'IsITMarkersSet' has been set to 'Yes'
                if (IncidentItem.ReferralIsITMarkersSet == true && IncidentItem.ReferralSystemMarkedCodes == null)
                {
                    errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_REFERRAL_ITSYSTEMSMARKED), new List<string> { "IncidentItem.ReferralSystemMarkedCodes" }));
                }

                //Interested parties must be picked if 'IsNotifiedParties' has been set to 'Yes'
                if (IncidentItem.ReferralIsPreviousPartiesNotified == true && IncidentItem.ReferralInterestedPartyCodes == null)
                {
                    errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_REFERRAL_NOTIFIEDPARTIES), new List<string> { "IncidentItem.ReferralInterestedPartyCodes" }));
                }

                //If 'Banned from Office', 'To Be Seen By Named Officer' has been picked as a control measure
                if (IncidentItem.ReferralControlMeasureCodes != null)
                {
                    CacheManager cacheManager = new CacheManager();
                    string codeForNamedOfficerControlMeasure = Resources.VAL_CONTROLMEASURE_TOBESEENBYNAMEDOFFICER_GUID;
                    string codeForBannedFromOfficeControlMeasure = Resources.VAL_CONTROLMEASURE_BANNEDFROMOFFICE_GUID;

                    if (IncidentItem.ReferralIsImplementControlMeasures == true && IncidentItem.ControlMeasureCodes.Contains(codeForBannedFromOfficeControlMeasure))
                    {
                        if (String.IsNullOrEmpty(IncidentItem.ReferralBannedFromOffices))
                        {
                            errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_REFERRAL_BANNEDFROMOFFICES), new List<string> { "IncidentItem.ReferralBannedFromOffices" }));
                        }
                        if (IncidentItem.ReferralBannedFromOfficesEndDate == null)
                        {
                            errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_REFERRAL_BANNEDFROMOFFICESENDDATE), new List<string> { "IncidentItem.ReferralBannedFromOfficesEndDate" }));
                        }
                    }

                    if (IncidentItem.ReferralIsImplementControlMeasures == true && IncidentItem.ControlMeasureCodes.Contains(codeForNamedOfficerControlMeasure))
                    {
                        if (String.IsNullOrEmpty(IncidentItem.ReferralNamedOfficer))
                        {
                            errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_REFERRAL_NAMEDOFFICER), new List<string> { "IncidentItem.ReferralNamedOfficer" }));
                        }
                        if (String.IsNullOrEmpty(IncidentItem.ReferralTelephoneContactNumber))
                        {
                            errors.Add(new ValidationResult(string.Format(Resources.VAL_REQUIRED, Resources.LABEL_REFERRAL_TELEPHONECONTACTNUMBER), new List<string> { "IncidentItem.ReferralTelephoneContactNumber" }));
                        }
                    }

                }

                if (IncidentItem.ReferralIsControlMeasuresStillApply != null)
                {
                    if (IncidentItem.ReferralIsImplementControlMeasures != IncidentItem.ReferralIsControlMeasuresStillApply)
                    {
                        errors.Add(new ValidationResult(Resources.VAL_INCIDENT_CONTROLMEASUREINCONSISTENCY, new List<string> { "IncidentItem.ReferralIsImplementControlMeasures", "IncidentItem.ReferralIsControlMeasuresStillApply" }));
                    }
                }               
            }

            return errors;
        }
    
    }

    

    public enum IncidentAccessContext
    {
        Create,
        View,
        Edit
    }
    public enum IncidentContext
    {
        FromLink,
        FromApp
    }
    public class IncidentStatus
    {
        public const string Creation = "Creation"; 
        public const string New = "New";
        public const string Submitted = "Submitted";
        public const string Live = "Live";
        public const string Archived = "Archived";
    }

    public class IncidentType
    {
        public const string Incident = "Incident";
        public const string Referral = "Referral";
    }
}
