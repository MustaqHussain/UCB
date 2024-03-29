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
    public partial class ReportCategoryController : BaseController
    {

		private IUcbService UcbService;

		// Dependency Injection enabled constructors
        public ReportCategoryController()
            : this(new UcbServiceClient(),new SessionManager(), new CacheManager())
        {
        }

        public ReportCategoryController(IUcbService UcbService, ISessionManager sessionManager, ICacheManager cacheManager)
			:base(sessionManager,cacheManager)
        {
            this.UcbService = UcbService;
        }

		#region Edit
		
        // GET: /ReportCategory/Edit
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        public ActionResult Edit()
        {
			// Retrieve ID from session
			string code = sessionManager.ReportCategoryCode;
			
            ReportCategoryVM model = new ReportCategoryVM();
			
            // Not from staff or error
            if (String.IsNullOrEmpty(code))
            {
                //If session has lists then use them
                RepopulateListsFromCacheSession(model);

                //Assume we are in create mode as no code passed
                model.ReportCategoryItem = new ReportCategoryModel(){IsActive = true};
            }
            //if we have been passed a code then assume we are in edit situation and we need to retrieve from the database.
            else
            {
				// Create service instance
				IUcbService sc = UcbService;
				
                try
                {
					// Call service to get ReportCategory item and any associated lookups    
                    ReportCategoryVMDC returnedObject = sc.GetReportCategory(CurrentUser, CurrentUser, appID, "", code);

					// Close service communication
					((ICommunicationObject)sc).Close();

                    //Get view model from service
                    model = ConvertReportCategoryDC(returnedObject);

                    ResolveFieldCodesToFieldNamesUsingLists(model);

                    //Store the service version
                    sessionManager.ReportCategoryServiceVersion = model.ReportCategoryItem;
                }
				catch (Exception e)
				{
					// Handle the exception
					string message = ExceptionManager.HandleException(e, (ICommunicationObject)sc);
					model.Message = message;
					
					return View(model);
				}
            }

            //Adds current retrieved ReportCategory to session
            sessionManager.CurrentReportCategory = model.ReportCategoryItem;
            SetAccessContext(model);
            
            return View(model);
        }    
		
		#endregion
		
		#region Create/Update

        // POST: /ReportCategory/Edit with Create button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult CreateReportCategory(FormCollection collection)
        {
            return UpdateReportCategory();
        }

        // POST: /ReportCategory/Edit with Save button submitting
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult SaveReportCategory(FormCollection collection)
        {
            return UpdateReportCategory();
        }

        //This method is shared between create and save
        private ActionResult UpdateReportCategory()
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
                    ReportCategoryDC ReportCategoryItem = Mapper.Map<ReportCategoryDC>(model.ReportCategoryItem);

					ReportCategoryVMDC returnedObject = null;

                    if (null == model.ReportCategoryItem.Code || model.ReportCategoryItem.Code == Guid.Empty)
                    {
						// Call service to create new ReportCategory item
                        returnedObject = sc.CreateReportCategory(CurrentUser, CurrentUser, appID, "", ReportCategoryItem);
                    }
                    else
                    {
						// Call service to update ReportCategory item
                        returnedObject = sc.UpdateReportCategory(CurrentUser, CurrentUser, appID, "", ReportCategoryItem);
                    }

					// Close service communication
					((ICommunicationObject)sc).Close();
					
					// Retrieve item returned by service
                    var createdReportCategory = returnedObject.ReportCategoryItem;

					// Map data contract to model
                    model.ReportCategoryItem = Mapper.Map<ReportCategoryModel>(createdReportCategory);
					
                    //After creation some of the fields are display only so we need the resolved look up nmames
                    ResolveFieldCodesToFieldNamesUsingLists(model);

					// Set access context to Edit mode
                    model.AccessContext = ReportCategoryAccessContext.Edit;

					// Save version of item returned by service into session
                    sessionManager.ReportCategoryServiceVersion = model.ReportCategoryItem;
                    sessionManager.CurrentReportCategory = model.ReportCategoryItem;
                    
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

        // POST: /ReportCategory/Edit with Exit button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult ExitReportCategory(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IsExitConfirmed == "True" || !model.IsViewDirty)
            {
                //Set flags false
                SetFlagsFalse(model);

                model.IsExitConfirmed = "False";
                
                //remove the current values from session
                sessionManager.CurrentReportCategory = null;
                sessionManager.ReportCategoryServiceVersion = null;

                return RedirectToAction("Search", "ReportCategory");
                
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

        // POST: /ReportCategory/Edit with Delete button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult DeleteReportCategory(FormCollection collection)
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
                    sc.DeleteReportCategory(CurrentUser, CurrentUser, appID, "", model.ReportCategoryItem.Code.ToString(), model.ReportCategoryItem.RowIdentifier.ToString());

					// Close service communication
					((ICommunicationObject)sc).Close();
					
                    // Remove the current values from session
                    sessionManager.CurrentReportCategory = null;
                    sessionManager.ReportCategoryServiceVersion = null;

                    // Remove the state from the model as these are being populated by the controller and the HTML helpers are being populated with
					// the POSTED values and not the changed ones.
                    ModelState.Clear();
					
					// Create new item but keep any lists
					model.ReportCategoryItem = new ReportCategoryModel(){IsActive = true};
					
					// Set message to return to user
                    model.Message = Resources.MESSAGE_DELETE_SUCCEEDED;
					
					// Set access context to Edit mode
                    model.AccessContext = ReportCategoryAccessContext.Create;
					
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

        // POST: /ReportCategory/Edit with New button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult NewReportCategory(FormCollection collection)
        {
            var model = GetUpdatedModel();
            //Set flags false
            SetFlagsFalse(model);
            
            //Clear Down Session
			sessionManager.ReportCategoryCode = null;
            sessionManager.CurrentReportCategory = null;
            sessionManager.ReportCategoryServiceVersion = null;
            
            //Go to the Edit Screen
            return RedirectToAction("Edit", "ReportCategory");
        }
		
		#endregion

		#region Search

        // GET: /ReportCategory/Search
        //This is called when first entering search ReportCategory screen or when paging
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        public ActionResult Search(int page = 1)
        {   
			// Create service instance
			IUcbService sc = UcbService;
			
			// Create model
			ReportCategorySearchVM model = new ReportCategorySearchVM();
			
			try
			{
				ReportCategorySearchVMDC response = sc.SearchReportCategory(CurrentUser, CurrentUser, appID, "", null, page, PageSize, true);

				// Close service communication
				((ICommunicationObject)sc).Close();

				//Map response back to view model
				model.MatchList = Mapper.Map<IEnumerable<ReportCategorySearchMatchDC>, List<ReportCategorySearchMatchModel>>(response.MatchList);

				// Set paging values
				model.TotalRows = response.RecordCount;
				model.PageSize = sessionManager.PageSize;
				model.PageNumber = page;
				
				// Store the page number we were on
				sessionManager.ReportCategoryPageNumber = model.PageNumber;
	        
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

        // POST: /ReportCategory/Search
        //This is called when clicking search button
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
		[HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult SearchPost(ReportCategorySearchVM model, int page = 1)
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
					sessionManager.ReportCategoryCode = Value.ToString();
					
					// Call out to Edit screen
                    return RedirectToAction("Edit", "ReportCategory", new { code = Value });

                }
            }

            // Return to the Screen
            return View(model);
        }

        #endregion
		
		#region Private methods
		
        private void SetFlagsFalse(ReportCategoryVM model)
        {
            model.IsDeleteConfirmed = "False";
            model.IsExitConfirmed = "False";
            model.IsNewConfirmed = "False";

            //Stop the binder resetting the posted values
            ModelState.Remove("IsDeleteConfirmed");
            ModelState.Remove("IsExitConfirmed");
            ModelState.Remove("IsNewConfirmed");
        }

        private void ResolveFieldCodesToFieldNamesUsingLists(ReportCategoryVM model)
        {
			//TODO:
        }

        /// <summary>
        /// Private method to merge in the model 
        /// </summary>
        /// <returns></returns>
        private ReportCategoryVM GetUpdatedModel()
        {
            ReportCategoryVM model = new ReportCategoryVM();
            RepopulateListsFromCacheSession(model);
            model.Message = "";

            if (sessionManager.CurrentReportCategory != null)
            {
                model.ReportCategoryItem = sessionManager.CurrentReportCategory;
            }

            //***************************************NEED WHITE LIST ---- BLACK LIST ------ TO PREVENT OVERPOSTING **************************
            bool result = TryUpdateModel(model);//This also validates and sets ModelState
            //*******************************************************************************************************************************
            if (sessionManager.CurrentReportCategory != null)
            {
                //*****************************************PREVENT OVER POSTING ATTACKS******************************************************
                //Get the values for read only fields from session
                MergeNewValuesWithOriginal(model.ReportCategoryItem);
                //***************************************************************************************************************************
            }

            SetAccessContext(model);

            return model;
        }
		
        private ReportCategoryVM ConvertReportCategoryDC(ReportCategoryVMDC returnedObject)
        {
            ReportCategoryVM model = new ReportCategoryVM();
            
			// Map ReportCategory Item
			model.ReportCategoryItem = Mapper.Map<ReportCategoryDC, ReportCategoryModel>(returnedObject.ReportCategoryItem);
            
			// Map lookup data lists
        
            return model;
        }
		
		private void RepopulateListsFromCacheSession(ReportCategoryVM model)
        {
			// Populate cached lists if they are empty. Will invoke service call
            ReportCategoryLookupListsCacheObject CachedLists = cacheManager.ReportCategoryListCache;

			// Retrieve any cached lists to model
   
        }

        private void MergeNewValuesWithOriginal(ReportCategoryModel modelFromView)
        {
            //***************************The values that are display only will not be posted back so need to get them from session**************************

            ReportCategoryModel OriginalValuesFromSession = sessionManager.CurrentReportCategory;

        }
		
		private void SetAccessContext(ReportCategoryVM model)
        {
            //Decide on access context
            if (null == model.ReportCategoryItem || model.ReportCategoryItem.Code == Guid.Empty)
            {
				// Create context
                model.AccessContext = ReportCategoryAccessContext.Create;
            }
            else
            {
				// Edit context
                model.AccessContext = ReportCategoryAccessContext.Edit;
            }
        }
		
		private void DetermineIsDirty(ReportCategoryVM model)
        {
            //Compare the ReportCategory to the original session
            if (model.ReportCategoryItem.PublicInstancePropertiesEqual(sessionManager.ReportCategoryServiceVersion, "RowIdentifier"))
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
