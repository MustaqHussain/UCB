using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using System.Net;
using UcbWeb.Helpers;
using Dwp.Adep.Ucb.ResourceLibrary;
using UcbWeb.Models;

namespace UcbWeb.Reports
{
    public partial class Reports : System.Web.UI.Page
    {
        private SessionManager _sessionManager = new SessionManager();

        private int QUERY_LASTNAME = 0;
        private int QUERY_NINO = 1;
        private int QUERY_POSTCODE = 2;
        private int QUERY_INCIDENTID = 3;

        protected string UserName
        {
            get
            {
                return _sessionManager.UserName ?? string.Empty; 
            }
        }

        protected string UserID
        {
            get 
            {
                return _sessionManager.UserID ?? string.Empty;
            }
        }

        protected string Title
        {
            get
            {
                string title = Request.QueryString.Get("title");
                if (String.IsNullOrEmpty(title))
                    return Resources.SYS_NAME;
                else
                    return title;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //prevent caching of reports - fixes issues of 'reports execution has expired or cannot be found' error
            this.Response.CacheControl = "no-cache";

            // Ensure SSRSQueryParameter exist
            if (_sessionManager.SSRSQueryParameter == null)
            {
                _sessionManager.SSRSQueryParameter = new List<string> { "", "", "", "" };
            }

            // Add to session user Query from SSRS/RDL
            if (this.OperationalReportViewer != null && this.OperationalReportViewer.ServerReport != null && this.OperationalReportViewer.ServerReport.ReportServerCredentials != null)
            {
                // Query parameter assumed to be 3rd paramter
                ReportParameterInfoCollection reportParm = this.OperationalReportViewer.ServerReport.GetParameters();
                if (reportParm.Count() > 2 && reportParm.ToList<ReportParameterInfo>()[2].Values.Count() > 0)
                {

                    switch (reportParm.ToList<ReportParameterInfo>()[2].Name)
                    {
                        case "LastName":
                            _sessionManager.SSRSQueryParameter[QUERY_LASTNAME] = reportParm.ToList<ReportParameterInfo>()[2].Values[0].ToString();
                            break;
                        case "NINO":
                            _sessionManager.SSRSQueryParameter[QUERY_NINO] = reportParm.ToList<ReportParameterInfo>()[2].Values[0].ToString();
                            break;
                        case "Postcode":
                            _sessionManager.SSRSQueryParameter[QUERY_POSTCODE] = reportParm.ToList<ReportParameterInfo>()[2].Values[0].ToString();
                            break;
                        case "IncidentID":
                            _sessionManager.SSRSQueryParameter[QUERY_INCIDENTID] = reportParm.ToList<ReportParameterInfo>()[2].Values[0].ToString();
                            break;
                        default:
                            break;
                    }
                }
            }
            

            if (!Page.IsPostBack)
            {
                AuthorisationManager authManager = new AuthorisationManager();
                authManager.GetUserAuthorisationInfo(new HttpContextWrapper(HttpContext.Current), "UCB");

                // Check authorised user
                var httpContextBase = new HttpContextWrapper(HttpContext.Current);
                if (!httpContextBase.User.IsInRole(AppRoles.APPLICATION))
                {
                    Response.Redirect("~/Home/UnAuthorized");
                }

                if (!string.IsNullOrEmpty(_sessionManager.MessageFromPageFrom))
                {
                    this.Message.InnerText = _sessionManager.MessageFromPageFrom;
                    _sessionManager.MessageFromPageFrom = null;
                }
                else
                {
                    this.Message.InnerText = "";
                }
                // Get report server and location information
                string reportpath;
                string ReportServer = ConfigurationManager.AppSettings["ReportServer"];
                string ReportLocation = ConfigurationManager.AppSettings["ReportLocation"];
                ScriptManager MyScriptManager = ScriptManager.GetCurrent(this);
                MyScriptManager.AsyncPostBackTimeout = 600;

                this.OperationalReportViewer.ServerReport.ReportServerUrl = new Uri(ReportServer);
                this.OperationalReportViewer.ServerReport.ReportServerCredentials = new ReportCredentials();
				this.OperationalReportViewer.InteractiveDeviceInfos.Add("AccessibleTablix", "true");
                
                if (_sessionManager.PageFrom != "MIMenu")
                {
                    reportpath = ConfigurationManager.AppSettings["ReportLocation"] + _sessionManager.IsExpected(_sessionManager.RequestedReport);//.Request.QueryString.Get("report");
                    switch (_sessionManager.IsExpected(_sessionManager.RequestedReport))
                    {
                        case "MyForwardLookReport":
                        case "MyNewReportsReport":
                        case "MyReviewsReport":
                        case "SPLByAllCases":
                        case "SPLByArchived":
                        case "SPLByBusinessUnit":
                        case "SPLByControlMeasure":
                        case "SPLByDistrict":
                        case "SPLByName":
                        case "SPLByNino":
                        case "SPLByPostcode":
                        case "SPLByIncidentID":
                        case "SPLBy3rdPartyReferrals":
                        case "SPLBySite":
                            this.OperationalReportViewer.ShowPrintButton = false;
                            this.OperationalReportViewer.ShowExportControls = false;
                            break;
                        default:
                            this.OperationalReportViewer.ShowPrintButton = true;
                            this.OperationalReportViewer.ShowExportControls = true;
                            break;
                    }
                }
                else
                {
                    reportpath = ConfigurationManager.AppSettings["PublishedLocation"] + _sessionManager.RequestedReport;
                    StandardReportModel Report = _sessionManager.CurrentStandardReport;
                    if (Report != null)
                    {
                        if (Report.IsPrintable)
                        {
                            this.OperationalReportViewer.ShowPrintButton = true;
                        }
                        else
                        {
                            this.OperationalReportViewer.ShowPrintButton = false;
                        }
                        if (Report.IsExportable)
                        {
                            this.OperationalReportViewer.ShowExportControls = true;
                        }
                        else
                        {
                            this.OperationalReportViewer.ShowExportControls = false;
                        }
                        reportpath = ConfigurationManager.AppSettings["PublishedLocation"] + Report.ReportName;
                    }
                }
                this.OperationalReportViewer.ServerReport.ReportPath = reportpath;
                
                
                try
                {
                    /*
                    //Need to pass these 
                    SessionManager.UserIdentity; 
                    then call stored proc in report (have as Look in repositories for further info) 
                    (Guid,Guid,"AccuracyMonitoring",string.Empty) (ConfigurationAllowDefinition NetPipeStyleUriParser 
                    */


                    int startOfBaseUrl = HttpContext.Current.Request.Url.ToString().IndexOf("/Reports/Reports.aspx");
                    string baseUrl = HttpContext.Current.Request.Url.ToString().Substring(0, startOfBaseUrl);

                    List<ReportParameter> ParameterList = new List<ReportParameter>();
                    if (_sessionManager.PageFrom != "MIMenu")
                    {
                        ParameterList.Add(new ReportParameter("BaseUrl", baseUrl));
                        ParameterList.Add(new ReportParameter("UserID", _sessionManager.UserID));

                        // Add Query from session, which was saved when RDL executed
                        switch (_sessionManager.IsExpected(_sessionManager.RequestedReport))
                        {
                            case "SPLByName":
                                ParameterList.Add(new ReportParameter("LastName", _sessionManager.SSRSQueryParameter[QUERY_LASTNAME]));
                                break;
                            case "SPLByNino":
                                ParameterList.Add(new ReportParameter("NINO", _sessionManager.SSRSQueryParameter[QUERY_NINO]));
                                break;
                            case "SPLByPostcode":
                                ParameterList.Add(new ReportParameter("Postcode", _sessionManager.SSRSQueryParameter[QUERY_POSTCODE]));
                                break;
                            case "SPLByIncidentID":
                                ParameterList.Add(new ReportParameter("IncidentID", _sessionManager.SSRSQueryParameter[QUERY_INCIDENTID]));
                                break;
                            default:
                                break;
                        }
                    }
                    else 
                    {
                        ParameterList.Add(new ReportParameter("ReportUserName", _sessionManager.UserName));
                        ParameterList.Add(new ReportParameter("ReportUserCode", _sessionManager.UserID));
                    }
                    this.OperationalReportViewer.ShowParameterPrompts = true;
                    this.OperationalReportViewer.ShowPromptAreaButton = true;
                    this.OperationalReportViewer.PromptAreaCollapsed = false;
                    this.OperationalReportViewer.ServerReport.SetParameters(ParameterList);
                }
                catch
                {
                    //If ReportUser parameter doesn't exist ignore it.
                }

            }
        }
    }
    public class ReportCredentials : IReportServerCredentials
    {

        #region IReportServerCredentials Members

        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;

            // Not using form credentials
            return false;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                //set the credentials
                string UserReportID = ConfigurationManager.AppSettings["userReportID"];
                string UserReportPassword = ConfigurationManager.AppSettings["userReportPassword"];
                string UserReportDomain = ConfigurationManager.AppSettings["userReportDomain"];

                return new NetworkCredential(UserReportID, UserReportPassword, UserReportDomain);
            }
        }

        #endregion
    }
}
