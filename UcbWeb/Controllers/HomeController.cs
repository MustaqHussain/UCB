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
    public class HomeController : BaseController
    {
        private IUcbService UcbService;

		// Dependency Injection enabled constructors
        public HomeController()
            : this(new UcbServiceClient(),new SessionManager(), new CacheManager())
        {
        }

        public HomeController(IUcbService UcbService, ISessionManager sessionManager, ICacheManager cacheManager)
			:base(sessionManager,cacheManager)
        {
            this.UcbService = UcbService;
        }

        #region Index

        // GET: /Home/Index
        // Only authorized users should be able to accesss the home page
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.BUSINESS_AREA_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.READ_ONLY + "," + AppRoles.TRADE_UNION)]
        public ActionResult Index()
        {
            IntroductoryInformationVM model = new IntroductoryInformationVM();
          
            // Create service instance
            UcbServiceClient sc = new UcbServiceClient();

            try
            {

                // Get users localisation
                LanguageManager language = new LanguageManager(sessionManager);
                string locale = language.GetLocale();

                // Call service to get IntroductoryInformation item and any associated lookups based on locale
                IntroductoryInformationVMDC returnedObject = sc.GetIntroductoryInformationByLocale(CurrentUser, CurrentUser, appID, "", locale);

                // Close service communication
                sc.Close();

                //Get view model from service
                model = ConvertIntroductoryInformationDC(returnedObject);

                ResolveFieldCodesToFieldNamesUsingLists(model);

                //Store the service version
                sessionManager.IntroductoryInformationServiceVersion = model.IntroductoryInformationItem;
            }
            catch (Exception e)
            {
                // Handle the exception
                string message = ExceptionManager.HandleException(e, sc);
                model.Message = message;

                return View(model);
            }

            if (sessionManager.PageFrom == "EditIncident" || sessionManager.PageFrom == "EditReferral")
            {
                if (!string.IsNullOrEmpty(sessionManager.MessageFromPageFrom))
                {
                    model.Message = sessionManager.MessageFromPageFrom;
                    sessionManager.MessageFromPageFrom = null;
                }
            }

            //Adds current retrieved IntroductoryInformation to session
            sessionManager.CurrentIntroductoryInformation = model.IntroductoryInformationItem;
            SetAccessContext(model);

            return View(model);
        }

        #endregion

        #region Edit

        // POST: /Home/Index with Edit button submitting
        [CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamIndex(Prefix = "Index::")]
        [HttpPost]
        public ActionResult EditIntroductoryInformation(FormCollection collection)
        {
            var model = GetUpdatedModel();

            // Set access context to Edit mode
            model.AccessContext = IntroductoryInformationAccessContext.Edit;

            return View(model);
        }

        #endregion

        #region Save

        // POST: /Home/Index with Save button submitting
        [CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamIndex(Prefix = "Index::")]
        [HttpPost]
        public ActionResult SaveIntroductoryInformation(FormCollection collection)
        {
            // Get the updated model
            var model = GetUpdatedModel();

            // Test to see if there are any errors
            var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors[0].ErrorMessage })
                    .ToArray();

            //Set flags false
            SetFlagsFalse(model);

            // Test to see if the model has validated correctly
            if (ModelState.IsValid)
            {
                // Create service instance
                UcbServiceClient sc = new UcbServiceClient();

                //Attempt update
                try
                {
                    // Map model to data contract
                    IntroductoryInformationDC IntroductoryInformationItem = Mapper.Map<IntroductoryInformationDC>(model.IntroductoryInformationItem);

                    IntroductoryInformationVMDC returnedObject = null;

                    // Call service to update IntroductoryInformation item
                    returnedObject = sc.UpdateIntroductoryInformation(CurrentUser, CurrentUser, appID, "", IntroductoryInformationItem);

                    // Close service communication
                    sc.Close();

                    // Retrieve item returned by service
                    var createdIntroductoryInformation = returnedObject.IntroductoryInformationItem;

                    // Map data contract to model
                    model.IntroductoryInformationItem = Mapper.Map<IntroductoryInformationModel>(createdIntroductoryInformation);

                    //After creation some of the fields are display only so we need the resolved look up nmames
                    ResolveFieldCodesToFieldNamesUsingLists(model);

                    // Set access context to Edit mode
                    model.AccessContext = IntroductoryInformationAccessContext.Edit;

                    // Save version of item returned by service into session
                    sessionManager.IntroductoryInformationServiceVersion = model.IntroductoryInformationItem;
                    sessionManager.CurrentIntroductoryInformation = model.IntroductoryInformationItem;

                    // Remove the state from the model as these are being populated by the controller and the HTML helpers are being populated with
                    // the POSTED values and not the changed ones.
                    ModelState.Clear();
                    model.Message = Resources.MESSAGE_UPDATE_SUCCEEDED;
                }
                catch (Exception e)
                {
                    // Handle the exception
                    string message = ExceptionManager.HandleException(e, sc);
                    model.Message = message;

                    return View(model);
                }
            }

            return View(model);
        }

        #endregion

        #region Exit

        // POST: /Home/Index with Exit button submitting
        [CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamIndex(Prefix = "Index::")]
        [HttpPost]
        public ActionResult ExitIntroductoryInformation(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IsExitConfirmed == "True" || !model.IsViewDirty)
            {
                //Set flags false
                SetFlagsFalse(model);

                model.IsExitConfirmed = "False";

                //remove the current values from session
                sessionManager.CurrentIntroductoryInformation = null;
                sessionManager.IntroductoryInformationServiceVersion = null;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                //Set flags false
                SetFlagsFalse(model);
                model.Message = Resources.MESSAGE_EXITCONFIRMATION;
                model.IsExitConfirmed = "True";
            }

            return View(model);
        }

        #endregion

        #region Private methods

        private void SetFlagsFalse(IntroductoryInformationVM model)
        {
            model.IsDeleteConfirmed = "False";
            model.IsExitConfirmed = "False";
            model.IsNewConfirmed = "False";

            //Stop the binder resetting the posted values
            ModelState.Remove("IsDeleteConfirmed");
            ModelState.Remove("IsExitConfirmed");
            ModelState.Remove("IsNewConfirmed");
        }

        private void ResolveFieldCodesToFieldNamesUsingLists(IntroductoryInformationVM model)
        {
            //TODO:
        }

        /// <summary>
        /// Private method to merge in the model 
        /// </summary>
        /// <returns></returns>
        private IntroductoryInformationVM GetUpdatedModel()
        {
            IntroductoryInformationVM model = new IntroductoryInformationVM();
            RepopulateListsFromCacheSession(model);
            model.Message = "";

            if (sessionManager.CurrentIntroductoryInformation != null)
            {
                model.IntroductoryInformationItem = sessionManager.CurrentIntroductoryInformation;
            }

            //***************************************NEED WHITE LIST ---- BLACK LIST ------ TO PREVENT OVERPOSTING **************************
            bool result = TryUpdateModel(model);//This also validates and sets ModelState
            //*******************************************************************************************************************************
            if (sessionManager.CurrentIntroductoryInformation != null)
            {
                //*****************************************PREVENT OVER POSTING ATTACKS******************************************************
                //Get the values for read only fields from session
                MergeNewValuesWithOriginal(model.IntroductoryInformationItem);
                //***************************************************************************************************************************
            }

            SetAccessContext(model);

            return model;
        }

        private IntroductoryInformationVM ConvertIntroductoryInformationDC(IntroductoryInformationVMDC returnedObject)
        {
            IntroductoryInformationVM model = new IntroductoryInformationVM();

            // Map IntroductoryInformation Item
            model.IntroductoryInformationItem = Mapper.Map<IntroductoryInformationDC, IntroductoryInformationModel>(returnedObject.IntroductoryInformationItem);

            // Map lookup data lists

            return model;
        }

        private void RepopulateListsFromCacheSession(IntroductoryInformationVM model)
        {
            // Populate cached lists if they are empty. Will invoke service call
            IntroductoryInformationLookupListsCacheObject CachedLists = cacheManager.IntroductoryInformationListCache;

            // Retrieve any cached lists to model

        }

        private void MergeNewValuesWithOriginal(IntroductoryInformationModel modelFromView)
        {
            //***************************The values that are display only will not be posted back so need to get them from session**************************

            IntroductoryInformationModel OriginalValuesFromSession = sessionManager.CurrentIntroductoryInformation;

        }

        private void SetAccessContext(IntroductoryInformationVM model)
        {
            // Edit context
            model.AccessContext = IntroductoryInformationAccessContext.View;
        }

        private void DetermineIsDirty(IntroductoryInformationVM model)
        {
            //Compare the IntroductoryInformation to the original session
            if (model.IntroductoryInformationItem.PublicInstancePropertiesEqual(sessionManager.IntroductoryInformationServiceVersion, "RowIdentifier"))
            {
                model.IsViewDirty = false;
            }
            else
            {
                model.IsViewDirty = true;
            }

        }
        #endregion

        #region About

        public ActionResult About()
        {
            return View();
        }

        #endregion

        public ActionResult UnAuthorized()
        {
            return View();
        }
    }
}
