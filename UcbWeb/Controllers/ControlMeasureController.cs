//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    public partial class ControlMeasureController : BaseController
    {

		private IUcbService UcbService;

		// Dependency Injection enabled constructors
        public ControlMeasureController()
            : this(new UcbServiceClient(),new SessionManager(), new CacheManager())
        {
        }

        public ControlMeasureController(IUcbService UcbService, ISessionManager sessionManager, ICacheManager cacheManager)
			:base(sessionManager,cacheManager)
        {
            this.UcbService = UcbService;
        }

		#region Edit
		
        // GET: /ControlMeasure/Edit
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        public ActionResult Edit()
        {
			// Retrieve ID from session
			string code = sessionManager.ControlMeasureCode;
			
            ControlMeasureVM model = new ControlMeasureVM();
			
            // Not from staff or error
            if (String.IsNullOrEmpty(code))
            {
                //If session has lists then use them
                RepopulateListsFromCacheSession(model);

                //Assume we are in create mode as no code passed
                model.ControlMeasureItem = new ControlMeasureModel(){IsActive = true};
            }
            //if we have been passed a code then assume we are in edit situation and we need to retrieve from the database.
            else
            {
				// Create service instance
				IUcbService sc = UcbService;
				
                try
                {
					// Call service to get ControlMeasure item and any associated lookups    
                    ControlMeasureVMDC returnedObject = sc.GetControlMeasure(CurrentUser, CurrentUser, appID, "", code);

					// Close service communication
					((ICommunicationObject)sc).Close();

                    //Get view model from service
                    model = ConvertControlMeasureDC(returnedObject);

                    ResolveFieldCodesToFieldNamesUsingLists(model);

                    //Store the service version
                    sessionManager.ControlMeasureServiceVersion = model.ControlMeasureItem;
                }
				catch (Exception e)
				{
					// Handle the exception
					string message = ExceptionManager.HandleException(e, (ICommunicationObject)sc);
					model.Message = message;
					
					return View(model);
				}
            }

            //Adds current retrieved ControlMeasure to session
            sessionManager.CurrentControlMeasure = model.ControlMeasureItem;
            SetAccessContext(model);
            
            return View(model);
        }    
		
		#endregion
		
		#region Create/Update

        // POST: /ControlMeasure/Edit with Create button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult CreateControlMeasure(FormCollection collection)
        {
            return UpdateControlMeasure();
        }

        // POST: /ControlMeasure/Edit with Save button submitting
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult SaveControlMeasure(FormCollection collection)
        {
            return UpdateControlMeasure();
        }

        //This method is shared between create and save
        private ActionResult UpdateControlMeasure()
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
				IUcbService sc = UcbService;
				
                //Attempt update
                try
                {
                    // Map model to data contract
                    ControlMeasureDC ControlMeasureItem = Mapper.Map<ControlMeasureDC>(model.ControlMeasureItem);

					ControlMeasureVMDC returnedObject = null;

                    if (null == model.ControlMeasureItem.Code || model.ControlMeasureItem.Code == Guid.Empty)
                    {
						// Call service to create new ControlMeasure item
                        returnedObject = sc.CreateControlMeasure(CurrentUser, CurrentUser, appID, "", ControlMeasureItem);
                    }
                    else
                    {
						// Call service to update ControlMeasure item
                        returnedObject = sc.UpdateControlMeasure(CurrentUser, CurrentUser, appID, "", ControlMeasureItem);
                    }

					// Close service communication
					((ICommunicationObject)sc).Close();
					
					// Retrieve item returned by service
                    var createdControlMeasure = returnedObject.ControlMeasureItem;

					// Map data contract to model
                    model.ControlMeasureItem = Mapper.Map<ControlMeasureModel>(createdControlMeasure);
					
                    //After creation some of the fields are display only so we need the resolved look up nmames
                    ResolveFieldCodesToFieldNamesUsingLists(model);

					// Set access context to Edit mode
                    model.AccessContext = ControlMeasureAccessContext.Edit;

					// Save version of item returned by service into session
                    sessionManager.ControlMeasureServiceVersion = model.ControlMeasureItem;
                    sessionManager.CurrentControlMeasure = model.ControlMeasureItem;
                    
                    // Remove the state from the model as these are being populated by the controller and the HTML helpers are being populated with
					// the POSTED values and not the changed ones.
                    ModelState.Clear();
                    model.Message = Resources.MESSAGE_UPDATE_SUCCEEDED;
                }
				catch (Exception e)
				{
					// Handle the exception
					string message = ExceptionManager.HandleException(e, (ICommunicationObject)sc);
					model.Message = message;
					
					return View(model);
				}
            }

            return View(model);
        }

        #endregion
		
		#region Exit

        // POST: /ControlMeasure/Edit with Exit button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult ExitControlMeasure(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IsExitConfirmed == "True" || !model.IsViewDirty)
            {
                //Set flags false
                SetFlagsFalse(model);

                model.IsExitConfirmed = "False";
                
                //remove the current values from session
                sessionManager.CurrentControlMeasure = null;
                sessionManager.ControlMeasureServiceVersion = null;

                return RedirectToAction("Search", "ControlMeasure");
                
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
		
		#region Delete

        // POST: /ControlMeasure/Edit with Delete button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult DeleteControlMeasure(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IsDeleteConfirmed == "True")
            {
                //Set flags false
                SetFlagsFalse(model);
            
                model.IsDeleteConfirmed = "False";
				
				// Create service instance
				IUcbService sc = UcbService;
				
                try
                {
					// Call service to delete the item
                    sc.DeleteControlMeasure(CurrentUser, CurrentUser, appID, "", model.ControlMeasureItem.Code.ToString(), model.ControlMeasureItem.RowIdentifier.ToString());

					// Close service communication
					((ICommunicationObject)sc).Close();
					
                    // Remove the current values from session
                    sessionManager.CurrentControlMeasure = null;
                    sessionManager.ControlMeasureServiceVersion = null;

                    // Remove the state from the model as these are being populated by the controller and the HTML helpers are being populated with
					// the POSTED values and not the changed ones.
                    ModelState.Clear();
					
					// Create new item but keep any lists
					model.ControlMeasureItem = new ControlMeasureModel(){IsActive = true};
					
					// Set message to return to user
                    model.Message = Resources.MESSAGE_DELETE_SUCCEEDED;
					
					// Set access context to Edit mode
                    model.AccessContext = ControlMeasureAccessContext.Create;
					
					// Redirect to the search screen
                    return View(model);
                }
				catch (Exception e)
                {
					// Handle the exception
					string message = ExceptionManager.HandleException(e, (ICommunicationObject)sc);
					model.Message = message;
					
					return View(model);
                }
            }
            else
            {
                //Set flags false
                SetFlagsFalse(model);
                model.Message = Resources.MESSAGE_DELETECONFIRMATION;
                model.IsDeleteConfirmed = "True";
            }
            
            return View(model);
        }
		
		#endregion
		
		#region New

        // POST: /ControlMeasure/Edit with New button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult NewControlMeasure(FormCollection collection)
        {
            var model = GetUpdatedModel();
            //Set flags false
            SetFlagsFalse(model);
            
            //Clear Down Session
			sessionManager.ControlMeasureCode = null;
            sessionManager.CurrentControlMeasure = null;
            sessionManager.ControlMeasureServiceVersion = null;
            
            //Go to the Edit Screen
            return RedirectToAction("Edit", "ControlMeasure");
        }
		
		#endregion

		#region Search

        // GET: /ControlMeasure/Search
        //This is called when first entering search ControlMeasure screen or when paging
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        public ActionResult Search(int page = 1)
        {   
			// Create service instance
			IUcbService sc = UcbService;
			
			// Create model
			ControlMeasureSearchVM model = new ControlMeasureSearchVM();
			
			try
			{
				ControlMeasureSearchVMDC response = sc.SearchControlMeasure(CurrentUser, CurrentUser, appID, "", null, page, PageSize, true);

				// Close service communication
				((ICommunicationObject)sc).Close();

				//Map response back to view model
				model.MatchList = Mapper.Map<IEnumerable<ControlMeasureSearchMatchDC>, List<ControlMeasureSearchMatchModel>>(response.MatchList);

				// Set paging values
				model.TotalRows = response.RecordCount;
				model.PageSize = sessionManager.PageSize;
				model.PageNumber = page;
				
				// Store the page number we were on
				sessionManager.ControlMeasurePageNumber = model.PageNumber;
	        
				return View(model);
			}
			catch (Exception e)
			{
				// Handle the exception
				string message = ExceptionManager.HandleException(e, (ICommunicationObject)sc);
				model.Message = message;
				
				return View(model);
			}

        }

        // POST: /ControlMeasure/Search
        //This is called when clicking search button
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
		[HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult SearchPost(ControlMeasureSearchVM model, int page = 1)
        {
		
			// Iterate through form keys
            foreach (string Key in Request.Form.Keys)
            {
				// Test if Select button was clicked...
                if (Key.StartsWith("Search::SearchPost_"))
                {
					// Retrieve ID for entity which was selected
                    Guid Value = Guid.Parse(Key.Substring(19));
									
					// Store ID for Edit screen
					sessionManager.ControlMeasureCode = Value.ToString();
					
					// Call out to Edit screen
                    return RedirectToAction("Edit", "ControlMeasure", new { code = Value });

                }
            }

            // Return to the Screen
            return View(model);
        }

        #endregion
		
		#region Private methods
		
        private void SetFlagsFalse(ControlMeasureVM model)
        {
            model.IsDeleteConfirmed = "False";
            model.IsExitConfirmed = "False";
            model.IsNewConfirmed = "False";

            //Stop the binder resetting the posted values
            ModelState.Remove("IsDeleteConfirmed");
            ModelState.Remove("IsExitConfirmed");
            ModelState.Remove("IsNewConfirmed");
        }

        private void ResolveFieldCodesToFieldNamesUsingLists(ControlMeasureVM model)
        {
			//TODO:
        }

        /// <summary>
        /// Private method to merge in the model 
        /// </summary>
        /// <returns></returns>
        private ControlMeasureVM GetUpdatedModel()
        {
            ControlMeasureVM model = new ControlMeasureVM();
            RepopulateListsFromCacheSession(model);
            model.Message = "";

            if (sessionManager.CurrentControlMeasure != null)
            {
                model.ControlMeasureItem = sessionManager.CurrentControlMeasure;
            }

            //***************************************NEED WHITE LIST ---- BLACK LIST ------ TO PREVENT OVERPOSTING **************************
            bool result = TryUpdateModel(model);//This also validates and sets ModelState
            //*******************************************************************************************************************************
            if (sessionManager.CurrentControlMeasure != null)
            {
                //*****************************************PREVENT OVER POSTING ATTACKS******************************************************
                //Get the values for read only fields from session
                MergeNewValuesWithOriginal(model.ControlMeasureItem);
                //***************************************************************************************************************************
            }

            SetAccessContext(model);

            return model;
        }
		
        private ControlMeasureVM ConvertControlMeasureDC(ControlMeasureVMDC returnedObject)
        {
            ControlMeasureVM model = new ControlMeasureVM();
            
			// Map ControlMeasure Item
			model.ControlMeasureItem = Mapper.Map<ControlMeasureDC, ControlMeasureModel>(returnedObject.ControlMeasureItem);
            
			// Map lookup data lists
        
            return model;
        }
		
		private void RepopulateListsFromCacheSession(ControlMeasureVM model)
        {
			// Populate cached lists if they are empty. Will invoke service call
            ControlMeasureLookupListsCacheObject CachedLists = cacheManager.ControlMeasureListCache;

			// Retrieve any cached lists to model
   
        }

        private void MergeNewValuesWithOriginal(ControlMeasureModel modelFromView)
        {
            //***************************The values that are display only will not be posted back so need to get them from session**************************

            ControlMeasureModel OriginalValuesFromSession = sessionManager.CurrentControlMeasure;

        }
		
		private void SetAccessContext(ControlMeasureVM model)
        {
            //Decide on access context
            if (null == model.ControlMeasureItem || model.ControlMeasureItem.Code == Guid.Empty)
            {
				// Create context
                model.AccessContext = ControlMeasureAccessContext.Create;
            }
            else
            {
				// Edit context
                model.AccessContext = ControlMeasureAccessContext.Edit;
            }
        }
		
		private void DetermineIsDirty(ControlMeasureVM model)
        {
            //Compare the ControlMeasure to the original session
            if (model.ControlMeasureItem.PublicInstancePropertiesEqual(sessionManager.ControlMeasureServiceVersion, "RowIdentifier"))
            {
                model.IsViewDirty = false;
            }
            else
            {
                model.IsViewDirty = true;
            }

        }
        #endregion
       
    }
}
