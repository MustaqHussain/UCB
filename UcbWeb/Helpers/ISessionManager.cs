using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UcbWeb.Models;
using UcbWeb.ViewModels;

namespace UcbWeb.Helpers
{
    public partial interface ISessionManager
    {
        T IsExpected<T>(T sessionObject);

        Guid? CurrentApplicationCode { get; set; }
        string CurrentPage { get; set; }
        string PageFrom { get; set; }
        string MessageFromPageFrom { get; set; }
        int PageSize { get; set; }
        System.Collections.Generic.List<string> StaffRoleList { get; set; }
        string UserID { get; set; }
        string UserName { get; set; }
        string Copy { get; set; }
        string[] UserRoles { get; set; }
        UcbWeb.Models.NarrativeModel IncidentNarrativeServiceVersion { get; set; }
        UcbWeb.Models.NarrativeModel CurrentIncidentNarrative { get; set; }
        UcbWeb.Models.NarrativeModel LineManagerNarrativeServiceVersion { get; set; }
        UcbWeb.Models.NarrativeModel CurrentLineManagerNarrative { get; set; }
        UcbWeb.Models.NarrativeModel FurtherInfoNarrativeServiceVersion { get; set; }
        UcbWeb.Models.NarrativeModel CurrentFurtherInfoNarrative  { get; set; }
        UcbWeb.Models.NarrativeModel DeficienciesNarrativeServiceVersion {get; set;}
        UcbWeb.Models.NarrativeModel CurrentDeficienciesNarrative { get; set; }
        UcbWeb.Models.NarrativeModel ReviewActionNarrativeServiceVersion { get; set; }
        UcbWeb.Models.NarrativeModel CurrentReviewActionNarrative { get; set; }
        UcbWeb.Models.StaffModel CurrentOwner { get; set; }

        string LineManagerEmailAddress { get; set; }
        List<string> LineManagerEmailAddressList { get; set; }

        bool? ShowIncidentDetail { get; set; }
        bool? ShowAbuseType { get; set; }
        int? TransferSearchPageNumber { get; set; }

        List<AttachmentModel> CurrentRIDDORAttachmentList { get; set; }
        List<AttachmentModel> CurrentFastTrackAttachmentList { get; set; }
        List<AttachmentModel> CurrentRepeatBehaviourAttachmentList { get; set; }
        List<AttachmentModel> CurrentGeneralEvidenceAttachmentList { get; set; }
        List<AttachmentModel> CurrentFurtherInfoAttachmentList { get; set; }
        List<AttachmentModel> CurrentReferralEvidenceAttachmentList { get; set; }


        string NominatedManager { get; set; }
        string DeputyNominatedManagers { get; set; }
        string NominatedManagerEmailAddress { get; set; }
        string FromEmailAddress { get; set; }


        List<IncidentLinkModel> LinkedIncidents { get; set; }
        List<IncidentUpdateEventModel> CurrentIncidentUpdateEventList { get; set; }

        string IncidentType { get; set; }

        bool? IsLinkFromEmail { get; set; }

        string Locale { get; set; }

        EmailVM EmailDetails { get; set; }

        OrganisationSearchCriteriaModel OrganisationSearchCriteria { get; set; }
        TransferSiteSearchCriteriaModel TransferSiteSearchCriteria { get; set; }

        List<StaffModel> SiteNominatedManagersSearch { get; set; }
        List<StaffModel> SiteDeputyNominatedManagersSearch { get; set; }
        List<StaffModel> SiteDeputyNominatedManagers { get; set; }
        string RequestedReport { get; set; }
        string ErrorMessage { get; set; }

        List<string> SSRSQueryParameter { get; set; }
        bool? IsLinkingIncident { get; set; }
        bool? IsLinkedIncidentReadOnly { get; set; }
        string LinkedIncidentReadOnlyIncidentCode { get; set; }
    }
}