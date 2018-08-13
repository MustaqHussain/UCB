using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using System.Web.Configuration;
using UCBWeb;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace UcbWeb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            #region Parameter validation
            // Validate parameters
            if (null == filters) throw new ArgumentOutOfRangeException("filters");
            #endregion

            filters.Add(new SessionExpiryAttribute());
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            #region Parameter validation
            // Validate parameters
            if (null == routes) throw new ArgumentOutOfRangeException("routes");
            #endregion

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Initialise the Inversion of Control container
            //BootStrapper.InitializeIoc();

            //Registration of filters, routes and binders
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            //RegisterBinders.DefineBindings();

            // Add mappings for Models to data contracts and vice versa
            TypeMappings.DefineTypeMappings();

            string sessionCookieName = null;

            // Get session state section from web.config file
            SessionStateSection sessionStateValue = ConfigurationManager.GetSection("system.web/sessionState") as SessionStateSection;

            // Get session cookie name if it is provided otherwise use the default value of "ASP.NET_SessionId" 
            if (sessionStateValue != null && !string.IsNullOrEmpty(sessionStateValue.CookieName))
            {
                sessionCookieName = sessionStateValue.CookieName;
            }
            else
            {
                sessionCookieName = "ASP.NET_SessionId";
            }

            // Store in application state
            Application["sessionCookieName"] = sessionCookieName;
        }

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            // Set caching options
            if (HttpContext.Current.CurrentHandler is MvcHandler)
            {
                HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                HttpContext.Current.Response.Cache.SetNoStore();
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            }
            else
            {
                HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddHours(12));
            }
        }

        protected void Application_PostMapRequestHandler(object sender, EventArgs e)
        {
            // Retrieve session cookie name from application state
            string sessionCookieName = Application["sessionCookieName"].ToString();
            string tempSessionCookieName = sessionCookieName + "Temp";

            if (Request.Cookies[tempSessionCookieName] != null)
            {
                if (Request.Cookies[sessionCookieName] == null)
                    Request.Cookies.Add(new HttpCookie(sessionCookieName, Request.Cookies[tempSessionCookieName].Value));
                else
                    Request.Cookies[sessionCookieName].Value = Request.Cookies[tempSessionCookieName].Value;
            }
        }

        protected void Application_PostRequestHandlerExecute(object sender, EventArgs e)
        {
            // Retrieve session cookie name from application state
            string sessionCookieName = Application["sessionCookieName"].ToString();
            string tempSessionCookieName = sessionCookieName + "Temp";

            try
            {
                HttpCookie cookie = new HttpCookie(tempSessionCookieName, Session.SessionID);
                cookie.Expires = DateTime.Now.AddMinutes(Session.Timeout);
                Response.Cookies.Add(cookie);
            }
            catch (HttpException)
            { // ignore
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (IsMaxRequestExceededException(this.Server.GetLastError()))
            {
                this.Server.ClearError();
                Response.Redirect(VirtualPathUtility.ToAbsolute("~/Incident/EditWithError?msg=FileSize"));
            }
            //  A potentially dangerous Request.Form
            else if ( this.Server.GetLastError() is HttpRequestValidationException && this.Server.GetLastError().Message.Contains('<') )
            {
                String message = this.Server.GetLastError().Message.Replace("<", "LESSTHANHACK").Replace('\n', ' ');
                this.Server.ClearError();
                Response.Redirect(VirtualPathUtility.ToAbsolute("~/Incident/EditWithError?msg=" + message));
            }
            else
            {
                // Get the last error
                var lastError = Server.GetLastError();
                Response.Clear();

                // If there is an error
                if (null != lastError)
                {
                    // Publish the exception based on the defined exception policy
                    bool rethrow = ExceptionPolicy.HandleException(lastError, "AdepExceptionPolicy");
                }
                else
                {
                    // Log that an error occurred but we dont' know what it was
                    bool rethrow = ExceptionPolicy.HandleException(new Exception("An error occurred but no details were available."), "AdepExceptionPolicy");
                }
            }
        }

        private bool IsMaxRequestExceededException(Exception exception)
        {
            int TimeOutExceptionCode = -2147467259;
            Exception main;
            var unhandled = exception as HttpUnhandledException;
            if (unhandled != null && unhandled.ErrorCode == TimeOutExceptionCode)
            {
                main = unhandled.InnerException;
            }
            else
            {
                main = exception;
            }
            var http = main as HttpException;
            if (http != null && http.ErrorCode == TimeOutExceptionCode)
            {
                if (http.StackTrace.Contains("GetEntireRawContent"))
                {
                    return true;
                }
            }
            return false;
        }

        //protected void Application_BeginRequest(Object sender, EventArgs e)
        //{
        //    HttpContext context = ((HttpApplication)sender).Context;

        //    HttpRuntimeSection runTime = (HttpRuntimeSection)WebConfigurationManager.GetSection("system.web/httpRuntime");

        //    if (context.Request.ContentLength > runTime.MaxRequestLength)
        //    {
        //        context.Response.Redirect();
        //    }

        //}

    }
}