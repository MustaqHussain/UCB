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
    public class SearchesController : BaseController
    {
        private IUcbService UcbService;

		// Dependency Injection enabled constructors
        public SearchesController()
            : this(new UcbServiceClient(),new SessionManager(), new CacheManager())
        {
        }

        public SearchesController(IUcbService UcbService, ISessionManager sessionManager, ICacheManager cacheManager)
			:base(sessionManager,cacheManager)
        {
            this.UcbService = UcbService;
        }

        #region Search Reports

        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.BUSINESS_AREA_MANAGER + "," + AppRoles.NOMINATED_MANAGER)]
        public ActionResult MyNewReports()
        {
            sessionManager.PageFrom = "SearchMyNewReports";
            //Report name now passed in session
            sessionManager.RequestedReport = "MyNewReportsReport";
            return Redirect("~/Reports/Reports.aspx?title=" + Resources.LABEL_LINK_VIEWMYNEWREPORTS);
        }

        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.BUSINESS_AREA_MANAGER + "," + AppRoles.NOMINATED_MANAGER)]
        public ActionResult MyReviews()
        {
            sessionManager.PageFrom = "SearchMyReviews";
            //Report name now passed in session
            sessionManager.RequestedReport = "MyReviewsReport";
            return Redirect("~/Reports/Reports.aspx?title=" + Resources.LABEL_LINK_VIEWMYREVIEWS);
        }

        [CustomAuthorize(Roles = AppRoles.DEPUTY_NOMINATED_MANAGER)]
        public ActionResult DeputyMyNewReports()
        {
            sessionManager.PageFrom = "DeputySearchMyNewReports";
            //Report name now passed in session
            sessionManager.RequestedReport = "DeputyMyNewReportsReport";
            return Redirect("~/Reports/Reports.aspx?title=" + Resources.LABEL_LINK_VIEWMYNEWREPORTS);
        }

        [CustomAuthorize(Roles = AppRoles.DEPUTY_NOMINATED_MANAGER)]
        public ActionResult DeputyMyReviews()
        {
            sessionManager.PageFrom = "DeputySearchMyReviews";
            //Report name now passed in session
            sessionManager.RequestedReport = "DeputyMyReviewsReport";
            return Redirect("~/Reports/Reports.aspx?title=" + Resources.LABEL_LINK_VIEWMYREVIEWS);
        }

        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.BUSINESS_AREA_MANAGER + "," + AppRoles.NOMINATED_MANAGER)]
        public ActionResult MyForwardLook()
        {
            sessionManager.PageFrom = "SearchMyForwardLook";
            //Report name now passed in session
            sessionManager.RequestedReport = "MyForwardLookReport";
            return Redirect("~/Reports/Reports.aspx?title=" + Resources.LABEL_LINK_VIEWMYFORWARDLOOK);
        }

        #endregion

    }
}
