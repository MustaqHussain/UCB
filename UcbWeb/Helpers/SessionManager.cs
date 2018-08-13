using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UcbWeb.Models;
using UcbWeb.ViewModels;
using UCBWeb.Exceptions;

namespace UcbWeb.Helpers
{
    public partial class SessionManager : ISessionManager
    {
        //gary
        public string CurrentOwnerCode
        {
            get { return GetFromSession<string>(CurrentOwnerCodeKey); }
            set { SetInSession(CurrentOwnerCodeKey, value); }
        }

        private const string CurrentOwnerCodeKey = "CURRENTOWNERCODE";

        public StaffModel CurrentOwner
        {
            get { return GetFromSession<StaffModel>(CurrentOwnerKey); }
            set { SetInSession(CurrentOwnerKey, value); }
        }

        private const string CurrentOwnerKey = "CURRENTOWNER";

        private T GetFromSession<T>(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }

        private void SetInSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        // Method to use if session is expected
        public T IsExpected<T>(T sessionObject)
        {
            if (null == sessionObject) throw new SessionExpiredException("Session Lost");

            return sessionObject;
        }

        public string[] UserRoles
        {
            get { return GetFromSession<string[]>(UserRolesKey); }
            set { SetInSession(UserRolesKey, value); }
        }

        public string UserID
        {
            get { return GetFromSession<string>(UserIDKey); }
            set { SetInSession(UserIDKey, value); }
        }

        public string UserName
        {
            get { return GetFromSession<string>(UserNameKey); }
            set { SetInSession(UserNameKey, value); }
        }

        public string Copy
        {
            get { return GetFromSession<string>(CopyKey); }
            set { SetInSession(CopyKey, value); }
        }

        public string CurrentPage
        {
            get { return GetFromSession<string>(CurrentPageKey); }
            set { SetInSession(CurrentPageKey, value); }
        }

        public int PageSize
        {
            get { return GetFromSession<int>(PageSizeKey); }
            set { SetInSession(PageSizeKey, value); }
        }

        public List<string> StaffRoleList
        {
            get { return GetFromSession<List<string>>(StaffRoleListKey); }
            set { SetInSession(StaffRoleListKey, value); }
        }

        public Guid? CurrentApplicationCode
        {
            get { return GetFromSession<Guid?>(CurrentApplicationCodeKey); }
            set { SetInSession(CurrentApplicationCodeKey, value); }
        }

        public string PageFrom
        {
            get { return GetFromSession<string>(PageFromKey); }
            set { SetInSession(PageFromKey, value); }
        }
        public string MessageFromPageFrom
        {
            get { return GetFromSession<string>(MessageFromPageFromKey); }
            set { SetInSession(MessageFromPageFromKey, value); }
        }
        public NarrativeModel IncidentNarrativeServiceVersion
        {
            get { return GetFromSession<NarrativeModel>(IncidentNarrativeServiceVersionKey); }
            set { SetInSession(IncidentNarrativeServiceVersionKey, value); }
        }

        public NarrativeModel CurrentIncidentNarrative
        {
            get { return GetFromSession<NarrativeModel>(CurrentIncidentNarrativeKey); }
            set { SetInSession(CurrentIncidentNarrativeKey, value); }
        }

        public NarrativeModel LineManagerNarrativeServiceVersion
        {
            get { return GetFromSession<NarrativeModel>(LineManagerNarrativeServiceVersionKey); }
            set { SetInSession(LineManagerNarrativeServiceVersionKey, value); }
        }

        public NarrativeModel CurrentLineManagerNarrative
        {
            get { return GetFromSession<NarrativeModel>(CurrentLineManagerNarrativeKey); }
            set { SetInSession(CurrentLineManagerNarrativeKey, value); }
        }

        public NarrativeModel FurtherInfoNarrativeServiceVersion
        {
            get { return GetFromSession<NarrativeModel>(FurtherInfoNarrativeServiceVersionKey); }
            set { SetInSession(FurtherInfoNarrativeServiceVersionKey, value); }
        }

        public NarrativeModel CurrentFurtherInfoNarrative
        {
            get { return GetFromSession<NarrativeModel>(CurrentFurtherInfoNarrativeKey); }
            set { SetInSession(CurrentFurtherInfoNarrativeKey, value); }
        }

        public string LineManagerEmailAddress
        {
            get { return GetFromSession<string>(LineManagerEmailAddressKey); }
            set { SetInSession(LineManagerEmailAddressKey, value); }
        }
        public List<string> LineManagerEmailAddressList
        {
            get { return GetFromSession<List<string>>(LineManagerEmailAddressListKey); }
            set { SetInSession(LineManagerEmailAddressListKey, value); }
        }
        public bool? ShowIncidentDetail
        {
            get { return GetFromSession<bool?>(ShowIncidentDetailKey); }
            set { SetInSession(ShowIncidentDetailKey, value); }
        }
        public bool? ShowAbuseType
        {
            get { return GetFromSession<bool?>(ShowAbuseTypeKey); }
            set { SetInSession(ShowAbuseTypeKey, value); }
        }

