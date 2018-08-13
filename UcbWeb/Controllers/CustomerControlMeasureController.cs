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
    public partial class CustomerControlMeasureController : BaseController
    {

		private IUcbService UcbService;

		// Dependency Injection enabled constructors
        public CustomerControlMeasureController()
            : this(new UcbServiceClient(),new SessionManager(), new CacheManager())
        {
        }

        public CustomerControlMeasureController(IUcbService UcbService, ISessionManager sessionManager, ICacheManager cacheManager)
			:base(sessionManager,cacheManager)
        {
            this.UcbService = UcbService;
        }

		#region Edit
		
        // GET: /CustomerControlMeasure/Edit
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        public ActionResult Edit()
        {
			// Retrieve ID from session
			string code = sessionManager.CustomerControlMeasureCode;
			
            CustomerControlMeasureVM model = new CustomerControlMeasureVM();
			
            // Not from staff or error
            if (String.IsNullOrEmpty(code))
            {
                //If session has lists then use them
                RepopulateListsFromCacheSession(model);

                //Assume we are in create mode as no code passed
                model.CustomerControlMeasureItem = new CustomerControlMeasureModel();
            }
            //if we have been passed a code then assume we are in edit situation and we need to retrieve from the database.
            else
            {
				// Create service instance
				IUcbService sc = UcbService;
				
                try
                {
					// Call service to get CustomerControlMeasure item and any associated lookups    
                    CustomerControlMeasureVMDC returnedObject = sc.GetCustomerControlMeasure(CurrentUser, CurrentUser, appID, "", code);

					// Close service communication
					((ICommunicationObject)sc).Close();

                    //Get view model from service
                    model = ConvertCustomerControlMeasureDC(returnedObject);

                    ResolveFieldCodesToFieldNamesUsingLists(model);

                    //Store the service version
                    sessionManager.CustomerControlMeasureServiceVersion = model.CustomerControlMeasureItem;
                }
				catch (Exception e)
				{
					// Handle the exception
					string message = ExceptionManager.HandleException(e, (ICommunicationObject)sc);
					model.Message = message;
					
					return View(model);
				}
            }

            //Adds current retrieved CustomerControlMeasure to session
            sessionManager.CurrentCustomerControlMeasure = model.CustomerControlMeasureItem;
            SetAccessContext(model);
            
            return View(model);
        }    
		
		#endregion
		
		#region Create/Update

        // POST: /CustomerControlMeasure/Edit with Create button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult CreateCustomerControlMeasure(FormCollection collection)
        {
            return UpdateCustomerControlMeasure();
        }

        // POST: /CustomerControlMeasure/Edit with Save button submitting
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult SaveCustomerControlMeasure(FormCollection collection)
        {
            return UpdateCustomerControlMeasure();
        }

        //This method is shared between create and save
        private ActionResult UpdateCustomerControlMeasure()
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
                    CustomerControlMeasureDC CustomerControlMeasureItem = Mapper.Map<CustomerControlMeasureDC>(model.CustomerControlMeasureItem);

					CustomerControlMeasureVMDC returnedObject = null;

                    if (null == model.CustomerControlMeasureItem.Code || model.CustomerControlMeasureItem.Code == Guid.Empty)
                    {
						// Call service to create new CustomerControlMeasure item
                        returnedObject = sc.CreateCustomerControlMeasure(CurrentUser, CurrentUser, appID, "", CustomerControlMeasureItem);
                    }
                    else
                    {
						// Call service to update CustomerControlMeasure item
                        returnedObject = sc.UpdateCustomerControlMeasure(CurrentUser, CurrentUser, appID, "", CustomerControlMeasureItem);
                    }

					// Close service communication
					((ICommunicationObject)sc).Close();
					
					// Retrieve item returned by service
                    var createdCustomerControlMeasure = returnedObject.CustomerControlMeasureItem;

					// Map data contract to model
                    model.CustomerControlMeasureItem = Mapper.Map<CustomerControlMeasureModel>(createdCustomerControlMeasure);
					
                    //After creation some of the fields are display only so we need the resolved look up nmames
                    ResolveFieldCodesToFieldNamesUsingLists(model);

					// Set access context to Edit mode
                    model.AccessContext = CustomerControlMeasureAccessContext.Edit;

					// Save version of item returned by service into session
                    sessionManager.CustomerControlMeasureServiceVersion = model.CustomerControlMeasureItem;
                    sessionManager.CurrentCustomerControlMeasure = model.CustomerControlMeasureItem;
                    
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

        // POST: /CustomerControlMeasure/Edit with Exit button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult ExitCustomerControlMeasure(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IsExitConfirmed == "True" || !model.IsViewDirty)
            {
                //Set flags false
                SetFlagsFalse(model);

                model.IsExitConfirmed = "False";
                
                //remove the current values from session
                sessionManager.CurrentCustomerControlMeasure = null;
                sessionManager.CustomerControlMeasureServiceVersion = null;

                return RedirectToAction("Search", "CustomerControlMeasure");
                
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

        // POST: /CustomerControlMeasure/Edit with Delete button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult DeleteCustomerControlMeasure(FormCollection collection)
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
                    sc.DeleteCustomerControlMeasure(CurrentUser, CurrentUser, appID, "", model.CustomerControlMeasureItem.Code.ToString(), model.CustomerControlMeasureItem.RowIdentifier.ToString());

					// Close service communication
					((ICommunicationObject)sc).Close();
					
                    // Remove the current values from session
                    sessionManager.CurrentCustomerControlMeasure = null;
                    sessionManager.CustomerControlMeasureServiceVersion = null;

                    // Remove the state from the model as these are being populated by the controller and the HTML helpers are being populated with
					// the POSTED values and not the changed ones.
                    ModelState.Clear();
					
					// Create new item but keep any lists
					model.CustomerControlMeasureItem = new CustomerControlMeasureModel();
					
					// Set message to return to user
                    model.Message = Resources.MESSAGE_DELETE_SUCCEEDED;
					
					// Set access context to Edit mode
                    model.AccessContext = CustomerControlMeasureAccessContext.Create;
					
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

        // POST: /CustomerControlMeasure/Edit with New button submitting
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult NewCustomerControlMeasure(FormCollection collection)
        {
            var model = GetUpdatedModel();
            //Set flags false
            SetFlagsFalse(model);
            
            //Clear Down Session
			sessionManager.CustomerControlMeasureCode = null;
            sessionManager.CurrentCustomerControlMeasure = null;
            sessionManager.CustomerControlMeasureServiceVersion = null;
            
            //Go to the Edit Screen
            return RedirectToAction("Edit", "CustomerControlMeasure");
        }
		
		#endregion

		#region Search

        // GET: /CustomerControlMeasure/Search
        //This is called when first entering search CustomerControlMeasure screen or when paging
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
        public ActionResult Search(int page = 1)
        {   
			// Create service instance
			IUcbService sc = UcbService;
			
			// Create model
			CustomerControlMeasureSearchVM model = new CustomerControlMeasureSearchVM();
			
			try
			{
				CustomerControlMeasureSearchVMDC response = sc.SearchCustomerControlMeasure(CurrentUser, CurrentUser, appID, "", null, page, PageSize);

				// Close service communication
				((ICommunicationObject)sc).Close();

				//Map response back to view model
				model.MatchList = Mapper.Map<IEnumerable<CustomerControlMeasureSearchMatchDC>, List<CustomerControlMeasureSearchMatchModel>>(response.MatchList);

				// Set paging values
				model.TotalRows = response.RecordCount;
				model.PageSize = sessionManager.PageSize;
				model.PageNumber = page;
				
				// Store the page number we were on
				sessionManager.CustomerControlMeasurePageNumber = model.PageNumber;
	        
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

        // POST: /CustomerControlMeasure/Search
        //This is called when clicking search button
		[CustomAuthorize(Roles = AppRoles.ADMIN)]
		[HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult SearchPost(CustomerControlMeasureSearchVM model, int page = 1)
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
					sessionManager.CustomerControlMeasureCode = Value.ToString();
					
					// Call out to Edit screen
                    return RedirectToAction("Edit", "CustomerControlMeasure", new { code = Value });

                }
            }

            // Return to the Screen
            return View(model);
        }

        #endregion
		
		#region Private methods
		
        private void SetFlagsFalse(CustomerControlMeasureVM model)
        {
            model.IsDeleteConfirmed = "False";
            model.IsExitConfirmed = "False";
            model.IsNewConfirmed = "False";

            //Stop the binder resetting the posted values
            ModelState.Remove("IsDeleteConfirmed");
            ModelState.Remove("IsExitConfirmed");
            ModelState.Remove("IsNewConfirmed");
        }

        private void ResolveFieldCodesToFieldNamesUsingLists(CustomerControlMeasureVM model)
        {
			//TODO:
        }

        /// <summary>
        /// Private method to merge in the model 
        /// </summary>
        /// <returns></returns>
        private CustomerControlMeasureVM GetUpdatedModel()
        {
            CustomerControlMeasureVM model = new CustomerControlMeasureVM();
            RepopulateListsFromCacheSession(model);
            model.Message = "";

            if (sessionManager.CurrentCustomerControlMeasure != null)
            {
                model.CustomerControlMeasureItem = sessionManager.CurrentCustomerControlMeasure;
            }

            //***************************************NEED WHITE LIST ---- BLACK LIST ------ TO PREVENT OVERPOSTING **************************
            bool result = TryUpdateModel(model);//This also validates and sets ModelState
            //*******************************************************************************************************************************
            if (sessionManager.CurrentCustomerControlMeasure != null)
            {
                //*****************************************PREVENT OVER POSTING ATTACKS******************************************************
                //Get the values for read only fields from session
                MergeNewValuesWithOriginal(model.CustomerControlMeasureItem);
                //***************************************************************************************************************************
            }

            SetAccessContext(model);

            return model;
        }
		
        private CustomerControlMeasureVM ConvertCustomerControlMeasureDC(CustomerControlMeasureVMDC returnedObject)
        {
            CustomerControlMeasureVM model = new CustomerControlMeasureVM();
            
			// Map CustomerControlMeasure Item
			model.CustomerControlMeasureItem = Mapper.Map<CustomerControlMeasureDC, CustomerControlMeasureModel>(returnedObject.CustomerControlMeasureItem);
            
			// Map lookup data lists
			model.CustomerList = Mapper.Map<IEnumerable<CustomerDC>, List<CustomerModel>>(returnedObject.CustomerList);
			model.ControlMeasureList = Mapper.Map<IEnumerable<ControlMeasureDC>, List<ControlMeasureModel>>(returnedObject.ControlMeasureList);
        
            return model;
        }
		
		private void RepopulateListsFromCacheSession(CustomerControlMeasureVM model)
        {
			// Populate cached lists if they are empty. Will invoke service call
            CustomerControlMeasureLookupListsCacheObject CachedLists = cacheManager.CustomerControlMeasureListCache;

			// Retrieve any cached lists to model
			model.CustomerList = CachedLists.CustomerList;
			model.ControlMeasureList = CachedLists.ControlMeasureList;
   
        }

        private void MergeNewValuesWithOriginal(CustomerControlMeasureModel modelFromView)
        {
            //***************************The values that are display only will not be posted back so need to get them from session**************************

            CustomerControlMeasureModel OriginalValuesFromSession = sessionManager.CurrentCustomerControlMeasure;

        }
		
		private void SetAccessContext(CustomerControlMeasureVM model)
        {
            //Decide on access context
            if (null == model.CustomerControlMeasureItem || model.CustomerControlMeasureItem.Code == Guid.Empty)
            {
				// Create context
                model.AccessContext = CustomerControlMeasureAccessContext.Create;
            }
            else
            {
				// Edit context
                model.AccessContext = CustomerControlMeasureAccessContext.Edit;
            }
        }
		
		private void DetermineIsDirty(CustomerControlMeasureVM model)
        {
            //Compare the CustomerControlMeasure to the original session
            if (model.CustomerControlMeasureItem.PublicInstancePropertiesEqual(sessionManager.CustomerControlMeasureServiceVersion, "RowIdentifier"))
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
