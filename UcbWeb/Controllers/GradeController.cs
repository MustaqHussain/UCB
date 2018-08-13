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
    public partial class GradeController : BaseController
    {

		private IUcbService UcbService;

		// Dependency Injection enabled constructors
        public GradeController()
            : this(new UcbServiceClient(),new SessionManager(), new CacheManager())
        {
        }

        public GradeController(IUcbService UcbService, ISessionManager sessionManager, ICacheManager cacheManager)
			:base(sessionManager,cacheManager)
        {
            this.UcbService = UcbService;
        }

		#region Edit
		
        // GET: /Grade/Edit
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        public ActionResult Edit()
        {
			// Retrieve ID from session
			string code = sessionManager.GradeCode;
			
            GradeVM model = new GradeVM();
			
            // Not from staff or error
            if (String.IsNullOrEmpty(code))
            {
                //If session has lists then use them
                RepopulateListsFromCacheSession(model);

                //Assume we are in create mode as no code passed
                model.GradeItem = new GradeModel(){IsActive = true};
            }
            //if we have been passed a code then assume we are in edit situation and we need to retrieve from the database.
            else
            {
				// Create service instance
				IUcbService sc = UcbService;
				
                try
                {
					// Call service to get Grade item and any associated lookups    
                    GradeVMDC returnedObject = sc.GetGrade(CurrentUser, CurrentUser, appID, "", code);

					// Close service communication
					((ICommunicationObject)sc).Close();

                    //Get view model from service
                    model = ConvertGradeDC(returnedObject);

                    ResolveFieldCodesToFieldNamesUsingLists(model);

                    //Store the service version
                    sessionManager.GradeServiceVersion = model.GradeItem;
                }
				catch (Exception e)
				{
					// Handle the exception
					string message = ExceptionManager.HandleException(e, (ICommunicationObject)sc);
					model.Message = message;
					
					return View(model);
				}
            }

            //Adds current retrieved Grade to session
            sessionManager.CurrentGrade = model.GradeItem;
            SetAccessContext(model);
            
            return View(model);
        }    
		
		#endregion
		
		#region Create/Update

        // POST: /Grade/Edit with Create button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult CreateGrade(FormCollection collection)
        {
            return UpdateGrade();
        }

        // POST: /Grade/Edit with Save button submitting
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult SaveGrade(FormCollection collection)
        {
            return UpdateGrade();
        }

        //This method is shared between create and save
        private ActionResult UpdateGrade()
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
                    GradeDC GradeItem = Mapper.Map<GradeDC>(model.GradeItem);

					GradeVMDC returnedObject = null;

                    if (null == model.GradeItem.Code || model.GradeItem.Code == Guid.Empty)
                    {
						// Call service to create new Grade item
                        returnedObject = sc.CreateGrade(CurrentUser, CurrentUser, appID, "", GradeItem);
                    }
                    else
                    {
						// Call service to update Grade item
                        returnedObject = sc.UpdateGrade(CurrentUser, CurrentUser, appID, "", GradeItem);
                    }

					// Close service communication
					((ICommunicationObject)sc).Close();
					
					// Retrieve item returned by service
                    var createdGrade = returnedObject.GradeItem;

					// Map data contract to model
                    model.GradeItem = Mapper.Map<GradeModel>(createdGrade);
					
                    //After creation some of the fields are display only so we need the resolved look up nmames
                    ResolveFieldCodesToFieldNamesUsingLists(model);

					// Set access context to Edit mode
                    model.AccessContext = GradeAccessContext.Edit;

					// Save version of item returned by service into session
                    sessionManager.GradeServiceVersion = model.GradeItem;
                    sessionManager.CurrentGrade = model.GradeItem;
                    
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

        // POST: /Grade/Edit with Exit button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult ExitGrade(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IsExitConfirmed == "True" || !model.IsViewDirty)
            {
                //Set flags false
                SetFlagsFalse(model);

                model.IsExitConfirmed = "False";
                
                //remove the current values from session
                sessionManager.CurrentGrade = null;
                sessionManager.GradeServiceVersion = null;

                return RedirectToAction("Search", "Grade");
                
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

        // POST: /Grade/Edit with Delete button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult DeleteGrade(FormCollection collection)
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
                    sc.DeleteGrade(CurrentUser, CurrentUser, appID, "", model.GradeItem.Code.ToString(), model.GradeItem.RowIdentifier.ToString());

					// Close service communication
					((ICommunicationObject)sc).Close();
					
                    // Remove the current values from session
                    sessionManager.CurrentGrade = null;
                    sessionManager.GradeServiceVersion = null;

                    // Remove the state from the model as these are being populated by the controller and the HTML helpers are being populated with
					// the POSTED values and not the changed ones.
                    ModelState.Clear();
					
					// Create new item but keep any lists
					model.GradeItem = new GradeModel(){IsActive = true};
					
					// Set message to return to user
                    model.Message = Resources.MESSAGE_DELETE_SUCCEEDED;
					
					// Set access context to Edit mode
                    model.AccessContext = GradeAccessContext.Create;
					
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

        // POST: /Grade/Edit with New button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult NewGrade(FormCollection collection)
        {
            var model = GetUpdatedModel();
            //Set flags false
            SetFlagsFalse(model);
            
            //Clear Down Session
			sessionManager.GradeCode = null;
            sessionManager.CurrentGrade = null;
            sessionManager.GradeServiceVersion = null;
            
            //Go to the Edit Screen
            return RedirectToAction("Edit", "Grade");
        }
		
		#endregion

		#region Search

        // GET: /Grade/Search
        //This is called when first entering search Grade screen or when paging
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        public ActionResult Search(int page = 1)
        {   
			// Create service instance
			IUcbService sc = UcbService;
			
			// Create model
			GradeSearchVM model = new GradeSearchVM();
			
			try
			{
				GradeSearchVMDC response = sc.SearchGrade(CurrentUser, CurrentUser, appID, "", null, page, PageSize, true);

				// Close service communication
				((ICommunicationObject)sc).Close();

				//Map response back to view model
				model.MatchList = Mapper.Map<IEnumerable<GradeSearchMatchDC>, List<GradeSearchMatchModel>>(response.MatchList);

				// Set paging values
				model.TotalRows = response.RecordCount;
				model.PageSize = sessionManager.PageSize;
				model.PageNumber = page;
				
				// Store the page number we were on
				sessionManager.GradePageNumber = model.PageNumber;
	        
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

        // POST: /Grade/Search
        //This is called when clicking search button
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
		[HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult SearchPost(GradeSearchVM model, int page = 1)
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
					sessionManager.GradeCode = Value.ToString();
					
					// Call out to Edit screen
                    return RedirectToAction("Edit", "Grade", new { code = Value });

                }
            }

            // Return to the Screen
            return View(model);
        }

        #endregion
		
		#region Private methods
		
        private void SetFlagsFalse(GradeVM model)
        {
            model.IsDeleteConfirmed = "False";
            model.IsExitConfirmed = "False";
            model.IsNewConfirmed = "False";

            //Stop the binder resetting the posted values
            ModelState.Remove("IsDeleteConfirmed");
            ModelState.Remove("IsExitConfirmed");
            ModelState.Remove("IsNewConfirmed");
        }

        private void ResolveFieldCodesToFieldNamesUsingLists(GradeVM model)
        {
			//TODO:
        }

        /// <summary>
        /// Private method to merge in the model 
        /// </summary>
        /// <returns></returns>
        private GradeVM GetUpdatedModel()
        {
            GradeVM model = new GradeVM();
            RepopulateListsFromCacheSession(model);
            model.Message = "";

            if (sessionManager.CurrentGrade != null)
            {
                model.GradeItem = sessionManager.CurrentGrade;
            }

            //***************************************NEED WHITE LIST ---- BLACK LIST ------ TO PREVENT OVERPOSTING **************************
            bool result = TryUpdateModel(model);//This also validates and sets ModelState
            //*******************************************************************************************************************************
            if (sessionManager.CurrentGrade != null)
            {
                //*****************************************PREVENT OVER POSTING ATTACKS******************************************************
                //Get the values for read only fields from session
                MergeNewValuesWithOriginal(model.GradeItem);
                //***************************************************************************************************************************
            }

            SetAccessContext(model);

            return model;
        }
		
        private GradeVM ConvertGradeDC(GradeVMDC returnedObject)
        {
            GradeVM model = new GradeVM();
            
			// Map Grade Item
			model.GradeItem = Mapper.Map<GradeDC, GradeModel>(returnedObject.GradeItem);
            
			// Map lookup data lists
        
            return model;
        }
		
		private void RepopulateListsFromCacheSession(GradeVM model)
        {
			// Populate cached lists if they are empty. Will invoke service call
            GradeLookupListsCacheObject CachedLists = cacheManager.GradeListCache;

			// Retrieve any cached lists to model
   
        }

        private void MergeNewValuesWithOriginal(GradeModel modelFromView)
        {
            //***************************The values that are display only will not be posted back so need to get them from session**************************

            GradeModel OriginalValuesFromSession = sessionManager.CurrentGrade;

        }
		
		private void SetAccessContext(GradeVM model)
        {
            //Decide on access context
            if (null == model.GradeItem || model.GradeItem.Code == Guid.Empty)
            {
				// Create context
                model.AccessContext = GradeAccessContext.Create;
            }
            else
            {
				// Edit context
                model.AccessContext = GradeAccessContext.Edit;
            }
        }
		
		private void DetermineIsDirty(GradeVM model)
        {
            //Compare the Grade to the original session
            if (model.GradeItem.PublicInstancePropertiesEqual(sessionManager.GradeServiceVersion, "RowIdentifier"))
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
