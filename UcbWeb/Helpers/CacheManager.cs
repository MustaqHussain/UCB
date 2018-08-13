using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UcbWeb.Models;
using UcbWeb.UcbService;
using UcbWeb.ViewModels;
using AutoMapper;
using System.Web.Mvc;

namespace UcbWeb.Helpers
{
    public partial class CacheManager : ICacheManager
    {
        private static T Get<T>(string cacheID, Func<T> getItemCallback) where T : class
        {
            T item = HttpRuntime.Cache.Get(cacheID) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpContext.Current.Cache.Insert(cacheID, item, null, DateTime.Now.Add(new TimeSpan(0, 0, 3)), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            return item;
        }

        private const string SCLists = "UCBLISTS";

        private const string IncidentListsKey = "INCIDENTLISTS";

        public IncidentLookupListsCacheObject IncidentListCache
        {
            get { return Get<IncidentLookupListsCacheObject>(IncidentListsKey, GetIncidentAndLookups); }
        }

        private IncidentLookupListsCacheObject GetIncidentAndLookups()
        {

            UcbServiceClient sc = new UcbServiceClient();

            try
            {
                // Get instance of Language Manager
                LanguageManager language = new LanguageManager();

                // Get users localisation
                string locale = language.GetLocale();

                IncidentVMDC returnObject = sc.GetIncident(HttpContext.Current.User.Identity.Name, HttpContext.Current.User.Identity.Name, "Ucb", "", null, locale);

                sc.Close();

                IncidentLookupListsCacheObject CachedLists = new IncidentLookupListsCacheObject();

                CachedLists.JobRoleList = Mapper.Map<IEnumerable<JobRoleDC>, List<JobRoleModel>>(returnObject.JobRoleList);
                CachedLists.ReferrerList = Mapper.Map<IEnumerable<ReferrerDC>, List<ReferrerModel>>(returnObject.ReferrerList);
                CachedLists.StaffMemberBusinessList = Mapper.Map<IEnumerable<OrganisationDC>, List<OrganisationModel>>(returnObject.StaffMemberBusinessList.OrderBy(x => x.Name));
                CachedLists.StaffMemberBusinessAreaList = Mapper.Map<IEnumerable<OrganisationDC>, List<OrganisationModel>>(returnObject.StaffMemberBusinessAreaList.OrderBy(x => x.Name));
                //***Note - have removed sites/home offices from cache***
                //CachedLists.StaffMemberHomeOfficeList = Mapper.Map<IEnumerable<OrganisationDC>, List<OrganisationModel>>(returnObject.StaffMemberHomeOfficeList);
                CachedLists.EventLeadingToIncidentList = Mapper.Map<IEnumerable<EventLeadingToIncidentDC>, List<EventLeadingToIncidentModel>>(returnObject.EventLeadingToIncidentList);
                CachedLists.IncidentLocationList = Mapper.Map<IEnumerable<IncidentLocationDC>, List<IncidentLocationModel>>(returnObject.IncidentLocationList);
                CachedLists.IncidentCategoryList = Mapper.Map<IEnumerable<IncidentCategoryDC>, List<IncidentCategoryModel>>(returnObject.IncidentCategoryList);
                CachedLists.IncidentTypeList = Mapper.Map<IEnumerable<IncidentTypeDC>, List<IncidentTypeModel>>(returnObject.IncidentTypeList);
                CachedLists.IncidentDetailsList = Mapper.Map<IEnumerable<IncidentDetailDC>, List<IncidentDetailModel>>(returnObject.IncidentDetailsList);
                CachedLists.AbuseTypeList = Mapper.Map<IEnumerable<AbuseTypeDC>, List<AbuseTypeModel>>(returnObject.AbuseTypeList);
                CachedLists.FastTrackAttachmentList = Mapper.Map<IEnumerable<AttachmentDC>, List<AttachmentModel>>(returnObject.FastTrackAttachmentList);
                CachedLists.RIDDORAttachmentList = Mapper.Map<IEnumerable<AttachmentDC>, List<AttachmentModel>>(returnObject.RIDDORAttachmentList);

                CachedLists.AdminEmail = returnObject.AdminEmail;
                CachedLists.DeputyAdminEmail = returnObject.DeputyAdminEmail;

                CachedLists.RelationshipToCustomerList = Mapper.Map<IEnumerable<RelationshipToCustomerDC>, List<RelationshipToCustomerModel>>(returnObject.RelationshipToCustomerList);
                CachedLists.ContingencyArrangementBaseList = Mapper.Map<IEnumerable<ContingencyArrangementDC>, List<ContingencyArrangementModel>>(returnObject.ContingencyArrangementList);
                CachedLists.ControlMeasureBaseList = Mapper.Map<IEnumerable<ControlMeasureDC>, List<ControlMeasureModel>>(returnObject.ControlMeasureList);
                CachedLists.SystemMarkedBaseList = Mapper.Map<IEnumerable<SystemMarkedDC>, List<SystemMarkedModel>>(returnObject.SystemMarkedList);
                CachedLists.InterestedPartyBaseList = Mapper.Map<IEnumerable<InterestedPartyDC>, List<InterestedPartyModel>>(returnObject.InterestedPartyList);

                //Select Lists
                CachedLists.ContingencyArrangementList = returnObject.ContingencyArrangementList.Select(x => new SelectListItem() { Value = x.Code.ToString(), Text = x.Description }).ToList();
                CachedLists.ControlMeasureList = returnObject.ControlMeasureList.Select(x => new SelectListItem() { Value = x.Code.ToString(), Text = x.ControlMeasureDescription }).ToList();
                CachedLists.SystemMarkedList = returnObject.SystemMarkedList.Select(x => new SelectListItem() { Value = x.Code.ToString(), Text = x.Description }).ToList();
                CachedLists.InterestedPartyList = returnObject.InterestedPartyList.Select(x => new SelectListItem() { Value = x.Code.ToString(), Text = x.Description }).ToList();



                return CachedLists;
            }
            catch (Exception e)
            {
                // Handle the exception
                string message = ExceptionManager.HandleException(e, sc);
                
                return null;
            }
        }
    }

