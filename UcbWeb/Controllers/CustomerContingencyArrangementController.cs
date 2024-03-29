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
    public partial class CustomerContingencyArrangementController : BaseController
    {

		private IUcbService UcbService;

		// Dependency Injection enabled constructors
        public CustomerContingencyArrangementController()
            : this(new UcbServiceClient(),new SessionManager(), new CacheManager())
        {
        }

        public CustomerContingencyArrangementController(IUcbService UcbService, ISessionManager sessionManager, ICacheManager cacheManager)
			:base(sessionManager,cacheManager)
        {
            this.UcbService = UcbService;
        }

		#region Edit
		
        // GET: /CustomerContingencyArrangement/Edit
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        public ActionResult Edit()
        {
			// Retrieve ID from session
			string code = sessionManager.CustomerContingencyArrangementCode;
			
            CustomerContingencyArrangementVM model = new CustomerContingencyArrangementVM();
			
            // Not from staff or error
            if (String.IsNullOrEmpty(code))
            {
                //If session has lists then use them
                RepopulateListsFromCacheSession(model);

                //Assume we are in create mode as no code passed
                model.CustomerContingencyArrangementItem = new CustomerContingencyArrangementModel();
            }
            //if we have been passed a code then assume we are in edit situation and we need to retrieve from the database.
            else
            {
				// Create service instance
				IUcbService sc = UcbService;
				
                try
                {
					// Call service to get CustomerContingencyArrangement item and any associated lookups    
                    CustomerContingencyArrangementVMDC returnedObject = sc.GetCustomerContingencyArrangement(CurrentUser, CurrentUser, appID, "", code);

					// Close service communication
					((ICommunicationObject)sc).Close();

                    //Get view model from service
                    model = ConvertCustomerContingencyArrangementDC(returnedObject);

                    ResolveFieldCodesToFieldNamesUsingLists(model);

                    //Store the service version
                    sessionManager.CustomerContingencyArrangementServiceVersion = model.CustomerContingencyArrangementItem;
                }
				catch (Exception e)
				{
					// Handle the exception
					string message = ExceptionManager.HandleException(e, (ICommunicationObject)sc);
					model.Message = message;
					
					return View(model);
				}
            }

            //Adds current retrieved CustomerContingencyArrangement to session
            sessionManager.CurrentCustomerContingencyArrangement = model.CustomerContingencyArrangementItem;
            SetAccessContext(model);
            
            return View(model);
        }    
		
		#endregion
		
		#region Create/Update

        // POST: /CustomerContingencyArrangement/Edit with Create button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult CreateCustomerContingencyArrangement(FormCollection collection)
        {
            return UpdateCustomerContingencyArrangement();
        }

        // POST: /CustomerContingencyArrangement/Edit with Save button submitting
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult SaveCustomerContingencyArrangement(FormCollection collection)
        {
            return UpdateCustomerContingencyArrangement();
        }

        //This method is shared between create and save
        private ActionResult UpdateCustomerContingencyArrangement()
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
                    CustomerContingencyArrangementDC CustomerContingencyArrangementItem = Mapper.Map<CustomerContingencyArrangementDC>(model.CustomerContingencyArrangementItem);

					CustomerContingencyArrangementVMDC returnedObject = null;

                    if (null == model.CustomerContingencyArrangementItem.Code || model.CustomerContingencyArrangementItem.Code == Guid.Empty)
                    {
						// Call service to create new CustomerContingencyArrangement item
                        returnedObject = sc.CreateCustomerContingencyArrangement(CurrentUser, CurrentUser, appID, "", CustomerContingencyArrangementItem);
                    }
                    else
                    {
						// Call service to update CustomerContingencyArrangement item
                        returnedObject = sc.UpdateCustomerContingencyArrangement(CurrentUser, CurrentUser, appID, "", CustomerContingencyArrangementItem);
                    }

					// Close service communication
					((ICommunicationObject)sc).Close();
					
					// Retrieve item returned by service
                    var createdCustomerContingencyArrangement = returnedObject.CustomerContingencyArrangementItem;

					// Map data contract to model
                    model.CustomerContingencyArrangementItem = Mapper.Map<CustomerContingencyArrangementModel>(createdCustomerContingencyArrangement);
					
                    //After creation some of the fields are display only so we need the resolved look up nmames
                    ResolveFieldCodesToFieldNamesUsingLists(model);

					// Set access context to Edit mode
                    model.AccessContext = CustomerContingencyArrangementAccessContext.Edit;

					// Save version of item returned by service into session
                    sessionManager.CustomerContingencyArrangementServiceVersion = model.CustomerContingencyArrangementItem;
                    sessionManager.CurrentCustomerContingencyArrangement = model.CustomerContingencyArrangementItem;
                    
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

        // POST: /CustomerContingencyArrangement/Edit with Exit button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult ExitCustomerContingencyArrangement(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IsExitConfirmed == "True" || !model.IsViewDirty)
            {
                //Set flags false
                SetFlagsFalse(model);

                model.IsExitConfirmed = "False";
                
                //remove the current values from session
                sessionManager.CurrentCustomerContingencyArrangement = null;
                sessionManager.CustomerContingencyArrangementServiceVersion = null;

                return RedirectToAction("Search", "CustomerContingencyArrangement");
                
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

        // POST: /CustomerContingencyArrangement/Edit with Delete button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult DeleteCustomerContingencyArrangement(FormCollection collection)
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
                    sc.DeleteCustomerContingencyArrangement(CurrentUser, CurrentUser, appID, "", model.CustomerContingencyArrangementItem.Code.ToString(), model.CustomerContingencyArrangementItem.RowIdentifier.ToString());

					// Close service communication
					((ICommunicationObject)sc).Close();
					
                    // Remove the current values from session
                    sessionManager.CurrentCustomerContingencyArrangement = null;
                    sessionManager.CustomerContingencyArrangementServiceVersion = null;

                    // Remove the state from the model as these are being populated by the controller and the HTML helpers are being populated with
					// the POSTED values and not the changed ones.
                    ModelState.Clear();
					
					// Create new item but keep any lists
					model.CustomerContingencyArrangementItem = new CustomerContingencyArrangementModel();
					
					// Set message to return to user
                    model.Message = Resources.MESSAGE_DELETE_SUCCEEDED;
					
					// Set access context to Edit mode
                    model.AccessContext = CustomerContingencyArrangementAccessContext.Create;
					
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

        // POST: /CustomerContingencyArrangement/Edit with New button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult NewCustomerContingencyArrangement(FormCollection collection)
        {
            var model = GetUpdatedModel();
            //Set flags false
            SetFlagsFalse(model);
            
            //Clear Down Session
			sessionManager.CustomerContingencyArrangementCode = null;
            sessionManager.CurrentCustomerContingencyArrangement = null;
            sessionManager.CustomerContingencyArrangementServiceVersion = null;
            
            //Go to the Edit Screen
            return RedirectToAction("Edit", "CustomerContingencyArrangement");
        }
		
		#endregion

		#region Search

        // GET: /CustomerContingencyArrangement/Search
        //This is called when first entering search CustomerContingencyArrangement screen or when paging
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        public ActionResult Search(int page = 1)
        {   
			// Create service instance
			IUcbService sc = UcbService;
			
			// Create model
			CustomerContingencyArrangementSearchVM model = new CustomerContingencyArrangementSearchVM();
			
			try
			{
				CustomerContingencyArrangementSearchVMDC response = sc.SearchCustomerContingencyArrangement(CurrentUser, CurrentUser, appID, "", null, page, PageSize);

				// Close service communication
				((ICommunicationObject)sc).Close();

				//Map response back to view model
				model.MatchList = Mapper.Map<IEnumerable<CustomerContingencyArrangementSearchMatchDC>, List<CustomerContingencyArrangementSearchMatchModel>>(response.MatchList);

				// Set paging values
				model.TotalRows = response.RecordCount;
				model.PageSize = sessionManager.PageSize;
				model.PageNumber = page;
				
				// Store the page number we were on
				sessionManager.CustomerContingencyArrangementPageNumber = model.PageNumber;
	        
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

        // POST: /CustomerContingencyArrangement/Search
        //This is called when clicking search button
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
		[HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult SearchPost(CustomerContingencyArrangementSearchVM model, int page = 1)
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
					sessionManager.CustomerContingencyArrangementCode = Value.ToString();
					
					// Call out to Edit screen
                    return RedirectToAction("Edit", "CustomerContingencyArrangement", new { code = Value });

                }
            }

            // Return to the Screen
            return View(model);
        }

        #endregion
		
		#region Private methods
		
        private void SetFlagsFalse(CustomerContingencyArrangementVM model)
        {
            model.IsDeleteConfirmed = "False";
            model.IsExitConfirmed = "False";
            model.IsNewConfirmed = "False";

            //Stop the binder resetting the posted values
            ModelState.Remove("IsDeleteConfirmed");
            ModelState.Remove("IsExitConfirmed");
            ModelState.Remove("IsNewConfirmed");
        }

        private void ResolveFieldCodesToFieldNamesUsingLists(CustomerContingencyArrangementVM model)
        {
			//TODO:
        }

        /// <summary>
        /// Private method to merge in the model 
        /// </summary>
        /// <returns></returns>
        private CustomerContingencyArrangementVM GetUpdatedModel()
        {
            CustomerContingencyArrangementVM model = new CustomerContingencyArrangementVM();
            RepopulateListsFromCacheSession(model);
            model.Message = "";

            if (sessionManager.CurrentCustomerContingencyArrangement != null)
            {
                model.CustomerContingencyArrangementItem = sessionManager.CurrentCustomerContingencyArrangement;
            }

            //***************************************NEED WHITE LIST ---- BLACK LIST ------ TO PREVENT OVERPOSTING **************************
            bool result = TryUpdateModel(model);//This also validates and sets ModelState
            //*******************************************************************************************************************************
            if (sessionManager.CurrentCustomerContingencyArrangement != null)
            {
                //*****************************************PREVENT OVER POSTING ATTACKS******************************************************
                //Get the values for read only fields from session
                MergeNewValuesWithOriginal(model.CustomerContingencyArrangementItem);
                //***************************************************************************************************************************
            }

            SetAccessContext(model);

            return model;
        }
		
        private CustomerContingencyArrangementVM ConvertCustomerContingencyArrangementDC(CustomerContingencyArrangementVMDC returnedObject)
        {
            CustomerContingencyArrangementVM model = new CustomerContingencyArrangementVM();
            
			// Map CustomerContingencyArrangement Item
			model.CustomerContingencyArrangementItem = Mapper.Map<CustomerContingencyArrangementDC, CustomerContingencyArrangementModel>(returnedObject.CustomerContingencyArrangementItem);
            
			// Map lookup data lists
			model.CustomerList = Mapper.Map<IEnumerable<CustomerDC>, List<CustomerModel>>(returnedObject.CustomerList);
			model.ContingencyArrangementList = Mapper.Map<IEnumerable<ContingencyArrangementDC>, List<ContingencyArrangementModel>>(returnedObject.ContingencyArrangementList);
        
            return model;
        }
		
		private void RepopulateListsFromCacheSession(CustomerContingencyArrangementVM model)
        {
			// Populate cached lists if they are empty. Will invoke service call
            CustomerContingencyArrangementLookupListsCacheObject CachedLists = cacheManager.CustomerContingencyArrangementListCache;

			// Retrieve any cached lists to model
			model.CustomerList = CachedLists.CustomerList;
			model.ContingencyArrangementList = CachedLists.ContingencyArrangementList;
   
        }

        private void MergeNewValuesWithOriginal(CustomerContingencyArrangementModel modelFromView)
        {
            //***************************The values that are display only will not be posted back so need to get them from session**************************

            CustomerContingencyArrangementModel OriginalValuesFromSession = sessionManager.CurrentCustomerContingencyArrangement;

        }
		
		private void SetAccessContext(CustomerContingencyArrangementVM model)
        {
            //Decide on access context
            if (null == model.CustomerContingencyArrangementItem || model.CustomerContingencyArrangementItem.Code == Guid.Empty)
            {
				// Create context
                model.AccessContext = CustomerContingencyArrangementAccessContext.Create;
            }
            else
            {
				// Edit context
                model.AccessContext = CustomerContingencyArrangementAccessContext.Edit;
            }
        }
		
		private void DetermineIsDirty(CustomerContingencyArrangementVM model)
        {
            //Compare the CustomerContingencyArrangement to the original session
            if (model.CustomerContingencyArrangementItem.PublicInstancePropertiesEqual(sessionManager.CustomerContingencyArrangementServiceVersion, "RowIdentifier"))
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