        public int? TransferSearchPageNumber
        {
            get { return GetFromSession<int?>(TransferSearchPageNumberKey); }
            set { SetInSession(TransferSearchPageNumberKey, value); }
        }

        public List<AttachmentModel> CurrentRIDDORAttachmentList
        {
            get { return GetFromSession<List<AttachmentModel>>(RIDDORAttachmentListKey); }
            set { SetInSession(RIDDORAttachmentListKey, value); }
        }

        public List<AttachmentModel> CurrentFastTrackAttachmentList
        {
            get { return GetFromSession<List<AttachmentModel>>(CurrentFastTrackAttachmentListKey); }
            set { SetInSession(CurrentFastTrackAttachmentListKey, value); }
        }

        public List<AttachmentModel> CurrentRepeatBehaviourAttachmentList
        {
            get { return GetFromSession<List<AttachmentModel>>(CurrentRepeatBehaviourAttachmentListKey); }
            set { SetInSession(CurrentRepeatBehaviourAttachmentListKey, value); }
        }

        public List<AttachmentModel> CurrentGeneralEvidenceAttachmentList
        {
            get { return GetFromSession<List<AttachmentModel>>(CurrentGeneralEvidenceAttachmentListKey); }
            set { SetInSession(CurrentGeneralEvidenceAttachmentListKey, value); }
        }

        public List<AttachmentModel> CurrentFurtherInfoAttachmentList
        {
            get { return GetFromSession<List<AttachmentModel>>(CurrentFurtherInfoListKey); }
            set { SetInSession(CurrentFurtherInfoListKey, value); }
        }

        public List<AttachmentModel> CurrentReferralEvidenceAttachmentList
        {
            get { return GetFromSession<List<AttachmentModel>>(CurrentReferralEvidenceAttachmentListKey); }
            set { SetInSession(CurrentReferralEvidenceAttachmentListKey, value); }
        }

        public string NominatedManager
        {
            get { return GetFromSession<string>(NominatedManagerKey); }
            set { SetInSession(NominatedManagerKey, value); }
        }

        public string NominatedManagerEmailAddress
        {
            get { return GetFromSession<string>(NominatedManagerEmailAddressKey); }
            set { SetInSession(NominatedManagerEmailAddressKey, value); }
        }

        public string FromEmailAddress
        {
            get { return GetFromSession<string>(FromEmailAddressKey); }
            set { SetInSession(FromEmailAddressKey, value); }
        }

        public string DeputyNominatedManagers
        {
            get { return GetFromSession<string>(DeputyNominatedManagersKey); }
            set { SetInSession(DeputyNominatedManagersKey, value); }
        }

        public List<IncidentLinkModel> LinkedIncidents 
        {
            get { return GetFromSession<List<IncidentLinkModel>>(LinkIncidentKey); }
            set { SetInSession(LinkIncidentKey, value); }
        }

        public List<IncidentUpdateEventModel> CurrentIncidentUpdateEventList
        {
            get { return GetFromSession<List<IncidentUpdateEventModel>>(CurrentIncidentUpdateEventListKey); }
            set { SetInSession(CurrentIncidentUpdateEventListKey, value); }
        }

        public NarrativeModel DeficienciesNarrativeServiceVersion
        {
            get { return GetFromSession<NarrativeModel>(DeficienciesNarrativeServiceVersionKey); }
            set { SetInSession(DeficienciesNarrativeServiceVersionKey, value); }
        }

        public NarrativeModel CurrentDeficienciesNarrative
        {
            get { return GetFromSession<NarrativeModel>(CurrentDeficienciesNarrativeKey); }
            set { SetInSession(CurrentDeficienciesNarrativeKey, value); }
        }

        public NarrativeModel ReviewActionNarrativeServiceVersion
        {
            get { return GetFromSession<NarrativeModel>(ReviewActionNarrativeServiceVersionKey); }
            set { SetInSession(ReviewActionNarrativeServiceVersionKey, value); }
        }

        public NarrativeModel CurrentReviewActionNarrative
        {
            get { return GetFromSession<NarrativeModel>(CurrentReviewActionNarrativeKey); }
            set { SetInSession(CurrentReviewActionNarrativeKey, value); }
        }

        public string IncidentType
        {
            get { return GetFromSession<string>(IncidentTypeKey); }
            set { SetInSession(IncidentTypeKey, value); }
        }

