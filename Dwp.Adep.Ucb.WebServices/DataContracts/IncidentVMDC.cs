
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace Dwp.Adep.Ucb.WebServices.DataContracts
{
    [DataContract]
    public partial class IncidentVMDC
    {

        [DataMember]
        public IncidentDC IncidentItem { get; set; }

        [DataMember]
        public List<IncidentDC> IncidentList { get; set; }

        [DataMember]
        public List<ReferrerDC> ReferrerList { get; set; }

        [DataMember]
        public List<JobRoleDC> JobRoleList { get; set; }

        [DataMember]
        public List<OrganisationDC> StaffMemberBusinessList { get; set; }

        [DataMember]
        public List<OrganisationDC> StaffMemberBusinessAreaList { get; set; }

        [DataMember]
        public List<SiteDC> StaffMemberHomeOfficeList { get; set; }

        [DataMember]
        public CustomerDC CustomerItem { get; set; }

        //gary
        [DataMember]
        public StaffDC CurrentOwnerItem { get; set; }

        [DataMember]
        public List<EventLeadingToIncidentDC> EventLeadingToIncidentList { get; set; }

        [DataMember]
        public List<IncidentLocationDC> IncidentLocationList { get; set; }

        [DataMember]
        public List<IncidentCategoryDC> IncidentCategoryList { get; set; }

        [DataMember]
        public List<IncidentTypeDC> IncidentTypeList { get; set; }

        [DataMember]
        public List<IncidentDetailDC> IncidentDetailsList { get; set; }

        [DataMember]
        public List<AbuseTypeDC> AbuseTypeList { get; set; }

        [DataMember]
        public NarrativeDC IncidentNarrativeItem { get; set; }

        [DataMember]
        public NarrativeDC LineManagerNarrativeItem { get; set; }

        [DataMember]
        public List<AttachmentDC> FastTrackAttachmentList { get; set; }

        [DataMember]
        public List<AttachmentDC> RIDDORAttachmentList { get; set; }

        [DataMember]
        public List<AttachmentDC> RepeatBehaviourAttachmentList { get; set; }

        [DataMember]
        public List<AttachmentDC> GeneralEvidenceAttachmentList { get; set; }

        [DataMember]
        public List<AttachmentDC> FurtherInfoAttachmentList { get; set; }

        [DataMember]
        public List<AttachmentDC> ReferralEvidenceAttachmentList { get; set; }

        [DataMember]
        public NarrativeDC FurtherInfoNarrativeItem { get; set; }

        [DataMember]
        public NarrativeDC DeficienciesNarrativeItem { get; set; }

        [DataMember]
        public NarrativeDC ReviewActionNarrativeItem { get; set; }

        [DataMember]
        public List<SiteDC> SiteList { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string AdminEmail { get; set; }

        [DataMember]
        public string DeputyAdminEmail { get; set; }

        [DataMember]
        public bool? ShowAbuseType { get; set; }

        [DataMember]
        public bool? ShowIncidentDetail { get; set; }

        [DataMember]
        public List<RelationshipToCustomerDC> RelationshipToCustomerList { get; set; }

        [DataMember]
        public List<IncidentLinkDC> IncidentLinkList { get; set; }

        [DataMember]
        public List<ContingencyArrangementDC> ContingencyArrangementList { get; set; }

        [DataMember]
        public List<ControlMeasureDC> ControlMeasureList { get; set; }

        [DataMember]
        public List<String> ContingencyArrangementCodes { get; set; }

        [DataMember]
        public List<String> ControlMeasureCodes { get; set; }

        [DataMember]
        public List<SystemMarkedDC> SystemMarkedList { get; set; }

        [DataMember]
        public List<String> SystemMarkedCodes { get; set; }

        [DataMember]
        public List<InterestedPartyDC> InterestedPartyList { get; set; }

        [DataMember]
        public List<String> InterestedPartyCodes { get; set; }

        [DataMember]
        public string NominatedManager { get; set; }

        [DataMember]
        public Guid NominatedManagercode { get; set; }

        [DataMember]
        public string DeputyNominatedManagers { get; set; }

        [DataMember]
        public List<IncidentUpdateEventDC> IncidentUpdateEvents { get; set; }

        [DataMember]
        public bool IsReadOnly { get; set; }

        [DataMember]
        public string IncidentAdvisoryNote { get; set; }     
    }
}
