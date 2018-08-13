using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UcbWeb.UcbService;
using UcbWeb.Helpers;
using UcbWeb.Models;
using AutoMapper;

namespace UcbWeb.Controllers
{
    public class PublishedReportController : BaseController
    {
        private IUcbService UcbService;

        // Dependency Injection enabled constructors
        public PublishedReportController()
            : this(new UcbServiceClient(), new SessionManager(), new CacheManager())
        {
        }

        public PublishedReportController(IUcbService UcbService, ISessionManager sessionManager, ICacheManager cacheManager)
            : base(sessionManager, cacheManager)
        {
            this.UcbService = UcbService;
        }

        #region Search Reports

        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.BUSINESS_AREA_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.READ_ONLY + "," + AppRoles.TRADE_UNION)]
        public ActionResult MIReport()
        {
            sessionManager.PageFrom = "MIMenu";
            string ReportCode = Request.QueryString["ReportID"];
            UcbServiceClient sc = new UcbServiceClient();
            StandardReportVMDC report=null;
            try
            {
                report = sc.GetStandardReport(CurrentUser, CurrentUser, "Ucb", "", ReportCode);
                sc.Close();
            }
            catch (Exception e)
            {
                ExceptionManager.HandleException(e, sc);
            }
            sessionManager.CurrentStandardReport = Mapper.Map<StandardReportModel>(report.StandardReportItem);
            //Report Name now passed in session
            sessionManager.RequestedReport = report.StandardReportItem.ReportName;
            return Redirect("~/Reports/Reports.aspx");
        }
       
        

        #endregion

    }
}