        public bool? IsLinkFromEmail
        {
            get { return GetFromSession<bool?>(IsLinkFromEmailKey); }
            set { SetInSession(IsLinkFromEmailKey, value); }
        }

        public string Locale
        {
            get { return GetFromSession<string>(LocaleKey); }
            set { SetInSession(LocaleKey, value); }
        }

        public EmailVM EmailDetails
        {
            get { return GetFromSession<EmailVM>(EmailDetailsKey); }
            set { SetInSession(EmailDetailsKey, value); }
        }

        public OrganisationSearchCriteriaModel OrganisationSearchCriteria
        {
            get { return GetFromSession<OrganisationSearchCriteriaModel>(OrganisationSearchCriteriaKey); }
            set { SetInSession(OrganisationSearchCriteriaKey, value); }
        }

        public TransferSiteSearchCriteriaModel TransferSiteSearchCriteria
        {
            get { return GetFromSession<TransferSiteSearchCriteriaModel>(TransferSiteSearchCriteriaKey); }
            set { SetInSession(TransferSiteSearchCriteriaKey, value); }
        }

        public List<StaffModel> SiteDeputyNominatedManagersSearch
        {
            get { return GetFromSession<List<StaffModel>>(SiteDeputyNominatedManagersSearchKey); }
            set { SetInSession(SiteDeputyNominatedManagersSearchKey, value); }
        }

        public List<StaffModel> SiteNominatedManagersSearch
        {
            get { return GetFromSession<List<StaffModel>>(SiteNominatedManagersSearchKey); }
            set { SetInSession(SiteNominatedManagersSearchKey, value); }
        }

        public string RequestedReport
        {
            get { return GetFromSession<string>(RequestedReportKey); }
            set { SetInSession(RequestedReportKey, value); }
        }

        public List<StaffModel> SiteDeputyNominatedManagers
        {
            get { return GetFromSession<List<StaffModel>>(SiteDeputyNominatedManagersKey); }
            set { SetInSession(SiteDeputyNominatedManagersKey, value); }
        }

        public string ErrorMessage
        {
            get { return GetFromSession<string>(ErrorMessageKey); }
            set { SetInSession(ErrorMessageKey, value); }
        }

        //public string SSRSQueryParameter
        //{
        //    get { return GetFromSession<string>(SSRSQueryParameterKey); }
        //    set { SetInSession(SSRSQueryParameterKey, value); }
        //}
        public List<string> SSRSQueryParameter
        {
            get { return GetFromSession<List<string>>(SSRSQueryParameterKey); }
            set { SetInSession(SSRSQueryParameterKey, value); }
        }

        public bool? IsLinkingIncident
        {
            get { return GetFromSession<bool?>(IsLinkingIncidentKey); }
            set { SetInSession(IsLinkingIncidentKey, value); }
        }

        public bool? IsLinkedIncidentReadOnly
        {
            get { return GetFromSession<bool?>(IsLinkedIncidentReadOnlyKey); }
            set { SetInSession(IsLinkedIncidentReadOnlyKey, value); }
        }

        public string LinkedIncidentReadOnlyIncidentCode
        {
            get { return GetFromSession<string>(LinkedIncidentReadOnlyIncidentCodeKey); }
            set { SetInSession(LinkedIncidentReadOnlyIncidentCodeKey, value); }
        }

        private const string SiteNominatedManagersSearchKey = "SITENOMINATEDMANAGERSSEARCH";
        private const string SiteDeputyNominatedManagersSearchKey = "SITEDEPUTYNOMINATEDMANAGERSSEARCH";
        private const string SiteDeputyNominatedManagersKey = "SITEDEPUTYNOMINATEDMANAGERS";
        private const string OrganisationSearchCriteriaKey = "ORGANISATIONSEARCHCRITERIA";
        private const string TransferSiteSearchCriteriaKey = "TRANSFERSITESEARCHCRITERIA";
        private const string LocaleKey = "LOCALE";
        private const string IsLinkFromEmailKey = "ISLINKFROMEMAIL";
        private const string UserRolesKey = "USERROLES";
        private const string UserIDKey = "USERID";
        private const string UserNameKey = "USERNAME";
        private const string CopyKey = "COPY";
        private const string PasteKey = "PASTE";
        private const string CurrentPageKey = "CURRENTSEARCHPAGE";
        private const string PageSizeKey = "PAGESIZE";
        private const string StaffAccessListKey = "STAFFACCESSLIST";
        private const string ApplicationListKey = "APPLICATIONLIST";
        private const string OrganisationByTypeListKey = "ORGANISATIONBYTYPELIST";
        private const string AllOrganisationsForApplicationByTypesListKey = "ALLORGANISATIONFORAPPLICATIONBYTYPELIST";
        private const string StaffApplicationAttributeListKey = "STAFFAPPLICATIONATTRIBUTELIST";
        private const string StaffApplicationAttributeListDBVersionKey = "STAFFAPPLICATIONATTRIBUTEDBLIST";
        private const string StaffOrganisationListKey = "STAFFORGANISATIONLIST";
        private const string StaffOrganisationListDBKey = "STAFFORGANISATIONDBLIST";
        private const string StaffRoleListKey = "STAFFROLELIST";
        private const string RootOrganisationKey = "ROOTORGANISATION";
        private const string CurrentApplicationCodeKey = "CURRENTAPPLICATIONCODE";
        private const string CurrentStaffForAdminKey = "CURRENTSTAFFFORADMIN";
        private const string PageFromKey = "PAGEFROM";
        private const string MessageFromPageFromKey = "MESSAGEFROMPAGEFROM";
        private const string StaffSearchCriteraKey = "STAFFSEARCHCRITERIA";

