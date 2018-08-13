using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UcbWeb.UcbService;
using UcbWeb.Helpers;
using UcbWeb.Models;
using Dwp.Adep.Ucb.ResourceLibrary;

namespace UcbWeb.Controllers
{
    public class SplController : BaseController
    {
        private IUcbService UcbService;

        // Dependency Injection enabled constructors
        public SplController()
            : this(new UcbServiceClient(), new SessionManager(), new CacheManager())
        {
        }

        public SplController(IUcbService UcbService, ISessionManager sessionManager, ICacheManager cacheManager)
            : base(sessionManager, cacheManager)
        {
            this.UcbService = UcbService;
        }

        #region Search Reports

        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.BUSINESS_AREA_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.READ_ONLY + "," + AppRoles.TRADE_UNION)]
        public ActionResult SPLByAllCases()
        {
            sessionManager.PageFrom = "SPLByAllCases";
            //Report name now passed in session
            sessionManager.RequestedReport = "SPLByAllCases";
            return Redirect("~/Reports/Reports.aspx?title=" + Resources.LABEL_LINK_SPLBYALLCASES);
        }
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.BUSINESS_AREA_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.READ_ONLY + "," + AppRoles.TRADE_UNION)]
        public ActionResult SPLByArchived()
        {
            sessionManager.PageFrom = "SPLByArchived";
            //Report name now passed in session
            sessionManager.RequestedReport = "SPLByArchived";
            return Redirect("~/Reports/Reports.aspx?title=" + Resources.LABEL_LINK_SPLBYARCHIVED);
        }
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.BUSINESS_AREA_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.READ_ONLY + "," + AppRoles.TRADE_UNION)]
        public ActionResult SPLByBusinessUnit()
        {
            sessionManager.PageFrom = "SPLByBusinessUnit";
            //Report name now passed in session
            sessionManager.RequestedReport = "SPLByBusinessUnit";
            return Redirect("~/Reports/Reports.aspx?title=" + Resources.LABEL_LINK_SPLBYBUSINESSUNIT);
        }
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.BUSINESS_AREA_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.READ_ONLY + "," + AppRoles.TRADE_UNION)]
        public ActionResult SPLByControlMeasure()
        {
            sessionManager.PageFrom = "SPLByControlMeasure";
            //Report name now passed in session
            sessionManager.RequestedReport = "SPLByControlMeasure";
            return Redirect("~/Reports/Reports.aspx?title=" + Resources.LABEL_LINK_SPLBYCONTROLMEASURE);
        }
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.BUSINESS_AREA_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.READ_ONLY + "," + AppRoles.TRADE_UNION)]
        public ActionResult SPLByDistrict()
        {
            sessionManager.PageFrom = "SPLByDistrict";
            //Report name now passed in session
            sessionManager.RequestedReport = "SPLByDistrict";
            return Redirect("~/Reports/Reports.aspx?title=" + Resources.LABEL_LINK_SPLBYDISTRICT);
        }
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.BUSINESS_AREA_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.READ_ONLY + "," + AppRoles.TRADE_UNION)]
        public ActionResult SPLByName()
        {
            sessionManager.PageFrom = "SPLByName";
            //Report name now passed in session
            sessionManager.RequestedReport = "SPLByName";
            return Redirect("~/Reports/Reports.aspx?title=" + Resources.LABEL_LINK_SPLBYNAME);
        }
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.BUSINESS_AREA_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.READ_ONLY + "," + AppRoles.TRADE_UNION)]
        public ActionResult SPLByNino()
        {
            sessionManager.PageFrom = "SPLByNino";
            //Report name now passed in session
            sessionManager.RequestedReport = "SPLByNino";
            return Redirect("~/Reports/Reports.aspx?title=" + Resources.LABEL_LINK_SPLBYNINO);
        }
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.BUSINESS_AREA_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.READ_ONLY + "," + AppRoles.TRADE_UNION)]
        public ActionResult SPLByPostcode()
        {
            sessionManager.PageFrom = "SPLByPostcode";
            //Report name now passed in session
            sessionManager.RequestedReport = "SPLByPostcode";
            return Redirect("~/Reports/Reports.aspx?title=" + Resources.LABEL_LINK_SPLBYPOSTCODE);
        }
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.BUSINESS_AREA_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.READ_ONLY + "," + AppRoles.TRADE_UNION)]
        public ActionResult SPLByIncidentID()
        {
            sessionManager.PageFrom = "SPLByIncidentID";
            //Report name now passed in session
            sessionManager.RequestedReport = "SPLByIncidentID";
            return Redirect("~/Reports/Reports.aspx?title=" + Resources.LABEL_LINK_SPLBYINCIDENTID);
        }
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.BUSINESS_AREA_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.READ_ONLY + "," + AppRoles.TRADE_UNION)]
        public ActionResult SPLByRegion()
        {
            sessionManager.PageFrom = "SPLByRegion";
            //Report name now passed in session
            sessionManager.RequestedReport = "SPLByRegion";
            return Redirect("~/Reports/Reports.aspx?title=" + Resources.LABEL_LINK_SPLBYREGION);
        }
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.BUSINESS_AREA_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.READ_ONLY + "," + AppRoles.TRADE_UNION)]
        public ActionResult SPLBySite()
        {
            sessionManager.PageFrom = "SPLBySite";
            //Report name now passed in session
            sessionManager.RequestedReport = "SPLBySite";
            return Redirect("~/Reports/Reports.aspx?title=" + Resources.LABEL_LINK_SPLBYSITE);
        }
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.BUSINESS_AREA_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.READ_ONLY + "," + AppRoles.TRADE_UNION)]
        public ActionResult SPLBy3rdPartyReferrals()
        {
            sessionManager.PageFrom = "SPLBy3rdPartyReferrals";
            //Report name now passed in session
            sessionManager.RequestedReport = "SPLBy3rdPartyReferrals";
            return Redirect("~/Reports/Reports.aspx?title=" + Resources.LABEL_LINK_SPLBY3RDPARTYREFERRALS);
        }
        
        #endregion

    }
}
