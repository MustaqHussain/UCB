using System;
using System.Web;
using System.Security.Principal;
using System.Threading;
using UcbWeb.AuthorisationService;
using UcbWeb.Helpers;
using System.Collections.Generic;
using System.Web.Security;
using System.ServiceModel;
using UcbWeb.Models;

namespace UcbWeb
{
    public class AuthorisationManager
    {
        private IAuthorisationService authorisationService;
        private ISessionManager sessionManager;

        // Dependency Injection enabled constructors
        public AuthorisationManager()
            : this(new AuthorisationServiceClient(), new SessionManager())
        {
        }

        public AuthorisationManager(IAuthorisationService authorisationService, ISessionManager sessionManager)
        {
            this.authorisationService = authorisationService;
            this.sessionManager = sessionManager;
        }

        /// <summary>
        /// Gets authorisation information relating to the current user
        /// </summary>
        /// <remarks>
        /// Retrieves application roles based on a users group membership
        /// </remarks>
        /// <param name="context">HttpContext</param>
        /// <param name="appID">Application ID</param>
        public void GetUserAuthorisationInfo(HttpContextBase context, string appID)
        {

            string[] roles = null;

            if (context.User.Identity.IsAuthenticated && context.Request.LogonUserIdentity != null && context.Request.LogonUserIdentity.IsAuthenticated)
            {
                //Get roles from session if available
                if (null != sessionManager.UserRoles)
                {
                    roles = sessionManager.UserRoles;
                }
                else
                // If roles not in session then retrieve from authorisation service
                {
                    AuthorisationDC authorisationResult = null;

                    //Create instance of Authorisation service
                    //AuthorisationServiceClient sc = new AuthorisationServiceClient();
                    IAuthorisationService sc = authorisationService;

                    //Get user name for current user
                    string userName = context.User.Identity.Name;

                    try
                    {
#if DEBUG
                        authorisationResult = new AuthorisationDC();
                        authorisationResult.Roles = new string[] { };

                        //authorisationResult.Roles = new string[] { AppRoles.APPLICATION, AppRoles.NOMINATED_MANAGER, AppRoles.TRADE_UNION, AppRoles.UCB_MI_USER, AppRoles.STAFFADMIN };

                        authorisationResult.Roles = new string[] { AppRoles.APPLICATION, AppRoles.NOMINATED_MANAGER, AppRoles.DEPUTY_NOMINATED_MANAGER, AppRoles.ADMIN, AppRoles.TRADE_UNION, AppRoles.BUSINESS_AREA_MANAGER, AppRoles.UCB_MI_USER, AppRoles.STAFFADMIN };
//                        authorisationResult.Roles = new string[] { AppRoles.APPLICATION, AppRoles.NOMINATED_MANAGER, AppRoles.DEPUTY_NOMINATED_MANAGER, AppRoles.ADMIN, AppRoles.TRADE_UNION, AppRoles.BUSINESS_AREA_MANAGER, AppRoles.UCB_MI_USER, AppRoles.STAFFADMIN };
//                        authorisationResult.Roles = new string[] { "UCB-APPLICATION", "UCB-ADMIN" };
                        authorisationResult.UserID = "DF8752EF-74DB-4AEE-A55A-1D3D5F6AA6FE";
                        authorisationResult.UserName = "Marvin Mustard-DEV";
#else
                        string[] adGroups = Roles.GetRolesForUser();

                        //Authorise user and retrieve user roles
                        authorisationResult = sc.GetUserAuthorisationInfo(userName, appID, adGroups);

                        //Close service communication
                        ((ICommunicationObject)sc).Close();
#endif

                        //Store roles in session so we don't have to call service each time
                        sessionManager.UserRoles = roles = authorisationResult.Roles;

                        //Store user's ID in session
                        sessionManager.UserID = authorisationResult.UserID;

                        //Store user's Name in session
                        sessionManager.UserName = authorisationResult.UserName;

                    }
                    catch(FaultException<AuthorisationFailureFault>)
                    {
                        ((ICommunicationObject)sc).Close();

                        //Store user's ID in session
                        sessionManager.UserID = userName;

                        //Store roles as empty array
                        sessionManager.UserRoles = new string[] { };

                    }
                    catch (Exception e)
                    {

                        ExceptionManager.HandleException(e, (ICommunicationObject)sc);
                    }

                }

            }

            //Create new principal object and attach roles
            GenericPrincipal principal = new GenericPrincipal(context.User.Identity, roles);

            //Attach new principal to current thread
            Thread.CurrentPrincipal = context.User = principal;
        }

        public List<StaffAccessDC> GetUserApplicationInfo(string token)
        {
            return null;
        }

    }
}
