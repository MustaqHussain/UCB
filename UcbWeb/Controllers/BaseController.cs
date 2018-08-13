using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UcbWeb.ViewModels;
using System.Web.Security;
using System.Security.Principal;
using System.Threading;
using Dwp.Adep.Ucb.ResourceLibrary;
using UcbWeb.Models;
using UcbWeb.UcbService;
using AutoMapper;
using UcbWeb.Helpers;
using UcbWeb;
using System.Configuration;
using UCBWeb.Exceptions;

namespace UcbWeb.Controllers
{
    public class BaseController : Controller
    {
        public ISessionManager sessionManager;
        public ICacheManager cacheManager;

        public BaseController(ISessionManager sessionManager, ICacheManager cacheManager)
        {
            this.sessionManager = sessionManager;
            this.cacheManager = cacheManager;
        }

        #region Class fields

        public int page = 1;
        public string appID = "Ucb";

        public int PageSize
        {
            get
            {
                int pageSize;

                try
                {
                    pageSize = sessionManager.PageSize;
                }
                catch (Exception e)
                {
                    pageSize = int.Parse(ConfigurationManager.AppSettings.Get("DefaultPageSize"));

                    sessionManager.PageSize = pageSize;
                }

                return pageSize;
            }
        }

        // Gets the current user's guid and stores in session
        public string CurrentUser
        {
            get
            {
                string userID = sessionManager.UserID;
                if (null == userID)
                {
                    // If user's ID is null then call authorisation process
                    AuthorisationManager authManager = new AuthorisationManager();
                    authManager.GetUserAuthorisationInfo(this.HttpContext, appID);
                    userID = sessionManager.UserID;
                }

                return userID;
            }
        }

        // Gets the user's localisation and stores in session
        public string Locale
        {
            get
            {
                // Get instance of Language Manager
                LanguageManager language = new LanguageManager(sessionManager);

                // Get users localisation
                return language.GetLocale();
            }
        }

        #endregion

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            AuthorisationManager authManager = new AuthorisationManager();
            authManager.GetUserAuthorisationInfo(this.HttpContext, appID);

        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //WriteLog(string message) yadda yadda

            if (filterContext.Exception is SessionExpiredException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = View("SessionExpired");
            }
            else if (filterContext.Exception is ResolveFieldCodesException)
            {
                filterContext.ExceptionHandled = true;
                TempData["ResolveFieldExceptionMessage"] = filterContext.Exception.Message;
                filterContext.Result = View("ResolveFieldCodesException");
            }
            else if (filterContext.Exception is System.TimeoutException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = View("TimeoutException");
            }
            else if (filterContext.Exception is System.ServiceModel.FaultException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = View("FaultException");
            }
            else if (filterContext.Exception is System.ServiceModel.CommunicationException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = View("CommunicationException");
            }
            else if (filterContext.Exception is System.InvalidOperationException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = View("InvalidOperationException");
            }
            else
            {
                base.OnException(filterContext);
            }
        }
    }
}