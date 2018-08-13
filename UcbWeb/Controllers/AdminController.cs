using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel;
using Dwp.Adep.Ucb.ResourceLibrary;
using AutoMapper;
using UcbWeb.Helpers;
using UcbWeb.ViewModels;
using UcbWeb.Models;
using UcbWeb.UcbService;

namespace UcbWeb.Controllers
{
    public class AdminController : BaseController
    {
        
		private IUcbService UcbService;

		// Dependency Injection enabled constructors
        public AdminController()
            : this(new UcbServiceClient(),new SessionManager(), new CacheManager())
        {
        }

        public AdminController(IUcbService UcbService, ISessionManager sessionManager, ICacheManager cacheManager)
			:base(sessionManager,cacheManager)
        {
            this.UcbService = UcbService;
        }

        #region AdminMenu

        // GET: /Admin/AdminMenu
        [CustomAuthorize(Roles = AppRoles.ADMIN)]
        public ActionResult AdminMenu()
        {
            // Retrieve model containing all Admin menu items
            var model = GetModel();

            return View(model);
        }

        // POST: /Admin/AdminMenu
        [HttpPost]
        public ActionResult AdminMenu(bool isUseless = false)
        {
            // Search request object keys
            foreach (string Key in Request.Form.Keys)
            {
                // Find Select button which was clicked
                if (Key.StartsWith("SelectButton_"))
                {
                    // Retrieve the ID assoicated with this button
                    int value = int.Parse(Key.Substring(13));

                    // Retrieve model containing all Admin menu items
                    var model = GetModel();

                    // Find item selected
                    AdminMenuModel selectedMenuItem = model.AdminList.Find(x => x.ID == value);

                    string[] location = selectedMenuItem.Location.Split('/');

                    // Redirect to Admin screen of item selected
                    return RedirectToAction(location[1], location[0]);

                }
            }

            return RedirectToAction("Index");
        }

        private AdminMenuVM GetModel()
        {
            var model = new AdminMenuVM();
            model.AdminList = new List<AdminMenuModel>();

            model.AdminList.Add(new AdminMenuModel { ID = 1, Name = Resources.LABEL_ADMINMENU_ABUSE_TYPE, Location = "AbuseType/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 2, Name = Resources.LABEL_ADMINMENU_CONTINGENCY_ARRANGEMENTS_MADE, Location = "ContingencyArrangement/Search" });
            //model.AdminList.Add(new AdminMenuModel { ID = 3, Name = Resources.LABEL_ADMINMENU_CONTENT, Location = "Content/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 4, Name = Resources.LABEL_ADMINMENU_CONTROL_MEASURES, Location = "ControlMeasure/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 5, Name = Resources.LABEL_ADMINMENU_EVENT_LEADING_UP_TO_INCIDENT, Location = "EventLeadingToIncident/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 6, Name = Resources.LABEL_ADMINMENU_INCIDENT_CATEGORY, Location = "IncidentCategory/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 7, Name = Resources.LABEL_ADMINMENU_INCIDENT_DETAILS, Location = "IncidentDetail/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 8, Name = Resources.LABEL_ADMINMENU_INCIDENT_LOCATION, Location = "IncidentLocation/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 9, Name = Resources.LABEL_ADMINMENU_INCIDENT_TYPE, Location = "IncidentType/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 10, Name = Resources.LABEL_ADMINMENU_INTERESTED_PARTIES, Location = "InterestedParty/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 11, Name = Resources.LABEL_ADMINMENU_IT_SYSTEMS_MARKED, Location = "SystemMarked/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 12, Name = Resources.LABEL_ADMINMENU_JOB_ROLE, Location = "JobRole/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 13, Name = Resources.LABEL_ADMINMENU_SITE, Location = "Organisation/SiteOrganisationSearch" });
            model.AdminList.Add(new AdminMenuModel { ID = 14, Name = Resources.LABEL_ADMINMENU_REFERRER, Location = "Referrer/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 15, Name = Resources.LABEL_ADMINMENU_RELATIONSHIP_TO_CUSTOMER, Location = "RelationshipToCustomer/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 16, Name = Resources.LABEL_ADMINMENU_REPORT_CATEGORY, Location = "ReportCategory/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 17, Name = Resources.LABEL_ADMINMENU_STANDARD_REPORT, Location = "StandardReport/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 18, Name = Resources.LABEL_ADMINMENU_SYSTEM_PARAMETER, Location = "SystemParameter/Search" });
            
            return model;
        }
        #endregion

    }
}