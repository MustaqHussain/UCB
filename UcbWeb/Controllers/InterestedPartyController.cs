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
    public partial class InterestedPartyController : BaseController
    {

		private IUcbService UcbService;

		// Dependency Injection enabled constructors
        public InterestedPartyController()
            : this(new UcbServiceClient(),new SessionManager(), new CacheManager())
        {
        }

        public InterestedPartyController(IUcbService UcbService, ISessionManager sessionManager, ICacheManager cacheManager)
			:base(sessionManager,cacheManager)
        {
            this.UcbService = UcbService;
        }

		#region Edit
		
        // GET: /InterestedParty/Edit
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        public ActionResult Edit()
        {
			// Retrieve ID from session
			string code = sessionManager.InterestedPartyCode;
			
            InterestedPartyVM model = new InterestedPartyVM();
			
            // Not from staff or error
            if (String.IsNullOrEmpty(code))
            {
                //If session has lists then use them
                RepopulateListsFromCacheSession(model);

                //Assume we are in create mode as no code passed
                model.InterestedPartyItem = new InterestedPartyModel(){IsActive = true};
            }
            //if we have been passed a code then assume we are in edit situation and we need to retrieve from the database.
            else
            {
				// Create service instance
				IUcbService sc = UcbService;
				
                try
                {
					// Call service to get InterestedParty item and any associated lookups    
                    InterestedPartyVMDC returnedObject = sc.GetInterestedParty(CurrentUser, CurrentUser, appID, "", code);

					// Close service communication
					((ICommunicationObject)sc).Close();

                    //Get view model from service
                    model = ConvertInterestedPartyDC(returnedObject);

                    ResolveFieldCodesToFieldNamesUsingLists(model);

                    //Store the service version
                    sessionManager.InterestedPartyServiceVersion = model.InterestedPartyItem;
                }
				catch (Exception e)
				{
					// Handle the exception
					string message = ExceptionManager.HandleException(e, (ICommunicationObject)sc);
					model.Message = message;
					
					return View(model);
				}
            }

            //Adds current retrieved InterestedParty to session
            sessionManager.CurrentInterestedParty = model.InterestedPartyItem;
            SetAccessContext(model);
            
            return View(model);
        }    
		
		#endregion
		
		#region Create/Update

        // POST: /InterestedParty/Edit with Create button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult CreateInterestedParty(FormCollection collection)
        {
            return UpdateInterestedParty();
        }

        // POST: /InterestedParty/Edit with Save button submitting
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult SaveInterestedParty(FormCollection collection)
        {
            return UpdateInterestedParty();
        }

        //This method is shared between create and save
        private ActionResult UpdateInterestedParty()
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
                    InterestedPartyDC InterestedPartyItem = Mapper.Map<InterestedPartyDC>(model.InterestedPartyItem);

					InterestedPartyVMDC returnedObject = null;

                    if (null == model.InterestedPartyItem.Code || model.InterestedPartyItem.Code == Guid.Empty)
                    {
						// Call service to create new InterestedParty item
                        returnedObject = sc.CreateInterestedParty(CurrentUser, CurrentUser, appID, "", InterestedPartyItem);
                    }
                    else
                    {
						// Call service to update InterestedParty item
                        returnedObject = sc.UpdateInterestedParty(CurrentUser, CurrentUser, appID, "", InterestedPartyItem);
                    }

					// Close service communication
					((ICommunicationObject)sc).Close();
					
					// Retrieve item returned by service
                    var createdInterestedParty = returnedObject.InterestedPartyItem;

					// Map data contract to model
                    model.InterestedPartyItem = Mapper.Map<InterestedPartyModel>(createdInterestedParty);
					
                    //After creation some of the fields are display only so we need the resolved look up nmames
                    ResolveFieldCodesToFieldNamesUsingLists(model);

					// Set access context to Edit mode
                    model.AccessContext = InterestedPartyAccessContext.Edit;

					// Save version of item returned by service into session
                    sessionManager.InterestedPartyServiceVersion = model.InterestedPartyItem;
                    sessionManager.CurrentInterestedParty = model.InterestedPartyItem;
                    
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

        // POST: /InterestedParty/Edit with Exit button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult ExitInterestedParty(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IsExitConfirmed == "True" || !model.IsViewDirty)
            {
                //Set flags false
                SetFlagsFalse(model);

                model.IsExitConfirmed = "False";
                
                //remove the current values from session
                sessionManager.CurrentInterestedParty = null;
                sessionManager.InterestedPartyServiceVersion = null;

                return RedirectToAction("Search", "InterestedParty");
                
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

        // POST: /InterestedParty/Edit with Delete button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult DeleteInterestedParty(FormCollection collection)
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
                    sc.DeleteInterestedParty(CurrentUser, CurrentUser, appID, "", model.InterestedPartyItem.Code.ToString(), model.InterestedPartyItem.RowIdentifier.ToString());

					// Close service communication
					((ICommunicationObject)sc).Close();
					
                    // Remove the current values from session
                    sessionManager.CurrentInterestedParty = null;
                    sessionManager.InterestedPartyServiceVersion = null;

                    // Remove the state from the model as these are being populated by the controller and the HTML helpers are being populated with
					// the POSTED values and not the changed ones.
                    ModelState.Clear();
					
					// Create new item but keep any lists
					model.InterestedPartyItem = new InterestedPartyModel(){IsActive = true};
					
					// Set message to return to user
                    model.Message = Resources.MESSAGE_DELETE_SUCCEEDED;
					
					// Set access context to Edit mode
                    model.AccessContext = InterestedPartyAccessContext.Create;
					
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

        // POST: /InterestedParty/Edit with New button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult NewInterestedParty(FormCollection collection)
        {
            var model = GetUpdatedModel();
            //Set flags false
            SetFlagsFalse(model);
            
            //Clear Down Session
			sessionManager.InterestedPartyCode = null;
            sessionManager.CurrentInterestedParty = null;
            sessionManager.InterestedPartyServiceVersion = null;
            
            //Go to the Edit Screen
            return RedirectToAction("Edit", "InterestedParty");
        }
		
		#endregion

		#region Search

        // GET: /InterestedParty/Search
        //This is called when first entering search InterestedParty screen or when paging
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        public ActionResult Search(int page = 1)
        {   
			// Create service instance
			IUcbService sc = UcbService;
			
			// Create model
			InterestedPartySearchVM model = new InterestedPartySearchVM();
			
			try
			{
				InterestedPartySearchVMDC response = sc.SearchInterestedParty(CurrentUser, CurrentUser, appID, "", null, page, PageSize, true);

				// Close service communication
				((ICommunicationObject)sc).Close();

				//Map response back to view model
				model.MatchList = Mapper.Map<IEnumerable<InterestedPartySearchMatchDC>, List<InterestedPartySearchMatchModel>>(response.MatchList);

				// Set paging values
				model.TotalRows = response.RecordCount;
				model.PageSize = sessionManager.PageSize;
				model.PageNumber = page;
				
				// Store the page number we were on
				sessionManager.InterestedPartyPageNumber = model.PageNumber;
	        
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

        // POST: /InterestedParty/Search
        //This is called when clicking search button
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
		[HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult SearchPost(InterestedPartySearchVM model, int page = 1)
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
					sessionManager.InterestedPartyCode = Value.ToString();
					
					// Call out to Edit screen
                    return RedirectToAction("Edit", "InterestedParty", new { code = Value });

                }
            }

            // Return to the Screen
            return View(model);
        }

        #endregion
		
		#region Private methods
		
        private void SetFlagsFalse(InterestedPartyVM model)
        {
            model.IsDeleteConfirmed = "False";
            model.IsExitConfirmed = "False";
            model.IsNewConfirmed = "False";

            //Stop the binder resetting the posted values
            ModelState.Remove("IsDeleteConfirmed");
            ModelState.Remove("IsExitConfirmed");
            ModelState.Remove("IsNewConfirmed");
        }

        private void ResolveFieldCodesToFieldNamesUsingLists(InterestedPartyVM model)
        {
			//TODO:
        }

        /// <summary>
        /// Private method to merge in the model 
        /// </summary>
        /// <returns></returns>
        private InterestedPartyVM GetUpdatedModel()
        {
            InterestedPartyVM model = new InterestedPartyVM();
            RepopulateListsFromCacheSession(model);
            model.Message = "";

            if (sessionManager.CurrentInterestedParty != null)
            {
                model.InterestedPartyItem = sessionManager.CurrentInterestedParty;
            }

            //***************************************NEED WHITE LIST ---- BLACK LIST ------ TO PREVENT OVERPOSTING **************************
            bool result = TryUpdateModel(model);//This also validates and sets ModelState
            //*******************************************************************************************************************************
            if (sessionManager.CurrentInterestedParty != null)
            {
                //*****************************************PREVENT OVER POSTING ATTACKS******************************************************
                //Get the values for read only fields from session
                MergeNewValuesWithOriginal(model.InterestedPartyItem);
                //***************************************************************************************************************************
            }

            SetAccessContext(model);

            return model;
        }
		
        private InterestedPartyVM ConvertInterestedPartyDC(InterestedPartyVMDC returnedObject)
        {
            InterestedPartyVM model = new InterestedPartyVM();
            
			// Map InterestedParty Item
			model.InterestedPartyItem = Mapper.Map<InterestedPartyDC, InterestedPartyModel>(returnedObject.InterestedPartyItem);
            
			// Map lookup data lists
        
            return model;
        }
		
		private void RepopulateListsFromCacheSession(InterestedPartyVM model)
        {
			// Populate cached lists if they are empty. Will invoke service call
            InterestedPartyLookupListsCacheObject CachedLists = cacheManager.InterestedPartyListCache;

			// Retrieve any cached lists to model
   
        }

        private void MergeNewValuesWithOriginal(InterestedPartyModel modelFromView)
        {
            //***************************The values that are display only will not be posted back so need to get them from session**************************

            InterestedPartyModel OriginalValuesFromSession = sessionManager.CurrentInterestedParty;

        }
		
		private void SetAccessContext(InterestedPartyVM model)
        {
            //Decide on access context
            if (null == model.InterestedPartyItem || model.InterestedPartyItem.Code == Guid.Empty)
            {
				// Create context
                model.AccessContext = InterestedPartyAccessContext.Create;
            }
            else
            {
				// Edit context
                model.AccessContext = InterestedPartyAccessContext.Edit;
            }
        }
		
		private void DetermineIsDirty(InterestedPartyVM model)
        {
            //Compare the InterestedParty to the original session
            if (model.InterestedPartyItem.PublicInstancePropertiesEqual(sessionManager.InterestedPartyServiceVersion, "RowIdentifier"))
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
