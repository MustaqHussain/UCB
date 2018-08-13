using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UcbWeb.ViewModels;
using UcbWeb.UcbService;
using UcbWeb.Helpers;
using UcbWeb.Models;

namespace UcbWeb.Controllers
{
    public class ConfirmationController : BaseController
    {
        
         // Dependency Injection enabled constructors

        private IUcbService UcbService;
        public ConfirmationController()
            : this(new UcbServiceClient(), new SessionManager(), new CacheManager())
        {
        }

        public ConfirmationController(IUcbService UcbService, ISessionManager sessionManager, ICacheManager cacheManager)
            : base(sessionManager, cacheManager)
        {
            this.UcbService = UcbService;
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Confirmation/
        public ActionResult EmailConfirmation(FormCollection collection)
        {
            EmailVM model = new EmailVM();

            model = sessionManager.EmailDetails;

            return View(model);
        }

        public ActionResult LineManagerUpdateConfirmation(FormCollection collection)
        {
            IncidentVM model = new IncidentVM();

            model.IncidentItem = sessionManager.CurrentIncident;
            model.NominatedManager = sessionManager.NominatedManager;
            model.NominatedManagerEmailAddress = sessionManager.NominatedManagerEmailAddress;
            return View(model);
        }

        public ActionResult LineManagerCompletedActionConfirmation(FormCollection collection)
        {
            IncidentVM model = new IncidentVM();

            model.IncidentItem = sessionManager.CurrentIncident;
            model.NominatedManager = sessionManager.NominatedManager;
            model.NominatedManagerEmailAddress = sessionManager.NominatedManagerEmailAddress;
            return View(model);
        }


    }
}