    // Manages cached lookup lists for Incident
    public class IncidentLookupListsCacheObject
    {
        public List<JobRoleModel> JobRoleList { get; set; }
        public List<ReferrerModel> ReferrerList { get; set; }
        public List<OrganisationModel> StaffMemberBusinessList { get; set; }
        public List<OrganisationModel> StaffMemberBusinessAreaList { get; set; }
        public List<OrganisationModel> StaffMemberHomeOfficeList { get; set; }
        public List<SiteModel> StaffMemberHomeOfficeSiteList { get; set; }
        public List<CustomerModel> CustomerList { get; set; }
        public List<EventLeadingToIncidentModel> EventLeadingToIncidentList { get; set; }
        public List<IncidentLocationModel> IncidentLocationList { get; set; }
        public List<IncidentCategoryModel> IncidentCategoryList { get; set; }
        public List<IncidentTypeModel> IncidentTypeList { get; set; }
        public List<IncidentDetailModel> IncidentDetailsList { get; set; }
        public List<AbuseTypeModel> AbuseTypeList { get; set; }
        public List<NarrativeModel> IncidentNarrativeList { get; set; }
        public List<NarrativeModel> LineManagerNarrativeList { get; set; }
        public List<AttachmentModel> FastTrackAttachmentList { get; set; }
        public List<AttachmentModel> RIDDORAttachmentList { get; set; }
        public List<NarrativeModel> FurtherInfoNarrativeList { get; set; }
        public List<OrganisationModel> OrganisationList { get; set; }
        public List<SiteModel> SiteList { get; set; }
        public string AdminEmail { get; set; }
        public string DeputyAdminEmail { get; set; }
        public List<RelationshipToCustomerModel> RelationshipToCustomerList { get; set; }
        public List<SelectListItem> ContingencyArrangementList { get; set; }
        public List<SelectListItem> ControlMeasureList { get; set; }
        public List<SelectListItem> SystemMarkedList { get; set; }
        public List<SelectListItem> InterestedPartyList { get; set; }
        public List<ContingencyArrangementModel> ContingencyArrangementBaseList { get; set; }
        public List<ControlMeasureModel> ControlMeasureBaseList { get; set; }
        public List<SystemMarkedModel> SystemMarkedBaseList { get; set; }
        public List<InterestedPartyModel> InterestedPartyBaseList { get; set; }
        public string IncidentAdvisoryNote { get; set; }        

    }
}