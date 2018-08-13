using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UcbWeb.UcbService;
using UcbWeb.Helpers;
using UcbWeb.ViewModels;
using System.ServiceModel;
using UcbWeb.Models;
using AutoMapper;
using Dwp.Adep.Ucb.ResourceLibrary;


namespace UcbWeb.Controllers
{
    public class IntranetStaffProtectionController : BaseController
    {
        private IUcbService UcbService;

        // GET: /IntranetStaffProtection/

        // Dependency Injection enabled constructors
        public IntranetStaffProtectionController()
            : this(new UcbServiceClient(), new SessionManager(), new CacheManager())
        {

        }

        public IntranetStaffProtectionController(IUcbService UcbService, ISessionManager sessionManager, ICacheManager cacheManager)
            : base(sessionManager, cacheManager)
        {
            this.UcbService = UcbService;
        }

        //Get
        public ActionResult Search()
        {

            IntranetStaffProtectionVM model = new IntranetStaffProtectionVM();
            model.StaffProtectionList = new IntranetStaffProtectionModel();

            return View(model);
        }

        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult Search(string nino)
        {
            IntranetStaffProtectionVM model = new IntranetStaffProtectionVM();            

            if (ModelState.IsValid)
            {
                
                model.NINO = nino;

                // Create service instance
                IUcbService sc = UcbService;

                // Create model

                try
                {
                    IntranetStaffProtectionResult response = sc.IntranetStaffProtection(CurrentUser, CurrentUser, appID, "", model.NINO);                    

                    // Close service communication
                    ((ICommunicationObject)sc).Close();                    

                    //Map response back to view model
                    model.StaffProtectionList = Mapper.Map<IntranetStaffProtectionResult, IntranetStaffProtectionModel>(response);


                    if (response.ControlMeasures == null)
                    {
                        model.Message = Resources.LABEL_ISP_No_data_found;

                    }

                    return View(model);
                }
                catch (Exception e)
                {
                    // Handle the exception
                    string message = ExceptionManager.HandleException(e, (ICommunicationObject)sc);
                    model.Message = message;

                    
                }
            }
            return View(model);
        }

    }
}