        private const string CurrentIncidentNarrativeKey = "CURRENTINCIDENTNARRATIVE";
        private const string IncidentNarrativeServiceVersionKey = "INCIDENTNARRATIVESERVICEVERSION";

        private const string CurrentLineManagerNarrativeKey = "CURRENTLINEMANAGERNARRATIVE";
        private const string LineManagerNarrativeServiceVersionKey = "LINEMANAGERNARRATIVESERVICEVERSION";

        private const string CurrentFurtherInfoNarrativeKey = "CURRENTFURTHERINFONARRATIVE";
        private const string FurtherInfoNarrativeServiceVersionKey = "FURTHERINFONARRATIVESERVICEVERSION";

        private const string DeficienciesNarrativeServiceVersionKey = "DEFICIENCIESNARRATIVESERVICEVERSION";
        private const string CurrentDeficienciesNarrativeKey = "CURRENTDEFICIENCIESNARRATIVE";

        private const string ReviewActionNarrativeServiceVersionKey = "REVIEWACTIONNARRATIVESERVICEVERSION";
        private const string CurrentReviewActionNarrativeKey = "CURRENTREVIEWACTIONNARRATIVE";

        private const string LineManagerEmailAddressKey = "LINEMANAGEREMAILADDRESS";
        private const string LineManagerEmailAddressListKey = "LINEMANAGEREMAILADDRESSLIST";

        private const string FromEmailAddressKey = "FROMEMAILADDRESS";

        private const string ShowAbuseTypeKey = "SHOWABUSETYPE";
        private const string ShowIncidentDetailKey = "SHOWINCIDENTDETAIL";
        private const string TransferSearchPageNumberKey = "TRANSFERSEARCHPAGENUMBERKEY";

        private const string RIDDORAttachmentListKey = "RIDDORATTACHMENTLISTKEY";
        private const string CurrentFastTrackAttachmentListKey = "CURRENTFASTTRACKATTACHMENTLISTKEY";
        private const string CurrentRepeatBehaviourAttachmentListKey = "CURRENTREPEATBEHAVIOURATTACHMENTLISTKEY";
        private const string CurrentGeneralEvidenceAttachmentListKey = "CURRENTGENERALEVIDENCEATTACHMENTLISTKEY";
        private const string CurrentFurtherInfoListKey = "CURRENTFURTHERINFOLISTKEY";
        private const string CurrentReferralEvidenceAttachmentListKey = "CURRENTREFERRALEVIDENCEATTACHMENTLISTKEY";

        private const string NominatedManagerKey = "NOMINATEDMANAGERKEY";
        private const string DeputyNominatedManagersKey = "DEPUTYNOMINATEDMANAGERSKEY";

        private const string NominatedManagerEmailAddressKey = "NOMINATEDMANAGEREMAILADDRESSKEY";
        private const string LinkIncidentKey = "LINKINCIDENTKEY";

        private const string CurrentIncidentUpdateEventListKey = "CURRENTINCIDENTUPDATEEVENTLISTKEY";
        private const string RequestedReportKey = "REQUESTEDREPORTKEY";

        private const string IncidentTypeKey = "INCIDENTTYPE";

        private const string EmailDetailsKey = "EMAILDETAILS";
        private const string ErrorMessageKey = "ERRORMESSAGE";

        private const string SSRSQueryParameterKey = "SSRSQUERYPARAMETERKEY";
        private const string IsLinkingIncidentKey = "ISLINKINGINCIDENTKEY";
        private const string IsLinkedIncidentReadOnlyKey = "ISLINKEDINCIDENTREADONLYKEY";
        private const string LinkedIncidentReadOnlyIncidentCodeKey = "LINKEDINCIDENTREADONLYINCIDENTCODEKEY";
    }
    
}
